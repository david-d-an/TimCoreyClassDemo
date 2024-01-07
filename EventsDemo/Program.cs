using EventsDemo;

var process = new SignalProcessor();
var signalHandler = new SignalHandler();
process.SignalHandler += signalHandler.OnSignal;

process.LoopSignal();
/* This works only if SignalHandler is a non-event delegate. */
/* This is a syntax error if SignalHandler is an event. */
// process.SignalHandler(null, null);
