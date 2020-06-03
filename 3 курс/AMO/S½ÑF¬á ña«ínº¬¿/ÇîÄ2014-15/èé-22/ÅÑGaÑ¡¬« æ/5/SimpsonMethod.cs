using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    class SimpsonMethod
    {
        public double DoCalculations(Func<double, double> f, double eps, double b, double a)
        {
            int n = (int)((b - a) / (Math.Pow(eps, 0.25)));
            if (n % 2 != 0) n++;
            double I1 = SimpsonRule(f, n, b, a);
            double I2 = SimpsonRule(f, 2 * n, b, a);

            while (Math.Abs(I1 - I2) / 15.0 > eps)
            {
                I1 = SimpsonRule(f, n, b, a);
                n *= 2;
                I2 = SimpsonRule(f, n, b, a);
                if (n > 4000) break; 
            }

            return I2;
        }

        private double SimpsonRule(Func<double, double> f, int n, double b, double a)
        {
            double step = (b - a) / n;
            double sigma1 = 0;
            double sigma2 = 0;
            double x = a;
            for (int i = 0; i < n - 1; i++)
            {
                x += step;
                if (i % 2 == 0)
                    sigma1 += f(x);
                else
                    sigma2 += f(x);
            }

            return (step / 3) * (f(a) + f(b) + 4 * sigma1 + 2 * sigma2);
        }
    }
}
