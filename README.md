# StopwatchBenchmarks

Quick and dirty solution for benchmarking [this dotnet PR](https://github.com/dotnet/runtime/pull/66619).

Has 4 benchmarks:

 - `ElapsedBenchmark` - calling the ElapsedXXX properties both when the stopwatch is running and when stopped
 - `RandomizedElapsedBenchmark` - similar to `ElapsedBenchmark` but over an array of stopwatches which are randomly either running or stopped
    * this aims to confuse any branch prediction that's going on
- `StartNewStopBenchmark` - calls `StartNew()` followed by `Stop`
- `StartStopBenchmark` - calls `Start()` followed by `Stop` on pre-allocated stopwatches

Main goal is to reduce the allocation of size of stopwatches, which is best illustrated by `StartNewStopBenchmark`.  All other benchmarks aim for minimal regressions in performance.

## Results

Environment

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22572
AMD Ryzen 7 Microsoft Surface Edition, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  DefaultJob : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT


```

### ElapsedBenchmark

|  Method | LeaveRunning |      Mean |     Error |    StdDev | Ratio | Allocated |
|-------- |------------- |----------:|----------:|----------:|------:|----------:|
| **Current** |        **False** |  **2.556 μs** | **0.0188 μs** | **0.0166 μs** |  **1.00** |         **-** |
| Updated |        False |  2.566 μs | 0.0262 μs | 0.0245 μs |  1.00 |         - |
|         |              |           |           |           |       |           |
| **Current** |         **True** | **56.362 μs** | **0.0158 μs** | **0.0132 μs** |  **1.00** |         **-** |
| Updated |         True | 56.153 μs | 0.0370 μs | 0.0328 μs |  1.00 |         - |

### RandomizedElapsedBenchmark

|  Method |     Mean |     Error |    StdDev | Ratio | Allocated |
|-------- |---------:|----------:|----------:|------:|----------:|
| Current | 9.194 μs | 0.0050 μs | 0.0042 μs |  1.00 |         - |
| Updated | 9.202 μs | 0.0302 μs | 0.0283 μs |  1.00 |         - |

### StartNewStopBenchmark

|  Method |     Mean |    Error |   StdDev | Ratio |   Gen 0 | Allocated |
|-------- |---------:|---------:|---------:|------:|--------:|----------:|
| Current | 45.94 μs | 0.354 μs | 0.314 μs |  1.00 | 19.1040 |     39 KB |
| Updated | 44.76 μs | 0.078 μs | 0.069 μs |  0.97 | 11.4746 |     23 KB |

### StartStopBenchmark

|  Method |     Mean |    Error |   StdDev | Ratio | Allocated |
|-------- |---------:|---------:|---------:|------:|----------:|
| Current | 41.91 μs | 0.028 μs | 0.022 μs |  1.00 |         - |
| Updated | 44.33 μs | 0.144 μs | 0.135 μs |  1.06 |         - |