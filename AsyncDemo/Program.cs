using System;
using System.Net.Http;
using System.Threading.Tasks;

// await new Worker().RunAsync();
// await new ListOps().Run();

var downloading = DownloadDocsMainPageAsync();
// This is the only time downloading is evalueated by await.
await downloading;

// Subsequent await downloading will only reuse the previous eval.
var bytesLoaded = await downloading;
Console.WriteLine($"1. Main: Downloaded {bytesLoaded} bytes.");
var bytesLoaded2 = await downloading;
Console.WriteLine($"2. Main: Downloaded {bytesLoaded2} bytes.");
return;

// ReturnAwaitTest.GetIFooAsyncNoWait doesn't work due to type incompatibility
// IFoo foo = await ReturnAwaitTest.GetIFooAsync();
// Console.WriteLine($"foo.id = {foo.id}     foo.timestamp = {foo.timestamp}");

static async Task<int> DownloadDocsMainPageAsync() {
    Console.WriteLine($"{nameof(DownloadDocsMainPageAsync)}: About to start downloading.");

    var client = new HttpClient();
    var content = await client.GetByteArrayAsync("https://docs.microsoft.com/en-us/");

    Console.WriteLine($"{nameof(DownloadDocsMainPageAsync)}: Finished downloading.");
    return content.Length;
}
