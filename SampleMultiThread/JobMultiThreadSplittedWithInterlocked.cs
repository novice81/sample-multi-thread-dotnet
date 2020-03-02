using System.Collections.Generic;
using System.Threading;

namespace SampleMultiThread
{
    class JobMultiThreadSplittedWithInterlocked : Job
    {
        public string Name => "JobMultiThreadSplittedWithInterlocked";

        public long Result { get => result; }

        private volatile int result;

        public void Batch()
        {
            var threadList = new List<Thread>();
            var splittedJobSize = Job.LOOP_COUNT / Job.SPLIT_SIZE;

            for (int t = 0; t < Job.SPLIT_SIZE; ++t)
            {
                var thread = new Thread((index) => {
                    var curIndex = (int)index;

                    for (int i = curIndex * splittedJobSize; i < (curIndex + 1) * splittedJobSize; ++i)
                    {
                        Interlocked.Increment(ref result);
                        Thread.Sleep(Job.SLEEP_MS);
                    }
                });

                threadList.Add(thread);

                thread.Start(t);
            }

            for (int t = 0; t < threadList.Count; ++t)
            {
                threadList[t].Join();
            }
        }
    }
}