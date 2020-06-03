using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AMO_Lab6
{
    delegate double Function(double x);

    class Spline
    {
        private int n;
        private double[] a;
        private double[] b;
        private double[] c;
        private double[] d;
        private double[] x;

        public Spline(int n, double left, double right, Function f)
        {
            this.n = n;

            this.x = new double[n + 1];
            double _h = (right - left) / n;
            for (int i = 1; i < n; i++)
            {
                x[i] = left + _h * i;
            }
            x[0] = left; x[n] = right;
            
            this.a = new double[n + 1];
            for (int i = 0; i <= n; i++) this.a[i] = f(this.x[i]);

            double[] h = new double[n + 1];
            for (int i = 1; i <= n; i++) h[i] = x[i] - x[i - 1];

            // making tri-diagonal matrix
            double[,] A = new double[n + 1, n + 1];
            double[] B = new double[n + 1];
            for (int i = 1; i < n; i++)
            {
                A[i, i - 1] = h[i]; // c[i - 1]
                A[i, i] = 2 * (h[i] + h[i + 1]); // c[i]
                A[i, i + 1] = h[i + 1]; // c[i + 1]
                B[i] = 6 * (((f(x[i + 1]) - f(x[i])) / h[i + 1]) - ((f(x[i]) - f(x[i - 1])) / h[i]));
            }

            this.c = new double[n + 1];
            c[0] = 0; c[n] = 0;
            A[1, 0] = 0; // because c[0] = 0
            A[n - 1, n] = 0; // because c[n] = 0

            // run
            // for i in [1, n - 1]
            // Ai = A[i, i - 1]
            // Bi = A[i, i + 1]
            // Ci = A[i, i]
            // Fi = B[i]

            double[] alpha = new double[n + 1];
            double[] beta = new double[n + 1];
            beta[1] = B[1] / A[1, 1]; // F1 / C1
            alpha[1] = -beta[1] / A[1, 1]; // - beta1 / C1
            for (int i = 1; i < n - 1; i++)
            {
                alpha[i + 1] = -A[i, i + 1] 
                             / (A[i, i - 1] * alpha[i] + A[i, i]); // -Bi / (Ai * alphai + Ci)
                beta[i + 1] = (B[i] - A[i, i - 1] * beta[i]) 
                            / (A[i, i - 1] * alpha[i] + A[i, i]);// (Fi - Ai * betai) / (Ai * alphai + Ci)
            }
            c[n - 1] = (B[n - 1] - A[n - 1, n - 2] * beta[n - 1]) 
                     / (A[n - 1, n - 2] * alpha[n - 1] + A[n - 1, n - 1]); //xn = (Fn - An * beta) / (An * alphan + Cn)

            for (int i = n - 2; i > 0; i--)
            {
                c[i] = alpha[i + 1] * c[i + 1] + beta[i + 1];
            }

            // calculate b and d
            this.b = new double[n + 1];
            this.d = new double[n + 1];
            for (int i = 1; i <= n; i++)
            {
                d[i] = (c[i] - c[i - 1]) / h[i];
                b[i] = h[i] * c[i] / 2 - h[i] * h[i] * d[i] / 6 + (f(x[i]) - f(x[i - 1])) / h[i];
            }
        }

        public double si(int i, double x)
        {
            if (i > n)
            {
                Console.WriteLine("ERROR: i > n in Spline.si(i, x)\n");
                return 0;
            }

            return a[i] + b[i] * (x - this.x[i]) + c[i] * (x - this.x[i]) * (x - this.x[i]) / 2.0
                + d[i] * (x - this.x[i]) * (x - this.x[i]) * (x - this.x[i]) / 6;
        }

        public double s(double x)
        {
            int i = 1;
            while (i <= n)
            {
                if (x >= this.x[i - 1] && x <= this.x[i]) break;
                i++;
            }
            return si(i, x);
        }
    }

    class Program
    {
        static Function f = x => Math.Log10(x) * Math.Log(10 * x) * Math.Sin(2.5 * x);
        const double a = 2.0;
        const double b = 10.0;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            int n = 100;
            Spline s = new Spline(n, a, b, f);

            n = 1000; // count of points
            string[] output = new string[n];
            double h = (b - a) / n;
            for (int i = 0; i < n; i++)
            {
                double x = a + h * i;
                output[i] = x.ToString() + ";" + s.s(x).ToString();
            }
            output[n - 1] = b.ToString() + ";" + s.s(b).ToString();
            File.WriteAllLines("output.csv", output);
        }
    }
}
