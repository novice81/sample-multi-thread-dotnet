using System.Collections.Generic;
using System.Threading;

namespace SampleMultiThread
{
    class JobMultiThreadSplitted : Job
    {
        public string Name => "JobMultiThreadSplitted";

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

                    var resultEachThread = 0;

                    for (int i = curIndex * splittedJobSize; i < (curIndex + 1) * splittedJobSize; ++i)
                    {
                        ++resultEachThread;
                        Thread.Sleep(Job.SLEEP_MS);
                    }

                    Interlocked.Add(ref result, resultEachThread);
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