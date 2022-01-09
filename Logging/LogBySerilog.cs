using Serilog;

public class LogBySerilog {
    public LogBySerilog() {}

    public void Run() {
        var log = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();

        log.Information("This is a log message.", "Arg 0", "Arg 1");;
    }
}