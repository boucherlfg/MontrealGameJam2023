public class Observed<T>
{
    public Observed(T value = default)
    {
        this.value = value;
    }

    public event System.Action Changed;
    private T value;
    public T Value
    {
        get => value;
        set
        {
            if (this.value.Equals(value)) return;

            this.value = value;
            Changed?.Invoke();
        }
    }
    public override string ToString()
    {
        return value.ToString();
    }
    public static implicit operator Observed<T>(T value) => new Observed<T>(value);
    public static implicit operator T(Observed<T> obs) => obs.value;
}