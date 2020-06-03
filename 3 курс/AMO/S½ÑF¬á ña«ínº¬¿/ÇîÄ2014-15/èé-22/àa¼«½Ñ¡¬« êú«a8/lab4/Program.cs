using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMO4
{
    class Lab_4
    {
        public Lab_4()
            {
                
            }

        public double Function(double x)
        {
            return (Math.Sqrt(2 * x + 1) / 2);
        }

        public double Antidirevative(double x)
        {
            return (((2 * x + 1) / 6) * Math.Sqrt(2 * x + 1));
        }

        public double Newton_Leibnitz(double a, double b)
        {
            return (Antidirevative(b) - Antidirevative(a));
        }
        
        public double Composite_Trapezium_Rule(double a, double b, double h)
        {
            double x0 = a;
            double res = Function(x0) / 2;
           
            int n = (int)Math.Ceiling((b - a) / h);
            if (n % 2 != 0) n++;

            h = (b - a) / n;
            x0 += h; 

            for (int i = 1; i < n ; i++)
            {
                res += Function(x0);
                x0 += h;
            }
           res += Function(x0) / 2;

            return res * h;
        }

        public double Get_H(double a, double b, double eps)
        {
            return Math.Sqrt(((b - a) * eps) / ( 12 * 0.5 )) ;
        }

        public double[] Get_Print_Tab_1()
        {
            double[] eps_vec = { 0.1, 0.01, 0.001, 0.0001, 0.00001 };
            double[] delta_vec = new double[5];
            System.Console.WriteLine("Composite trapezium rule:");
            System.Console.WriteLine("EPS           STEP            ACC.RES.        TRAP.RES.        DELTA");
            System.Console.WriteLine("______________________________________________________________________");
            double acc_res = Newton_Leibnitz(0, 15);
            for (int i = 0; i < eps_vec.Length; i++ )
            {   
                double trap_res = Composite_Trapezium_Rule(0, 15, Get_H(0, 15, eps_vec[i]));
                delta_vec[i] = Math.Abs(acc_res - trap_res);
                System.Console.WriteLine("{0:f5}     {1:f7}       {2:f7}       {3:f7}       {4:f7}", 
                    eps_vec[i], Get_H(0, 15, eps_vec[i]), acc_res, trap_res, delta_vec[i]);
            }
            return delta_vec;
        }

        public double Refined_Calculation(double a, double b, double eps, out int n)
        {
            n = (int)Math.Ceiling(1 / Math.Sqrt(eps));
            if (n % 2 != 0) n++;

            double h = (b - a) / n;
            double In = Composite_Trapezium_Rule(a, b, h);
            n = n * 2;
            h = (b - a) / n;
            double I2n = Composite_Trapezium_Rule(a, b, h);
            double cof = (1.0 / (2 * 2 - 1)); 
            double Rn = cof * Math.Abs(In - I2n);
            n = n * 2;
            h = (b - a) / n;

            while (Rn > eps){
                In = I2n;
                I2n = Composite_Trapezium_Rule(a, b, h);
                n = n * 2;
                h = (b - a) / n;
                Rn = cof * Math.Abs(In - I2n);
            }

            n = n / 2;
            return I2n;
        }

        public void Get_Print_Tab_2(double[] deltas)
        {
            
            System.Console.WriteLine("Refined Calculations:");
            System.Console.WriteLine("DELTA                  STEP            RES           GOTTEN DELTA");
            System.Console.WriteLine("___________________________________________________________________");
            for (int i = 0; i < deltas.Length; i++)
            {
                int n;
                var integral = Refined_Calculation(0, 15, deltas[i], out n);
                System.Console.WriteLine("{0:f6}            {1:f8}         {2:f7}          {3:f7}",
                    deltas[i], 15.0/n, integral,
                    Newton_Leibnitz(0, 15) - integral);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Lab_4 lab = new Lab_4();
            double[] deltas = lab.Get_Print_Tab_1();
            System.Console.WriteLine();
            lab.Get_Print_Tab_2(deltas);
            Console.ReadLine();
        }
    }
}
