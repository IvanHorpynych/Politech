using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMOLAB3
{
    class Program
    {

        class Lab3
        {
            private double[,] Matrix;
            private double[] FreeMembersVector;
            private double[] Roots;
            private const int n=4;
            public Lab3()
            {
                Matrix = new double[n,n] {{2, 11, 13, 15},{12, 38, 18, 17},{20, 1, 27, 5},{15, 2, 15, 5}};
                FreeMembersVector = new double[n] { 33, 114, 3, 6 };
                Roots = new double[n] { 0, 0, 0, 0 };
            }


            public void printRoots()
            {
                Console.WriteLine("Roots: ");
                for (int i = 0; i < n; i++)
                    Console.WriteLine("{0:0.####}",Roots[i]);
            }
            public void Gauss_Seigel()
            {
                double eps = 1E-14;
                Console.WriteLine("Gauss_Seigel method: ");
                Console.WriteLine();
                PrintMatrix();
                Console.WriteLine();
                Console.WriteLine("New matrix:");
                for (int i = 0; i < n; i++)
                {
                    double temp = Matrix[3, i];
                    Matrix[3, i] = Matrix[0, i];
                    Matrix[0, i] = temp - Matrix[0,i];
                }
                double temp1 = FreeMembersVector[3];
                FreeMembersVector[3] = FreeMembersVector[0];
                FreeMembersVector[0] = temp1 - FreeMembersVector[3];
                PrintMatrix();

                double d =0;
                double Sum = 0, max = 0;


                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++){
                        Sum += Math.Abs(Matrix[i, j]); 
                    }
                    if (Sum > max)
                        max = Sum;
                }

                eps *= Math.Abs((1 - max) / max);
               
               do  {
                        d = 0;
                        for (int i = 0; i < n; i++)
                        {
                            double s = 0;
                            for (int j = 0; j < n; j++)
                                if (i != j)
                                    s += Matrix[i, j] * Roots[j];
                            double temp = Roots[i];
                            Roots[i] = (FreeMembersVector[i] - s) / Matrix[i, i];
                            d = Math.Abs(temp - Roots[i]);
                        }

                  } while (eps < d);

                printRoots();

            }
            public void Gauss_Jordan(){

                Console.WriteLine("Gauss-Jordan method:");
                Console.WriteLine();
                for (int i = 0; i < n; i++)
                {
                    double temp = Matrix[i,i];
                    FreeMembersVector[i] /= temp;
                  
                    for (int j = 0; j < n; j++)
                    {
                        Matrix[i, j] /= temp;   // делим рядок на A[i,i]                      
                    }

                    for (int k = 0; k < n; k++)
                    {
                        if (k == i)
                            continue;
                        double temp1 = Matrix[k, i];
                        FreeMembersVector[k] -= FreeMembersVector[i] * temp1;//Matrix[k,i];
                        for (int z = 0; z < n; z++)
                        {
                            Matrix[k, z] -= Matrix[i, z] * temp1;//Matrix[ k,i] ;
                        }
                    }


                    PrintMatrix();
                }


            }

            public void PrintMatrix(){


                Console.WriteLine("Matrix and free members vector: ");
                Console.WriteLine();

              
                for (int i = 0; i < n; i++)
                {
                    
                    for  (int j = 0; j < n; j++){
                        Console.Write(String.Format("{0:0.####}", Matrix[i, j] ) +"\t");
                    }
                    Console.Write("|" + String.Format("{0:0.00}",FreeMembersVector[i]) + "\t");
                    Console.WriteLine();
                }

                Console.WriteLine();
            } 
 
        }
        static void Main(string[] args)
        {
            Lab3 obj = new Lab3();
            obj.Gauss_Seigel();
            Console.WriteLine();
            Lab3 obj1 = new Lab3();
            obj.Gauss_Jordan();
            Console.Read();
            
        }
    }
}
