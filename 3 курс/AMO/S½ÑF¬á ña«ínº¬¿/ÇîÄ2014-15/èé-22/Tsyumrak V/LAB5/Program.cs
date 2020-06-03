using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AMO_Lab5
{

    class ApproximatePolynomial
    {
        private double[] a;
        private LegendresPolynomial lp;

        public ApproximatePolynomial(double[] _a, LegendresPolynomial _lp)
        {
            a = _a;
            lp = _lp;
        }

        public double P(double x)
        {
            double result = 0;
            for (int i = 0; i < a.Length; i++)
                result += a[i] * lp.P(i)(x);
            return result;
        }

        public void makeCSV(double a, double b, int n)
        {
            string[] output = new string[n];
            double h = (b - a) / n;
            for (int i = 0; i < n; i++)
            {
                double x = a + h * i;
                output[i] = x.ToString() + ";" + P(x).ToString();
            }
            output[n-1] = b.ToString() + ";" + P(b).ToString();
            File.WriteAllLines("output.csv", output);
        }
    }

    class Program
    {
        static Function f = x => Math.Log10(x) * Math.Log(10 * x) * Math.Sin(2.5 * x);
        const double a = 2.0;
        const double b = 10.0;

        public static double leastSquaresDeviation(ApproximatePolynomial ap, Integrator i)
        {
            return Math.Sqrt(i.calculate(x => (f(x) - ap.P(x)) * (f(x) - ap.P(x)), a, b) / (b - a));
        }

        public static SoLE getSoLE(int n, LegendresPolynomial lp, Integrator integrator)
        {
            double[,] A = new double[n, n];
            double[] B = new double[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = integrator.calculate(x => lp.P(i)(x) * lp.P(j)(x), a, b);
                }
                B[i] = integrator.calculate(x => f(x) * lp.P(i)(x), a, b);
            }

            return new SoLE(n, A, B);
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            double globalEpsilon = 1e-2;
            double integrationEpsilon = 1e-2;
            Integrator integrator = new Integrator(integrationEpsilon);
            LegendresPolynomial lp = new LegendresPolynomial();
            
            int n = 2;
            
            double[] A = Solver.solve(getSoLE(n, lp, integrator));
            ApproximatePolynomial ap = new ApproximatePolynomial(A, lp);
            
            while (leastSquaresDeviation(ap, integrator) > globalEpsilon)
            {
                integrationEpsilon *= 100;
                integrator.setEpsilon(integrationEpsilon);
                
                n++;
                
                Console.WriteLine("N = {0}", n);
                Console.WriteLine("LSD = {0}", leastSquaresDeviation(ap, integrator));
                
                A = Solver.solve(getSoLE(n, lp, integrator));
                for (int i = 0; i < A.Length; i++)
                    Console.Write("A[{0}] = {1}\n", i, A[i]);
                ap = new ApproximatePolynomial(A, lp);
            }

            Console.WriteLine("Final N = {0}", n);
            Console.WriteLine("LSD = {0}", leastSquaresDeviation(ap, integrator));
            
            ap.makeCSV(a, b, 500);
        }
    }
}
