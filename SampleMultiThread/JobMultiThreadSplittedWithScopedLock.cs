using System.Collections.Generic;
using System.Threading;

namespace SampleMultiThread
{
    class JobMultiThreadSplittedWithScopedLock : Job
    {
        public string Name => "JobMultiThreadSplittedWithScopedLock";

        public long Result { get => result; }

        private volatile int result;

        private object lockObj = new object();

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
                        lock (lockObj)
                        {
                            ++result;
                        }
                        
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