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

internal class EditBase
{
    internal int Parity { get; init; }
    internal Change Change { get; set; }
    internal virtual EditBase DeepCopy() { return new(); }

    // Unused for CellOperation<T>
    internal int Index { get; init; }
    internal virtual void InvertOperation() { }
}

internal class RowEdit<T> : EditBase
    {
    internal RowDefinition<T> Row { get; init; }

    internal override void InvertOperation()
    {
        Change = Change == Change.InsertRow ? Change.RemoveRow : Change.InsertRow;
    }

    internal override RowEdit<T> DeepCopy() => new(Parity, Index, Change, Row);

    internal RowEdit(int parity, int index, Change change, RowDefinition<T> row)
    {
        Parity = parity;
        Index = index;
        Change = change;
        Row = row;
    }
}

internal class ColumnEdit<T> : EditBase
    {
    internal ColumnDefinition<T> Column { get; init; }

    internal override void InvertOperation()
    {
        Change = Change == Change.InsertColumn ? Change.RemoveColumn : Change.InsertColumn;
    }

    internal override ColumnEdit<T> DeepCopy() => new(Parity, Index, Change, Column);

    internal ColumnEdit(int parity, int index, Change change, ColumnDefinition<T> column)
    {
        Parity = parity;
        Index = index;
        Change = change;
        Column = column;
    }
}

internal class CellEdit<T> : EditBase
{
    internal CellDefinition<T> Cell { get; set; }

    internal override CellEdit<T> DeepCopy() => new(Parity, Cell);

    internal CellEdit(int parity, CellDefinition<T> cell)
    {
        Parity = parity;
        Cell = cell;
    }
}
