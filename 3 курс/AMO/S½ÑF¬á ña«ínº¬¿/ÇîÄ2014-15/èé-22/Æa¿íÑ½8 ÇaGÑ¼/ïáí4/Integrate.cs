using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration
{
    class Integrate
    {
        public double NewtonLeybnic(Func<double,double>F,double b, double a)
        {
            return Math.Abs(F(b) - F(a));
        }


        public double Simpson(Func<double,double>F,double b,double a,double eps, out double h)
        {
            double M4 = 1012500;
            h = Math.Pow((180 * eps / ((Math.Abs(b - a)) * M4)),0.25);
            return SimpsonMethod(F, b, a,ref h);
        }

        private static double SimpsonMethod(Func<double, double> F, double b, double a,ref double h)
        {
            double sigma1 = 0, sigma2 = 0;
            int n = (int)Math.Ceiling((Math.Abs(b - a) / h));
            if (n % 2 != 0) n += 1;
            h = Math.Abs(b - a) / n;
            double x = a;
            for (int i = 0; i < n - 1; i++)
            {
                x += h;
                if (i % 2 == 0)
                {
                    sigma2 += F(x);
                }
                else sigma1 += F(x);
            }
            return (h / 3) * (F(a) + F(b) + 4 * sigma2 + 2 * sigma1);
        }
        //overloaded Simpson
        private static double SimpsonMethod(Func<double, double> F, double b, double a, double h)
        {
            double sigma1 = 0, sigma2 = 0;
            int n = (int)(Math.Abs(b - a) / h);
            if (n % 2 != 0) n += 1;
            h = Math.Abs(b - a) / n;
            double x = a;
            for (int i = 0; i < n - 1; i++)
            {
                x += h;
                if (i % 2 == 0)
                {
                    sigma2 += F(x);
                }
                else sigma1 += F(x);
            }
            return h / 3 * (F(a) + F(b) + 4 * sigma2 + 2 * sigma1);
        }
        public double RungeMethod(Func<double,double>F,double b, double a,double eps, out double h)
        {
            double n =(int) (b-a)/ Math.Pow(eps, 0.25);
            if (n % 2 == 0) n++;
            h = Math.Abs(b - a) / n;
            double Iprev = SimpsonMethod(F, b, a, h);
            n *= 2;
            h = Math.Abs(b-a)/ n;
            double Inext = SimpsonMethod(F,b,a,h);
            
            while ((Math.Abs(Iprev - Inext)) / 15>eps)
            {
                n *= 2;
                h = Math.Abs(b - a) / n;
                Iprev = Inext;
                Inext = SimpsonMethod(F, b, a,h);
            }
            return Inext;
        }
    }
}
