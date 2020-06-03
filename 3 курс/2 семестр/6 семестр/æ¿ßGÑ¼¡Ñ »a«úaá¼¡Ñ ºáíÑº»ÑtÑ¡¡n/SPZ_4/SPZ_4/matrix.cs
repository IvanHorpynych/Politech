using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPZ_4
{
    class matrix
    {

        static int size = 6;
        int[,] matr = new int[size, size];
        int[,] start_matr = new int[2, size];
        int subm=0, subn=0;
        
        int[] vect = { 1,0,1,0,1,0,
                       0,1,1,1,1,1,
                       1,0,1,0,1,0,
                       0,1,1,1,1,1,
                       1,0,1,0,1,0,
                       1,0,1,0,1,0};
        

        int[] vect2 = {
                         1,1,1,0,0,0,
                         1,1,1,1,0,1,
                         1,1,1,0,0,0,
                         1,1,1,1,1,0,
                         1,1,1,1,1,1,
                         1,1,1,1,1,1
                     };
         
        public matrix()
        {
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    //matr[i, j] = rnd.Next(0, 2);
                    matr[i, j] = vect[i*size + j];
                }

            for (int i = 0; i < size; i++)
            {
                start_matr[0, i] = i;
                start_matr[1, i] = i;
            }
        }

        public void PrintMatr()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    Console.Write("{0} ", matr[i, j]);
                Console.Write("\n");
            }
        }

        int FndMaxCol( int start_pos )
        {
            int max_col = -1, max_num = 0;
            int num = 0;
            for (int j = start_pos; j < size; j++)
            {
                num = 0;
                for (int i = 0; i < size; i++)
                    if (matr[i, j] > 0)
                        num++;
                if (num > max_num)
                {
                    max_col = j;
                    max_num = num;
                }
            }
            return max_col;
        }

        int FndMaxRow(int start_pos)
        {
            int max_row = -1, max_num = 0;
            int num = 0;
            for (int i = start_pos; i < size; i++)
            {
                num = 0;
                for (int j = 0; j < size; j++)
                    if (matr[i, j] == 0 )
                        num++;
                if (num > max_num)
                {
                    max_row = i;
                    max_num = num;
                }
            }
            return max_row;
        }

        void ExchangeCol(int m, int n)
        {
            int pos = start_matr[1, m];
            start_matr[1, m] = start_matr[1, n];
            start_matr[1, n] = pos;

            for (int i = 0; i < size; i++)
            {
                int buf = matr[i, n];
                matr[i, n] = matr[i, m];
                matr[i, m] = buf;
            }
        }

        void ExchangeRow(int m, int n)
        {
            int pos = start_matr[0, m];
            start_matr[0, m] = start_matr[0, n];
            start_matr[0, n] = pos;

            for (int i = 0; i < size; i++)
            {
                int buf = matr[n, i];
                matr[n, i] = matr[m, i];
                matr[m, i] = buf;
            }

           
        }

        public void Transform()
        {
            for (int i = 0; i < size; i++)
            {
                int col = FndMaxCol(i);
                if (col < 0  || i == col )
                    continue;
                ExchangeCol(i, col);

            }

            for (int i = 0; i < size; i++)
            {
                int row = FndMaxRow(i);
                if (row < 0 || i == row )
                    continue;
                ExchangeRow(i, row);
            }
        }

        bool CheckZeroMatr(int m,int n)
        {
            for ( int i = 0; i<m; i++)
            for (int j = n; j < size; j++)
                if (matr[i, j] == 1 )
                    return false;
            return true;
        }

        public void FndSubMatrix()
        {
            int m = size, n = 0;
 
            for ( int i = size-2; i>0; i-- )
                for (int j = i-1; j < size; j++)
                {
                  
                    if (CheckZeroMatr(i, j))
                    {
                        if ((i * (size-j)) > (subm * (size-subn)))
                        {
                            subm = i;
                            subn = j;
                        }
                    }
                }
        }

        public void FndSolution()
        {
            Console.WriteLine("SubMatrix starts from {0} col and {1} row", subn, subm);
          
            for (int i = 0; i < subm; i++)
                for (int j = subn; j < size; j++)
                {
                    if ( i != j )
                        matr[j,i] = 0;
                }

           PrintMatr();
        }

        public void ShowMatrix()
        {
            matrix buf = new matrix();
            for (int i = 0; i < size; i++)
            {
                int ii = start_matr[0, i];
                for (int j = 0; j < size; j++)
                {
                    int jj = start_matr[1, j];
                    buf.matr[ii, jj] = matr[i, j];
                }
            }
            buf.PrintMatr();
        }
    }
}
