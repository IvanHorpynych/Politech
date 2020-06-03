using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5AMO
{
    class Simpson
    {
        private static double SimpsonMethod(Func<double, double> F, double b, double a, double h)
        {
            double step = (b - a) / h;
            double sigma1 = 0;
            double sigma2 = 0;
            double x = a;
            for (int i = 0; i < h - 1; i++)
            {
                x += step;
                if (i % 2 == 0)
                    sigma1 += F(x);
                else
                    sigma2 += F(x);
            }

            return (step / 3) * (F(a) + F(b) + 4 * sigma1 + 2 * sigma2);
        }
        public double RungeMethod(Func<double, double> F, double eps,double b, double a )
        {
            
            int n = (int)((b - a) / (Math.Pow(eps, 0.25)));
            if (n % 2 != 0) n++;
            double IPrev = SimpsonMethod(F, b, a,n);
            double Inext = SimpsonMethod(F, b, a,2*n);

            while (Math.Abs((IPrev - Inext) / Inext) > 15 * eps)
            {
                IPrev = Inext;
                n *= 2;
                Inext = SimpsonMethod(F, b, a, n);
            }

            return Inext;
        }
    }
}
