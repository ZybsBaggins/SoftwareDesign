using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var files = Directory.EnumerateFiles(@"C:\(…)\Books", "*.txt").AsParallel();
        var wordCounts = files.MapReduce(
            path => Map(path),
            word => word.Length,
            group => Reduce(group));
        foreach (var pair in wordCounts)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }
    }

    public static IEnumerable<string> Map(string path)
    {
        return File.ReadLines(path)
            .SelectMany(line => line.ToLower().Split(new char[] { ' ', ',', '.', '-', '!', '?', ';' }));
    }

    public static IEnumerable<KeyValuePair<int, int>> Reduce(IGrouping<int, string> group)
    {
        yield return new KeyValuePair<int, int>(group.Key, group.Count());
    }
}

public static class ParallelExtensions
{
    public static ParallelQuery<TResult> MapReduce<TSource, TMapped, TKey, TResult>(
        this ParallelQuery<TSource> source,
        Func<TSource, IEnumerable<TMapped>> map,
        Func<TMapped, TKey> keySelector,
        Func<IGrouping<TKey, TMapped>, IEnumerable<TResult>> reduce)
    {
        return source
            .SelectMany(map)
            .GroupBy(keySelector)
            .SelectMany(reduce);
    }
}
