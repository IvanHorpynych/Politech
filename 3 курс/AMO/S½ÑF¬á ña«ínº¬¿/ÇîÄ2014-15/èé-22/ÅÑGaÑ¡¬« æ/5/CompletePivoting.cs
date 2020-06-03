using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    class ComletePivoting
    {
        public int RowCount; //amount of rows
        public int ColumCount; //amount of columns
        public double[,] Matrix; 
        public double[] RightPart; //Free members
        public double[] Answer;
        //private const int n = 4;
        public ComletePivoting(double[,] matrix, double[] rightPart, int n)
        {
            Matrix = new double[n, n];
            RightPart = new double[n];
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


        private void SortRows(int SortIndex)  //row with main element goes to upper position
        {

            double MaxElement = Matrix[SortIndex, SortIndex];
            int MaxElementIndex = SortIndex;
            for (int i = SortIndex + 1; i < RowCount; i++)
            {
                if (Matrix[i, SortIndex] > MaxElement)
                {
                    MaxElement = Matrix[i, SortIndex];
                    MaxElementIndex = i;
                }
            }

            //max element goes to the first place
            if (MaxElementIndex > SortIndex)//if it is not first element
            {
                double Temp;

                Temp = RightPart[MaxElementIndex];
                RightPart[MaxElementIndex] = RightPart[SortIndex];
                RightPart[SortIndex] = Temp;

                for (int i = 0; i < ColumCount; i++)
                {
                    Temp = Matrix[MaxElementIndex, i];
                    Matrix[MaxElementIndex, i] = Matrix[SortIndex, i];
                    Matrix[SortIndex, i] = Temp;
                }
            }
        }

        public int SolveMatrix()
        {
            if (RowCount != ColumCount)
                return 1;

            for (int i = 0; i < RowCount - 1; i++)
            {
                SortRows(i);
                for (int j = i + 1; j < RowCount; j++)
                {
                    if (Matrix[i, i] != 0) //if main elemenet !=0 then do calculations
                    {
                        double MultElement = Matrix[j, i] / Matrix[i, i];
                        for (int k = i; k < ColumCount; k++)
                            Matrix[j, k] -= Matrix[i, k] * MultElement;
                        RightPart[j] -= RightPart[i] * MultElement;
                    }
                }
            }

            //find roots
            for (int i = (int)(RowCount - 1); i >= 0; i--)
            {
                Answer[i] = RightPart[i];

                for (int j = (int)(RowCount - 1); j > i; j--)
                    Answer[i] -= Matrix[i, j] * Answer[j];

                if (Matrix[i, i] == 0)
                    if (RightPart[i] == 0)
                        return 2; //a lot of roots
                    else
                        return 1; //no roots

                Answer[i] /= Matrix[i, i];

            }
            return 0;
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
                S += "\tx" + i.ToString() + " = " + Answer[i].ToString("F03");

            }
            return S;
        }
    }
}
