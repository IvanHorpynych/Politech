using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMO_Lab1
{
    class ShSeries
    {
        private List<double> members; // members of series
        private List<double> sums; // sums of series
        private int N; // current N of series
        private double x; // current point

        public ShSeries(double _x)
        {
            x = _x;
            N = 1;

            members = new List<double>();
            members.Add(x);
            
            sums = new List<double>();
            sums.Add(x);
        }

        // evaluates and returns next member of the series
        private double next()
        {
            members.Add(x * x * members.Last() / (2 * N * (2 * N + 1)));
            N++;
            sums.Add(sums.Last() + members.Last());
            return members.Last();
        }

        // returns n-th member of the series
        public double getMember(int n)
        {
            if (n < 1) Console.WriteLine("Invalid N to get member of series.");
            if (n <= N) return members[n - 1];
            else
            {
                next();
                return this.getMember(n);
            }
        }

        // returns the sum of n first members of the series
        public double getSumOf(int n)
        {
            if (n < 1) Console.WriteLine("Invalid N to sum of members of series.");
            if (n <= N) return sums[n - 1];
            else
            {
                next();
                return this.getSumOf(n);
            }
        }

        // returns reminder term for n
        public double getReminderOf(int n) { return getMember(n) / 3; }

        public void setX(double _x) 
        { 
            x = _x;
            reevaluate();
        }

        public double getX() { return x; }

        private void reevaluate()
        {
            int oldN = N;
            N = 1;

            members = new List<double>();
            members.Add(x);

            sums = new List<double>();
            sums.Add(x);

            for (int n = 1; n < oldN; n++) next();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            // first step
            Console.WriteLine("\nFIRST STEP");
            Console.WriteLine("\nEPS\tN\tAbsolute error\t\tReminder term");
            double a = -8.8;
            double b = 1.9;
            double x = (b + a) / 2;
            double shx = (Math.Exp(x) - Math.Exp(-x)) / 2; // "real" value of the function
            double eps;

            ShSeries s = new ShSeries(x);

            int n = 1; int nextN = 1;
            for (eps = 1e-2; eps >= 1e-14; eps *= 1e-3)
            {
                Console.Write(eps.ToString() + "\t");

                while (Math.Abs(s.getMember(n)) >= eps) n++;
                n--;
                Console.Write(n.ToString() + "\t");

                Console.Write(Math.Abs(shx - s.getSumOf(n)) + "\t");
                Console.WriteLine(s.getReminderOf(n));
                //Console.WriteLine(shx.ToString() + " == " + s.getSumOf(n));

                if (eps == 1e-8) nextN = n;
            }

            // second step
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
                Console.Write(Math.Abs(s.getSumOf(n) - shx).ToString() + "\t");
                Console.WriteLine(s.getReminderOf(n));

                output.Add(s.getX().ToString() + ";" + Math.Abs(s.getSumOf(n) - shx).ToString());
            }

            System.IO.File.WriteAllLines("output.csv", output.ToArray());
        }
    }
}
