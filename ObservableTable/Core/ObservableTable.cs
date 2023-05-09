using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTest")]
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
    private readonly Stack<IEdit> undo = new();
    private readonly Stack<IEdit> redo = new();
    private bool recordTransactions;
    private int parity;

    // Constructors
    public ObservableTable() 
    {
        recordTransactions = true;
    }

    public ObservableTable(IEnumerable<T> headers, params IList<T?>[] records)
    {
        this.headers = new(headers);
        InsertRow(0, records);
        recordTransactions = true;
    }

    // Methods: Record modifications
    public void InsertRow(int index, params IList<T?>[] rows)
    {
        parity = rows.Length;
        foreach (var row in rows)
        {
            parity--;
            InsertRow(index++, row);
        }
    }

    private void InsertRow(int index, IList<T?> row)
    {
        row = row.PadRight(headers.Count);
        ObservableCollection<T?> toAdd = new(row);
        toAdd.CollectionChanged += RecordChanged;
        Records.Insert(index, toAdd);

        RecordTransaction(new RowEdit<T>(parity, true, index, row));
    }

    public void RemoveRow(params ObservableCollection<T?>[] rows)
    {
        parity = rows.Length;
        foreach (var row in rows)
        {
            parity--;
            RemoveRow(row);
        }
    }

    private void RemoveRow(ObservableCollection<T?> row)
    {
        RecordTransaction(new RowEdit<T>(parity, false, Records.IndexOf(row), row));
        Records.Remove(row);
    }

    public void RenameColumn(int index, T header)
    {
        RecordTransaction(new ColumnRenameEdit<T>(parity, index, headers[index]));
        headers[index] = header;
    }

    public void InsertColumn(int index, params Column<T>[] columns)
    {
        parity = columns.Length;
        foreach (var column in columns)
        {
            parity--;
            InsertColumn(index++, column);
        }
    }

    private void InsertColumn(int index, Column<T> column)
    {
        var values = column.Values.PadRight(Records.Count);

        for (int i = 0; i < Records.Count; i++)
        {
            Records[i].Insert(index, values[i]);
        }

        headers.Insert(index, column.Header);

        RecordTransaction(new ColumnEdit<T>(parity, true, index, column));
    }

    public void RemoveColumn(params T[] headers)
    {
        parity = headers.Length;
        foreach (var header in headers)
        {
            parity--;
            RemoveColumn(header);
        }
    }

    private void RemoveColumn(T header)
    {
        // Remove header first to prevent binding failures
        int index = headers.IndexOf(header);
        headers.Remove(header);

        List<T?> column = new();
        foreach (var record in Records)
        {
            column.Add(record[index]);
            record.RemoveAt(index);
        }

        RecordTransaction(new ColumnEdit<T>(parity, false, index, header, column));
    }

    public void SetCell(params Cell<T>[] cells)
    {
        parity = cells.Length;
        foreach (var cell in cells)
        {
            parity--;
            SetCell(cell);
        }
    }

    private void SetCell(Cell<T> cell)
    {
        // Let RecordChanged record the transcation
        Records[cell.RowIndex][cell.ColumnIndex] = cell.Value;
    }

    // Methods: History
    internal void RecordChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (
            e.Action != NotifyCollectionChangedAction.Replace
            || sender is null
            || e.OldItems is null
        ) { return; }

        // Handles inline changes
        var record = (ObservableCollection<T?>)sender;
        var row = Records.IndexOf(record);
        var cell = (T?)e.OldItems[0];

        RecordTransaction(new CellEdit<T>(parity, row, e.OldStartingIndex, cell));
    }

    private void RecordTransaction(IEdit operation)
    {
        if (!recordTransactions) { return; }
        undo.Push(operation);
        redo.Clear();

        if (TableModified is null) { return; }
        TableModified(this, new());
    }

    internal IEdit UpdateCellEdit(IEdit edit)
    {
        edit = edit.DeepClone<T>();

        if (edit is ColumnRenameEdit<T> renameEdit)
        {
            renameEdit.Header = Headers[renameEdit.Index];
            return renameEdit;
        }

        if (edit is CellEdit<T> cellEdit)
        {
            cellEdit.Value = Records[cellEdit.RowIndex][cellEdit.ColumnIndex];
            return cellEdit;
        }

        return edit;
    }

    private void RevertHistory(IEdit edit)
    {
        recordTransactions = false;
        switch (edit)
        {
            case RowEdit<T> row when edit.IsInsert:
                Records.Insert(row.Index, new(row));
                break;

            case RowEdit<T> row:
                Records.RemoveAt(row.Index);
                break;

            case ColumnRenameEdit<T> column:
                RenameColumn(column.Index, column.Header);
                break;

            case ColumnEdit<T> column when edit.IsInsert:
                InsertColumn(column.Index, column);
                break;

            case ColumnEdit<T> column:
                RemoveColumn(column.Header);
                break;

            case CellEdit<T> cell:
                SetCell(cell);
                break;
        }
        recordTransactions = true;
    }

    public void Undo()
    {
        if (undo.Count == 0) { return; }

        var last = undo.Pop();
        redo.Push(UpdateCellEdit(last));

        last.IsInsert = !last.IsInsert;
        RevertHistory(last);

        if (undo.Count == 0) { return; }
        if (last.Parity == undo.Peek().Parity - 1) { Undo(); }
    }

    public void Redo()
    {
        if (redo.Count == 0) { return; }

        var last = redo.Pop();
        undo.Push(UpdateCellEdit(last));

        RevertHistory(last);

        if (redo.Count == 0) { return; }
        if (last.Parity == redo.Peek().Parity + 1) { Redo(); }
    }
}
