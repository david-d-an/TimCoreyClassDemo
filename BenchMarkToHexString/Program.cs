using BenchmarkDotNet.Running;
using BenchMarkToHexString;

var summary = BenchmarkRunner.Run<HexStringConverter>();
