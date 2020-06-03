using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMO_Lab5
{
    class Integrator
    {       
        private double eps;
        private double r = 2.0;

        public Integrator(double epsilon)
        {
            setEpsilon(epsilon);
        }

        public void setEpsilon(double e) { eps = e; }

        public double calculate(Function f, double a, double b)
        {
            int n = (int)Math.Ceiling(1 / Math.Pow(eps, 1 / r)) + 1;
            
            double prev = (f(b) - f(a)) / 2;
            double result = calculate(f, a, b, n);
            double R = Math.Abs(result - prev) / (Math.Pow(2.0, r) - 1);
            while (R > eps)
            {
                prev = result;
                n *= 2;
                result = calculate(f, a, b, n);
                R = Math.Abs(result - prev) / (Math.Pow(2.0, r) - 1);
            }

             return result;
        }

        private double calculate(Function f, double a, double b, int n)
        {
            double h = (b - a) / n;
            n = (int)Math.Ceiling((b - a) / h) + 1;
            h = (b - a) / n;
            double result = h * (f(a) + f(b)) / 2;

            for (int i = 1; i < n; i++)
                result += h * f(a + h * i);

            return result;
        }
    }
}
