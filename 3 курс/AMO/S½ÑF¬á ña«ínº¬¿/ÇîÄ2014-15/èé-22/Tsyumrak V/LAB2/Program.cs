using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMO_Lab2
{
    class Program
    {
        static int seedCounter = 0;

        // a simple range like [a..b]
        class Range
        {
            private double left_edge;
            private double right_edge;

            public Range(double a, double b)
            {
                if (a < b)
                {
                    left_edge = a;
                    right_edge = b;
                }
                else
                {
                    left_edge = b;
                    right_edge = a;
                }
            }

            public double a() { return left_edge; }
            public double b() { return right_edge; }

            public bool contains(double x) { return x >= a() && x <= b(); }

            public void write()
            {
                Console.Write("[ ");
                Console.Write("{0:F1}", a());
                Console.Write("; ");
                Console.Write("{0:F1}", b());
                Console.Write(" ]");
            }
        }

        // function class for method of iteration
        class I_Function
        {
            private double lambda;
            private bool negative_derivative = false;

            public I_Function(double _lambda)
            {
                this.lambda = _lambda;
            }

            public I_Function(double _lambda, bool n_g)
            {
                this.lambda = _lambda;
                this.negative_derivative = n_g;
            }

            public double f(double x)
            {
                return x - 5 * Math.Sin(x - Math.PI / 4);
            }

            public double fi(double x)
            {
                if (!negative_derivative)
                    return x - lambda * f(x);
                else return x + lambda * f(x);
            }
        }

        // function class for Newton's method
        class N_Function
        {
            public N_Function() {}

            public double f(double x)
            {
                return x - 5 * Math.Sin(x - Math.PI / 4);
            }

            public double derivative(double x)
            {
                return 1 - 5 * Math.Cos(x - Math.PI / 4);
            }
        }

        struct Solution
        {
            public double x;
            public double precision;
            public int n;

            public Solution(double _x, double _p, int _n)
            {
                x = _x;
                precision = _p;
                n = _n;
            }
        }

        abstract class Solver
        {
            protected Range r;
            protected double epsilon;
            
            protected double x;
            protected int n = 0;

            public void setEpsilon(double _e) { epsilon = _e; }

            public virtual void roll()
            {
                Random G = new Random((int)(DateTime.Now.TimeOfDay.TotalSeconds + seedCounter++));
                x = r.a() + G.NextDouble() * (r.b() - r.a());
                n = 0;
            }

            public abstract Solution solve();
        }

        // method of iteration
        class I_Solver : Solver
        {
            private I_Function func;
            private double q;

            public I_Solver(I_Function _func, Range _r, double _q, double _epsilon)
            {
                r = _r;
                q = _q;
                func = _func;
                setEpsilon(_epsilon);

                roll();
            }

            public override Solution solve()
            {
                n++;
                double next = func.fi(x);
                
                if (!r.contains(next))
                    Console.WriteLine("Oooops, seems like x is not inside range now.");

                if (Math.Abs(next - x) <= epsilon * (1 - q) / q)
                    return new Solution(x, Math.Abs(next - x) * q / (1 - q), n);
                else
                {
                    //Console.WriteLine(x);
                    x = next;
                    return solve();
                }
            }
        }

        // Newton's method
        class N_Solver : Solver
        {
            private N_Function func;
            private double m1;

            public N_Solver(N_Function _func, Range _r, double _m1, double _epsilon)
            {
                r = _r;
                m1 = _m1;
                func = _func;
                setEpsilon(_epsilon);

                roll();
            }

            // set x0
            public override void roll()
            { 
                x = r.a();
                n = 0;
            }

            public override Solution solve()
            {
                n++;
                if (!r.contains(x))
                    Console.WriteLine("Oooops, seems like x is not inside range now.");

                if (Math.Abs(func.f(x)) / m1 <= epsilon)
                    return new Solution(x, Math.Abs(func.f(x)) / m1, n);
                else
                {
                    //Console.WriteLine(x);
                    x = x - func.f(x) / func.derivative(x);
                    return solve();
                }
            }
        }

        static void Main(string[] args)
        {
            //Console.ForegroundColor = ConsoleColor.Green;

            Range[] r = 
            {
                new Range(-3.0, -1.0),
                new Range(0.0, 2.0),
                new Range(3.0, 4.0)
            };
            
            double[] m = {1.0, 0.5, 3.0};
            double[] M = {7.0, 5.0, 7.0};

            double[] q = new double[3];
            for (int i = 0; i < 3; i++)
                q[i] = 1.0 - m[i] / M[i];

            double[] lambda = new double[3];
            for (int i = 0; i < 3; i++)
                lambda[i] = 1.0 / M[i];

            bool[] negative_derivative = { false, true, false };

            Solution[] i_sol = new Solution[5];
            Solution[] n_sol = new Solution[5];

            // method of iteration test
            for (int k = 0; k < 3; k++)
            {
                I_Function func = new I_Function(lambda[k], negative_derivative[k]);
                I_Solver solver = new I_Solver(func, r[k], q[k], 1e-2);

                Console.Write("\nMethod of iteration test in range ");
                r[k].write();
                Console.WriteLine();
                Console.WriteLine("EPS\tx\t\t\tprecision");

                int i = 0;
                for (double eps = 1e-2; eps >= 1e-14; eps *= 1e-3)
                {
                    solver.setEpsilon(eps);
                    solver.roll();
                    
                    Solution solution = solver.solve();
                    Console.Write(eps.ToString() + "\t");
                    Console.Write("{0:F16}", solution.x);
                    Console.Write("\t");
                    Console.WriteLine("{0:E}", solution.precision);

                    if (k == 0) i_sol[i++] = solution;
                }
            }

            // Newton's method test
            for (int k = 0; k < 3; k++)
            {
                N_Function func = new N_Function();
                N_Solver solver = new N_Solver(func, r[k], m[k], 1e-2);

                Console.Write("\nNewton's method test in range ");
                r[k].write();
                Console.WriteLine();
                Console.WriteLine("EPS\tx\t\t\tprecision");

                int i = 0;
                for (double eps = 1e-2; eps >= 1e-14; eps *= 1e-3)
                {
                    solver.setEpsilon(eps);
                    solver.roll();

                    Solution solution = solver.solve();
                    Console.Write(eps.ToString() + "\t");
                    Console.Write("{0:F16}", solution.x);
                    Console.Write("\t");
                    Console.WriteLine("{0:E}", solution.precision);

                    if (k == 0) n_sol[i++] = solution;
                }
            }

            // comparsion
            Console.Write("\nMethods' comparsion in range ");
            r[0].write();
            Console.WriteLine();
            Console.WriteLine("EPS\tIteration\tNewton's");

            double e = 1e-2;
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(e.ToString() + "\t" + 
                    i_sol[i].n.ToString() + "\t\t" +
                    n_sol[i].n.ToString());

                e *= 1e-3;
            }
        }
    }
}
 