using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMO3
{
    
    class Program
    {
        static double[] GaussJordanMethod(double[,] a, double[] b, int n)
        {
            Console.WriteLine();
            Console.WriteLine("Gauss Jordan Method");
            for (int i = 0; i < n; i++)
            {
                double temp = a[i, i];
                
                for (int j = i; j < n; j++)
                {
                    a[i, j] /= temp;
                }
                        
                b[i] /= temp;
               
                for (int k = 0; k<n; k++)
                {
                    if (k != i)
                    {
                        double temp1 = a[k, i];
			 b[k]-=b[i] * temp1;
                        for (int z = 0; z < n; z++)
                        {
                            a[k, z] -= a[i, z] * temp1;
                        }
                    }
                }
                PrintMatrix(a, b, n);        
            }
            return b;
        }

        static double[] ZeidelIteration(double[,] a,double[] b,int n,double eps)
        {
            Console.WriteLine("Zeidel Iteration Method:");
            double[] x = new double[n];

            for (int i = 0; i < n; i++)
                x[i] = 0;
            Console.WriteLine("Matrix:");
            PrintMatrix(a, b, n);
           
            for (int i = 0; i < n; i++)
            {
                double temp = a[3, i];
                a[3, i] = a[0, i];
                a[0, i] = temp;
            }
            double temp1 = b[3];
            b[3] = b[0];
            b[0] = temp1;
            Console.WriteLine("New Matrix:");
            PrintMatrix(a,b,n);
            double flag = 0;       
            do
            {
                flag = 0;
                for (int i = 0; i < n; i++)
                {
                    double var = 0;
                    for (int j = 0; j < i; j++)
                        var += (a[i, j] * x[j]);
                    for (int j = i+1; j < n; j++)
                        var += (a[i, j] * x[j]);
                   double t = x[i];
                    x[i] = (b[i] - var) / a[i, i];
                   flag = Math.Abs(t - x[i]);
                                    }
            } while (eps<flag);
        
            return x;
    }

        
        private static void PrintMatrix(double[,] a, double[] b, int n)
        {
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    if(a[i, j].ToString().Length>7)
                        Console.Write("{0:0.F15}", a[i, j].ToString().Remove(7) + new String(' ', 2));
                    else
                        Console.Write("{0:0.F15}", a[i, j] + new String(' ', Math.Abs(9 - a[i, j].ToString().Length)));
                if (b[i].ToString().Length > 7) Console.Write("    " + b[i].ToString().Remove(7));
                else  Console.Write("    "+b[i]);
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            double[,] Matrix = new double[4, 4];
            double[] b = new double[4]{148,87,157,169};
            Matrix = new double[4, 4] { { 192, 4, 8, 20 }, { 8, 257, 12, 6 }, { 16, 13, 460, 16 }, { 19, 10, 17, 177} };

            double[] x=new double[4];
            x = ZeidelIteration(Matrix, b, 4, 1E-17);

            Matrix = new double[4, 4] { { 192, 4, 8, 20 }, { 8, 257, 12, 6 }, { 16, 13, 460, 16 }, { 19, 10, 17, 117 } };
            b = new double[4] { 224,283, 505, 163 };


            Console.WriteLine("Roots: ");
            for (int i = 0; i < 4; i++)
                Console.Write("{0:0.F15}","x"+(i+1)+"="+x[i]+"   ");
           
            x = GaussJordanMethod(Matrix, b, 4);
            Console.WriteLine("Roots: ");
            for (int i = 0; i < 4; i++)
                Console.Write("{0:0.F15}", "x" + (i + 1) + "=" + x[i].ToString() + "   ");
              
            Console.ReadKey();
        }
    }
}
