
using Microsoft.Extensions.Logging;

public class LogByDefault {
    private const string StandardLogMsg =
        "This is a log messagewith parameters:";

    ILogger<LogByDefault> _logger;
    public LogByDefault() {
        using var loggerFactory = LoggerFactory.Create(builder => {
            builder
                .AddConsole()
                .SetMinimumLevel(LogLevel.Warning);
        });
        _logger = new Logger<LogByDefault>(loggerFactory);  
    }

    public void Run() {
        _logger.LogInformation(StandardLogMsg, "Arg 0", "Arg 1");;
    }
}