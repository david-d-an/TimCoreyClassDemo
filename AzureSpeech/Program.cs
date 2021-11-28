using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GenericsDemo {
    class Program {
        private static readonly string subscriptionKey = "3532c019287f4cb08503d22e8c264671";
        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com/";

        // Add your location, also known as region. The default is global.
        // This is required if using a Cognitive Services resource.
        private static readonly string location = "eastus2";
        private const int StandardDelay = 3000;
        private static readonly int NumberOfChatRooms = 30;

        static async Task Main(string[] args) {
            // Input and output languages are defined as parameters.
            // Console.Write("Enter your text: ");
            // string textToTranslate = Console.ReadLine();
            string[] textToTranslate = {
                "My very photogenic mother died in a freak accident.",
                "On offering to help the blind man, the man who then stole his car.",
                "The French are certainly misunderstood: — but whether the fault is theirs, in not sufficiently explaining themselves.",
                "two best traits of human nature and to be found in much more hardened criminals than this one, a simple car-thief without any hope of advancing in his profession.",
                "My very photogenic mother died in a freak accident (picnic, lightning) when I was three, and, save for a pocket of warmth in the darkest past, nothing of her subsists within the hollows and dells of memory, over which, if you can still stand my style",
            };

            string route = "/translate?api-version=3.0&from=en&to=es&to=de&to=ko";
            int maxResponse = 0;

            using (var client = new HttpClient()) {
                for(int i = 0; i < 10000; i++) {
                    int choice = new Random((int)DateTime.Now.Ticks).Next(0, 5);
                    object[] body = new object[] { new { Text = textToTranslate[choice] } };
                    var requestBody = JsonConvert.SerializeObject(body);

                    var request = new HttpRequestMessage {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(endpoint + route),
                        Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
                    };
                    request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                    request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                    DateTime startTime = DateTime.Now;
                    HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                    string result = await response.Content.ReadAsStringAsync();
                    DateTime endTime = DateTime.Now;
                    TimeSpan span = endTime.Subtract(startTime);
                    var spanMilSec = (int)span.TotalMilliseconds;
                    maxResponse = Math.Max(maxResponse, spanMilSec);

                    string json = result;
                    dynamic vals = JsonConvert.DeserializeObject<dynamic>(json);

                    var its = ((JArray)vals).First.First.First;
                    foreach(var v in its) {
                        var prop = ((JObject)v);
                        var text = ((JValue)(prop["text"])).Value;
                        var toLang = ((JValue)(prop["to"])).Value;
                    }

                    Console.WriteLine($"Iteration {i}: Choice {choice}: Response {span.TotalMilliseconds} ms: Max Response {maxResponse} ms");
                    Console.WriteLine(result);
                    Console.WriteLine();

                    // if (spanMilSec > 100) Console.ReadLine();

                    if (result.Contains("The server rejected")) {
                        Console.WriteLine("############ Maximum excceded ############");
                        break;
                    }

                    await Task.Delay((choice+1) * StandardDelay / NumberOfChatRooms);
                }
            }
        }
    }
}
