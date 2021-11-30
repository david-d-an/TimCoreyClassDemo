using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ElectronNET.API;

namespace ElectronApp {
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Use Electron APIs
                    webBuilder.UseElectron(args);
                    webBuilder.UseEnvironment("Development");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
