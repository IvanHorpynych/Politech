using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amo_Lab3
{
    class GausSeidel
    {
        public int RowCount ;
        public int ColumCount ;
        public double [,] Matrix; 
        public double[] RightPart;
        public double[] Answer;
        //private const int n = 4;
        public GausSeidel(double[,] matrix , double[] rightPart,  int n)
        {
            Matrix = new double[n, n] ; 
            RightPart = new double[n] ;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Matrix[i, j] = matrix[i, j];
                }
                RightPart[i] = rightPart[i]; 
            }
            Answer = new double[n];
            RowCount = n;
            ColumCount = n;
        }

        public void Solve()
        {
            double eps = 1E-14;
            for (int i = 0; i < ColumCount; i++)
            {
                double temp = Matrix[3, i];
                Matrix[3, i] = Matrix[0, i];
                Matrix[0, i] = temp - Matrix[0, i];
            }
            double temp1 = RightPart[3];
            RightPart[3] = RightPart[0];
            RightPart[0] = temp1 - RightPart[3];
            

            double d = 0;
            do
            {
                d = 0;
                for (int i = 0; i < RowCount; i++)
                {
                    double s = 0;
                    for (int j = 0; j < ColumCount; j++)
                        if (i != j)
                            s += Matrix[i, j] * Answer[j];
                    double temp = Answer[i];
                    Answer[i] = (RightPart[i] - s) / Matrix[i, i];
                    d = Math.Abs(temp - Answer[i]);
                }

            } while (eps < d);

        }

        public override String ToString()
        {
            String S = "";
            for (int i = 0; i < RowCount; i++)
            {
                S += "\r\n";
                for (int j = 0; j < ColumCount; j++)
                {
                    S += Matrix[i, j].ToString("F02") + "\t";
                }
                S += "\t" + RightPart[i].ToString("F02") + "\t";
                S += "\tx"+i.ToString()+" = " + Answer[i].ToString("F03");

            }
            return S;
        }

    }
}
