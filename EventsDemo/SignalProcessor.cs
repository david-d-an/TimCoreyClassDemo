using System;
using System.Linq;

namespace EventsDemo;

public interface ISignalProcessor {
    event EventHandler SignalHandler;
}

public class SignalProcessor : ISignalProcessor {
    public event EventHandler SignalHandler;

    public void SendTestSignal(int repeat = 1) {
        Enumerable.Range(0, repeat).ToList().ForEach(_ => OnSignal());
    }
    protected virtual void OnSignal() {
        SignalHandler?.Invoke(this, EventArgs.Empty);
    }

    public void LoopSignal() {
        for (long i = 0; i < long.MaxValue; i++)
        {
            if ((i % 1000) != 0) continue;

            RaiseEvent(EventArgs.Empty);
            var duration = new Random().Next(1000, 5000);
            System.Threading.Thread.Sleep(duration);
        }
    }

    private void RaiseEvent(EventArgs args) {
        SignalHandler?.Invoke(this, args);
    }
}