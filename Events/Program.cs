using System;

namespace EventsDemo {
    class Program {
        static void Main(string[] args) {
            var process = new SignalProcessor();
            process.SignalHandler += Signal_Emission;
            process.SendTestSignal(3);
        }

        private static void Signal_Emission(object sender, EventArgs e) {
            Console.WriteLine("Event Triggered");
        }
    }
}
