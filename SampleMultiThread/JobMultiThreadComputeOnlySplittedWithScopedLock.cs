using System.Collections.Generic;
using System.Threading;

namespace SampleMultiThread
{
    class JobMultiThreadComputeOnlySplittedWithScopedLock : Job
    {
        public string Name => "JobMultiThreadComputeOnlySplittedWithScopedLock";

        public long Result { get => result; }

        private volatile int result;

        List<Thread> threadList = new List<Thread>();

        const int splittedJobSize = Job.LOOP_COUNT / Job.SPLIT_SIZE;

        private object lockObj = new object();

        public JobMultiThreadComputeOnlySplittedWithScopedLock()
        {
            for (int t = 0; t < Job.SPLIT_SIZE; ++t)
            {
                var thread = new Thread(ThreadJob);
                threadList.Add(thread);
            }
        }

        private void ThreadJob(object index)
        {
            var curIndex = (int)index;

            for (int i = curIndex * splittedJobSize; i < (curIndex + 1) * splittedJobSize; ++i)
            {
                for (int j = 0; j < 10000; ++j)
                {
                    lock (lockObj)
                    {
                        ++result;
                    }
                }
            }
        }

        public void Batch()
        {
            for (int t = 0; t < threadList.Count; ++t)
            {
                threadList[t].Start(t);
            }

            for (int t = 0; t < threadList.Count; ++t)
            {
                threadList[t].Join();
            }
        }
    }
}