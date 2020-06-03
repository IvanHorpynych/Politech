using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4amo
{
    class Program
    {
        delegate double MathFunc(double x);

        static double myfunc(double x)
        {
            return 1 / (1 + x * x);
        }

        static double integral(MathFunc f, int n)
        {
            double h, x, a, b;
            double s = 0;
            a = -575;
            b = 575;
            h = (b - a) / n;

            s += f(a);
            for (int i = 0; i <= n; i++)
            {
                x = a + i * h;
                s += ((i % 2) * 2 + 2) * f(x);
            }
            s += f(b);

            return s * h / 3;
        }


        static void Main(string[] args)
        {
            double NewtoneValue = 2 * Math.Atan(575);
            MathFunc func = myfunc;
            double eps = 10E-3;
            double a = -575;
            double b = 575;
            double h;
            int n;
            double R;
            Console.WriteLine(" epsilon     h      integral value    absolute error  ");
            for (int i = 1; i < 20; i++)
            {
                n = (int)(1 / Math.Pow(eps, 1 / 4f));
                h = (b - a) / n;

                Console.WriteLine(" {0,7}   {1,6:0.00}  {2,-16:0.#############}  {3,-16:0.#############} ",
                    eps, h, integral(func, n), Math.Abs(NewtoneValue - integral(func, n)));
                eps /= 10;
            }
            Console.WriteLine();
            Console.WriteLine("    delta    step   integral value        absolute error  ");
            for (int i = 2; i <= 8; i++)
            {

                eps = Math.Pow(10, -i);
                n = (int)(1 / Math.Pow(eps, 1 / 4f));
                double resN, res2N;
                while (true)
                {
                    resN = integral(func, n);
                    res2N = integral(func, 2 * n);

                    R = 1 / (Math.Pow(2, 4) - 1) * Math.Abs(resN - res2N);
                    if (R < eps)
                    {
                        break;
                    }
                    n *= 2;
                }

       
                Console.WriteLine(" {0,7}   {1,6:0.####}  {2,-16:0.#############}  {3,-16:0.#############} ",
                    eps, (b - a) / n, res2N, R);
                //  return 0;
            }
        }
    }
}
