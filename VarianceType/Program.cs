using System;

namespace VarianceType
{
    class Program
    {
        public class Control {}
        public class Button : Control {}

        // in: Contra
        // out: Co

        // Contravariant delegate.
        public delegate void DContravariant<in T>(T argument);
        public delegate T DCovariant<out T>();

        // Methods that match the delegate signature.
        public static void ContravariantControl(Control argument)
        { }
        public static void ContravariantButton(Button argument)
        { }

        public static Control CovariantControl()
        { return new Control(); }
        public static Button CovariantButton()
        { return new Button(); }


        static void Main(string[] args)
        {
            Func<int, int, double> foo = (a, b) => a + b + GetVal();
            Console.WriteLine($"Hello World: {foo(2, 3)}");

            // Contravariant
            // DContravariant<Control> dContraControl = ContravariantControl;
            // DContravariant<Button> dContraButton = ContravariantButton;
            // dContraButton = dContraControl;
            DContravariant<Button> dContraButton = ContravariantControl;
            dContraButton(new Button());

            // Covariant
            // DCovariant<Control> dCoControl = CovariantControl;
            // DCovariant<Button> dCoButton = CovariantButton;
            // dCoControl = dCoButton;
            DCovariant<Control> dCoControl = CovariantButton;
            Control c = dCoControl();
        }

        private static int GetVal() {
            return new Random().Next();
        }
    }
}
