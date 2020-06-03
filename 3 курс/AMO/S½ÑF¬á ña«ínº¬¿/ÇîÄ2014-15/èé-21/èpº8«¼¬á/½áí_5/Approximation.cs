using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AMO_Lab5
{
    class Approximation
    {
        public static int n; //Розмір матриці
        private double[,] Matrix;
        private double[] Free_Member; //вільний член
        private double[] Polynomial; //многочлени Чебишева 
        private double a, b; //межі інтегрування
        private double eps; //похибка

        public Approximation()
        {
            Matrix = new double[n, n];
            Matrix.Initialize();
            Free_Member = new double[n];
            Free_Member.Initialize();
            Polynomial = new double[n];
            Polynomial.Initialize();
            eps = 1E-5;
            a = 3;
            b = 10.5;
        }

        private double Function(double x)
        {
            return 0.1 * x * x * Math.Log10(Math.Abs(Math.Sin(x / 1.7)) / 2);
        }

        private double Error_Function(double x)
        {
            Chebyshev_Polynomial(x);
            double Sum = Free_Member[0] * Polynomial[0];
            for (int i = 1; i < n; i++)
                Sum += Free_Member[i] * Polynomial[i];
            return Sum;
        }

        private void Chebyshev_Polynomial(double x)
        {
            Polynomial[0] = 1;
            Polynomial[1] = x;  

            for (int i = 2; i < n; i++)
                Polynomial[i] = 2 * x * Polynomial[i - 1] - Polynomial[i - 2];
        }

        private double Error_Intergrand(double x) 
        {
            return Math.Pow((Function(x) - Error_Function(x)), 2);
        }

        private double Integrand(double x, int i, int j)  
        {
            Chebyshev_Polynomial(x);

            if (j > n)
                return Polynomial[i] * Function(x);

            return Polynomial[i] * Polynomial[j];
        }

        private double Simpson(double n, double h, double x, int i, int j, int flag)
        {
            double Sigma_1 = 0.0, Sigma_2 = 0.0;

            if (flag == 0)
            {
                for (int k = 1; k < n; k++)
                {
                    x += h;
                    
                    if (k % 2 != 0)
                        Sigma_1 += Integrand(x, i, j);
                    if (k % 2 == 0)
                        Sigma_2 += Integrand(x, i, j);
                }
                return ((h / 3) * (Integrand(a, i, j) + Integrand(b, i, j) + 4 * Sigma_1 + 2 * Sigma_2));
            }
            else
            {
                for (int k = 1; k < n; k++)
                {
                    x += h;
                    if (k % 2 != 0)
                        Sigma_1 += Error_Intergrand(x);
                    if (k % 2 == 0)
                        Sigma_2 += Error_Intergrand(x);
                }
                return ((h / 3) * (Error_Intergrand(a) + Error_Intergrand(b) + 4 * Sigma_1 + 2 * Sigma_2));
            }
        }

        private double Simpson_Method(int i, int j)
        {
            int n = (int)Math.Pow(eps, -0.25);
            if (n % 2 != 0)
                n++;
            double h = (b - a) / n;
            int flag = 0;

            double In = Simpson(n, h, a, i, j, flag);
            h = (b - a) / (2 * n);
            double I2n = Simpson(2 * n, h, a, i, j, flag);

            while (15*eps < Math.Abs((In - I2n) / I2n))
            {
                n *= 2;
                h = (b - a) / n;
                In = Simpson(n, h, a, i, j, flag);
                h = (b - a) / (2 * n);
                I2n = Simpson(2 * n, h, a, i, j, flag);
            }

            return (I2n);
        }

        public void Equations() 
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    Matrix[i, j] = Simpson_Method(i, j);

            for (int i = 0; i < n; i++)
                Free_Member[i] = Simpson_Method(i, n + 1);

            Print_Equations(Matrix);
        }

        public void Print_Equations(double[,] Matrix)
        {
            string Name = "matrix_" + n.ToString() + ".txt";

            using (StreamWriter writer = File.CreateText(Name))
            {
                for (int i = 0; i < n; ++i)
                {
                    for (int j = 0; j < n; j++)
                    {
                        writer.Write("{0:0.000000}", Matrix[i, j]);
                        writer.Write("\t");
                    }
                    writer.Write(Free_Member[i]);
                    writer.WriteLine();
                }
            }
        }

        public void Monomial()
        {
            Console.Write("\n\nMonomials:");
            for (int i = 0; i < n; i++)
                Console.Write("\n\n" + "a" + i + "=  " + Free_Member[i]);
        }

        private void Copy_Matrix(double[,] Matrix, double[,] MatrixCopy) 
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    MatrixCopy[i, j] = Matrix[i, j];
        }

        public void Single_division()
        {
            double[,] Matrix_1 = new double[n, n];
            Copy_Matrix(Matrix, Matrix_1);

            for (int i = 0; i < n - 1; i++)    
            {
                double tmp = Matrix_1[i, i];
                Free_Member[i] = Free_Member[i] / tmp;

                for (int j = i; j < n; j++) 
                    Matrix_1[i, j] = Matrix_1[i, j] / tmp;
                for (int k = i; k < n; k++)
                {
                    if (k == i)
                        continue;
                    double tmp1 = Matrix_1[k, i];
                    Free_Member[k] -= Free_Member[i] * tmp1;
                    for (int l = 0; l < n; l++)
                        Matrix_1[k, l] -= Matrix_1[i, l] * tmp1;
                }
                if (i == (n - 2))
                {
                    Free_Member[i + 1] = Free_Member[i + 1] / Matrix_1[i + 1, i + 1];
                    Matrix_1[i + 1, i + 1] = 1;
                }

            }
            for (int i = n - 2; i >= 0; i--)  
            {
                int j = 0;
                for (j = n - 1; j > i; j--)
                    Free_Member[i] -= Matrix_1[i, j] * Free_Member[j];
            }
        }

        public void Error_Fix()
        {
            int n = (int)Math.Pow(eps, -0.25);
            if (n % 2 != 0)
                n++;
            double h = (b - a) / n;
            int flag = 1;

            double In = Simpson(n, h, a, flag, flag, flag);
            h = (b - a) / (2 * n);
            double I2n = Simpson(2 * n, h, a, flag, flag, flag);

            while (15*eps < Math.Abs((In - I2n) / I2n))
            {
                n *= 2;
                h = (b - a) / n;
                In = Simpson(n, h, a, flag, flag, flag);
                h = (b - a) / (2 * n);
                I2n = Simpson(2 * n, h, a, flag, flag, flag);
            }
            Console.WriteLine("Error = " + Math.Sqrt((I2n) / (b - a)));
        }

        public void Output_xy()
        {
            double y = 0.0, x = a;
            string Name = "output_";
            Name += n.ToString() + ".csv";
            using (StreamWriter writer = File.CreateText(Name))
            {
                while (x <= b)
                {
                    y = Error_Function(x);
                    writer.WriteLine(x + " " + y);
                    x += 0.1;
                }
            }
        }
    }
}


