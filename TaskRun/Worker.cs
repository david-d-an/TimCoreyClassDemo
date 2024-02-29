namespace TaskRun;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;

    public async Task Foo(CancellationToken stoppingToken)
    {
        // throw new Exception("@@@@@@@@@@ Thrown");
        await Task.Delay(2000, stoppingToken);
        Console.WriteLine("********** This is Foo");
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(0, stoppingToken);
        Console.WriteLine("########## Before Task.Run");
        try
        {
            // await Task.Run(async () => await Foo(stoppingToken), stoppingToken);
            await Task.Run<Task>(() => Foo(stoppingToken), stoppingToken);
            // Task.Run(async () => await Foo(stoppingToken), stoppingToken);
            // Task.Run(() => Foo(stoppingToken), stoppingToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        Console.WriteLine("########## After Task.Run");
        
        // while (!stoppingToken.IsCancellationRequested)
        // {
        //     if (_logger.IsEnabled(LogLevel.Information))
        //     {
        //         _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        //     }
        //
        //     await Task.Delay(1000, stoppingToken);
        // }
    }
}