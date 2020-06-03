using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class Program
    {
        private static Random rand = new Random();
        public delegate double Func(double x);
        public static double F(double x)
        {
            return 15 / x - x * x + 15;
        }
        public static double FirstDerivative(double x)
        {
            return -15/x/x - 2*x;
        }

        static double RandDouble(double l, double r)
        {
            if (l > r) throw new ArgumentException();
            //random point in [a,b]
            return l + (r - l) * rand.NextDouble();
        }
        static double IterationFunc(Func F, double x, double lambda)
        {
            //x^2 = 15 - 15/x
            //x = sqrt(15 - 15/x)

            return x - lambda * F(x);
        }

        static bool IterationAccuracy(double prev_root, double cur_root, double eps, double q)
        {
            double left_part = Math.Abs(prev_root - cur_root);
            double right_part = (1 - q) / q;
            return left_part <= right_part * eps;
        }
        public static double Iteration_Root(Func F, Func FirstD, double eps, double l, double r, double m, double M, ref int n, ref double accuracy)
        {
            double lambda = Math.Abs(1 / M);
            double q = Math.Abs(m * lambda);

            if (FirstD(l) <= 0 || FirstD(r) <= 0) lambda *= -1;

            double prev_root = RandDouble(l, r);
            double cur_root = IterationFunc(F, prev_root, lambda);
            n = 1;

            while (!IterationAccuracy(prev_root, cur_root, eps, q))
            {
                prev_root = cur_root;
                cur_root = IterationFunc(F, prev_root, lambda);
                n++;
                if (n > 10e6) throw new ArgumentException("No roots on this interval");
            }

            accuracy = Math.Abs(q / (1 - q) * (cur_root - prev_root));
            return cur_root;
        }


        public static double Bisection_Root(Func F, double eps, double l, double r, ref int n, ref double accuracy)
        {
            if (F(l)*F(r) > 0)
            {
                //Console.WriteLine("No roots on [a,b]!");
                throw new ArgumentException ("No roots in this interval");
            }
            if (l > r)
            {
                //Console.WriteLine("No roots on [a,b]!");
                throw new ArgumentException("Wrong interval");
            }
            double m = 0;
            double a = l;
            double b = r;
            int nn = 0;
            while (b - a > 2 * eps)
            {
                nn++;
                double middle = (a + b) / 2;
                m = F(middle);
                if (m == 0)
                {
                    return m;
                }
                else
                {
                    if (F(middle) * F(a) < 0) b = middle;
                    else a = middle;
                }
            }
            m = (a + b) / 2;
            n = nn;
            accuracy = (r - l) / Math.Pow(2, nn + 1 );
            return m;
        }

        static void Main(string[] args)
        {
            int n = 0;
            double accuracy = 0;
            //double eps = 0.01;
            double l = 4;
            double r = 5;
            double m = -8.9375;
            double M = -10.6;
            double res = 0;
            Console.WriteLine("First root of the equation calculated using iteration method:");
            for (double i = 0.1; i >= 10e-13; i*= 10e-4)
            {
                try
                {
                    res = Iteration_Root(F, FirstDerivative, i, l, r, m, M, ref n, ref accuracy);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(string.Format("Error! {0}", ex.Message));
                }
                // res = Iteration_Root(F, FirstDerivative, i, l, r, m, M, ref n, ref accuracy);
                 Console.WriteLine("Eps = {0}, Root = {1}, accuracy = {2}, n = {3}" , i , res , accuracy, n);
            }
            l = -4;
            r = -3;
            m = 4.33;
            M = 7.0625;
            Console.WriteLine("Second root of the equation calculated using iteration method:");
            for (double i = 0.1; i >= 10e-13; i *= 10e-4)
            {
                try
                {
                    res = Iteration_Root(F, FirstDerivative, i, l, r, m, M, ref n, ref accuracy);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(string.Format("Error! {0}", ex.Message));
                } 
                Console.WriteLine("Eps = {0}, Root = {1}, accuracy = {2}, n = {3}", i, res, accuracy, n);
            }
            l = -1.5;
            r = -1;
            m = -3.66;
            M = -13;
            Console.WriteLine("Third root of the equation calculated using iteration method:");
            for (double i = 0.1; i >= 10e-13; i *= 10e-4)
            {
                try
                {
                    res = Iteration_Root(F, FirstDerivative, i, l, r, m, M, ref n, ref accuracy);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(string.Format("Error! {0}", ex.Message));
                } 
                Console.WriteLine("Eps = {0}, Root = {1}, accuracy = {2}, n = {3}", i, res, accuracy, n);
            }

            l = 4;
            r = 5;
            res = 0;
            Console.WriteLine("First root of the equation calculated using bisection method:");
            for (double i = 0.1; i >= 10e-13; i *= 10e-4)
            {
                try
                {
                    res = Bisection_Root(F, i, l, r, ref n, ref accuracy);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(string.Format("Error! {0}", ex.Message));
                } 
                Console.WriteLine("Eps = {0}, Root = {1}, accuracy = {2}, n = {3}", i, res, accuracy, n);
            }
            l = -4;
            r = -3;
            Console.WriteLine("Second root of the equation calculated using bisection method:");
            for (double i = 0.1; i >= 10e-13; i *= 10e-4)
            {
                try
                {
                    res = Bisection_Root(F, i, l, r, ref n, ref accuracy);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(string.Format("Error! {0}", ex.Message));
                } 
                Console.WriteLine("Eps = {0}, Root = {1}, accuracy = {2}, n = {3}", i, res, accuracy, n);
            }
            l = -1.5;
            r = -1;
            Console.WriteLine("Third root of the equation calculated using bisection method:");
            for (double i = 0.1; i >= 10e-13; i *= 10e-4)
            {
                try
                {
                    res = Bisection_Root(F, i, l, r, ref n, ref accuracy);
                }
                catch (Exception ex)
                {

                    Console.WriteLine(string.Format("Error! {0}", ex.Message));
                } 
                Console.WriteLine("Eps = {0}, Root = {1}, accuracy = {2}, n = {3}", i, res, accuracy, n);
            }
           
            Console.ReadLine();
        }
    }
}
