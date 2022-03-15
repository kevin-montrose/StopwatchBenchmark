using BenchmarkDotNet.Attributes;

namespace StopwatchBenchmark
{
    [MemoryDiagnoser]
    public class ElapsedBenchmark
    {
        private const int ITERATIONS = 1_000;

        [Params(false, true)]
        public bool LeaveRunning { get; set; }

        private readonly CurrentStopwatch current = new CurrentStopwatch();
        private readonly UpdatedStopwatch updated = new UpdatedStopwatch();

        [GlobalSetup]
        public void Setup()
        {
            current.Start();
            updated.Start();

            Thread.Sleep(10);
            if (!LeaveRunning)
            {
                current.Stop();
                updated.Stop();
            }
        }

        [Benchmark(Baseline = true)]
        public void Current()
        {
            for (var i = 0; i < ITERATIONS; i++)
            {
                _ = current.Elapsed;
                _ = current.ElapsedMilliseconds;
                _ = current.ElapsedTicks;
            }
        }

        [Benchmark]
        public void Updated()
        {
            for (var i = 0; i < ITERATIONS; i++)
            {
                _ = updated.Elapsed;
                _ = updated.ElapsedMilliseconds;
                _ = updated.ElapsedTicks;
            }
        }
    }
}
