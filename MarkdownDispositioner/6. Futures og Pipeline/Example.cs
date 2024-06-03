using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

// Interface for pipeline stages
public interface IStage
{
    void Process(BlockingCollection<int> input, BlockingCollection<int> output);
}

// Stage 1: Produces numbers from 0 to 9
public class Stage1 : IStage
{
    public void Process(BlockingCollection<int> input, BlockingCollection<int> output)
    {
        for (int i = 0; i < 10; i++)
        {
            output.Add(i);
        }
        output.CompleteAdding(); // Signal that adding is complete
    }
}

// Stage 2: Processes each item by multiplying by 2
public class Stage2 : IStage
{
    public void Process(BlockingCollection<int> input, BlockingCollection<int> output)
    {
        foreach (var item in input.GetConsumingEnumerable())
        {
            var result = item * 2; // Example processing
            output.Add(result);
        }
        output.CompleteAdding(); // Signal that adding is complete
    }
}

public class PipelinesExample
{
    public static void Main(string[] args)
    {
        var stage1Output = new BlockingCollection<int>(); // First stage output queue
        var stage2Output = new BlockingCollection<int>(); // Second stage output queue

        IStage stage1 = new Stage1(); // Instantiate Stage 1
        IStage stage2 = new Stage2(); // Instantiate Stage 2

        // Start Stage 1 and Stage 2 tasks
        Task.Run(() => stage1.Process(null, stage1Output));
        Task.Run(() => stage2.Process(stage1Output, stage2Output));

        // Consume the final output from Stage 2
        foreach (var item in stage2Output.GetConsumingEnumerable())
        {
            Console.WriteLine($"Processed item: {item}");
        }
    }
}
