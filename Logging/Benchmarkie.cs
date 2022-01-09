using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;

namespace Logging.Benchmarks;

[MemoryDiagnoser]
public class Benchmarkie
{
    private const string LogMessageWithParameters =
        "This is a log messagewith parameters. Parameters: {0}, {1}";
    private const string LogMessage = "This is a log message.";

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
        _logger.LogInformation(LogMessage);
    }

    [Benchmark]
    public void Log_WithIf() {
        if (_logger.IsEnabled(LogLevel.Information)) {
            _logger.LogInformation(LogMessage);
        }
    }

    [Benchmark]
    public void Log_WithoutIfWithParams() {
        _logger.LogInformation(LogMessageWithParameters, 69, 420);

        // This is expensive
        // _logger.LogInformation("Message: {0}", Random.Shared.Next());
        // This is terribly expensive. Expecting far mroe GC than above
        // _logger.LogInformation($"Message: {Random.Shared.Next()}");
    }

    [Benchmark]
    public void Log_WithIfWithParams() {
        if (_logger.IsEnabled(LogLevel.Information)) {
            _logger.LogInformation(LogMessageWithParameters, 69, 420);
        }
    }

    [Benchmark]
    public void LogAdapter_WithParams() {
        _loggerAdapter.LogInformation(LogMessageWithParameters, 69, 420);
    }
}