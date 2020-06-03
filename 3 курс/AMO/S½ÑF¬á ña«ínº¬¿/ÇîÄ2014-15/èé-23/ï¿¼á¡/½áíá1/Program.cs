using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1AMO
{
    class Program
    {
        const double a = -0.8;
        const double b = 6.9;
        static double calc_sh(double x, double eps, out int k, out double last)
        {
            double Uk = x;
            double sh = Uk;
            k = 1;
            while (Math.Abs(Uk) >= eps)
            {
                Uk = x * x / (2 * k * (2 * k + 1)) * Uk;
                sh += Uk;
                k++;
            }
            last = Uk;
            return sh + Uk/3; 
        }

        static int first()
        {
            double eps = 1e-2;
            double x = (b + a) / 2;
            double last,c = 0;
            int k;
            int n = 0;
            double abs = 0;
            Console.WriteLine("         eps       |   n   |         abs.p            |         zal.c      ");
            for (int i = 0; i < 4; i++)
            {
                c = calc_sh(x, eps, out k, out last);
                abs = Math.Abs(Math.Sinh(x) - c);
                Console.WriteLine("   {0:f11}   |   {1}   |   {2:f18}   |   {3:f18}   ", eps, k, abs, last / 3);
                eps *= 1e-3;
                if (i == 2)
                    n = k;
            }
            return n;
        }
        static double calc_sh_second(double x, int n, out double last)
        {
            double Uk = x;
            double sh = Uk;
            for (int i = 1; i <= n; i++)
            {
                Uk = x * x / (2 * i * (2 * i + 1)) * Uk;
                sh += Uk;
            }
            last = Uk;
            double Rn = Uk / 3;
            return sh + Rn;
        }
        static void second(int n)
        {
            double h = (b - a) / 10;
            double x = 0;
            double last;
            double c = 0;
            double abs = 0;
            Console.WriteLine("         xi         |       abs.p            |         zal.c      ");
            for (int i = 0; i <= 10; i++)
            {
                x = a + h * i;
                c = calc_sh_second(x, n, out last);
                abs = Math.Abs(Math.Sinh(x) - c);
                if(x<0)
                Console.WriteLine("   {0:f11}   | {1:f18}   |   {2:f18}   ", x, abs, last / 3);
                else Console.WriteLine("    {0:f11}   | {1:f18}   |   {2:f18}   ", x, abs, last / 3);
            }
        }
        static void Main(string[] args)
        {
            int a = first();
            Console.WriteLine();
            second(a);
            Console.ReadLine();
        }
    }
}
