using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        LotsOfWork work = new LotsOfWork();
        Thread myThread = new Thread(work.DoLotsOfWork);
        myThread.Start();
        Console.ReadKey();
    }
}

public class LotsOfWork
{
    public void DoLotsOfWork()
    {
        for (int i = 0; i < 1000; i++)
        {
            // Simuler en masse arbejde her..
            Console.WriteLine($"Iteration: {i}");
        }
    }
}
