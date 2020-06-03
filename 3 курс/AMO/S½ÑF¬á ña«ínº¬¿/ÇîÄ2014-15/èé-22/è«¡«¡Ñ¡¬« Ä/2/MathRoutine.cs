using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Forms
{
    public class AlgorithmsCompareData
    {
        public double eps;
        public double iterRoot;
        public int iterCount;
        public double iterPrecision;
        public double hordeRoot;
        public int hordeCount;
        public double hordePrecision;
    }

    static class MathRoutine
    {
        public const double l1 = -1;
        public const double r1 = 0;
        public const double firstIntervalMin = -3;
        public const double firstIntervalMax = 6.9699;
        public const double l2 = 0;
        public const double r2 = 1;


        public static double F(double x)
        {
            return Math.Sin (3*x) + 2*x*x -1;
        }

        public  static double FirstDerivative(double x)
        {
            return 4 * x + 3 * Math.Cos(3 * x);
        }

        public static double SecondDerivative(double x)
        {
            return 4 - 9*Math.Sin(3*x);
        }
    }
}
