using System;
using System.IO;

namespace Lab6
{
    class Program
    {
        private static double Func(double x)
        {
            return 3.5*Math.Log(x)*Math.Sin(x);
        }

        static void Main(string[] args)
        {
            const double a = 2.0;
            const double b = 20.0;
            const int stepCount = 200;
            const double step = (b - a) / stepCount;
            var input = new double[stepCount];
            var output = new double[stepCount];


            for (int i = 0; i < stepCount; i++)
            {
                double x = a + step * i;
                input[i] = x;
                output[i] = Func(x);
            }

            var approximator = new CubicSplinesApproximation(input, output, stepCount * 2);
            OutputToFile(approximator, a, b, step / 2);
        }

        static void OutputToFile(CubicSplinesApproximation approximator, double a, double b, double step)
        {
            using (var writer = File.CreateText("D:\\MyProjects\\Amo_Lab6\\Lab6\\graph.csv"))
            {
                for (double x = a; x < b; x += step)
                {
                    double res = approximator.Calc(x);
                    writer.WriteLine(x + ";" + res);
                    //  writer.WriteLine(x);
                }
            }
        }
    }
}
