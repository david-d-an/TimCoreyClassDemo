using System;
using System.Threading.Tasks;

namespace AsyncDemo {
    public interface IFoo {
        int id { get; set; }
        DateTime timestamp { get; set; }
    }
    public class Foo : IFoo {
        public int id { get; set; }
        public DateTime timestamp { get; set; }

        public async Task InitializeAsync() {
            await Task.Delay(5000);
            id = new Random(Guid.NewGuid().GetHashCode()).Next();
            timestamp = DateTime.Now;
        }

        public static async Task<Foo> CreateFooAsync() {
            await Task.Delay(5000);
            return new Foo();
        }
    }
}



