namespace ObservableTable.Core;

public readonly struct Column<T>
{
    public T Header { get; init; }
    public IList<T?> Values { get; init; }

    public Column(T header)
    {
        Header = header;
        Values = new List<T?>();
    }

    public Column(T header, IList<T?> values)
    {
        Header = header;
        Values = values;
    }
}

public readonly struct Cell<T>
{
    public int Row { get; init; }
    public int Column { get; init; }
    public T? Value { get; init; }

    public Cell(int row, int column, T? value)
    {
        Row = row;
        Column = column;
        Value = value;
    }
}
