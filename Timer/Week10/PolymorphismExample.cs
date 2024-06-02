using System;
using System.Collections.Generic;

public abstract class Bird
{
    public abstract double GetSpeed();
}

public class European : Bird
{
    public override double GetSpeed()
    {
        return 10.0; // Example base speed
    }
}

public class African : Bird
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

public class NorwegianBlue : Bird
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
        List<Bird> birds = new List<Bird>
        {
            new European(),
            new African(3),
            new NorwegianBlue(true, 5)
        };

        foreach (var bird in birds)
        {
            Console.WriteLine($"Bird speed: {bird.GetSpeed()}");
        }
    }
}
