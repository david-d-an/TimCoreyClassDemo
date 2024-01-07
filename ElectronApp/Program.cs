using ElectronApp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ElectronNET.API;

CreateHostBuilder(args).Build().Run();
return;

static IHostBuilder CreateHostBuilder(string[] args) => Host
    .CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        // Use Electron APIs
        webBuilder.UseElectron(args);
        webBuilder.UseEnvironment("Development");
        webBuilder.UseStartup<Startup>();
    });
