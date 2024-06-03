using System;
using System.Threading.Tasks;

public class FuturesExample
{
    public static async Task Main(string[] args)
    {
        var a = 10; // Initial value
        // Start a task to run F1(a) concurrently
        Task<int> futureb = Task.Run(() => F1(a));
        
        // Continue executing other functions
        var c = F2(a);
        var d = F3(c);
        
        // Await the result of the future task
        var result = await futureb;
        // Combine the results and process them
        F4(result, d);
    }

    // Example function to simulate some work
    public static int F1(int a)
    {
        // Simulate some work
        return a * 2;
    }

    // Another function
    public static int F2(int a)
    {
        return a + 3;
    }

    // Another function
    public static int F3(int c)
    {
        return c - 1;
    }

    // Function to combine results
    public static void F4(int b, int d)
    {
        Console.WriteLine($"Result: {b}, {d}");
    }
}
