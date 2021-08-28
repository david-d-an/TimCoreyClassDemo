using System;

namespace EventsDemo {
    public interface ISignalProcessor {
        event EventHandler SignalHandler;
    }

    public class SignalProcessor : ISignalProcessor {
        public event EventHandler SignalHandler;

        public void SendTestSignal(int repeat = 1) {
            for(int i = 0; i < repeat; i++) {
                OnSignal();
            }
        }

        protected virtual void OnSignal() {
            SignalHandler?.Invoke(this, EventArgs.Empty);
        }
    }
}