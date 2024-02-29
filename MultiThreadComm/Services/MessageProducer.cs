using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace MultiThreadComm.Services;

public interface IMessageProducer
{
    void Produce();
}

public class MessageProducer(ILogger<MessageProducer> logger, BlockingCollection<string> messageQueue)
    : IMessageProducer
{
    public void Produce()
    {
        for (var i = 1; i <= 10; i++)
        {
            var message = $"Message {i}";
            // Console.WriteLine($"Producing: {message}");
            logger.LogInformation("Producing: {m}", message);
            messageQueue.Add(message); // Add message to the queue
            Thread.Sleep(100); // Simulate some work
        }

        // Signal that no more items will be added
        messageQueue.CompleteAdding();
    }
}
