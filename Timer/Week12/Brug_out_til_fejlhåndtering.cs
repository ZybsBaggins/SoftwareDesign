using System;

class Program
{
    static void Main()
    {
        if (int.TryParse("123", out int result))
        {
            Console.WriteLine("Parsed successfully: " + result);
        }
        else
        {
            Console.WriteLine("Failed to parse.");
        }
    }
}
