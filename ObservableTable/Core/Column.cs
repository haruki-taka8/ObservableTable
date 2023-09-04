namespace ObservableTable.Core;

/// <summary>
/// Describe a column of type <typeparamref name="T"/>.
/// </summary>
public readonly struct Column<T> : IEquatable<Column<T>> where T : notnull
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


    // IEquatable implementation
    public override int GetHashCode()
    {
        HashCode hashCode = new();
        hashCode.Add(Header);

        foreach (var item in Values)
        {
            hashCode.Add(item);
        }

        return hashCode.ToHashCode();
    }
    
    public bool Equals(Column<T> other)
    {
        return GetHashCode() == other.GetHashCode();
    } 
    
    public override bool Equals(object? obj)
    {
        return obj is Column<T> column && Equals(column);
    }
    
    public static bool operator ==(Column<T> left, Column<T> right)
    {
        return left.Equals(right);
    }
    
    public static bool operator !=(Column<T> left, Column<T> right)
    {
        return !(left == right);
    }
}