using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab5
{
    static class Program
    {
        static void Main()
        {
            int n = 5;
            int i, j, m;
            int a = 0;
            int b = 12;
            double EPS = 0.01;
            Legendre eq = new Legendre();
            Trapezium mytrapez = new Trapezium(a,b,0.001);
            Gaus solution_for_matrix;
            double[,] mymatr;
            double delta;
            do
            {
                n += 2;
                m = n + 1;
                mymatr = new double[n, m];
                for (i = 0; i < n; i++)
                {
                    for (j = 0; j < n; j++) //заполнение матрицы
                    {
                        Func<double,double> func_to_calc = x => eq.GetPolynomial(i,x)*eq.GetPolynomial(j,x);// все коэф
                        mymatr[i, j] = mytrapez.Solve(func_to_calc);
                    }
                    Func<double, double> func_to_calcul = x => eq.GetPolynomial(i,x)*eq.in_function(x);//все члены
                    mymatr[i, j] = mytrapez.Solve(func_to_calcul);
                }
                solution_for_matrix = new Gaus (mymatr);
                eq.FillCoef(solution_for_matrix.Solution());// закидываем в метод лежандра все полиномиальные коеф.
                Func<double, double> func = x => Math.Pow(eq.in_function(x) - eq.ResultPolynom(x), 2);//формируем подинтгр выражения 
                delta = Math.Sqrt(mytrapez.Solve(func)/(b-a));//закидывается в решение интеграла
            } while (delta > EPS);
            eq.OutFile(a, b);
            Console.WriteLine("Power of polynomial is {0}", n);
        }
    }
}
