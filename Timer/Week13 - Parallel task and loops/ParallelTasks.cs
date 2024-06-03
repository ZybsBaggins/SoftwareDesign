using System;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        DoAll();

        // Example: Passing data to tasks
        DoWork();

        Console.ReadKey();
    }

    public static void DoAll()
    {
        Task t1 = Task.Run((Action)DoLeft);
        Task t2 = Task.Run((Action)DoRight);
        Task.WaitAll(t1, t2);
    }

    public static void DoLeft()
    {
        Console.WriteLine("Left work done.");
    }

    public static void DoRight()
    {
        Console.WriteLine("Right work done.");
    }

    public static void DoWork()
    {
        int data1 = 42;
        string data2 = "The Answer";
        Task.Run(() =>
        {
            Console.WriteLine($"{data2}: {data1}");
        }).Wait();
    }
}
