using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMO_Lab5
{
    class SoLE
    {
        private double[,] A;
        private double[] B;
        private int n;

        public SoLE(int _n, double[,] _a, double[] _b)
        {
            if (_n != Math.Sqrt(_a.Length) || _n != _b.Length)
                Console.WriteLine("Fatal error: incorrect n for SOLE.");
            else
            {
                n = _n;
                A = _a;
                B = _b;
            }
        }

        public double a(int i, int j) { return A[i, j]; }
        public double b(int i) { return B[i]; }
        public int size() { return n; }
        public void set_a(int i, int j, double value) { A[i, j] = value; }
        public void set_b(int i, double value) { B[i] = value; }

        public override string ToString()
        {
            string result = "A =\n";

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    result += a(i, j).ToString() + "\t";
                result += "\n";
            }
            result += "\nB =\t";
            for (int i = 0; i < n; i++)
                result += b(i).ToString() + "\t";
            result += "\n";

            return result;
        }
    }

    static class Solver
    {
        static public double[] solve(SoLE s)
        {
            int n = s.size();
            List<double> result = new List<double>();

            if (n < 1) return result.ToArray();
            else if (n == 1)
                // solving a * x = b: x = b / a;
                result.Add(s.b(0) / s.a(0, 0));
            else
            {
                for (int j = 1; j < n; j++)
                {
                    // at first equation
                    // a[0, j] = a[0, j] / a[0, 0]
                    s.set_a(0, j,
                        s.a(0, j) / s.a(0, 0));
                }
                s.set_b(0, s.b(0) / s.a(0, 0)); // b[0] = b[0] / a[0, 0]
                s.set_a(0, 0, 1.0); // a[0, 0] = 1
                /*
                Console.WriteLine("After first line transformation");
                Console.WriteLine(s.ToString());
                */
                double[,] A = new double[n - 1, n - 1];
                double[] B = new double[n - 1];

                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - 1; j++)
                        A[i, j] = s.a(i + 1, j + 1) - s.a(0, j + 1) * s.a(i + 1, 0);
                    B[i] = s.b(i + 1) - s.a(i + 1, 0) * s.b(0);
                }

                SoLE next = new SoLE(n - 1, A, B);
                /*
                Console.WriteLine("Next SOLE:");
                Console.WriteLine(next.ToString());
                */
                double[] temp_result = solve(next);

                // x[0] = b[0] - a[0, 1] * x[1] - .. - a[0, n] * x[n]
                result.Add(s.b(0));
                for (int j = 0; j < n - 1; j++)
                    result[0] -= s.a(0, j + 1) * temp_result[j];
                result.AddRange(temp_result);
            }

            return result.ToArray();
        }
    }
}
