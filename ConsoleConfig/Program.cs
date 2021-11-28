using System.IO;
using Microsoft.Extensions.Configuration;

namespace ConsoleConfig {
    class Program {
        static void Main(string[] args) {

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            var first_Option1 = config.GetSection("MyFirstClass:Option1").Value;
            var first_Option2 = config.GetSection("MyFirstClass:Option2").Value;

            var mySecondClass = config.GetSection("MySecondClass").Get<MySecondClass>();
        }
    }

    public class MySecondClass {
        public string SettingOne { get; set; }
        public int SettingTwo { get; set; }
    }
}
