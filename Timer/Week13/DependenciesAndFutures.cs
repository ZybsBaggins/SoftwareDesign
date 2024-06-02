using System;
using System.Threading.Tasks;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        ContinuationsExample();
        FuturesExample();

        Console.ReadKey();
    }

    static void ContinuationsExample()
    {
        var f = Task.Factory;

        var build1 = f.StartNew(() => Build("project1"));
        var build2 = f.StartNew(() => Build("project2"));
        var build3 = f.StartNew(() => Build("project3"));

        var build4 = f.ContinueWhenAll(new[] { build1 }, _ => Build("project4"));
        var build5 = f.ContinueWhenAll(new[] { build1, build2, build3 }, _ => Build("project5"));
        var build6 = f.ContinueWhenAll(new[] { build3, build4 }, _ => Build("project6"));
        var build7 = f.ContinueWhenAll(new[] { build5, build6 }, _ => Build("project7"));
        var build8 = f.ContinueWhenAll(new[] { build5 }, _ => Build("project8"));

        Task.WaitAll(build1, build2, build3, build4, build5, build6, build7, build8);

        Console.WriteLine("*** Continuations Example Complete ***");
    }

    static void Build(string project)
    {
        Console.WriteLine($"Building {project}...");
        Thread.Sleep(1000); // Simulate build time
        Console.WriteLine($"{project} built.");
    }

    static void FuturesExample()
    {
        var a = "A";
        Task<string> futureB = Task.Run(() => F1(a));
        var c = F2(a);
        var d = F3(c);
        var f = F4(futureB.Result, d);
        Console.WriteLine($"Final result: {f}");
    }

    static string F1(string input)
    {
        Console.WriteLine($"F1 with {input}");
        return input + "1";
    }

    static string F2(string input)
    {
        Console.WriteLine($"F2 with {input}");
        return input + "2";
    }

    static string F3(string input)
    {
        Console.WriteLine($"F3 with {input}");
        return input + "3";
    }

    static string F4(string input1, string input2)
    {
        Console.WriteLine($"F4 with {input1} and {input2}");
        return input1 + input2 + "4";
    }
}

