using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MultiThreadComm;

var messageQueue = new BlockingCollection<string>(boundedCapacity: 10);

var serviceCollection = new ServiceCollection();
var serviceProvider = serviceCollection
    .AddLogging(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Information))
    .BuildServiceProvider();

new Worker(serviceProvider, messageQueue).Start();