using System;

public abstract class Option<T>
{
    public abstract bool Exists();
    public abstract T Get();
}

public class Some<T> : Option<T>
{
    private T _value;

    public Some(T value)
    {
        _value = value;
    }

    public override bool Exists() => true;

    public override T Get() => _value;
}

public class None<T> : Option<T>
{
    public override bool Exists() => false;

    public override T Get() => throw new InvalidOperationException("No value present.");
}

class Program
{
    static void Main()
    {
        Option<int> some = new Some<int>(42);
        Option<int> none = new None<int>();

        Console.WriteLine("Some exists: " + some.Exists());
        Console.WriteLine("Some value: " + some.Get());

        Console.WriteLine("None exists: " + none.Exists());
        try
        {
            Console.WriteLine("None value: " + none.Get());
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Caught exception: " + ex.Message);
        }
    }
}
