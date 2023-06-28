namespace ObservableTable.Core;

public class ModificationEventArgs : EventArgs
{
    public Action Action { get; init; }
    public Action OppositeAction { get; init; }
    public int Parity { get; init; }

    public ModificationEventArgs(Action action, Action opposite, int parity)
    {
        Action = action;
        OppositeAction = opposite;
        Parity = parity;
    }

    internal ModificationEventArgs(Edit edit)
    {
        Action = edit.Redo;
        OppositeAction = edit.Undo;
        Parity = edit.Parity;
    }
}
