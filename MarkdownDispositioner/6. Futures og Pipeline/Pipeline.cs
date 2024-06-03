using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

public class PipelinesExample
{
    public static void Main(string[] args)
    {
        var stage1Output = new BlockingCollection<int>(); // First stage output queue
        var stage2Output = new BlockingCollection<int>(); // Second stage output queue

        // Start Stage 1 and Stage 2 tasks
        Task.Run(() => Stage1(stage1Output));
        Task.Run(() => Stage2(stage1Output, stage2Output));

        // Consume the final output from Stage 2
        foreach (var item in stage2Output.GetConsumingEnumerable())
        {
            Console.WriteLine($"Processed item: {item}");
        }
    }

    // Stage 1: Produces numbers from 0 to 9
    public static void Stage1(BlockingCollection<int> output)
    {
        for (int i = 0; i < 10; i++)
        {
            output.Add(i);
        }
        output.CompleteAdding(); // Signal that adding is complete
    }

    // Stage 2: Processes each item by multiplying by 2
    public static void Stage2(BlockingCollection<int> input, BlockingCollection<int> output)
    {
        foreach (var item in input.GetConsumingEnumerable())
        {
            var result = item * 2; // Example processing
            output.Add(result);
        }
        output.CompleteAdding(); // Signal that adding is complete
    }
}
