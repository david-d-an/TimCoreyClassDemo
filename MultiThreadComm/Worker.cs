
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MultiThreadComm.Services;

namespace MultiThreadComm;

public class Worker(
    IServiceProvider serviceProvider,
    BlockingCollection<string> messageQueue)
{
    public void Start()
    {
        var logger = GetLogger<Worker>();

        // Create instances of producer and consumer
        var producer = new MessageProducer(GetLogger<MessageProducer>(), messageQueue);
        var consumer = new MessageConsumer(GetLogger<MessageConsumer>(), messageQueue);

        // Start the producer and consumer threads
        logger.LogInformation("Creating thread: {type}", producer.GetType());
        var producerTask = Task.Run(() => producer.Produce());
        logger.LogInformation("Creating thread: {type}", consumer.GetType());
        var consumerTask = Task.Run(() => consumer.Consume());

        // Wait for both threads to complete
        logger.LogInformation("Waiting on threads");
        Task.WaitAll(producerTask, consumerTask);
        logger.LogInformation("All threads are finished.");
    }

    private ILogger<T> GetLogger<T>()
    {
        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
        return loggerFactory!.CreateLogger<T>();
    }
}