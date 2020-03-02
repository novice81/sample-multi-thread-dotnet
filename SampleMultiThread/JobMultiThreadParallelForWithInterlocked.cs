using System.Threading;
using System.Threading.Tasks;

namespace SampleMultiThread
{
    class JobMultiThreadParallelForWithInterlocked : Job
    {
        public string Name => "JobMultiThreadParallelForWithInterlocked";

        public long Result { get => result; }

        private int result;

        public void Batch()
        {
            Parallel.For(0, Job.LOOP_COUNT, i => {
                Interlocked.Increment(ref result);
                Thread.Sleep(Job.SLEEP_MS);
            });
        }
    }
}