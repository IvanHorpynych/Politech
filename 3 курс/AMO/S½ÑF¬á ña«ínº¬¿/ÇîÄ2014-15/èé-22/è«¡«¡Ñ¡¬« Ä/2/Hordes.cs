using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Forms
{
    class Hordes
    {
        private bool HordePrecision(double cur, double m1, double eps)
        {
            return Math.Abs(cur / m1) <= eps;
        }

        public delegate double MyFunc(double x);

        private double NewApproximation(double curF, double curX, double c, double fC)
        {
            return curX - curF / (curF - fC) * (curX - c);
        }

        public double Solve(MyFunc f, MyFunc secDer, double m1, double eps, double l, double r, out int n, out double precision)
        {
            
            double multiplier = secDer(l) <= 0 || secDer(r) <= 0 ? -1 : 1;
            n = 1;
            //setting non-movable edge

            double res;
            double c;
            if (multiplier * f(l) * secDer(l) >= 0)
            {
                c = l;
                res = r;
            }
            else
            {
                c = r;
                res = l;
            }
            double fC = f(c);


            while (!HordePrecision(f(res), m1, eps))
            {
                Console.WriteLine("{0}: X = {1}, f(x) = {2}", n, res, f(res));
                res = NewApproximation(multiplier * f(res), res, c, fC);
                n++;
                if (n > 10e6) throw new ArgumentException("Seems like there are no roots on interval");
            }

            precision = Math.Abs(f(res)) / m1;
            return res;
        }


    }
}
