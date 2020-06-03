using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba3
{
    class Matrix
    {
        double[,] MyAr;
        public Matrix(double[,] ar) { MyAr = (double[,])ar.Clone();  }
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
            for (i=0; i < from.Length; i++)
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
        public int Length (int dimension)
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
        public double mNorma()
        {
            int i,j;
            double[] tmp = new double[MyAr.GetLength(0)];
            for (i = 0; i < MyAr.GetLength(0); i++) 
                for (j = 0; j < MyAr.GetLength(0); j++) 
                    tmp[i] += Math.Abs (MyAr[i,j]);
            Array.Sort(tmp);
            return tmp[MyAr.GetLength(0) - 1];
        }

    };
    class Gaus
    {
        Matrix MyMatr;
        public Gaus(double[,] ar) { MyMatr = new Matrix(ar); }
        public double[] Solution()
        {
            int i,j;
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
                result[i] = MyMatr.ResFromRow(i,i+1);
                for (j = i - 1; j >=0; j--) 
                    MyMatr.InsertElem(j, i, MyMatr.GetElem(j,i)*result[i]);
            }
            return result;      
        }
    };
    class Iter
    {
        double mNorma(double[] matr)
        {
            Array.Sort(matr);
            return matr[matr.GetLength(0) - 1];
        }
        Matrix MyMatr;
        double EPS;
        public Iter(double[,] ar, double eps) { MyMatr = new Matrix(ar); EPS = eps; }
        public double[] Sol () 
        {
            int i, j, length0 = MyMatr.Length(0);
            double q, cmp;
            double[] tmp = new double[length0];
            double[] result = new double[length0];
            for (i = 0; i < length0; i++)
            {
                tmp = MyMatr.DivRow(i, -MyMatr.GetElem(i, i));
                tmp[i] = 0;
                tmp[length0] = -tmp[length0];
                MyMatr.FillRow(i, tmp);
                result[i] = MyMatr.GetElem(i, length0); //заполнение начального условия 
            }
            q = MyMatr.mNorma();
            cmp = (1 - q) / q * EPS;
            do
            {
                tmp = (double[])result.Clone();
                Array.Clear(result, 0, result.Length);
                for (i = 0; i < length0; i++) //для прохода по строкам mymatr
                {
                    for (j = 0; j < length0; j++)  //для прохода по столбикам mymatr, кроме свободных членов
                        result[i] += MyMatr.GetElem(i, j) * tmp[j];
                    result[i] += MyMatr.GetElem(i, j);
                }
            } while (mNorma(MyMatr.SubRow(result, tmp)) > cmp);
            return result;
        }
    };
    class Program
    {
        static void Main(string[] args)
        {
         //   double[,] ar_in = {{6,3,11,17,155}, {0,16,5,10,161}, {11,5,25,8,147}, {5,5,11,15,151}};
            double[,] ar_in = { {10,-7,-91,-12 }, {41,-92,12,60 }, {-21,-96,-95,49 } };
            double[,] ar_iter = { {72,-3,16,-1 }, {41,-92,12,60 }, {10,-7,-91,-12} };
         //   double[,] ar_iter = { {} , { 0, 16, 5 ,10, 161 }, { 11, 5 ,25, 8 ,147 }, {} };
            Gaus Sol = new Gaus (ar_in);
            double[] r = Sol.Solution();
            Console.WriteLine("Gaussian elimination");
            for (int i=0; i < r.Length; i++) 
                Console.WriteLine("x{0} = {1:f18}", i, r[i]);
      
            Iter Solution = new Iter(ar_iter, 1e-4);
            r = Solution.Sol();
            Console.WriteLine("Direct iteration");
            for (int i = 0; i < r.Length; i++)
                Console.WriteLine("x{0} = {1:f18}", i, r[i]);
        }
    }
}
