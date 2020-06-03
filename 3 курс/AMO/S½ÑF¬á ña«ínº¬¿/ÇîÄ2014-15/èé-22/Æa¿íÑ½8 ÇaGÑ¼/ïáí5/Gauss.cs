using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5AMO
{
    class Gauss
    {
        private void Sub(double[,] a, int k)
        {
            for (int i = k + 1; i < a.GetLength(0); i++)
            {
                double multiplier = a[i, k];
                for (int j = k; j < a.GetLength(1); j++)
                {
                    a[i, j] = a[k, j] * multiplier - a[i, j];
                }
            }
        }

        private void divDiagonal(double[,] a, int i)
        {
            double mElement = a[i, i];
            if (mElement == 0)
            {
                throw new ArgumentException("Main element is 0!");
            }

            for (int j = i; j < a.GetLength(1); j++)
            {
                a[i, j] = a[i, j] / mElement;
            }
        }

        public double[] GaussRoots(double[,] matrix)
        {
            var a = matrix;
            for (int i = 0; i < a.GetLength(0); i++)
            {
                divDiagonal(a, i);
                Sub(a, i);
               
            }
            //x4 = tempMatrix[3, 4];
            //x3 = tempMatrix[2, 4] - tempMatrix[2, 3] * x4;
            //x2 = tempMatrix[1, 4] - tempMatrix[1, 3] * x4 - tempMatrix[1, 2] * x3;
            //x1 = tempMatrix[0, 4] - tempMatrix[0, 3] * x4 - tempMatrix[0, 2] * x3 - tempMatrix[0, 1] * x2;
            var res = CalcRoots(a);
            return res;
        }

        private double[] CalcRoots(double[,] a)
        {
            var res = new double[a.GetLength(1) - 1]; 
            res[res.Length - 1] = a[a.GetLength(0) - 1, a.GetLength(1) - 1];
            for (int i = res.Length - 2; i >= 0; i--)
            {
                res[i] = a[i, a.GetLength(1) - 1];
                for (int j = i + 1; j < res.Length; j++)
                {
                    res[i] += res[j] * (-a[i, j]);
                }
            }

            return res;
        }

    }
}
