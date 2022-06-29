using BenchmarkDotNet.Running;
using Logging.Benchmark;

BenchmarkRunner.Run<Benchmarker>();

// var log = new LoggerConfiguration()
//     .MinimumLevel.Information()
//     .WriteTo.Console()
//     .CreateLogger();

