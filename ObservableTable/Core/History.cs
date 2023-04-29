using System.Runtime.CompilerServices;
using System.Text.Json;

[assembly: InternalsVisibleTo("UnitTest")]
namespace ObservableTable.Core;

internal interface IEdit
{
    internal int Parity { get; init; }

    // Unused for CellEdit<T>
    internal bool IsInsert { get; set; }
    internal int Index { get; init; }

    public IEdit DeepClone<T>()
    {
        if (this is RowEdit<T> row)
        {
            return new RowEdit<T>(row.Parity, row.IsInsert, row.Index, row);
        }
        if (this is ColumnEdit<T> column)
        {
            return new ColumnEdit<T>(column.Parity, column.IsInsert, column.Index, column.Header, column.Values);
        }
        var cell = (CellEdit<T>)this;
        return new CellEdit<T>(cell.Parity, cell.Row, cell.Column, cell.Value);
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
}

internal class ColumnEdit<T> : Column<T>, IEdit
{
    public int Parity { get; init; }
    public int Index { get; init; }
    public bool IsInsert { get; set; }

    internal void DeepCopy(out IEdit edit)
    {
        var serialized = JsonSerializer.Serialize(this);
        edit = JsonSerializer.Deserialize<RowEdit<T>>(serialized) ?? throw new NullReferenceException(serialized);
    }

    internal ColumnEdit(int parity, bool isInsert, int index, T header, IList<T?> values) : base(header, values)
    {
        Parity = parity;
        IsInsert = isInsert;
        Index = index;
    }
    internal ColumnEdit(int parity, bool isInsert, int index, Column<T> column) : this(parity, isInsert, index, column.Header, column.Values)
    { }
}

internal class CellEdit<T> : Cell<T>, IEdit
{
    public int Parity { get; init; }

    // Unused members
    public int Index { get; init; }
    public bool IsInsert { get; set; }

    internal CellEdit(int parity, int row, int column, T? value) : base(row, column, value)
    {
        Parity = parity;
    }
    internal CellEdit(int parity, Cell<T> cell) : base(cell.Row, cell.Column, cell.Value)
    { 
        Parity = parity;
    }
}
