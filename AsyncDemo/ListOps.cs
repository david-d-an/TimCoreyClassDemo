using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo {

    public static class StringExtension {
        public static async Task<string> MakeUpper(this string @this) {
            await Task.Delay(0);
            return @this.ToUpper();
        }
        public static async Task<string> MakeTrim(this string @this) {
            await Task.Delay(0);
            return @this.Trim();
        }
        public static async Task<bool> StringEqual(this string @this, string other) {
            await Task.Delay(0);
            return @this == other;
        }
    }

    public class ListOps {

        public async Task Run() {
            List<string> list = new() {
                " abc ",
                "  def",
                "ghi  ",
                "jkl   "
            };

            IEnumerable<Task<string>> tasks1 = list
                .Select(s => s.MakeUpper());

            IEnumerable<Task<string>> tasks2 = list
                // Where can't take AsyncDemo predicate
                // .Where(async s => await s.StringEqual("ABC"))
                .Select(async s => {
                    string ups = await s.MakeUpper();
                    return await ups.MakeTrim();
                });

            string[] result1 = await Task.WhenAll(tasks1);
            string[] result2 = await Task.WhenAll(tasks2);
        }


        static readonly CancellationTokenSource cts = new();

        public async Task RunAsync() {
            Console.WriteLine("Press ENTER key to cancel...\n");
            Task cancelTask = Task.Run(() => {
                while (Console.ReadKey().Key != ConsoleKey.Enter) {
                    Console.WriteLine("Press the ENTER key to cancel...");
                }

                Console.WriteLine("\nENTER key pressed: cancelling operations.\n");
                cts.Cancel();
            });

            Console.WriteLine("Long task started.\n");

            IEnumerable<Task<int>> tasks = GetTaskList();
            var result = await Task.WhenAll<int>(tasks);

            Console.WriteLine();
            Console.WriteLine("Planned schedule:");
            foreach (var r in result) {
                Console.Write($"{r}, ");
            }

            int duration = result.Max();
            Console.WriteLine($"\b\b");
            Console.WriteLine($"Maximum: {duration} milliseconds");
            Console.WriteLine();
            Console.WriteLine($"All tasks done after wating {duration} milliseconds.");
        }

        private IEnumerable<Task<int>> GetTaskList() {
            // return new List<Task<int>> {
            //     LongTaskAsync(),
            //     LongTaskAsync(),
            //     LongTaskAsync()
            // };
            return Enumerable.Range(0, 10).Select(x => LongTaskAsync());
        }

        public async Task<int> LongTaskAsync() {
            return await Task.Run(() => LongTask());
        }

        public int LongTask() {
            int delay = new Random().Next(1000, 10000);
            Console.WriteLine($"{delay} milliseconds delay started ticking.");

            bool cancelled = StartWait(delay);
            return delay;
        }

        private bool StartWait(int delay) {
            Stopwatch watch = new();
            watch.Start();
            bool cancelled = cts.Token.WaitHandle.WaitOne(delay);
            watch.Stop();

            var actionType = cancelled ? "cacelled" : "ended";
            Console.WriteLine($"LongTask {actionType} after {MillisecondStr(watch.Elapsed)} ms.");

            return cancelled;
        }

        private string MillisecondStr(TimeSpan ts) {
            return ((int)ts.TotalMilliseconds).ToString();
        }

        // public String DownloadStringV3(String url) 
        // { 
        //     // NOT SAFE, instant deadlock when called from UI thread
        //     // deadlock when called from threadpool, works fine on console
        //     var request = HttpClient.GetAsync(url).Result; 
        //     var download = request.Content.ReadAsStringAsync().Result; 
        //     return download; 
        // }

        // public String DownloadStringV4(String url) 
        // { 
        //     // NOT SAFE, deadlock when called from threadpool
        //     // works fine on UI thread or console main 
        //     return Task.Run(async () => { 
        //         var request = await HttpClient.GetAsync(url); 
        //         var download = await request.Content.ReadAsStringAsync(); 
        //         return download; 
        //     }).Result; 
        // }

        // public String DownloadStringV5(String url) 
        // { 
        //     // REALLY REALLY BAD CODE,
        //     // guaranteed deadlock 
        //     return Task.Run(() => { 
        //         var request = HttpClient.GetAsync(url).Result; 
        //         var download = request.Content.ReadAsStringAsync().Result; 
        //         return download; 
        //     }).Result; 
        // }
    }
}