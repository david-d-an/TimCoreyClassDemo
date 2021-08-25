using Microsoft.Extensions.Logging;

namespace DependencyInjectionDemo {
    public interface IFooService {
        void DoFooThing(int number);
    }

    public class FooService : IFooService {
        private readonly ILogger<FooService> _logger;

        public FooService(ILogger<FooService> logger) {
            _logger = logger;
        }

        public void DoFooThing(int number) {
            _logger.LogInformation($"Doing the thing {number}");
        }
    }
}
