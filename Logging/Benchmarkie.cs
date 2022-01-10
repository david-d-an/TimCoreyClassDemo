using System;
using System.Text;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;

namespace Logging.Benchmarks;

[MemoryDiagnoser]
public class Benchmarkie
{
    private const string LogMessageWithParameters =
        "This is a log messagewith parameters. Parameters: {0}, {1}";
    private const string LogMessage = "This is a log message.";

    int a = 69;
    int b = 420;


    private readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder => {
        builder
            .AddConsole()
            .SetMinimumLevel(LogLevel.Warning);
    });

    private ILogger<Benchmarkie> _logger;
    private ILoggerAdapter<Benchmarkie> _loggerAdapter;

    public Benchmarkie() {
        _logger = new Logger<Benchmarkie>(_loggerFactory);
        _loggerAdapter = new LoggerAdapter<Benchmarkie>(_logger);
    }

    [Benchmark]
    public void Log_WithoutIf() {
        _logger.LogInformation("This is a log messagewith parameters. Parameters: {0}, {1}", a, b);
    }

    [Benchmark]
    public void Log_WithoutIf_Interpolation() {
        _logger.LogInformation($"This is a log messagewith parameters. Parameters: {a}, {b}");
    }

    [Benchmark]
    public void Log_WithIf() {
        if (!_logger.IsEnabled(LogLevel.Information)) return;
        _logger.LogInformation("This is a log messagewith parameters. Parameters: {0}, {1}", a, b);
    }

    [Benchmark]
    public void Log_WithIf_Interpolation() {
        if (!_logger.IsEnabled(LogLevel.Information)) return;
        _logger.LogInformation($"This is a log messagewith parameters. Parameters: {a}, {b}");
    }

    [Benchmark]
    public void LogAdapter() {
        // This only passess string parameters to LogInformation(). String rebuilding happens only
        // if the logging happens to save time.
        _loggerAdapter.LogInformation("This is a log messagewith parameters. Parameters: {0}, {1}", a, b);
    }

    [Benchmark]
    public void LogAdapter_Interpolation() {
        // Interpolation is happening before the process reaches the If statement
        // inside LogInformation(), whic costs a lot.
        _loggerAdapter.LogInformation($"This is a log messagewith parameters. Parameters: {a}, {b}");
    }
}