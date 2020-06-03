using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    class Legendre
    {
        double[] coef_for_polynom;
        public double in_function(double x)
        {
            return 0.001 * Math.Exp(x + 3) * Math.Sin(x) - Math.Sin(x / 3);
        }
        public double ResultPolynom(double x)
        {
            int i;
            double result=0;
            for (i = 0; i < coef_for_polynom.Length; i++)
                result += Math.Pow(x, i) * coef_for_polynom[i];
            return result;
        }
        public void FillCoef(double[] coef)
        {
            int i = 0;
            Console.WriteLine("Coefficients for polynomial");
            foreach (double elem in coef)
                Console.WriteLine("c{0} = {1:f18}", i++, elem);
            coef_for_polynom = (double[])coef.Clone();
        }
        public double GetPolynomial(int n, double x)
        {
            if (n < 2)
                if (n == 1)
                    return x;
                else
                    return 1;
            else
                return (2 * n + 1) / (n + 1) * x * GetPolynomial((n-1), x) - n / (n + 1) * GetPolynomial((n - 2), x); 
        }
        public void OutFile (int a, int b)
        {
            int i;
            int n = 100;
            double step = (double) (b - a) / n;
            double x;
            StreamWriter table_values = File.CreateText("graphic.csv");
            for (i = 0; i < n; i++)
            {
                x = a + step * i;
                table_values.WriteLine(x + ";" + ResultPolynom(x));
            }
            table_values.Close();
        }
    }
}
