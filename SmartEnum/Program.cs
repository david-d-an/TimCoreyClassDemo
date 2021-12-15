
using System;

namespace SmartEnum;

class Program {
    static void Main(string[] args) {
        Console.WriteLine(Weekday.Monday);
        Console.WriteLine(Weekday.Monday.Name);
        Console.WriteLine(Weekday.Monday.Value);
        Console.WriteLine(Weekday.Monday.Alias);

        Console.WriteLine(Weekday.Sunday);
        Console.WriteLine(Weekday.Sunday.Name);
        Console.WriteLine(Weekday.Sunday.Value);
        Console.WriteLine(Weekday.Sunday.Alias);

        Weekday mon1 = Weekday.Monday;
        Weekday mon2 = Weekday.Monday;

        Console.WriteLine($"Monday and 1 are of the same value: {mon1 == 1}");
        Console.WriteLine($"Monday's name is 'Monday': {mon1.Name == "Monday"}");
        Console.WriteLine($"Two Mondays are of the same value: {mon1 == mon2}");
        Console.WriteLine($"Two Mondays are the same object: {object.ReferenceEquals(mon1, mon2)}");

        Weekday dayByValue = Weekday.FromValue(1);
        Weekday dayByName = Weekday.FromName("Monday");

        Console.WriteLine($"daByValue: Name = {dayByValue}, Value = {dayByValue.Value}, Alias = {dayByValue.Alias}");
        Console.WriteLine($"daByName: Name = {dayByName}, Value = {dayByName.Value}, Alias = {dayByName.Alias}"); 
    }
}
