using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GenericsDemo {
    class Program {
        private static IConfiguration _Config;
        public static IConfiguration Config {
            get {
                if (_Config != null)
                    return _Config;

                return Config = 
                    new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables()
                    .Build();
            }
            set {
                if (_Config != null)
                    return;
                _Config = value;
            }
        }

        public static string SubscriptionKey { 
            get {
                return Config.GetSection("CognitiveService:SubscriptionKey").Value;
            } 
        }
        public static string Region { 
            get {
                return Config.GetSection("CognitiveService:Region").Value;
            } 
        }
        public static string EndPoint { 
            get {
                return Config.GetSection("CognitiveService:EndPoint").Value;
            } 
        }


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

            string route = "translate?api-version=3.0&from=en&to=es&to=de&to=ko";
            int maxResponse = 0;

            using (var client = new HttpClient()) {
                for(int i = 0; i < 10000; i++) {
                    int choice = new Random((int)DateTime.Now.Ticks).Next(0, 5);
                    object[] body = new object[] { new { Text = textToTranslate[choice] } };
                    var requestBody = JsonConvert.SerializeObject(body);

                    var request = new HttpRequestMessage {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri(EndPoint + route),
                        Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
                    };
                    request.Headers.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);
                    request.Headers.Add("Ocp-Apim-Subscription-Region", Region);

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
