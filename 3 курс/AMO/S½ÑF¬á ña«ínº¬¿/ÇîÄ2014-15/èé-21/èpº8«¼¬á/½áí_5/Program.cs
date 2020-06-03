using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMO_Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            Approximation.n = 5;

            while (Approximation.n < 50)
            {
                //Console.WriteLine("N = " + Approximation.n);
                Approximation tmp = new Approximation();
                tmp.Equations();
                tmp.Single_division();
                //tmp.Error_Fix();
                tmp.Output_xy();
                Approximation.n += 10;
            }
        }
    }
}


