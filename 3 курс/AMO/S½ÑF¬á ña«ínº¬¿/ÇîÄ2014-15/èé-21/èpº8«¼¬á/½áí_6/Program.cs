using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMO_Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            Cubic_Spline tmp = new Cubic_Spline();
            Cubic_Spline.n = 100;
            tmp.Build_Spline();
            tmp.Output_xy();
        }
    }
}

