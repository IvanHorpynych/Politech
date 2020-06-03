using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//y = sin(x)
namespace Amo_lab1
{
    class Program
    {
       
     
        static double sinX(double exp, double xx , ref int n ,ref double R) 
        {

            double x, s = 0.0d, t;
            x = xx;
            while (true)
            {
                if (x - 2 * Math.PI > 0)
                {
                    x -= 2 * Math.PI;
                }
                else break;
            }
            s = x;
            //x = xx;
            t = x;

            int i = 1;
            while (true)
            {

                t = (-t * x * x) / ((2 * i) * (2 * i + 1));
                if (Math.Abs(t) < exp)
                {
                    n = i;
                    R = Math.Abs(t);
                    break;
                }
                s = s + t;
                i++;
            }
            return s;
        }

        static void Main(string[] args)
        {
            double a = 0.033E+1;
            double b = 0.74E+1;
            int nn = 0;
            double R = 0;
            double absoluteErr = 0;
            double s = 0;
            double si = 0;
            for (double i = 0.1E-1; i >= 0.1E-13; i *= 0.1E-2)
            {
                s = sinX(i, (a + b) / 2, ref nn, ref R);
                si = Math.Sin((a + b) / 2);
                absoluteErr = Math.Abs(s - si);
                // Console.WriteLine(i.ToString(), nn.ToString(), absoluteErr.ToString(), R.ToString());
                Console.WriteLine("Eps = {0} , n = {1}, absolute error = {2}, Remainder term = {3}", i, nn, absoluteErr, R);
            }
           
            Console.WriteLine();
            double h = (b - a) / 10;
            for (int j = 0 ; j <= 10; j++)
            {
                s = sinX( 0.1E-7 , a + h*j, ref nn, ref R);
                si = Math.Sin(a + h * j);
                absoluteErr = Math.Abs(s - si);
                Console.WriteLine("Xi = {0}, absolute error = {1} , Remainder term = {2}", a + h * j, absoluteErr, R);
            }
            Console.Read();
        }
    }
}



