using TaskRun;
using TaskRun.Workers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services
    .AddHostedService<Worker1>()
    .AddHostedService<Worker2>()
    .AddHostedService<Worker3>();

var host = builder.Build();
host.Run();


// Console.WriteLine("########## Before Task.Run");
// try
// {
//     // await Task.Run(async () => await Foo());
//     // await Task.Run<Task>(() => Foo());
//     // Task.Run(async () => await Foo());
//     Task.Run(() => Foo());
// }
// catch (Exception e)
// {
//     Console.WriteLine(e.Message);
// }
// Console.WriteLine("########## After Task.Run");
//
//
// async Task Foo()
// {
//     throw new Exception("@@@@@@@@@@ Thrown");
//     await Task.Delay(2000);
//     Console.WriteLine("********** This is Foo");
// }
