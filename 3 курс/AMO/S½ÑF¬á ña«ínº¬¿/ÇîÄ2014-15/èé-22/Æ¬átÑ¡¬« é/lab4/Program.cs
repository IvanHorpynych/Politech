using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Program
    {
       static double a = 1.0, b = 22.0;
        private static double Func(double x)
        {
            return Math.Sqrt(1 + Math.Log(x))/x;
        }

        public static double NewtonLeibnicMethod() {

            double F1 = 0.0, F2 = 0.0;
            F1 = 2 * Math.Sqrt(Math.Pow(1 + Math.Log(a), 3)) / 3;
            F2 = 2 * Math.Sqrt(Math.Pow(1 + Math.Log(b), 3)) / 3; ;
            return Math.Abs(F2 - F1);
        }
        private static double SimpsonMethod (double h, double n)
        {
            double x = a;
            double Sigma1 = 0.0, Sigma2 = 0.0;
            for(int i = 1; i < n; i++)
            {   
                x += h;
                if (i % 2 != 0 )
                    Sigma1 += Func(x);
                if (i % 2 == 0)
                    Sigma2 += Func(x);
            }
            return  h * (Func(a) + Func(b) + 4 * Sigma1 + 2 * Sigma2)/3;
        }

        public static void Double_Calculations()
        {
            double h = 0.0, eps = 1E-3;
            double delta = 0;
            eps = 1E-3;
            int n = 8;
            double I1 = 0;
            double I2 = 1; ;
            Console.WriteLine("EPS " + "\t" + " Step " + "\t\t" + " Integral " + "\t\t" + " Accuracy");
            for (int i = 0; i < 5; i++)
            {
                while (Math.Abs(I1 - I2)/15 > eps)
                {
                    n *= 2;
                    h = (b - a) / n;
                    I1 = SimpsonMethod(h, n);
                    I2 = SimpsonMethod(h/2, n * 2);
                }
                delta = Math.Abs((I1 - I2)/15);
                Console.WriteLine(eps + "\t" + h +"\t" + I2 +"\t" + "\t" + delta);
                eps *= 1E-2;
            }
        }
        public static void RunSimpsonMethod()
        {
            double h = 0.0, eps = 1E-3;
            double I = 0;
            double delta = 0;
            int n = 2;
            double I1 = NewtonLeibnicMethod();
            Console.WriteLine("EPS " + "\t" + " Step " + "\t\t" + " Integral " + "\t\t" + "Accuracy");
            for (int i = 0; i < 5; i++)
            {
                while (Math.Abs(I - I1) > eps)
                {
                    n *= 2;
                    h = (b - a) / n;
                    I = SimpsonMethod(h, n);
                }
                delta = Math.Abs((I1 - I));
                Console.WriteLine(eps + "\t" + h + "\t" + I + "\t" + delta);
                eps *= 1E-2;
            }

            //double I1 = NewtonLeibMethod();
            //Console.WriteLine("EPS " + "\t" + " Step " + "\t\t" + " Integral " + "\t\t" + " Error");
            //for (int i = 0; i < 5; i++)
            //{
            //    double M4 = Math.Sinh(b);
            //    double h = Math.Sqrt(Math.Sqrt(180*eps/((b-a)*M4))) - eps*40;
            //    double nn = (b-a) / h;
            //    int n = Convert.ToInt32 (nn);
            //    double I = SimpsonMethod(h, n);
            //    Delta[i] = Math.Abs((I1 - I));
            //    Console.WriteLine(eps + "\t" + h + "\t" + I + "\t" + Delta[i]);
            //    eps *= 1E-2;
            //}
            
        }
        static void Main(string[] args)
        {
            RunSimpsonMethod();
            Double_Calculations();
            Console.Read();
        }
    }
}
