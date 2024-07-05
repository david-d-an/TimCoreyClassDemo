namespace TaskRun.Workers;

public class Worker3(
    ILogger<Worker3> logger,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    private readonly ILogger<Worker3> _logger = logger;
    private readonly IHostApplicationLifetime _hostApplicationLifetime = hostApplicationLifetime;
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(0, stoppingToken);
        _logger.LogInformation("########## Before Task.Run from Worker3");
        try
        {
            // await Task.Run(async () => await Foo(stoppingToken), stoppingToken);
            await Task.Run<Task>(() => Foo(stoppingToken), stoppingToken);
            // Task.Run(async () => await Foo(stoppingToken), stoppingToken);
            // Task.Run(() => Foo(stoppingToken), stoppingToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        _logger.LogInformation("########## After Task.Run from Worker3");
        
        // while (!stoppingToken.IsCancellationRequested)
        // {
        //     if (_logger.IsEnabled(LogLevel.Information))
        //     {
        //         _logger.LogInformation("Worker from Worker3 running at: {time}", DateTimeOffset.Now);
        //     }
        //
        //     await Task.Delay(1000, stoppingToken);
        // }
    }

    private async Task Foo(CancellationToken stoppingToken)
    {
        // throw new Exception("@@@@@@@@@@ Thrown from Worker3");
        await Task.Delay(2000, stoppingToken);
        _logger.LogInformation("********** This is Foo from Worker3");
    }
}