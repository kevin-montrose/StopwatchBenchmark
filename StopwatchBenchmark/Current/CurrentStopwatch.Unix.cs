// I'm testing on Windows... but here's the code for Unix-likes

//// Licensed to the .NET Foundation under one or more agreements.
//// The .NET Foundation licenses this file to you under the MIT license.

//namespace StopwatchBenchmark
//{
//    public partial class CurrentStopwatch
//    {
//        private static long QueryPerformanceFrequency()
//        {
//            const long SecondsToNanoSeconds = 1000000000;
//            return SecondsToNanoSeconds;
//        }

//        private static long QueryPerformanceCounter()
//        {
//            return (long)Interop.Sys.GetTimestamp();
//        }
//    }
//}