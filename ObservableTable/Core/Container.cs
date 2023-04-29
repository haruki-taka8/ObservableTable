﻿using System.Collections.ObjectModel;

namespace ObservableTable.Core;

public class Column<T>
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

public class Row<T> : Collection<T?>, IList<T?>
{
    public Row() : base() { }
    public Row(IList<T?> values) : base(values) { }
    }

public class Cell<T>
{
    public int Row { get; init; }
    public int Column { get; init; }
    public T? Value { get; set; }

    public Cell(int row, int column, T? value)
    {
        Row = row;
        Column = column;
        Value = value;
    }
}
