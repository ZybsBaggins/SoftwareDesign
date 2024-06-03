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
        var stage1Output = new BlockingCollection<string>();
        var stage2Output1 = new BlockingCollection<string>();
        var stage2Output2 = new BlockingCollection<string>();
        var finalOutput = new BlockingCollection<string>();

        CancellationTokenSource cts = new CancellationTokenSource();

        // Start stages
        Task.Run(() => LoadImages(input), cts.Token);
        Task.Run(() => ProcessImages(input, stage1Output, cts.Token), cts.Token);
        Task.Run(() => FilterImages(stage1Output, stage2Output1, cts.Token), cts.Token);
        Task.Run(() => FilterImages(stage1Output, stage2Output2, cts.Token), cts.Token);
        Task.Run(() => DisplayImages(new BlockingCollection<string>[] { stage2Output1, stage2Output2 }, finalOutput, cts.Token), cts.Token);

        Console.ReadKey();
        cts.Cancel();
    }

    static void LoadImages(BlockingCollection<string> output)
    {
        for (int i = 0; i < 10; i++)
        {
            output.Add($"Image {i}");
            Console.WriteLine($"Loaded Image {i}");
            Thread.Sleep(100); // Simulate load time
        }
        output.CompleteAdding();
    }

    static void ProcessImages(BlockingCollection<string> input, BlockingCollection<string> output, CancellationToken token)
    {
        foreach (var item in input.GetConsumingEnumerable(token))
        {
            if (token.IsCancellationRequested) break;
            var result = $"Processed {item}";
            output.Add(result, token);
            Console.WriteLine(result);
            Thread.Sleep(200); // Simulate processing time
        }
        output.CompleteAdding();
    }

    static void FilterImages(BlockingCollection<string> input, BlockingCollection<string> output, CancellationToken token)
    {
        foreach (var item in input.GetConsumingEnumerable(token))
        {
            if (token.IsCancellationRequested) break;
            var result = $"Filtered {item}";
            output.Add(result, token);
            Console.WriteLine(result);
            Thread.Sleep(300); // Simulate filtering time
        }
        output.CompleteAdding();
    }

    static void DisplayImages(BlockingCollection<string>[] inputs, BlockingCollection<string> output, CancellationToken token)
    {
        string item;
        while (!inputs.All(bc => bc.IsCompleted))
        {
            if (BlockingCollection<string>.TryTakeFromAny(inputs, out item, 50, token) != -1)
            {
                var result = $"Displayed {item}";
                output.Add(result, token);
                Console.WriteLine(result);
                Thread.Sleep(100); // Simulate display time
            }
        }
        output.CompleteAdding();
    }
}
