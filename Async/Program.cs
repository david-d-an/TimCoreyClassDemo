using System.Threading.Tasks;

namespace AsyncDemo {
    class Program {
        static async Task Main(string[] args) {
            await new Worker().RunAsync();
        }
    }
}
