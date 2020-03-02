namespace SampleMultiThread
{
    interface Job
    {
        const int LOOP_COUNT = 10000;
        const int SLEEP_MS = 1;

        static Job Create<T>() where T : Job, new()
        {
            return new T();
        }

        string Name { get; }

        long Result { get; }

        void Batch();
    }
}