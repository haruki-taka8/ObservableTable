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

    private ObservableCollection<T> headers { get; } = new();
    private readonly Stack<EditBase> undo = new();
    private readonly Stack<EditBase> redo = new();
    private bool recordTransaction;
    private int parity;

    // Constructors
    public ObservableTable() { }

    public ObservableTable(IEnumerable<T> headers, params IList<T?>[] records)
    {
        this.headers = new(headers);
        InsertRow(0, records);
        recordTransaction = true;
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
        IList<T?> baseToAdd = row.PadRight(headers.Count);
        ObservableCollection<T?> toAdd = new(baseToAdd);
        toAdd.CollectionChanged += RecordChanged;
        Records.Insert(index, toAdd);

        RecordTransaction(new RowEdit<T>(parity, index, Change.InsertRow, new RowDefinition<T>(baseToAdd)));
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
        RecordTransaction(new RowEdit<T>(parity, Records.IndexOf(row), Change.RemoveRow, new RowDefinition<T>(row)));
        Records.Remove(row);
    }

    public void InsertColumn(int index, params ColumnDefinition<T>[] columns)
    {
        parity = columns.Length;
        foreach (var column in columns)
        {
            parity--;
            InsertColumn(index++, column);
        }
    }

    private void InsertColumn(int index, ColumnDefinition<T> column)
    {
        var values = column.Values.PadRight(Records.Count);

        for (int i = 0; i < Records.Count; i++)
        {
            Records[i].Insert(index, values[i]);
        }

        headers.Insert(index, column.Header);

        RecordTransaction(new ColumnEdit<T>(parity, index, Change.InsertColumn, column));
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

        List<T?> removedColumn = new();
        foreach (var record in Records)
        {
            removedColumn.Add(record[index]);
            record.RemoveAt(index);
        }

        RecordTransaction(new ColumnEdit<T>(parity, index, Change.RemoveColumn, new ColumnDefinition<T>(header, removedColumn)));
    }

    public void SetCell(params CellDefinition<T>[] cells)
    {
        parity = cells.Length;
        foreach (var cell in cells)
        {
            parity--;
            SetCell(cell);
        }
    }

    private void SetCell(CellDefinition<T> cell)
    {
        // Let RecordChanged record the transcation
        Records[cell.Row][cell.Column] = cell.Value;
    }

    // Methods: History
    internal void RecordChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (
            e.Action != NotifyCollectionChangedAction.Replace
            || !recordTransaction
            || sender is null
            || e.OldItems is null
        ) { return; }

        // Handles inline changes
        var newRecord = (ObservableCollection<T?>)sender;
        var index = Records.IndexOf(newRecord);
        var oldCell = (T?)e.OldItems[0];

        RecordTransaction(new CellEdit<T>(parity, new(index, e.OldStartingIndex, oldCell)));
    }

    internal void RecordTransaction(EditBase operation)
    {
        if (!recordTransaction) { return; }
        undo.Push(operation);
        redo.Clear();
    }

    internal CellEdit<T> UpdateCellOperation(CellEdit<T> operation)
    {
        var old = operation.Cell;
        operation.Cell = new(old.Row, old.Column, Records[old.Row][old.Column]);
        return operation;
    }

    private void RevertHistory(EditBase operation)
    {
        recordTransaction = false;

        switch (operation.Change)
        {
            case Change.InsertRow:
                var row = (RowEdit<T>)operation;
                Records.Insert(row.Index, new(row.Row.Values));
                break;

            case Change.RemoveRow:
                Records.RemoveAt(operation.Index);
                break;

            case Change.InsertColumn:
                var column = (ColumnEdit<T>)operation;
                InsertColumn(column.Index, column.Column);
                break;

            case Change.RemoveColumn:
                column = (ColumnEdit<T>)operation;
                RemoveColumn(column.Column.Header);
                break;

            case Change.Cell:
                var cell = (CellEdit<T>)operation;
                SetCell(cell.Cell);
                break;
        }
        recordTransaction = true;
    }

    public void Undo()
    {
        if (undo.Count == 0) { return; }

        EditBase last = undo.Pop();

        var toPush = last.DeepCopy();
        if (last.Change == Change.Cell) { toPush = UpdateCellOperation((CellEdit<T>)toPush); }
        redo.Push(toPush);

        last.InvertOperation();
        RevertHistory(last);

        if (undo.Count == 0) { return; }
        if (last.Parity == undo.Peek().Parity - 1) { Undo(); }
    }

    public void Redo()
    {
        if (redo.Count == 0) { return; }

        EditBase last = redo.Pop();

        var toPush = last.DeepCopy();
        if (last.Change == Change.Cell) { toPush = UpdateCellOperation((CellEdit<T>)toPush); }
        undo.Push(toPush);

        RevertHistory(last);

        if (redo.Count == 0) { return; }
        if (last.Parity == redo.Peek().Parity + 1) { Redo(); }
    }
}
