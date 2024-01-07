using System;
using System.Collections.Generic;
using GenericsDemo.Models;
using GenericsDemo.WithGenerics;

DemonstrateTextFileStorage();

const string peopleFile = @"./temp/people.csv";
const string logFile = @"./temp/logs.csv";
return;

static void DemonstrateTextFileStorage() {
    InitDemoData(peopleFile, logFile);

    var newPeople = GenericFile.LoadFromTextFile<Person>(peopleFile);
    foreach (Person p in newPeople) {
        Console.WriteLine($"{p.FirstName} {p.LastName} (IsAlive = {p.IsAlive})");
    }

    var newLogs = GenericFile.LoadFromTextFile<LogEntry>(logFile);
    foreach (LogEntry log in newLogs) {
        Console.WriteLine($"{log.ErrorCode}: {log.Message} at {log.TimeOfEvent.ToShortTimeString()}");
    }

    /* Old way of doing things - non-generics */
    //OriginalTextFileProcessor.SaveLogs(logs, logFile);
    //var newLogs = OriginalTextFileProcessor.LoadLogs(logFile);
    //foreach (var log in newLogs) {
    //    Console.WriteLine($"{log.ErrorCode}: {log.Message} at {log.TimeOfEvent.ToShortTimeString()}");
    //}

    //OriginalTextFileProcessor.SavePeople(people, peopleFile);
    //var newPeople = OriginalTextFileProcessor.LoadPeople(peopleFile);
    //foreach (var p in newPeople) {
    //    Console.WriteLine($"{p.FirstName} {p.LastName} (IsAlive = {p.IsAlive})");
    //}
}

static void InitDemoData(string peopleFile, string logFile) {
    List<Person> people = new();
    List<LogEntry> logs = new();
    PopulateDemoLists(people, logs);

    GenericFile.SaveToTextFile<Person>(people, peopleFile);
    GenericFile.SaveToTextFile<LogEntry>(logs, logFile);
}

static void PopulateDemoLists(List<Person> people, List<LogEntry> logs) {
    people.Add(new Person { FirstName = "Tim", LastName = "Corey" });
    people.Add(new Person { FirstName = "Sue", LastName = "Storm", IsAlive = false });
    people.Add(new Person { FirstName = "Greg", LastName = "Olsen" });

    logs.Add(new LogEntry { Message = "I blew up", ErrorCode = 9999 });
    logs.Add(new LogEntry { Message = "I'm too awesome", ErrorCode = 1337 });
    logs.Add(new LogEntry { Message = "I was tired", ErrorCode = 2222 });
}
