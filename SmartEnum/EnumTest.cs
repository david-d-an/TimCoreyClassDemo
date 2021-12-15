using Ardalis.SmartEnum;

namespace SmartEnum;

public abstract class Weekday : SmartEnum<Weekday> {
    public static readonly Weekday Monday = new Mon();
    public static readonly Weekday Tuesday = new Tue();
    public static readonly Weekday Wednesday = new Wed();
    public static readonly Weekday Thursday = new Thr();
    public static readonly Weekday Friday = new Fri();
    public static readonly Weekday Saturday = new Sat();
    public static readonly Weekday Sunday = new Sun();

    // public static readonly Weekday Monday = new Weekday(nameof(Monday), 2);
    // public static readonly Weekday Tuesday = new Weekday(nameof(Tuesday), 2);
    // public static readonly Weekday Wednesday = new Weekday(nameof(Wednesday), 3);
    // public static readonly Weekday Thursday = new Weekday(nameof(Thursday), 4);
    // public static readonly Weekday Friday = new Weekday(nameof(Friday), 5);
    // public static readonly Weekday Saturday = new Weekday(nameof(Saturday), 6);
    // public static readonly Weekday Sunday = new Weekday(nameof(Sunday), 7);

    public abstract string Alias { get;}

    private Weekday(string name, int value) : base(name, value) {
    }

    private sealed class Mon : Weekday {
        public Mon() : base("Monday", 1) {}
        public override string Alias {
            get => "Mon";
        }
    }
    private sealed class Tue : Weekday {
        public Tue() : base("Tuesday", 2) {}
        public override string Alias { get; } = "Tue";
    }
    private sealed class Wed : Weekday {
        public Wed() : base("Wednesday", 3) {}
        public override string Alias => "Wed";
    }
    private sealed class Thr : Weekday {
        public Thr() : base("Thursday", 4) {}
        public override string Alias => "Thr";
    }
    private sealed class Fri : Weekday {
        public Fri() : base("Friday", 5) {}
        public override string Alias => "Fri";
    }
    private sealed class Sat : Weekday {
        public Sat() : base("Saturday", 6) {}
        public override string Alias => "Sat";
    }
    private sealed class Sun : Weekday {
        public Sun() : base("Sunday", 0) {}
        public override string Alias => "Sun";
    }
}
