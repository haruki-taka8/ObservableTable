using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace ObservableTable.Core;

public class ObservableTable<T>
{
    // Properties & Fields
    public ObservableCollection<ObservableCollection<T?>> Records { get; } = new();
    public ObservableCollection<T> Headers { get; init; } = new();

    public int UndoCount => UndoStack.Count;
    public int RedoCount => RedoStack.Count;

    private readonly ObservableStack<Operation<T>> UndoStack = new();
    private readonly ObservableStack<Operation<T>> RedoStack = new();
    private bool parity;

    // Constructors
    public ObservableTable() { }

    public ObservableTable(IList<T> headers, IEnumerable<T?[]> records)
    {
        Headers = new(headers);

        foreach (var record in records)
        {
            // Register CollectionChanged for each row
            ObservableCollection<T?> toAdd = new(record.PadRight(Headers.Count));
            toAdd.CollectionChanged += RecordChanged;
            Records.Add(toAdd);
        }
    }

    public ObservableTable(IEnumerable<T> headers, IEnumerable<T?[]> records) : this(new List<T>(headers), records) { }

    // Methods: Private
    private void RecordChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action != NotifyCollectionChangedAction.Replace
            || sender is null
            || e.OldItems is null)
        { return; }

        // Handles inline changes
        var newRecord = (ObservableCollection<T?>)sender;
        var index = Records.IndexOf(newRecord);
        var oldCell = (T?)e.OldItems[0];
        UndoStack.Push(new(Change.Inline, index, parity, oldCell, e.OldStartingIndex));
        CommitHistory();
    }

    // Methods: Row/Column Modifications
    public void InsertRow(int index, params IList<T?>[] items)
    {
        if (index < 0 || index > Records.Count - 1) { return; }

        foreach (var item in items.Reverse())
        {
            IList<T?> baseToAdd = item.PadRight(Headers.Count);
            ObservableCollection<T?> toAdd = new(baseToAdd);
            toAdd.CollectionChanged += RecordChanged;

            Records.Insert(index, toAdd);
            UndoStack.Push(new(Change.InsertRow, index, parity, baseToAdd));
        }
        CommitHistory();
    }

    public void RemoveRow(params ObservableCollection<T?>[] items)
    {
        foreach (var item in items)
        {
            UndoStack.Push(new(Change.RemoveRow, Records.IndexOf(item), parity, item));
            Records.Remove(item);
        }
        CommitHistory();
    }

    public void InsertColumn(int index, params (T Header, IEnumerable<T?> Content)[] payload)
    {
        if (index > Headers.Count - 1 || index < 0) { return; }

        foreach (var (header, content) in payload)
        {
            InsertColumn(index, header, content);
            UndoStack.Push(new(Change.InsertColumn, index, parity, header, content));
            index++;
        }
        CommitHistory();
    }

    private void InsertColumn(int index, T header, IEnumerable<T?> content)
    {
        for (int i = 0; i < Records.Count; i++)
        {
            Records[i].Insert(index, content.ElementAtOrDefault(i));
        }
        Headers.Insert(index, header);
    }

    public void RemoveColumn(params T[] headers)
    {
        foreach (var header in headers)
        {
            int index = Headers.IndexOf(header);
            if (index == -1) { return; }

            var removedColumn = RemoveColumn(index);
            UndoStack.Push(new(Change.RemoveColumn, index, parity, header, removedColumn));
        }
        CommitHistory();
    }

    private IEnumerable<T?> RemoveColumn(int index)
    {
        // Remove header first to prevent binding failures
        Headers.RemoveAt(index);

        List<T?> column = new();
        foreach (var record in Records)
        {
            column.Add(record[index]);
            record.RemoveAt(index);
        }
        return column;
    }

    // Methods: History
    private void RevertHistory(Operation<T> last)
    {
        switch (last.Change)
        {
            case Change.InsertRow:
                if (last.Row is null) { break; }
                Records.Insert(last.Index, new(last.Row));
                break;

            case Change.RemoveRow:
                Records.RemoveAt(last.Index);
                break;

            case Change.InsertColumn:
                if (last.Header is null || last.Column is null) { return; } 
                InsertColumn(last.Index, last.Header, last.Column);
                break;

            case Change.RemoveColumn:
                RemoveColumn(last.Index);
                break;

            case Change.Inline:
                // Avoid pushing this change to UndoStack
                Records[last.Index].CollectionChanged -= RecordChanged;
                Records[last.Index][last.CellIndex ?? 0] = last.Cell;
                Records[last.Index].CollectionChanged += RecordChanged;
                break;
        }
    }

    private Operation<T> UpdateCellInOperation(Operation<T> operation)
    {
        var output = operation.DeepCopy();
        if (output.Change == Change.Inline)
        {
            output.Cell = Records[output.Index][output.CellIndex ?? 0];
        }
        return output;
    }

    public void Undo()
    {
        if (UndoStack.Count == 0) { return; }
        Operation<T> last = UndoStack.Pop();
        RedoStack.Push(UpdateCellInOperation(last));

        last.InvertChange();
        RevertHistory(last);

        if (UndoStack.Count > 0 && last.Parity == UndoStack.Peek().Parity)
        { Undo(); }
    }

    public void Redo()
    {
        if (RedoStack.Count == 0) { return; }
        Operation<T> last = RedoStack.Pop();
        UndoStack.Push(UpdateCellInOperation(last));

        RevertHistory(last);

        if (RedoStack.Count > 0 && last.Parity == RedoStack.Peek().Parity)
        { Redo(); }
    }

    private void CommitHistory()
    {
        RedoStack.Clear();
        parity = !parity;
    }
}
