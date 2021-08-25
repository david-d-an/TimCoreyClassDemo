using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo
{
    class Program
    {
        public class Control {}
        public class Button : Control {}

        // in: Contra
        // out: Co

        // Contravariant delegate.
        public delegate void DContravariant<in T>(T argument);
        public delegate T DCovariant<out T>();

        // Methods that match the delegate signature.
        public static void ContravariantControl(Control argument)
        { }
        public static void ContravariantButton(Button argument)
        { }

        public static Control CovariantControl()
        { return new Control(); }
        public static Button CovariantButton()
        { return new Button(); }



        static async Task Main(string[] args)
        {
            Console.WriteLine("Long task started.\n");

            IEnumerable<Task<int>> tasks;
            
            // tasks = new List<Task<int>> {
            //     LongTaskAsync(),
            //     LongTaskAsync(),
            //     LongTaskAsync()
            // };

            tasks = Enumerable.Range(0, 10).Select(x => LongTaskAsync());
            
            var result = await Task.WhenAll<int>(tasks);

            Console.WriteLine();
            foreach (var r in result) {
                Console.Write($"{r} + ");
            }
            int v = result.Sum();
            Console.WriteLine($"\b\b = {v} milliseconds");
            Console.WriteLine();
            Console.WriteLine($"All tasks done after wating {v} milliseconds.");
        }

        public static async Task<int> LongTaskAsync() {
            return await Task.Run(() => LongTask());
        }
        public static int LongTask() {
            int delay  = new Random().Next(1000, 5000);
            Console.WriteLine($"{delay} milliseconds delay started ticking.");
            Thread.Sleep(delay);
            Console.WriteLine($"Long task has ended after {delay} milliseconds.");
            return delay;
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
