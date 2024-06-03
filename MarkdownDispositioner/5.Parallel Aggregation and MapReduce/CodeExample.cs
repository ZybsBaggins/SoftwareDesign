using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParallelAggregationAndMapReduce
{
    class Program
    {
        static void Main(string[] args)
        {
            // Parallel Aggregation Example
            var book = new List<string> { "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog", "quick", "quick", "quick" };
            ParallelAggregation(book);

            // MapReduce Example
            var documents = new List<string>
            {
                "The quick brown fox jumps over the lazy dog.",
                "The quick brown fox is quick."
            };
            MapReduce(documents);
        }

        static void ParallelAggregation(List<string> book)
        {
            int bigWords = 0;
            object lockObj = new object();
            Parallel.ForEach(book, word =>
            {
                if (word.Length > 6)
                {
                    lock (lockObj)
                    {
                        bigWords++;
                    }
                }
            });
            Console.WriteLine($"Number of big words: {bigWords}");
        }

        static void MapReduce(List<string> documents)
        {
            // Map phase
            var mapped = new List<KeyValuePair<string, int>>();
            foreach (var doc in documents)
            {
                mapped.AddRange(Map(doc));
            }

            // Group phase
            var grouped = mapped.GroupBy(pair => pair.Key)
                                .Select(group => new { Word = group.Key, Counts = group.Select(pair => pair.Value) });

            // Reduce phase
            var reduced = new Dictionary<string, int>();
            foreach (var group in grouped)
            {
                reduced[group.Word] = Reduce(group.Word, group.Counts);
            }

            // Output results
            Console.WriteLine("Word counts:");
            foreach (var kvp in reduced)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }

        static IEnumerable<KeyValuePair<string, int>> Map(string document)
        {
            var words = document.Split(new[] { ' ', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                yield return new KeyValuePair<string, int>(word, 1);
            }
        }

        static int Reduce(string word, IEnumerable<int> counts)
        {
            return counts.Sum();
        }
    }
}
