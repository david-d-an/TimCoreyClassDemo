using BenchmarkDotNet.Running;
using Logging.Benchmarks;
using Serilog;
using Microsoft.Extensions.Logging;

var bm = new Benchmarkie();

BenchmarkRunner.Run<Benchmarkie>();





var log = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

