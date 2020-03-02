using System.Threading;

namespace SampleMultiThread
{
    class JobSingleThread : Job
    {
        public string Name => "JobSingleThread";

        public long Result { get; private set; }

        public void Batch()
        {
            for (var i = 0; i < Job.LOOP_COUNT; ++i)
            {
                ++Result;
                Thread.Sleep(Job.SLEEP_MS);
            }
        }
    }
}