namespace ObservableTable.Core;

internal readonly struct Edit
{
    internal Action Undo { get; init; }
    internal Action Redo { get; init; }
    internal int Parity { get; init; }

    internal Edit(Action undo, Action redo, int parity)
    {
        Undo = undo;
        Redo = redo;
        Parity = parity;
    }

    internal void Invoke(bool isUndo)
    {
        if (isUndo) { Undo(); } else { Redo(); }
    }
}
