using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMO_Lab3
{
    class Program
    {
        // system of linear equations
        class SOLE
        {
            private double[,] A;
            private double[] B;
            private int n;

            public SOLE(int _n, double[,] _a, double[] _b)
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

        class GaussianElimination
        {
            public GaussianElimination() { }

            public double[] solve(SOLE s)
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

                    SOLE next = new SOLE(n - 1, A, B);
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

        class GaussSeidel
        {
            public GaussSeidel() { }

            public double[] solve(SOLE s)
            {
                int n = s.size();
                double[] x = new double[n];

                if (n < 1) return x;

                double[,] A = new double[n, n]; //alpha
                double[] B = new double[n]; // beta

                for (int i = 0; i < n; i++)
                {
                    B[i] = s.b(i) / s.a(i, i);
                    for (int j = 0; j < n; j++)
                        if (i == j) A[i, j] = 0;
                        else A[i, j] = -s.a(i, j) / s.a(i, i);
                    x[i] = B[i];
                }

                double[] prev_x = new double[n];
                double q = M(A);
                if (q >= 1)
                {
                    Console.WriteLine("M(A) >= 1: can not solve with GaussSeidel.");
                    return new double[0];
                }
                while (M(sub(x, prev_x)) > (1 - q) * 1e-16 / q)
                {
                    x.CopyTo(prev_x, 0);
                    // iteration
                    for (int i = 0; i < n; i++)
                    {
                        x[i] = B[i];
                        for (int j = 0; j < n; j++)
                        {
                            x[i] += A[i, j] * x[j];
                        }
                    }
                }

                return x;
            }

            private double M(double[,] a)
            {
                int n = (int)Math.Sqrt(a.Length);
                double max = 0;
                for (int j = 0; j < n; j++)
                    max += Math.Abs(a[0, j]);

                for (int i = 1; i < n; i++)
                {
                    double current = 0;
                    for (int j = 0; j < n; j++) current += Math.Abs(a[i, j]);
                    if (current > max) max = current;
                }

                return max;
            }

            private double M(double[] a)
            {
                int n = a.Length;
                double max = Math.Abs(a[0]);

                for (int i = 1; i < n; i++)
                    if (Math.Abs(a[i]) > max) max = Math.Abs(a[i]);

                return max;
            }

            private double[] sub(double[] a, double[] b)
            {
                int n;
                if (a.Length < b.Length) n = a.Length;
                else n = b.Length;
                
                double[] result = new double[n];

                for (int i = 0; i < n; i++)
                    result[i] = a[i] - b[i];

                return result;
            }
            
        }

        static void Main(string[] args)
        {
            SOLE s = new SOLE(4,
                new double[,] {
                { 10, 2, 0, 19 },
                { 2, 24, 7, 14 },
                { 10, 14, 29, 4},
                { 20, 13, 3, 8}},
                new double[] { 44, 114, 108, 61 });
            Console.WriteLine(s.ToString());

            Console.WriteLine("Gaussian elimination");

            GaussianElimination solver1 = new GaussianElimination();
            double[] solution = solver1.solve(s);
            for (int i = 0; i < solution.Length; i++)
                Console.WriteLine("x[" + (i + 1).ToString() + "] = " + solution[i].ToString());

            Console.WriteLine("\nGauss and Seidel\n\nIterationable system");
            SOLE s2 = new SOLE(4,
                new double[,] {
                { 19, 2, 0, 10 },
                { 14, 24, 7, 2 },
                { 4, 14, 29, 10},
                { 13, -9, -4, 28}}, // (swap A[i, 3] and A[i, 0]) - A[2, j] + A[0, j]
                new double[] { 44, 114, 108, -9 });

            GaussSeidel solver2 = new GaussSeidel();
            solution = solver2.solve(s2);
            Console.WriteLine(s2.ToString());

            

            // swap back
            double temp = solution[0];
            solution[0] = solution[3];
            solution[3] = temp;

            for (int i = 0; i < solution.Length; i++)
                Console.WriteLine("x[" + (i + 1).ToString() + "] = " + solution[i].ToString());

            Console.WriteLine();
        }
    }
}
