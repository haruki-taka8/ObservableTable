using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ObservableTable.Core;

public class ObservableTable<T>
{
    // Properties & Fields
    public ObservableCollection<ObservableCollection<T?>> Records { get; } = new();
    public ReadOnlyObservableCollection<T> Headers => new(headers);
    public int UndoCount => undo.Count;
    public int RedoCount => redo.Count;
    public event EventHandler? TableModified;

    private readonly ObservableCollection<T> headers = new();
    private Stack<Edit> undo = new();
    private Stack<Edit> redo = new();
    private bool recordTransactions;
    private int parity;

    // Constructors
    public ObservableTable()
    {
        recordTransactions = true;
    }

    public ObservableTable(IEnumerable<T> headers, IEnumerable<IList<T?>> records)
    {
        this.headers = new(headers);
        InsertRow(0, records);
        recordTransactions = true;
    }

    public ObservableTable(IEnumerable<T> headers, params IList<T?>[] records) : this(headers, records.AsEnumerable())
    { }

    // Methods: Record modifications
    public void InsertRow(int index, IEnumerable<IList<T?>> rows)
    {
        parity = rows.Count();
        foreach (var row in rows)
        {
            parity--;
            InsertRow(index++, row);
        }
    }

    public void InsertRow(int index, params IList<T?>[] rows)
    {
        InsertRow(index, rows.AsEnumerable());
    }

    private void InsertRow(int index, IList<T?> row)
    {
        row = row.PadRight(headers.Count);
        ObservableCollection<T?> toAdd = new(row);
        toAdd.CollectionChanged += RecordChanged;
        Records.Insert(index, toAdd);

        RecordTransaction(
            () => RemoveRow(Records[index]),
            () => InsertRow(index, row)
        );
    }

    public void RemoveRow(IEnumerable<ObservableCollection<T?>> rows)
    {
        parity = rows.Count();
        foreach (var row in rows)
        {
            parity--;
            RemoveRow(row);
        }
    }

    public void RemoveRow(params ObservableCollection<T?>[] rows)
    {
        RemoveRow(rows.AsEnumerable());
    }

    private void RemoveRow(ObservableCollection<T?> row)
    {
        int index = Records.IndexOf(row);
        Records.Remove(row);

        RecordTransaction(
            () => InsertRow(index, row),
            () => RemoveRow(Records[index])
        );
    }

    public void ReorderRow(int oldIndex, int newIndex)
    {
        Records.Move(oldIndex, newIndex);

        RecordTransaction(
            () => ReorderRow(newIndex, oldIndex),
            () => ReorderRow(oldIndex, newIndex)
        );
    }

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

    public void RenameColumn(int index, T header)
    {
        T oldHeader = headers[index];
        headers[index] = header;

        RecordTransaction(
            () => RenameColumn(index, oldHeader),
            () => RenameColumn(index, header)
        );
    }

    public void InsertColumn(int index, IEnumerable<Column<T>> columns)
    {
        parity = columns.Count();
        foreach (var column in columns)
        {
            parity--;
            InsertColumn(index++, column);
        }
    }

    public void InsertColumn(int index, params Column<T>[] columns)
    {
        InsertColumn(index, columns.AsEnumerable());
    }

    private void InsertColumn(int index, Column<T> column)
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

    public void RemoveColumn(IEnumerable<T> headers)
    {
        parity = headers.Count();
        foreach (var header in headers)
        {
            parity--;
            RemoveColumn(header);
        }
    }

    public void RemoveColumn(params T[] headers)
    {
        RemoveColumn(headers.AsEnumerable());
    }

    private void RemoveColumn(T header)
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

    private IEnumerable<T?> RemoveColumn(int index)
    {
        foreach (var record in Records)
        {
            yield return record[index];
            record.RemoveAt(index);
        }
    }

    public void SetCell(IEnumerable<Cell<T>> cells)
    {
        parity = cells.Count();
        foreach (var cell in cells)
        {
            parity--;
            SetCell(cell);
        }
    }

    public void SetCell(params Cell<T>[] cells)
    {
        SetCell(cells.AsEnumerable());
    }

    private void SetCell(Cell<T> cell)
    {
        // Let RecordChanged record the transaction
        Records[cell.Row][cell.Column] = cell.Value;
    }

    private static IEnumerable<Cell<T>> ReplacedCells(T from, T to, IEnumerable<Cell<T>> cells)
    {
        if (from is null) { yield break; }

        foreach (var cell in cells)
        {
            if (!from.Equals(cell.Value)) { continue; }

            Cell<T> newCell = new(cell.Row, cell.Column, to);
            yield return newCell;
        }
    }

    public void ReplaceCell(T from, T to, IEnumerable<Cell<T>>? cells = null)
    {
        cells ??= this.ListCells();

        var toChange = ReplacedCells(from, to, cells).ToList();
        SetCell(toChange);
    }

    public void ReplaceCell(T from, T to, params Cell<T>[] cells)
    {
        ReplaceCell(from, to, cells.ToList());
    }

    // Methods: History
    internal void RecordChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (
            e.Action != NotifyCollectionChangedAction.Replace
            || sender is null
        ) { return; }

        // Handles inline changes
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

    private void RecordTransaction(Action undoAction, Action redoAction)
    {
        TableModified?.Invoke(this, new());

        if (!recordTransactions) { return; }
        undo.Push(new(undoAction, redoAction, parity));
        redo.Clear();
    }

    private void RevertHistory(Edit edit, bool isUndo)
    {
        recordTransactions = false;
        edit.Invoke(isUndo);
        recordTransactions = true;
    }

    private void ProcessHistory(ref Stack<Edit> stack, ref Stack<Edit> opposite, bool isUndo)
    {
        int offset = isUndo ? -1 : 1;

        while (stack.TryPop(out Edit last))
        {
            opposite.Push(last);
            RevertHistory(last, isUndo);

            stack.TryPeek(out Edit next);
            if (last.Parity != next.Parity + offset) { return; }
        }
    }

    public void Undo()
    {
        ProcessHistory(ref undo, ref redo, true);
    }

    public void Redo()
    {
        ProcessHistory(ref redo, ref undo, false);
    }
}
