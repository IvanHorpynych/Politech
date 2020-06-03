using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration;

namespace lab4AMO
{
    class Program
    {
        static void Main(string[] args)
        {
            const double a = 2, b = 8;
            double eps = 1E-0;
            double h;
            Integrate FindInt = new Integrate();
            double NewLeib = FindInt.NewtonLeybnic(x => Math.Pow( 3 *x + 1, 6 ) / 18, b, a);
            Console.WriteLine("Newton-Leibnic Method:");
            Console.WriteLine("f(x) = {0:f8}", NewLeib);
            
            Console.WriteLine("\nSimpson Method:");
            Console.WriteLine("eps\t\t|h\t\t|Integral\t\t|delta");
            double[] MyEps = new double[5];
            for (int i = 0; i < 5; i++)
            {
                double fx = FindInt.Simpson(x => Math.Pow( 3 *x + 1, 5 ), b, a, eps, out h);
                Console.WriteLine("{0:f10}\t|{1:f8}\t|{2:f14}\t|{3:f12}", eps, h, fx, Math.Abs(NewLeib - fx));
                MyEps[i] = Math.Abs(NewLeib - fx);
                eps *= 1E-2;
            }
            //---------
            Console.WriteLine("\n\nRunge Method:");
            Console.WriteLine("eps\t\t|h\t\t|delta");
            for (int i = 0; i < 5; i++ )
            {
                double fx = FindInt.RungeMethod(x => Math.Pow(3 * x + 1, 5), b, a, MyEps[i], out h);
                Console.WriteLine("{0:f10}\t|{1:f8}\t|{2:f14}", MyEps[i],h, Math.Abs(NewLeib - fx));
            }
                Console.ReadLine();
        }
    }
}
