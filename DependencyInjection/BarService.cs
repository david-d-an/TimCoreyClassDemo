using System.Linq;
using Microsoft.Extensions.Logging;

namespace DependencyInjection {
    public interface IBarService {
        void DoBarThing();
    }

    public class BarService : IBarService {
        private readonly ILogger<BarService> _logger;
        private readonly IFooService _fooService;
        public BarService(
            ILogger<BarService> logger,
            IFooService fooService) {
            _logger = logger;
            _fooService = fooService;
        }

        public void DoBarThing() {
            var fooResult = Enumerable.Range(0, 10).Select(i => { 
                _fooService.DoFooThing(i);
                return i;
            }).ToArray();

            // for (int i = 0; i < 10; i++) {
            //     _fooService.DoFooThing(i);
            // }
        }
    }
}
