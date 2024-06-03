using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Example: Parallel For Loop
        Console.WriteLine("Parallel For Loop:");
        Parallel.For(0, 10, i =>
        {
            Console.WriteLine($"i is {i}");
        });
        
        // Example: Parallel ForEach Loop
        var numbers = new List<int> { 1, 2, 3, 4, 5 };
        Console.WriteLine("Parallel ForEach Loop:");
        Parallel.ForEach(numbers, number =>
        {
            Console.WriteLine($"Number is {number}");
        });

        Console.ReadKey();
    }
}
