using System.Collections.Specialized;
using System.ComponentModel;

namespace ObservableTable.Core;

/// <summary>
///     <see href="https://stackoverflow.com/a/56177896">ObservableStack&lt;T&gt;</see> implementation by
///     <see href="https://stackoverflow.com/users/1324284/ernie-s">Ernie S</see> used under
///     <see href="https://creativecommons.org/licenses/by/4.0/)">CC BY 4.0</see>
///     / Refactored INotifyCollectionChanged and INotifyPropertyChanged implementations
/// </summary>

internal class ObservableStack<T> : Stack<T>, INotifyCollectionChanged, INotifyPropertyChanged
{
    // Constructors
    public ObservableStack() : base() { }
    public ObservableStack(IEnumerable<T> collection) : base(collection) { }
    public ObservableStack(int capacity) : base(capacity) { }

    // Overrides
    public new virtual T Pop()
    {
        var item = base.Pop();
        OnCollectionChanged(NotifyCollectionChangedAction.Remove, item);
        return item;
    }

    public new virtual void Push(T item)
    {
        base.Push(item);
        OnCollectionChanged(NotifyCollectionChangedAction.Add, item);
    }

    public new virtual void Clear()
    {
        base.Clear();
        OnCollectionChanged(NotifyCollectionChangedAction.Reset);
    }

    // INotifyCollectionChanged
    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action, T item)
    {
        CollectionChanged?.Invoke(this, new(action, item));
        PropertyChanged?.Invoke(this, new(nameof(Count)));
    }

    protected virtual void OnCollectionChanged(NotifyCollectionChangedAction action)
    {
        if (action != NotifyCollectionChangedAction.Reset) { throw new ArgumentException("Reset only."); }
        CollectionChanged?.Invoke(this, new(action));
        PropertyChanged?.Invoke(this, new(nameof(Count)));
    }

    // INotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;
}