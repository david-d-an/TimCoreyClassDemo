using System;
using System.IO;
using ConsoleConfig.Models;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddEnvironmentVariables()
    .Build();

var firstOption1 = config.GetSection("MyFirstClass:Option1").Value;
var firstOption2 = config.GetSection("MyFirstClass:Option2").Value;


var firstClassByModel = config.GetSection("MyFirstClass").Get<ApplicationConfigs>();
var secondClassByModel = config.GetSection("MySecondClass").Get<ApplicationConfigs>();

Console.WriteLine($"firstOption1: {firstOption1}");
Console.WriteLine($"firstOption2: {firstOption2}");
Console.WriteLine($"firstClassByModel: {firstClassByModel}");
Console.WriteLine($"SecondClassByModel: {secondClassByModel}");