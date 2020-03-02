using System.Threading;
using System.Threading.Tasks;

namespace SampleMultiThread
{
    class JobMultiThreadParallelFor : Job
    {
        public string Name => "JobMultiThreadParallelFor";

        public long Result { get; private set; }

        public void Batch()
        {
            Parallel.For(0, Job.LOOP_COUNT, i => {
                ++Result;
                Thread.Sleep(Job.SLEEP_MS);
            });
        }
    }
}