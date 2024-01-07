using System;

namespace EventsDemo;

internal interface ISignalHandler
{
    void OnSignal(object sender, EventArgs e);
}

internal class SignalHandler : ISignalHandler
{
    public void OnSignal(object sender, EventArgs e) {
        Console.WriteLine("Event Triggered");
    }
}

