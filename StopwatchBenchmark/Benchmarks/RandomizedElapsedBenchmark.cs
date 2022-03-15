using BenchmarkDotNet.Attributes;

namespace StopwatchBenchmark
{
    [MemoryDiagnoser]
    public class RandomizedElapsedBenchmark
    {
        private const int NUM_STOPWATCHES = 1_000;

        private readonly CurrentStopwatch[] current = new CurrentStopwatch[NUM_STOPWATCHES];
        private readonly UpdatedStopwatch[] updated = new UpdatedStopwatch[NUM_STOPWATCHES];

        [GlobalSetup]
        public void Setup()
        {
            // roughly repeatable, but random
            var rand = new Random(2022_03_15);

            // we basically want any branch predictor to have a tough time
            // with whether the stopwatch is running or not
            for(var i = 0; i < NUM_STOPWATCHES; i++)
            {
                current[i] = CurrentStopwatch.StartNew();
                updated[i] = UpdatedStopwatch.StartNew();

                var leaveRunning = rand.Next(2) == 1;

                if (!leaveRunning)
                {
                    current[i].Stop();
                    updated[i].Stop();
                }
            }
        }

        [Benchmark(Baseline = true)]
        public void Current()
        {
            for(var i = 0; i < current.Length; i++)
            {
                _ = current[i].ElapsedTicks;
            }
        }

        [Benchmark]
        public void Updated()
        {
            for (var i = 0; i < updated.Length; i++)
            {
                _ = updated[i].ElapsedTicks;
            }
        }
    }
}
