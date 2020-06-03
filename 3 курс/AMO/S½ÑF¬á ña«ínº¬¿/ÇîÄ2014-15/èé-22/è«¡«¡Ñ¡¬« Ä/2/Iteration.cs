using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Forms
{
    class Iteration
    {
        public delegate double MyFunc(double x);
        private static Random rand = new Random();
        private bool IterationPrecision(double prev, double cur, double eps, double q)
        {
            double left = Math.Abs(prev - cur);
            double right = (1 - q) / q;
            return  left <= right * eps;
        }

        private static double IterationFunc(MyFunc f,double x, double lambda)
        {
            return x - lambda * f(x);
        }

        public double Solve(MyFunc f, MyFunc firDer, double eps, double l, double r, double m, double M, out int n, out double precision)
        {
            double lambda = Math.Abs(1 / M);
            double q = Math.Abs(m * lambda);
            
            if (firDer(l) <= 0 || firDer(r) <= 0) lambda *= -1;


            double prev = NextDoubleInRange(l, r);
            double cur = IterationFunc(f, prev, lambda);
            n = 1;

            while (!IterationPrecision(prev, cur, eps, q))
            {
                Console.WriteLine("{0}: X = {1}, f(x) = {2}", n, prev, cur);
                prev = cur;
                cur = IterationFunc(f, prev, lambda);
                n++;
                if (n > 10e6) throw new ArgumentException("Seems like there are no roots on interval");
            }

            precision = Math.Abs(q / (1 - q) * (cur - prev));
            return cur;
        }

        private double NextDoubleInRange(double l, double r)
        {
            if (l > r) throw new ArgumentException();
            //random point in [a,b]
            return l + (r - l) * rand.NextDouble();
        }

    }
}
