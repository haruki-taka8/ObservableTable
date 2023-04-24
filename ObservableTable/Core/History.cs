using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTest")]
namespace ObservableTable.Core;

internal enum Change
{
    Cell,
    InsertRow,
    RemoveRow,
    InsertColumn,
    RemoveColumn
}

internal class OperationBase
{
    internal int Parity { get; init; }
    internal Change Change { get; set; }
    internal virtual OperationBase DeepCopy() { return new(); }

    // Unused for CellOperation<T>
    internal int Index { get; init; }
    internal virtual void InvertOperation() { }
}

internal class RowOperation<T> : OperationBase
{
    internal RowDefinition<T> Row { get; init; }

    internal override void InvertOperation()
    {
        Change = Change == Change.InsertRow ? Change.RemoveRow : Change.InsertRow;
    }

    internal override RowOperation<T> DeepCopy() => new(Parity, Index, Change, Row);

    public RowOperation(int parity, int index, Change change, RowDefinition<T> row)
    {
        Parity = parity;
        Index = index;
        Change = change;
        Row = row;
    }
}

internal class ColumnOperation<T> : OperationBase
{
    internal ColumnDefinition<T> Column { get; init; }

    internal override void InvertOperation()
    {
        Change = Change == Change.InsertColumn ? Change.RemoveColumn : Change.InsertColumn;
    }

    internal override ColumnOperation<T> DeepCopy() => new(Parity, Index, Change, Column);

    public ColumnOperation(int parity, int index, Change change, ColumnDefinition<T> column)
    {
        Parity = parity;
        Index = index;
        Change = change;
        Column = column;
    }
}
internal class CellOperation<T> : OperationBase
{
    internal CellDefinition<T> Cell { get; set; }

    internal override CellOperation<T> DeepCopy() => new(Parity, Cell);

    public CellOperation(int parity, CellDefinition<T> cell)
    {
        Parity = parity;
        Cell = cell;
    }
}
