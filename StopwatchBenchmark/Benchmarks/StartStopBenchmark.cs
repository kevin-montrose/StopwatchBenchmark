using BenchmarkDotNet.Attributes;

namespace StopwatchBenchmark
{
    [MemoryDiagnoser]
    public class StartStopBenchmark
    {
        private const int ITERATIONS = 1_000;

        private readonly CurrentStopwatch current = new CurrentStopwatch();
        private readonly UpdatedStopwatch updated = new UpdatedStopwatch();

        [Benchmark(Baseline = true)]
        public void Current()
        {
            for (var i = 0; i < ITERATIONS; i++)
            {
                current.Start();
                current.Stop();
            }
        }

        [Benchmark]
        public void Updated()
        {
            for (var i = 0; i < ITERATIONS; i++)
            {
                updated.Start();
                updated.Stop();
            }
        }
    }
}
