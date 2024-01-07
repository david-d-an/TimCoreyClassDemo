using System.Linq;
using Microsoft.Extensions.Logging;

namespace DependencyInjection.Services;

public interface IBarService {
    void DoBarThing();
}

public class BarService(
    ILogger<BarService> logger,
    IFooService fooService) : IBarService
{
    public void DoBarThing()
    {
        var fooResult = Enumerable.Range(0, 10).Select(i => { 
            logger.LogInformation($"Bar is calling Foo service to do {i} thing");
            fooService.DoFooThing(i);
            return i;
        }).ToArray();
        logger.LogInformation($"Bar did {fooResult.Length} things by Foo");
        

        // for (int i = 0; i < 10; i++) {
        //     _fooService.DoFooThing(i);
        // }
    }
}