namespace ObservableTable.Core;

internal enum Change
{
    Inline,
    InsertRow,
    RemoveRow,
    InsertColumn,
    RemoveColumn
}

internal class Operation<T>
{
    // Properties
    #region Change, Index, Parity, Payloads
    internal Change Change { get; set; }
    internal int Index { get; init; }
    internal bool Parity { get; init; }

    // Properties (Payloads: column, row, cell)
    internal T? Header { get; init; }
    internal IEnumerable<T?>? Column { get; init; }
    internal IEnumerable<T?>? Row { get; init; }
    internal T? Cell { get; set; } // set; since cell content can change on each undo/redo
    internal int? CellIndex { get; init; }
    #endregion Fields

    // Constructors
    #region private Operation()
    private Operation(Change change, int index, bool parity)
    {
        Change = change;
        Index = index;
        Parity = parity;
    }

    /// <summary>
    /// Use this constructor for insertion/removal of a column
    /// </summary>
    /// <param name="change">Must be Change.InsertColumn or Change.RemoveColumn</param>
    internal Operation(Change change, int index, bool parity, T header, IEnumerable<T?> column) : this(change, index, parity)
    {
        Header = header;
        Column = column;
    }

    /// <summary>
    /// Use this constructor for insertion/removal of a row
    /// </summary>
    /// <param name="change">Must be Change.InsertRow or Change.RemoveRow</param>
    internal Operation(Change change, int index, bool parity, IList<T?> row) : this(change, index, parity)
    {
        Row = row;
    }

    /// <summary>
    /// Use this constructor for modification of a single cell
    /// </summary>
    /// <param name="change">Must be Change.Inline</param>
    /// <param name="cell">The original value of the cell</param>
    /// <param name="cellIndex">An non-negative integer indicating the column number of the modified cell</param>
    internal Operation(Change change, int index, bool parity, T? cell, int? cellIndex) : this(change, index, parity)
    {
        Cell = cell;
        CellIndex = cellIndex;
    }
    #endregion Constructors

    // Method
    /// <summary>
    ///     Deep copies all fields, including null properties.
    /// </summary>
    /// 
    /// <remarks>
    ///     The IClonable interface is not mentioned because of it does not show
    ///     whether the Clone() method carries out a shallow or a deep copy.
    /// </remarks>
    /// 
    /// <returns>
    ///     A deep-copied instance of OperationPlus
    /// </returns>
    public Operation<T> DeepCopy()
    {
        return new(Change, Index, Parity)
        {
            Header = Header,
            Column = Column,
            Row = Row,
            Cell = Cell,
            CellIndex = CellIndex
        };
    }

    public void InvertChange()
    {
        Change = Change switch
        {
            Change.RemoveRow => Change.InsertRow,
            Change.InsertRow => Change.RemoveRow,
            Change.InsertColumn => Change.RemoveColumn,
            Change.RemoveColumn => Change.InsertColumn,
            _ => Change.Inline
        };
    }
}
