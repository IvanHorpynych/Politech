using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    class Matrix 
    {
        double[,] MyAr;
        public Matrix(double[,] ar) { MyAr = (double[,])ar.Clone(); }
        public double GetElem(int row, int col) { return MyAr[row, col]; }
        public double[] GetRow(int row)
        {
            int j = 0;
            double[] vector = new double[MyAr.GetLength(1)];
            for (; j < MyAr.GetLength(1); j++)
                vector[j] = MyAr[row, j];
            return vector;
        }
        public double[] SubRow(double[] from, double[] what)
        {
            double[] result = new double[from.Length];
            int i;
            for (i = 0; i < from.Length; i++)
                result[i] = from[i] - what[i];
            return result;
        }
        public void FillRow(int row, double[] vector)
        {
            int j;
            for (j = 0; j < MyAr.GetLength(1); j++)
                MyAr[row, j] = vector[j];
        }
        public double[] DivRow(int row, double num)
        {
            double[] result = GetRow(row);
            int i = 0;
            for (; i < result.Length; i++)
                result[i] /= num;
            return result;
        }
        public double[] MulRow(int row, double num)
        {
            double[] result = GetRow(row);
            int i = 0;
            for (; i < result.Length; i++)
                result[i] *= num;
            return result;
        }
        public int Length(int dimension)
        {
            return MyAr.GetLength(dimension);
        }
        public void InsertElem(int row, int col, double val)
        {
            MyAr[row, col] = val;
        }
        public double ResFromRow(int row, int col)
        {
            int j, last = MyAr.GetLength(1) - 1;
            for (j = col; j < last; j++)
                MyAr[row, last] -= MyAr[row, j];
            return MyAr[row, last];
        }
    };
    class Gaus
    {
        Matrix MyMatr;
        public Gaus(double[,] ar) { MyMatr = new Matrix(ar); }
        public double[] Solution()
        {
            int i, j;
            double[] result = new double[MyMatr.Length(0)];
            for (i = 0; i < MyMatr.Length(0); i++)
            {
                if (MyMatr.GetElem(i, i) == 0)
                    return (new double[(MyMatr.Length(0) + 1)]);
                else
                {
                    MyMatr.FillRow(i, MyMatr.DivRow(i, MyMatr.GetElem(i, i)));
                    for (j = i + 1; j < MyMatr.Length(0); j++)
                        MyMatr.FillRow(j, MyMatr.SubRow(MyMatr.MulRow(i, MyMatr.GetElem(j, i)), MyMatr.GetRow(j)));
                }
            }
            for (i = MyMatr.Length(0) - 1; i >= 0; i--)
            {
                result[i] = MyMatr.ResFromRow(i, i + 1);
                for (j = i - 1; j >= 0; j--)
                    MyMatr.InsertElem(j, i, MyMatr.GetElem(j, i) * result[i]);
            }
            return result;
        }
    };
}
