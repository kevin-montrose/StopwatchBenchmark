using BenchmarkDotNet.Attributes;

namespace StopwatchBenchmark
{
    [MemoryDiagnoser]
    public class StartNewStopBenchmark
    {
        private const int ITERATIONS = 1_000;

        [Benchmark(Baseline = true)]
        public void Current()
        {
            for(var i = 0; i < ITERATIONS; i++)
            {
                var sw = CurrentStopwatch.StartNew();
                sw.Stop();
            }
        }

        [Benchmark]
        public void Updated()
        {
            for (var i = 0; i < ITERATIONS; i++)
            {
                var sw = UpdatedStopwatch.StartNew();
                sw.Stop();
            }
        }
    }
}
