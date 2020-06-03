using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AMO_Lab6
{
    class Cubic_Spline
    {
        private double a, b;
        public static int n;

        Spline_set[] spline; //сплайн

        //структура для опису сплайна
        private struct Spline_set
        {
            public double a, b, c, d, x;
        }

        public Cubic_Spline()
        {
            a = 3.0;
            b = 10.5;
        }
        private double Function(double x)
        {
            return 0.1 * x * x * Math.Log10(Math.Abs(Math.Sin(x / 1.7)) / 2); ;
        }

        public void Build_Spline()
        {
            double h = b / n;
            double x = a;
            spline = new Spline_set[n];
            spline[0].x = a;
            spline[0].a = Function(a);
            for (int i = 1; i < n; ++i)
            {
                x += h;
                spline[i].x = x;
                spline[i].a = Function(x);
            }
            spline[0].c = spline[n - 1].c = 0.0;
            x = a;
            double[] alpha = new double[n - 1];
            double[] beta = new double[n - 1];
            alpha[0] = beta[0] = 0.0;
            for (int i = 1; i < n - 1; ++i)
            {
                double hi = spline[i].x - spline[i - 1].x;
                double hi_1 = spline[i + 1].x - spline[i].x;
                double A = hi;
                double C = 2.0 * (hi + hi_1);
                double B = hi_1;
                double F = 6.0 * ((spline[i + 1].a - spline[i].a) / hi_1 - (spline[i].a - spline[i - 1].a) / hi);
                alpha[i] = -B / (A * alpha[i - 1] + C);
                beta[i] = (F - A * beta[i - 1]) / (A * alpha[i - 1] + C);
            }

            //зворотні хід метода прогону
            for (int i = n - 2; i > 0; --i)
                spline[i].c = alpha[i] * spline[i + 1].c + beta[i];

            //за коефіцієнтами c[i] знаходимо b[i] и d[i]
            for (int i = n - 1; i > 0; --i)
            {
                double hi = spline[i].x - spline[i - 1].x;
                spline[i].d = (spline[i].c - spline[i - 1].c) / hi;
                spline[i].b = hi * (2.0 * spline[i].c + spline[i - 1].c) / 6.0 + (spline[i].a - spline[i - 1].a) / hi;
            }
        }

        private double Interpolation(double x) //интерполяция
        {
            int n = spline.Length;
            Spline_set s;

            if (x <= spline[0].x)
                s = spline[0];
            else
                if (x >= spline[n - 1].x)
                    s = spline[n - 1];
                else //бінарний пошук потрібного елемента масива
                {
                    int i = 0;
                    int j = n - 1;
                    while (i + 1 < j)
                    {
                        int k = i + (j - i) / 2;
                        if (x <= spline[k].x)
                            j = k;
                        else
                            i = k;
                    }
                    s = spline[j];
                }
            double delta = x - s.x;
            return s.a + s.b * delta + s.c / 2 * delta * delta + s.d / 6 * delta * delta * delta;
        }

        public void Output_xy()
        {
            double y = 0.0, x = a;
            string Name = "output.csv";
            using (StreamWriter writer = File.CreateText(Name))
            {
                while (x <= b)
                {
                    y = Interpolation(x);
                    writer.WriteLine(x + " " + y);
                    x += 0.1;
                }
            }
        }
    }
}

