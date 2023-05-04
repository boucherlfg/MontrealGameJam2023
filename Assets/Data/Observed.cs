public class Observed<T>
{
    public Observed(T value = default, bool readOnly = false)
    {
        this.value = value;
        this.readOnly = readOnly;
    }

    public event System.Action Changed;
    private T value;
    private bool readOnly = false;
    public T Value
    {
        get => value;
        set
        {
            if (readOnly) return;
            if (this.value.Equals(value)) return;

            this.value = value;
            Changed?.Invoke();
        }
    }
    public override string ToString()
    {
        return value.ToString();
    }
    public static implicit operator Observed<T>(T value) => new Observed<T>(value, true);
    public static implicit operator T(Observed<T> obs) => obs.value;
}