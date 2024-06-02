using System;

class Program
{
    static void Main()
    {
        try
        {
            int result = Divide(10, 0);
            Console.WriteLine(result);
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("Error: Cannot divide by zero.");
            Console.WriteLine(ex.Message);
        }
    }

    static int Divide(int numerator, int denominator)
    {
        if (denominator == 0)
            throw new DivideByZeroException("Denominator cannot be zero.");
        return numerator / denominator;
    }
}
