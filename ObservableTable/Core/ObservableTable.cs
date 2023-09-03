using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reflection.Metadata.Ecma335;

namespace ObservableTable.Core;

public class ObservableTable<T> where T : notnull
{
    #region Properties

    // Records cannot be readonly because WPF and other implementations
    // access cells directly rather than using SetCell().
    public ObservableCollection<ObservableCollection<T?>> Records { get; } = new();
    public ReadOnlyObservableCollection<T> Headers => new(headers);
    public int UndoCount => undo.Count;
    public int RedoCount => redo.Count;
    public event EventHandler<EditEventArgs>? TableModified;

    #endregion Properties

    #region Fields

    private readonly ObservableCollection<T> headers = new();
    private readonly Stack<EditEventArgs> undo = new();
    private readonly Stack<EditEventArgs> redo = new();
    private bool recordTransactions;
    private int parity;

    #endregion Fields

    #region Constructors

    /// <summary>
    /// Initialize an ObservableTable without any columns or rows.
    /// </summary>
    public ObservableTable()
    {
        recordTransactions = true;
    }

    /// <summary>
    /// Initialize an ObservableTable with predefined columns and rows.
    /// </summary>
    /// <param name="headers">Columns to add</param>
    /// <param name="records">Rows to add</param>
    public ObservableTable(IEnumerable<T> headers, IEnumerable<IList<T?>> records)
    {
        this.headers = new(headers);
        InsertRow(0, records);
        recordTransactions = true;
    }

    /// <summary>
    /// Initialize an ObservableTable with predefined columns and rows.
    /// </summary>
    /// <param name="headers">Columns to add</param>
    /// <param name="records">Rows to add (optional)</param>
    public ObservableTable(IEnumerable<T> headers, params IList<T?>[] records) : this(headers, records.AsEnumerable())
    { }

    #endregion Constructors

    #region Public Methods

    /// <summary>
    /// Insert <paramref name="rows"/> starting from <paramref name="index"/>.
    /// </summary>
    public void InsertRow(int index, IEnumerable<IList<T?>> rows)
    {
        foreach (var row in rows)
        {
            InsertRow(index++, row);
            parity++;
        }
        parity = 0;
    }

    /// <summary>
    /// Insert <paramref name="row"/> at <paramref name="index"/>
    /// </summary>
    public void InsertRow(int index, IList<T?> row)
    {
        row = row.PadRight(headers.Count);
        ObservableCollection<T?> toAdd = new(row);
        toAdd.CollectionChanged += RecordChanged;
        Records.Insert(index, toAdd);

        RecordTransaction(
            () => RemoveRow(row),
            () => InsertRow(index, row)
        );
    }

    /// <summary>
    /// Remove <paramref name="rows"/> from the table. Non-existent rows are ignored.
    /// </summary>
    public void RemoveRow(IEnumerable<IList<T?>> rows)
    {
        foreach (var row in rows)
        {
            RemoveRow(row);
            parity++;
        }
        parity = 0;
    }

    /// <summary> 
    /// Remove <paramref name="row"/> from the table. An non-existent row is ignored.
    /// </summary>
    public void RemoveRow(IList<T?> row)
    {
        int index = RowIndexOf(row);
        RemoveRow(index);
    }

    /// <summary>
    /// Move a row from <paramref name="oldIndex"/> to <paramref name="newIndex"/>.
    /// </summary>
    public void ReorderRow(int oldIndex, int newIndex)
    {
        Records.Move(oldIndex, newIndex);

        RecordTransaction(
            () => ReorderRow(newIndex, oldIndex),
            () => ReorderRow(oldIndex, newIndex)
        );
    }

    /// <summary>
    /// Move a column from <paramref name="oldIndex"/> to <paramref name="newIndex"/>.
    /// </summary>
    public void ReorderColumn(int oldIndex, int newIndex)
    {
        headers.Move(oldIndex, newIndex);

        foreach (var record in Records)
        {
            record.Move(oldIndex, newIndex);
        }

        RecordTransaction(
            () => ReorderColumn(newIndex, oldIndex),
            () => ReorderColumn(oldIndex, newIndex)
        );
    }

