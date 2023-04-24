namespace ObservableTable.Core;

public readonly struct ColumnDefinition<T>
{
    public T Header { get; init; }
    public IList<T?> Values { get; init; }

    public ColumnDefinition(T header)
    {
        Header = header;
        Values = new List<T?>();
    }
    public ColumnDefinition(T header, IList<T?> values)
    {
        Header = header;
        Values = values;
    }
}

public readonly struct RowDefinition<T>
{
    public IList<T?> Values { get; init; }

    public RowDefinition()
    {
        Values = new List<T?>();
    }
    public RowDefinition(IList<T?> values)
    {
        Values = values;
    }
}

public readonly struct CellDefinition<T>
{
    public int Row { get; init; }
    public int Column { get; init; }
    public T? Value { get; init; }

    public CellDefinition(int row, int column, T? value)
    {
        Row = row;
        Column = column;
        Value = value;
    }
}
