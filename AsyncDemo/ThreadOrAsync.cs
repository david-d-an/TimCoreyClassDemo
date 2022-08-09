using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo
{

    public class ThreadOrAsync
    {
        public async Task RunAsync()
        {
            List<Task> taskList = new List<Task>();
            for (int i = 0; i <= 5; i++)
            {
                var i1 = i;
                // taskList.Add(Task.Run(async () => await this.foo(i1)));
                // taskList.Add(this.foo(i));
                // taskList.Add(Task.Run(() => this.bar(i1)));
                taskList.Add(this.BarAsync(i));
            }

            // Task.WaitAll(taskList.ToArray());
            await Task.WhenAll(taskList);
        }

        private async Task<int> Foo(int i)
        {
            await Task.Delay(4000);
            return i;
        }

        private int Bar(int i)
        {
            Thread.Sleep(4000);
            return i;
        }

        private Task<int> BarAsync(int i)
        {
            // return Task.Factory.StartNew(() => {
            //     Thread.Sleep(4000);
            //     return i;
            // });
            return Task.Run(() =>
            {
                Thread.Sleep(4000);
                return i;
            });
        }

    }
}