    /// <summary>
    /// Rename the column at <paramref name="index"/> to <paramref name="header"/>.
    /// </summary>
    public void RenameColumn(int index, T header)
    {
        T oldHeader = headers[index];
        headers[index] = header;

        RecordTransaction(
            () => RenameColumn(index, oldHeader),
            () => RenameColumn(index, header)
        );
    }

    /// <summary>
    /// Insert <paramref name="columns"/> starting from <paramref name="index"/>.
    /// </summary>
    public void InsertColumn(int index, IEnumerable<Column<T>> columns)
    {
        foreach (var column in columns)
        {
            InsertColumn(index++, column);
            parity++;
        }
        parity = 0;
    }

    /// <summary>
    /// Insert <paramref name="column"/> at <paramref name="index"/>.
    /// </summary>
    public void InsertColumn(int index, Column<T> column)
    {
        var values = column.Values.PadRight(Records.Count);

        for (int i = 0; i < Records.Count; i++)
        {
            Records[i].Insert(index, values[i]);
        }

        headers.Insert(index, column.Header);

        RecordTransaction(
            () => RemoveColumn(column.Header),
            () => InsertColumn(index, column)
        );
    }

    /// <summary>
    /// Remove <paramref name="columns"/>.
    /// </summary>
    /// <exception cref="IndexOutOfRangeException">A column does not exist.</exception>
    public void RemoveColumn(IEnumerable<T> headers)
    {
        foreach (var header in headers)
        {
            RemoveColumn(header);
            parity++;
        }
        parity = 0;
    }

    /// <summary>
    /// Remove <paramref name="column"/>.
    /// </summary>
    /// <exception cref="IndexOutOfRangeException">A column does not exist.</exception>
    public void RemoveColumn(T header)
    {
        // Remove header first to prevent binding failures
        int index = headers.IndexOf(header);
        headers.Remove(header);

        var removed = RemoveColumn(index).ToList();

        RecordTransaction(
            () => InsertColumn(index, new Column<T>(header, removed)),
            () => RemoveColumn(header)
        );
    }

    /// <summary>
    /// Apply the values in <paramref name="cells"/> to the table.
    /// </summary>
    public void SetCell(IEnumerable<Cell<T>> cells)
    {
        foreach (var cell in cells)
        {
            SetCell(cell);
            parity++;
        }
        parity = 0;
    }

    /// <summary>
    /// Apply the value in <paramref name="cell"/> to the table.
    /// </summary>
    public void SetCell(Cell<T> cell)
    {
        // Let RecordChanged record the transaction
        Records[cell.Row][cell.Column] = cell.Value;
    }

    /// <summary>
    /// Find <paramref name="value"/> in the designated <paramref name="cells"/>.
    /// </summary>
    /// <param name="cells">Range of cell to perform the lookup. Skip to indicate whole table.</param>
    /// <returns>An enumerable of <see cref="Cell{T}"/> having <paramref name="value"/></returns>
    public IEnumerable<Cell<T>> FindCell(T value, IEnumerable<Cell<T>>? cells = null)
    {
        cells ??= this.ToCells();
        return cells.Where(x => Equals(x.Value, value));
    }

    /// <summary>
    /// Replace <paramref name="from"/> with <paramref name="to"/> in the designated <paramref name="cells"/>.
    /// </summary>
    /// <param name="cells">Range of cell to perform the lookup. Skip to indicate whole table.</param>
    public void ReplaceCell(T from, T to, IEnumerable<Cell<T>>? cells = null)
    {
        cells = FindCell(from, cells);
        
        var newCells = cells.Select(x => new Cell<T>(x.Row, x.Column, to));
        SetCell(newCells);
    }

