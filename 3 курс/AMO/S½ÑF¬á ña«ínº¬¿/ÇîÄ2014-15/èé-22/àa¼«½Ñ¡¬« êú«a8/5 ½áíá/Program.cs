using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMOLAB5
{
    class Program
    {
        static void Main(string[] args)
        {
            LeastSquaresApproximation.n = 5;

            while(LeastSquaresApproximation.n < 16)
            {
                Console.WriteLine("\n\n" + "N = "+ LeastSquaresApproximation.n);
                LeastSquaresApproximation obj = new LeastSquaresApproximation();
                obj.GetNormalEquations();
                obj.GaussianElimination();
                //obj.PrintMonomials();
                obj.ErrorFix();
                obj.ShowXY();
                LeastSquaresApproximation.n += 5;
            }
            Console.ReadKey();
        }
    }
}
