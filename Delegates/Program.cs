using System;

namespace Delegates
{
    class Program
    {
        static double GetVal() { return 0.7; }

        static void Main(string[] args)
        {
            Func<int, int, double> foo = (a, b) => a + b + GetVal();

            Console.WriteLine($"Hello World: {foo(2, 3)}");
        }
    }
}
