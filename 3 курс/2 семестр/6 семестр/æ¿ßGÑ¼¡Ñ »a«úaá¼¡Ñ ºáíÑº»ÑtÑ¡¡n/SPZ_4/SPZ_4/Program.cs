using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPZ_4
{
    class Program
    {
        static void Main(string[] args)
        {
            matrix m = new matrix();
            m.PrintMatr();
            //Console.WriteLine();
            //m.ShowMatrix();
            Console.WriteLine();
            m.Transform();
            Console.WriteLine();
            m.PrintMatr();
            m.FndSubMatrix();
            m.FndSolution();
            Console.WriteLine();
            m.ShowMatrix();
        }
    }
}
