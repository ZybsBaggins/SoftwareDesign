using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        var input = new BlockingCollection<string>();
        var output = new BlockingCollection<string>();

        CancellationTokenSource cts = new CancellationTokenSource();

        // Start stages
        Task.Run(() => ToLowerCase(input, output, cts.Token), cts.Token);

        for (int i = 0; i < 10; i++)
        {
            input.Add($"STRING {i}");
            Console.WriteLine($"Added STRING {i}");
            Thread.Sleep(100); // Simulate add time
        }
        input.CompleteAdding();

        Console.ReadKey();
        cts.Cancel();
    }

    static void ToLowerCase(BlockingCollection<string> input, BlockingCollection<string> output, CancellationToken token)
    {
        try
        {
            foreach (var item in input.GetConsumingEnumerable(token))
            {
                if (token.IsCancellationRequested) break;
                var result = item.ToLower();
                output.Add(result, token);
                Console.WriteLine(result);
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation canceled.");
        }
        finally
        {
            output.CompleteAdding();
        }
    }
}
