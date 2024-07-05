using System;
using System.IO;
using ConsoleConfig.Models;
using Microsoft.Extensions.Configuration;
using NATS.Client;

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



var url = "nats://localhost:4222";
var subject = "foo";

// Create a connection to NATS server
var connectionFactory = new ConnectionFactory();

var xconn = connectionFactory.CreateConnection(url);
xconn.Dispose();
GC.Collect();
xconn.Dispose();


using (var connection = connectionFactory.CreateConnection(url))
{
    // Subscribe to the subject
    var subscription = connection.SubscribeAsync(subject);
    subscription.MessageHandler += (sender, msgArgs) =>
    {
        // Print the received message
        Console.WriteLine($"Received message: {System.Text.Encoding.UTF8.GetString(msgArgs.Message.Data)}");
    };

    // Start listening for incoming messages
    subscription.Start();

    Console.WriteLine($"Listening for messages on '{subject}'. Press any key to exit.");
    Console.ReadKey();
}