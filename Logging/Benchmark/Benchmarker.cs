using BenchmarkDotNet.Attributes;
using Logging.Logger;
using Microsoft.Extensions.Logging;

namespace Logging.Benchmark;

[MemoryDiagnoser]
public class Benchmarker
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

    private readonly ILogger<Benchmarker> _logger;
    private readonly ILoggerAdapter<Benchmarker> _loggerAdapter;

    public Benchmarker() {
        _logger = new Logger<Benchmarker>(_loggerFactory);
        _loggerAdapter = new LoggerAdapter<Benchmarker>(_logger);
    }

    [Benchmark]
    public void Log_WithoutIf() {
        _logger.LogInformation("This is a log message with parameters. Parameters: {0}, {1}", a, b);
    }

    [Benchmark]
    public void Log_WithoutIf_Interpolation() {
        _logger.LogInformation($"This is a log message with parameters. Parameters: {a}, {b}");
    }

    [Benchmark]
    public void Log_WithIf() {
        if (!_logger.IsEnabled(LogLevel.Information)) return;
        _logger.LogInformation("This is a log message with parameters. Parameters: {0}, {1}", a, b);
    }

    [Benchmark]
    public void Log_WithIf_Interpolation() {
        if (!_logger.IsEnabled(LogLevel.Information)) return;
        _logger.LogInformation($"This is a log message with parameters. Parameters: {a}, {b}");
    }

    [Benchmark]
    public void LogAdapter() {
        // This only passes string parameters to LogInformation(). String rebuilding happens only
        // if the logging happens to save time.
        _loggerAdapter.LogInformation("This is a log message with parameters. Parameters: {0}, {1}", a, b);
    }

    [Benchmark]
    public void LogAdapter_Interpolation() {
        // Interpolation is happening before the process reaches the If statement
        // inside LogInformation(), which costs a lot.
        _loggerAdapter.LogInformation($"This is a log message with parameters. Parameters: {a}, {b}");
    }
}