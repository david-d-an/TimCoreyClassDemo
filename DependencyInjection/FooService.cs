using Microsoft.Extensions.Logging;

namespace DependencyInjection;

public interface IFooService {
    void DoFooThing(int number);
}

public class FooService(ILogger<FooService> logger) : IFooService
{
    public void DoFooThing(int number) {
        logger.LogInformation($"Foo is doing the thing {number}");
    }
}