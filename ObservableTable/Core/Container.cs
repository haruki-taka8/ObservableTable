using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace ObservableTable.Core;

/// <summary>
/// Describe a column of type <typeparamref name="T"/>.
/// </summary>
public readonly struct Column<T>
{ 
    public T Header { get; init; }
    public IList<T?> Values { get; init; }

    /// <summary>
    /// Describe a column named <paramref name="header"/> with no rows.
    /// </summary>
    public Column(T header)
    {
        Header = header;
        Values = new List<T?>();
    }

    /// <summary>
    /// Describe a column named <paramref name="header"/> with rows <paramref name="values"/>.
    /// </summary>
    public Column(T header, IList<T?> values)
    {
        Header = header;
        Values = values;
    }

    public bool Equals(Column<T> column)
    {
        return Equals(column.Header, column.Values);
    }

    public bool Equals(T header, IEnumerable<T?> values)
    {
        return Header!.Equals(header)
            && Enumerable.SequenceEqual(Values, values);
    }
}

/// <summary>
/// Describe a cell of type <typeparamref name="T"/>.
/// </summary>
public readonly struct Cell<T>
{
    public int Row { get; init; }
    public int Column { get; init; }
    public T? Value { get; init; }

    /// <param name="row">Row index of the cell</param>
    /// <param name="column">Column index of the cell</param>
    /// <param name="value">Value of the cell</param>
    public Cell(int row, int column, T? value)
    {
        Row = row;
        Column = column;
        Value = value;
    }
}
