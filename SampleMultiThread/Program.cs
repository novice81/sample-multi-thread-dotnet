using System;
using System.Threading;
using System.Diagnostics;

namespace SampleMultiThread
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            Thread.Sleep(100);

            stopwatch.Stop();

            Console.WriteLine($"{stopwatch.Elapsed.TotalSeconds} secs");
        }
    }
}
