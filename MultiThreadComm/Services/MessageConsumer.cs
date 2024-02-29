using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace MultiThreadComm.Services;

public interface IMessageConsumer
{
    void Consume();
}

public class MessageConsumer(ILogger<MessageConsumer> logger,  BlockingCollection<string> messageQueue)
    : IMessageConsumer
{
    public void Consume()
    {
        foreach (var msg in messageQueue.GetConsumingEnumerable())
        {
            // Console.WriteLine($"Consuming: {msg}");
            logger.LogInformation("Consuming: {m}", msg);
            Thread.Sleep(2000); // Simulate some work
        }
    }
}