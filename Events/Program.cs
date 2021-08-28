using System;

namespace EventsDemo {
    class Program {
        static void Main(string[] args) {
            var process = new SignalProcessor();
            process.SignalHandler += Signal_Emission;

            process.LoopSignal();

            /* This works only if SignalHandler is a non-event delegate. */
            /* This is a syntax error if SignalHandler is an event. */
            // process.SignalHandler(null, null);
        }

        private static void Signal_Emission(object sender, EventArgs e) {
            Console.WriteLine("Event Triggered");
        }
    }
}
