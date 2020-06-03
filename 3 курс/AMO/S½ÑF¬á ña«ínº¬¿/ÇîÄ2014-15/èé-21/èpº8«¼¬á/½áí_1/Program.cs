using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMO_Lab1
{
    class ShSeries
    {
        private List<double> members; // члены ряда
        private List<double> sums; // суммы рядов
        private int N;     
        private double x;

        //конструктор
        public ShSeries(double _x)
        {
            x = _x;
            N = 1;

            members = new List<double>(); 
            members.Add(x);  

            sums = new List<double>();
            sums.Add(x);
        }

        //оценивает и возвращает следующий элемент ряда      
        private double next()
        {
            members.Add(x * x * members.Last() / (2 * N * (2 * N + 1)));
            N++;
            sums.Add(sums.Last() + members.Last());
            return members.Last();
        }

        // возвращает n-тый элемент ряда
        public double getMember(int n)
        {
            if (n < 1) 
                Console.WriteLine("Invalid N to get member of series."); 
            if (n <= N) 
                return members[n - 1];
            else
            {
                next();
                return this.getMember(n);
            }
        }

        // возвращает сумму первых n елементов ряда
        public double getSumOf(int n)
        {
            if (n < 1) 
                Console.WriteLine("Invalid N to sum of members of series.");
            if (n <= N) 
                return sums[n - 1];
            else
            {
                next();
                return this.getSumOf(n);
            }
        }

        //возвращает остаточний член для n
        public double getReminderOf(int n) 
        { 
            return getMember(n) / 3; 
        }

        public void setX(double _x) 
        { 
            x = _x;
            reevaluate(); 
        }

        public double getX() 
        { 
            return x; 
        }

        private void reevaluate()
        {
            int oldN = N;
            N = 1;

            members = new List<double>();  
            members.Add(x);

            sums = new List<double>();
            sums.Add(x);

            for (int n = 1; n < oldN; n++) 
                next();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // первый шаг
            Console.WriteLine("\nFIRST STEP");
            Console.WriteLine("\nEPS\tN\tAbsolute error\t\tReminder term");
  
            double a = -0.8;
            double b = 6.9;
            double x = (b + a)/2;
            double shx = (Math.Exp(x) - Math.Exp(-x)) / 2; //реальное значение ф-ции	
            double eps;

            ShSeries s = new ShSeries(x);

            int n = 1; int nextN = 1;
            for (eps = 1e-2; eps >= 1e-14; eps *= 1e-3)
            {                
                Console.Write(eps.ToString() + "\t");

                while (Math.Abs(s.getMember(n)) >= eps) 
                    n++;
                n--;
                Console.Write(n.ToString() + "\t");
                Console.Write(String.Format("{0:F15}", Math.Abs(shx - s.getSumOf(n))) + "\t");
                Console.WriteLine(String.Format("{0:F20}", s.getReminderOf(n)));
                if (eps == 1e-8) 
                    nextN = n;
            }

            // следующий шаг
            Console.WriteLine("\nSECOND STEP\n");
            Console.WriteLine("N = " + nextN.ToString());
            Console.WriteLine("\nXi\tAbsolute error\t\tReminder term");
            n = nextN; // now n is fixed
            double h = (b - a) / 10;

            List<string> output = new List<string>();

            for (int i = 0; i <= 10; i++)
            {
               s.setX(a + h * i);
               Console.Write(s.getX().ToString() + "\t");
               Console.Write(String.Format("{0:F15}", Math.Abs(shx - s.getSumOf(n))) + "\t");
               Console.WriteLine(String.Format("{0:F20}", s.getReminderOf(n)));
               output.Add(s.getX().ToString() + ";" + Math.Abs(s.getSumOf(n) - shx).ToString());
            }

            System.IO.File.WriteAllLines("output.csv", output.ToArray());
        }
    }
}
