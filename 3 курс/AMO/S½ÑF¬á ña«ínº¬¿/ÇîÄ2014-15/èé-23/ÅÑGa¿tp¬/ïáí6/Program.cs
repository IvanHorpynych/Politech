using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lab6AMO
{
    class Program
    {
        private static double Func(double x)
        {
            return 3.5 * Math.Log(x) * Math.Sin(x);
        }
               static void Main(string[] args)
        {
            const double a = 2;
            const double b = 20;
            const int stepCount = 100;
            const double step = (b - a) / stepCount;
            var input = new double[stepCount];
            var output = new double[stepCount];


            for (int i = 0; i < stepCount; i++)
            {
                double x = a + step * i;
                input[i] = x;
                output[i] = Func(x);
            }

            var approximator = new Splines(input, output, stepCount * 2);
            OutputToFile(approximator, a, b, step / 2);
        }

        static void OutputToFile(Splines approximator, double a, double b, double step)
        {
            using (var writer = File.CreateText("result.csv"))
            {
                for (double x = a; x < b; x += step)
                {
                    double res = approximator.Calc(x);
                    writer.WriteLine(x + ";" + res);
                }
            }
        }
    }
}