    /// <summary>
    /// Find the first occurrence of <paramref name="row"/> in the table.
    /// </summary>
    /// <returns>The zero-based index of <paramref name="row"/> in the table. If not found, -1.</returns>
    public int RowIndexOf(IList<T?> row)
    {
        if (row is ObservableCollection<T?> collection)
        {
            return Records.IndexOf(collection);
        }

        for (int i = 0; i < Records.Count; i++)
        {
            if (Enumerable.SequenceEqual(Records[i], row))
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Find the first occurrence of <paramref name="column"/> in the table.
    /// </summary>
    /// <returns>The zero-based index of <paramref name="column"/> in the table. If not found, -1.</returns>
    public int ColumnIndexOf(Column<T> column)
    {
        for (int i = 0; i < headers.Count; i++)
        {
            var extractedColumn = Records.Select(x => x[i]);

            if (column.Equals(headers[i], extractedColumn))
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Find the first occurrence of <paramref name="value"/>, from left-to-right, then top-to-bottom. 
    /// </summary>
    /// <returns>The row and column of the cell. If not found, (-1, -1).</returns>
    public (int, int) CellIndexOf(T value)
    {
        var cells = FindCell(value);

        if (cells.Any())
        {
            Cell<T> cell = cells.First();
            return (cell.Row, cell.Column);
        }

        return (-1, -1);
    }

    /// <summary>
    /// Undo the last transaction. Nothing happens if there are nothing to undo.
    /// </summary>
    public void Undo()
    {
        ProcessHistory(undo, redo, true);
    }

    /// <summary>
    /// Redo the last transaction. Nothing happens if there are nothing to redo.
    /// </summary>
    public void Redo()
    {
        ProcessHistory(redo, undo, false);
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Removes a column at <paramref name="index"/>.
    /// </summary>
    /// <returns>The removed column</returns>
    private IEnumerable<T?> RemoveColumn(int index)
    {
        foreach (var record in Records)
        {
            yield return record[index];
            record.RemoveAt(index);
        }
    }

    /// <summary>
    /// Removes a row at <paramref name="index"/>.
    /// </summary>
    private void RemoveRow(int index)
    {
        var row = Records[index];
        Records.RemoveAt(index);

        RecordTransaction(
            () => InsertRow(index, row),
            () => RemoveRow(index)
        );
    }

    /// <summary>
    /// Provides history-handling for SetCell() and direct cell access (Records[y][x] = ...)
    /// </summary>
    private void RecordChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (
            e.Action != NotifyCollectionChangedAction.Replace
            || sender is null
        ) { return; }

        var record = (ObservableCollection<T?>)sender;
        int row = Records.IndexOf(record);
        int column = e.OldStartingIndex;
        var oldCell = (T?)e.OldItems?[0];
        var newCell = (T?)e.NewItems?[0];

        RecordTransaction(
            () => SetCell(new Cell<T>(row, column, oldCell)),
            () => SetCell(new Cell<T>(row, column, newCell))
        );
    }

    /// <summary>
    /// Push a transaction to the history stack and invoke the TableModified event.
    /// </summary>
    /// <param name="undoAction">Action to reverse the transaction</param>
    /// <param name="redoAction">Action that caused this transaction</param>
    private void RecordTransaction(Action undoAction, Action redoAction)
    {
        EditEventArgs edit = new(undoAction, redoAction, parity);
        TableModified?.Invoke(this, edit);

        if (!recordTransactions) { return; }
        undo.Push(edit);
        redo.Clear();
    }

    /// <summary>
    /// Undo/redo a transaction without modifying the history stacks.
    /// </summary>
    private void RevertHistory(EditEventArgs edit, bool isUndo)
    {
        recordTransactions = false;
        edit.Invoke(isUndo);
        recordTransactions = true;
    }

    /// <summary>
    /// Handle both undo and redo with similar logic.
    /// </summary>
    /// <param name="stack">An undo or redo stack.</param>
    /// <param name="opposite">The stack opposite to <paramref name="stack"/> (i.e. redo if <paramref name="stack"/> is undo, and vice versa)</param>
    private void ProcessHistory(Stack<EditEventArgs> stack, Stack<EditEventArgs> opposite, bool isUndo)
    {
        int offset = isUndo ? 1 : -1;

        while (stack.TryPop(out EditEventArgs? last))
        {
            opposite.Push(last);
            RevertHistory(last, isUndo);

            stack.TryPeek(out EditEventArgs? next);
            if (last.Parity != next?.Parity + offset) { return; }
        }
    }

    #endregion Private Methods
}
