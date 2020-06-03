using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amo_Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            const int n = 4;
            double [,] Matrix = new double[n, n] { { 2, 11, 13, 15 }, { 12, 38, 18, 7 }, { 20, 1, 27, 5 }, { 15, 2, 15, 5 } };
            double [] RightPart = new double[n] { 33, 114, 3, 6 };
            MainElemMethod Gaus = new MainElemMethod(Matrix , RightPart, n);
            Gaus.SolveMatrix();
            Console.WriteLine("Complete pivoting:");
            Console.WriteLine(Gaus.ToString());
            Console.WriteLine();

            Console.WriteLine("Gauss-Seidel method:");
            GausSeidel gausSeidel = new GausSeidel(Matrix, RightPart, n);
            gausSeidel.Solve();
            Console.WriteLine(gausSeidel.ToString());
            Console.ReadLine();
        }
    }
}
