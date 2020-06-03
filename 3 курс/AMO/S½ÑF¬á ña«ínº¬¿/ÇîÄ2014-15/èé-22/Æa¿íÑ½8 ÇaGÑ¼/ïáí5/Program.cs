using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5AMO
{
    class Program
    {
        private const double a = 2.0;
        private const double b = 9.0;

        private static double Func(double x)
        {
            return 5 * Math.Log(x) * Math.Sin(x) * Math.Cos(2* x);
        }

        static void Main(string[] args)
        {
            String Directory = Environment.CurrentDirectory;
            Approximate MakeAprox = new Approximate(Func);

            double[] indexes = MakeAprox.Approximation(1e-2, a, b);

            foreach (var i in indexes)
            {
                Console.WriteLine(i);
            }
        }
    }
}
