using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        LotsOfWork work = new LotsOfWork();
        Thread myThread = new Thread(work.DoLotsOfWork);
        work.NumberOfIterations = 500;
        myThread.Start();
        Console.ReadKey();
    }
}

public class LotsOfWork
{
    public int NumberOfIterations { get; set; } = 0;

    public void DoLotsOfWork()
    {
        for (int i = 0; i < NumberOfIterations; i++)
        {
            // Simuler en masse arbejde her..
            Console.WriteLine($"Iteration: {i}");
        }
    }
}
