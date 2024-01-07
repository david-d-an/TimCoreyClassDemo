using BenchmarkDemo;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<HexStringConverter>();