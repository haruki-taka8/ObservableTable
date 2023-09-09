namespace ObservableTable.Core;

/// <summary>
/// Describe a transaction.
/// </summary>
public class EditEventArgs : EventArgs
{
    /// <summary>
    /// The action to reverse a transaction.
    /// </summary>
    public Action Undo { get; init; }

    /// <summary>
    /// The transaction done to the ObservableTable.
    /// </summary>
    public Action Redo { get; init; }

    /// <summary>
    /// A group indicator. <see cref="EditEventArgs"/> of consecutive parity is considered one transaction internally.
    /// </summary>
    public int Parity { get; init; }

    public EditEventArgs(Action undo, Action redo, int parity)
    {
        Undo = undo;
        Redo = redo;
        Parity = parity;
    }

    /// <summary>
    /// Undo or redo the transaction stored in this instance of <see cref="EditEventArgs"/>.
    /// </summary>
    /// <param name="isUndo"></param>
    public void Invoke(bool isUndo)
    {
        if (isUndo) { Undo(); } else { Redo(); }
    }
}
