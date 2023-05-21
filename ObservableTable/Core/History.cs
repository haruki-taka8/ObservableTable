using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnitTest")]
namespace ObservableTable.Core;

internal interface IEdit
{
    internal int Parity { get; init; }

    // Unused for CellEdit<T>
    internal bool IsInsert { get; set; }
    internal int Index { get; init; }

    public IEdit DeepClone()
    {
        throw new NotImplementedException();
    }
}

internal class RowEdit<T> : Row<T>, IEdit
{
    public int Parity { get; init; }
    public int Index { get; init; }
    public bool IsInsert { get; set; }

    internal RowEdit(int parity, bool isInsert, int index, IList<T?> row) : base(row)
    {
        Parity = parity;
        IsInsert = isInsert;
        Index = index;
    }

    IEdit IEdit.DeepClone()
    {
        return new RowEdit<T>(Parity, IsInsert, Index, this);
    }
}

internal class ColumnEdit<T> : Column<T>, IEdit
{
    public int Parity { get; init; }
    public int Index { get; init; }
    public bool IsInsert { get; set; }

    internal ColumnEdit(int parity, bool isInsert, int index, T header, IList<T?> values) : base(header, values)
    {
        Parity = parity;
        IsInsert = isInsert;
        Index = index;
    }
    internal ColumnEdit(int parity, bool isInsert, int index, Column<T> column) : this(parity, isInsert, index, column.Header, column.Values)
    { }

    IEdit IEdit.DeepClone()
    {
        return new ColumnEdit<T>(Parity, IsInsert, Index, Header, Values);
    }
}

internal class ColumnRenameEdit<T> : IEdit
{
    public int Parity { get; init; }
    public int Index { get; init; }
    public T Header { get; set; }

    // Unused members
    public bool IsInsert { get; set; }

    internal ColumnRenameEdit(int parity, int index, T header)
    {
        Parity = parity;
        Index = index;
        Header = header;
    }

    IEdit IEdit.DeepClone()
    {
        return new ColumnRenameEdit<T>(Parity, Index, Header);
    }
}

internal class CellEdit<T> : Cell<T>, IEdit
{
    public int Parity { get; init; }

    // Unused members
    public int Index { get; init; }
    public bool IsInsert { get; set; }

    internal CellEdit(int parity, int rowIndex, int columnIndex, T? value) : base(rowIndex, columnIndex, value)
    {
        Parity = parity;
    }
    internal CellEdit(int parity, Cell<T> cell) : base(cell.RowIndex, cell.ColumnIndex, cell.Value)
    { 
        Parity = parity;
    }

    IEdit IEdit.DeepClone()
    {
        return new CellEdit<T>(Parity, this);
    }
}

internal class ReorderEdit<T> : IEdit
{
    public int Parity { get; init; }
    public int OldIndex { get; init; }
    public int NewIndex { get; init; }
    public bool IsColumn { get; init; }
    public bool IsUndo { get; set; }

    // Unused members
    public int Index { get; init; }
    public bool IsInsert { get; set; }

    internal ReorderEdit(int parity, int oldIndex, int newIndex, bool isColumn)
    {
        Parity = parity;
        OldIndex = oldIndex;
        NewIndex = newIndex;
        IsColumn = isColumn;
    }

    IEdit IEdit.DeepClone()
    {
        return new ReorderEdit<T>(Parity, OldIndex, NewIndex, IsColumn);
    }
}
