using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SampleMultiThread
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();

            var jobList = new List<Job>{ Job.Create<JobSingleThread>(), Job.Create<JobMultiThread>() };

            foreach(var job in jobList)
            {
                stopwatch.Start();

                job.Batch();

                stopwatch.Stop();

                Console.WriteLine($"{job.Name} - {stopwatch.Elapsed.TotalSeconds} secs, Result : {job.Result}");
            }
        }
    }
}
