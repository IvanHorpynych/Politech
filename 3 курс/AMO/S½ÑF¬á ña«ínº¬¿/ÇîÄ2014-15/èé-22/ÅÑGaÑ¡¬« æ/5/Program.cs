using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    class Program
    {
        private static double Func(double x) //Function to approx
        {
            return 1.2 * Math.Tanh(x) * Math.Cos(10 * x) * x;
        }
        static void Main(string[] args)
        {
            double a = 1.0;
            double b = 3.0;
            double eps = 1E-5;
            double delta = 0.0;  //standard deviation
            Approximation.n = 4; // amount of Chebishev polinomials
            do
            {
                var approximator = new Approximation(a, b, eps, Func);
                approximator.Approximate();
                delta = approximator.delta;
                Console.WriteLine("N = {0}, Delta = {1}", Approximation.n, approximator.delta);
                Approximation.n += 2;
                if (Approximation.n > 25) break;
            } while (eps < delta);
            Console.ReadLine();
        }
    }
}
