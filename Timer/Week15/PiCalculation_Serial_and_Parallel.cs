using System;
using System.Threading.Tasks;

class Program
{
    private const int _nDarts = 10000;

    static void Main()
    {
        Console.WriteLine("Serial Estimation of Pi: " + SerialEstimationOfPi());
        Console.WriteLine("Parallel Estimation of Pi: " + ParallelEstimationOfPi());
        Console.WriteLine("Parallel Estimation of Pi with Partitioner: " + ParallelEstimationOfPiWithPartitioner());
    }

    private static double SerialEstimationOfPi()
    {
        double nInside = 0;
        double stepSize = 1 / (double)_nDarts;
        for (int i = 0; i < _nDarts; i++)
        {
            var x = i * stepSize;
            for (int j = 0; j < _nDarts; j++)
            {
                var y = j * stepSize;
                if (Math.Sqrt(x * x + y * y) < 1.0) ++nInside;
            }
        }
        return 4 * nInside / (_nDarts * _nDarts);
    }

    private static double ParallelEstimationOfPi()
    {
        var locker = new object();
        double nInsideCircle = 0;
        double stepSize = 1 / (double)_nDarts;
        Parallel.For(0, _nDarts,
            () => 0, 
            (i, state, nInside) =>
            {
                var x = i * stepSize;
                for (int j = 0; j < _nDarts; j++)
                {
                    var y = j * stepSize;
                    if (Math.Sqrt(x * x + y * y) < 1.0) ++nInside;
                }
                return nInside;
            },
            inside => { lock (locker) nInsideCircle += inside; });
        return 4 * nInsideCircle / (_nDarts * _nDarts);
    }

    private static double ParallelEstimationOfPiWithPartitioner()
    {
        var locker = new object();
        double nInsideCircle = 0;
        double stepSize = 1 / (double)_nDarts;
        Parallel.ForEach(Partitioner.Create(0, _nDarts), () => 0,
            (range, state, inside) =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    var x = i * stepSize;
                    for (int j = 0; j < _nDarts; j++)
                    {
                        var y = j * stepSize;
                        if (Math.Sqrt(x * x + y * y) < 1.0) ++inside;
                    }
                }
                return inside;
            },
            inside => { lock (locker) nInsideCircle += inside; });
        return 4 * nInsideCircle / (_nDarts * _nDarts);
    }
}
