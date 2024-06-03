using System;

abstract class Bird
{
    public abstract double GetSpeed();
}

class European : Bird
{
    public override double GetSpeed()
    {
        return 10.0; // Example base speed
    }
}

class African : Bird
{
    private int _numberOfCoconuts;

    public African(int coconuts)
    {
        _numberOfCoconuts = coconuts;
    }

    public override double GetSpeed()
    {
        return 10.0 - 1.0 * _numberOfCoconuts; // Example calculation
    }
}

class NorwegianBlue : Bird
{
    private bool _isNailed;
    private int _voltage;

    public NorwegianBlue(bool nailed, int voltage)
    {
        _isNailed = nailed;
        _voltage = voltage;
    }

    public override double GetSpeed()
    {
        return _isNailed ? 0 : 10.0 * _voltage; // Example calculation
    }
}

class Program
{
    static void Main()
    {
        Bird bird1 = new European();
        Bird bird2 = new African(3);
        Bird bird3 = new NorwegianBlue(true, 5);

        Console.WriteLine($"European bird speed: {bird1.GetSpeed()}");
        Console.WriteLine($"African bird speed: {bird2.GetSpeed()}");
        Console.WriteLine($"Norwegian Blue bird speed: {bird3.GetSpeed()}");
    }
}
