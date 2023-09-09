<<<<<<< HEAD:ObservableTable/Cell.cs
﻿namespace ObservableTable.Core;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObservableTable.Core;
>>>>>>> main:ObservableTable/Core/Cell.cs

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


    // IEquatable implementation
    public override int GetHashCode()
    {
        return HashCode.Combine(Row, Column, Value);
    }

    public bool Equals(Cell<T> other)
    {
        return GetHashCode() == other.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is Cell<T> other && Equals(other);
    }

    public static bool operator ==(Cell<T> left, Cell<T> right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Cell<T> left, Cell<T> right)
    {
        return !(left == right);
    }
}
