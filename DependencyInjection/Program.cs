using System;
using DependencyInjection;
using DependencyInjection.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

//setup our DI
var serviceCollection = new ServiceCollection();
var serviceProvider = serviceCollection
    .AddLogging(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Information))
    .AddSingleton<IFooService, FooService>()
    .AddSingleton<IBarService, BarService>()
    .BuildServiceProvider();

var logger = serviceProvider
    .GetService<ILoggerFactory>()
    .CreateLogger<Program>();
logger.LogDebug("Starting application");

//do the actual work here
var bar = serviceProvider.GetService<IBarService>();
bar.DoBarThing();
logger.LogDebug("All done!");
Console.ReadKey();
