using System.Collections.Generic;

public class Counter<T, U>
{
    public event System.Action Changed;
    private Dictionary<T, U> counters;
    public Counter()
    {
        counters = new Dictionary<T, U>();
    }
    public U this[T key]
    {
        get => counters.ContainsKey(key) ? counters[key] : default;
        set
        {
            counters[key] = value;
            Changed?.Invoke();
        }
    }
    public void Reset() => counters.Clear();
}