public class Observed<T>
{
    public Observed(T value) => this.value = value;

    public event System.Action Changed;
    private T value;
    public T Value
    {
        get => value;
        set
        {
            this.value = value;
            Changed?.Invoke();
        }
    }
    public override string ToString()
    {
        return value.ToString();
    }
}