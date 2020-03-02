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

            var jobList = new List<Job>{ 
                Job.Create<JobSingleThread>(), 
                Job.Create<JobMultiThreadParallelFor>(),
                Job.Create<JobMultiThreadParallelForWithInterlocked>(),
                Job.Create<JobMultiThreadSplitted>(),
                Job.Create<JobMultiThreadSplittedWithScopedLock>(),
                Job.Create<JobMultiThreadSplittedWithInterlocked>(),
                Job.Create<JobMultiThreadComputeOnlySplitted>(),
                Job.Create<JobMultiThreadComputeOnlySplittedWithScopedLock>(),
                Job.Create<JobMultiThreadComputeOnlySplittedWithInterlocked>(),
            };

            foreach(var job in jobList)
            {
                stopwatch.Restart();

                job.Batch();

                stopwatch.Stop();

                Console.WriteLine($"{job.Name} - {stopwatch.Elapsed.TotalSeconds} secs, Result : {job.Result}");
            }
        }
    }
}
