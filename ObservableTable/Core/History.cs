namespace ObservableTable.Core;

/// <summary>
/// Describe a transaction.
/// </summary>
/// <remarks>
/// Externally, it is equivalent to <see cref="ModificationEventArgs"/>.
/// </remarks>
internal readonly struct Edit
{
    /// <summary>
    /// The action to reverse a transaction.
    /// </summary>
    internal Action Undo { get; init; }

    /// <summary>
    /// The transaction done to the ObservableTable.
    /// </summary>
    internal Action Redo { get; init; }

    /// <summary>
    /// A group indicator. <see cref="Edit"/>'s with consecutive parity is considered one transaction internally.
    /// </summary>
    internal int Parity { get; init; }

    internal Edit(Action undo, Action redo, int parity)
    {
        Undo = undo;
        Redo = redo;
        Parity = parity;
    }

    /// <summary>
    /// Undo or redo the transaction stored in this instance of <see cref="Edit"/>.
    /// </summary>
    /// <param name="isUndo"></param>
    internal void Invoke(bool isUndo)
    {
        if (isUndo) { Undo(); } else { Redo(); }
    }
}
