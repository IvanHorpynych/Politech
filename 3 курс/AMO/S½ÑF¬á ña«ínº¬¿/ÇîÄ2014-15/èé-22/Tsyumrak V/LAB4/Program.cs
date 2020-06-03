using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMO_Lab4
{
    class Program
    {
        delegate double Function(double x);

        class TrapeziumIntegrator
        {
            private Function f;
            private double eps;
            private double r = 2.0;
            private double a;
            private double b;
            private int count;
            private double h;

            public TrapeziumIntegrator(Function func, double _a, double _b, double epsilon)
            {
                a = _a;
                b = _b;
                setFunction(func);
                setEpsilon(epsilon);
                count = 0;
            }

            public void setEpsilon(double e) { eps = e; }
            public void setFunction(Function func) { f = func; }

            public double calculate()
            {
                int n = (int)Math.Ceiling(1 / Math.Pow(eps, 1 / r)) + 1;
                h = (b - a) / n;
                double prev = (f(b) - f(a)) / 2;
                double result = calculate(h);
                double R = Math.Abs(result - prev) / (Math.Pow(2.0, r) - 1);
                count = 1;
                while (R > eps)
                {
                    prev = result;
                    n *= 2;
                    h = (b - a) / n;
                    result = calculate(h);
                    R = Math.Abs(result - prev) / (Math.Pow(2.0, r) - 1);
                    count++;
                }

                return result;
            }

            public double calculate(double _h)
            {
                h = _h;
                int n = (int)Math.Ceiling((b - a) / h) + 1;
                h = (b - a) / n;
                double result = h * (f(a) + f(b)) / 2;

                for (int i = 1; i < n; i++)
                    result += h * f(a + h * i);

                return result;
            }

            public int getCount() { return count; }
            public double getH() { return h; }
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Function f = x => Math.Exp(x) - x * x / 2 - x;
            double epsilon = 1e-02;
            double a = 1.0;
            double b = 17.0;

            double m = Math.Exp(b) - 1; // maximum of second derivative of f(x) in [a, b]

            Function antiderivative = x => Math.Exp(x) - x * x * x / 6 - x * x / 2;
            double preciseResult = antiderivative(b) - antiderivative(a);
            
            TrapeziumIntegrator integrator = new TrapeziumIntegrator(f, a, b, epsilon);

            List<double> delta = new List<double>();
            Console.WriteLine("epsilon\t\th\t\tprecise\t\tdelta\n");
            for (epsilon = 1e+4; epsilon > 1e-4; epsilon *= 1e-01)
            {
                double h = Math.Sqrt(12 * epsilon / (m * (b - a)));
                integrator.setEpsilon(epsilon);
                double result = integrator.calculate(h);
                delta.Add(Math.Abs(result - preciseResult));
                Console.Write("{0:E}\t{1:E}\t{2:E}\t{3:E}\n",
                    epsilon, h, preciseResult, delta.Last());
            }

            int i = 0;
            Console.WriteLine("\nepsilon\t\tN\th\t\tdelta\n");
            for (epsilon = 1e+4; epsilon > 1e-4; epsilon *= 1e-01)
            {
                integrator.setEpsilon(delta[i]);
                double result = integrator.calculate();
                Console.Write("{0:E}\t{1}\t{2:E}\t{3:E}\n",
                    delta[i], integrator.getCount(), integrator.getH(), Math.Abs(preciseResult - integrator.calculate()));
                i++;
            }
        }
    }
}
