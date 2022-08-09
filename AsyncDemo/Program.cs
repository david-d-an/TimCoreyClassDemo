using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncDemo {
    class Program {
        static async Task Main(string[] args) {
            // await new Worker().RunAsync();
            // await new ListOps().Run();

            Task<int> downloading = DownloadDocsMainPageAsync();
            // This is the only time downloading is evalueated by await.
            await downloading;

            // Subsequent await downloading will only reuse the previous eval.
            int bytesLoaded = await downloading;
            Console.WriteLine($"1. {nameof(Main)}: Downloaded {bytesLoaded} bytes.");
            int bytesLoaded2 = await downloading;
            Console.WriteLine($"2. {nameof(Main)}: Downloaded {bytesLoaded2} bytes.");

            // ReturnAwaitTest.GetIFooAsyncNoWait doesn't work due to type incompatibility
            // IFoo foo = await ReturnAwaitTest.GetIFooAsync();
            // Console.WriteLine($"foo.id = {foo.id}     foo.timestamp = {foo.timestamp}");
        }


        private static async Task<int> DownloadDocsMainPageAsync() {
            Console.WriteLine($"{nameof(DownloadDocsMainPageAsync)}: About to start downloading.");

            var client = new HttpClient();
            byte[] content = await client.GetByteArrayAsync("https://docs.microsoft.com/en-us/");

            Console.WriteLine($"{nameof(DownloadDocsMainPageAsync)}: Finished downloading.");
            return content.Length;
        }
    }
}
