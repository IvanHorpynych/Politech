using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMOLab1
{
    class EXPSeries
    {
        private List<double> members;
        private List<double> sums;
        private int N; 
        private double x;

        public EXPSeries(double _x)
        {
            x = _x;
            N = 1;

            members = new List<double>();
            members.Add(1);

            sums = new List<double>();
            sums.Add(1);
        }

        private double nextMember()
        {
            members.Add(x * members.Last() / N);
            N++;
            sums.Add(sums.Last() + members.Last());
            return members.Last();
        }

        public double getMember(int n)
        {
            if (n < 1) Console.WriteLine("Invalid N");
            if (n <= N) return members[n -1];
            else
            {
                nextMember();
                return this.getMember(n);
            }
        }

        public double getSum(int n)
        {
            if (n < 1) Console.WriteLine("Invalid N");
            if (n <= N) return sums[n -1];
            else
            {
                nextMember();
                return this.getSum(n);
            }
        }

        static long Factorial(long x)
        {
            return (x == 0) ? 1 : x * Factorial(x - 1);
        }

        public double reminderTerm(int n)
        {
            return (Math.Pow(Math.Abs(x), n)/Factorial(n)) * (Math.Abs(x)/n);
        }

        public void setX(double _x)
        {
            x = _x;
            reevaluate();
        }

        public double getX() { return x;}

        private void reevaluate()
        {
            int oldN = N;
            N = 1;

            members = new List<double>();
            members.Add(1);

            sums = new List<double>();
            sums.Add(1);

            for (int n = 1; n < oldN; n++) nextMember();
        }

    }
    class Program
    {
            
        static void Main(string[] args)
        {
            Console.WriteLine("\nFIRST STEP");
            Console.WriteLine("\nEPS\tN\tAbsolute error\t\tReminder term");
            double a = -3.9;
            double b = 14;
            double x = a;//(b + a) / 2;
            double  intx = Math.Truncate(x);
            double fractx = x - intx;
            double expx = Math.Exp(x); // "real" value of the function
            double eps;
            
            EXPSeries e = new EXPSeries(fractx);
            
            int n = 1; int nextN = 1;
            for (eps = 1e-2; eps >= 1e-14; eps *= 1e-3)
            {
                Console.Write(eps.ToString() + "\t");

                while (Math.Abs(e.getMember(n)) > eps) n++;
                n--;
                
                Console.Write(n.ToString() + "\t");
                Console.Write(Math.Abs(expx - (e.getSum(n) * Math.Exp(intx))) + "\t");
                Console.WriteLine(e.reminderTerm(--n));
                n++;
                if (eps == 1e-8) nextN = n;
            }

            Console.WriteLine("\nSECOND STEP\n");
            Console.WriteLine("N = " + nextN.ToString());
            Console.WriteLine("\nXi\tAbsolute error\t\tReminder term");
            n = nextN; 
            double h = (b - a) / 10;

            List<string> output = new List<string>();

            for (int i = 0; i <= 10; i++)
            {
                x = a + h * i;
                intx = Math.Truncate(x);
                fractx = x - intx;
                e.setX(fractx);

                Console.Write((e.getX() + intx).ToString() + "\t");
                double test = e.getSum(n) * Math.Exp(intx);
                Console.Write(Math.Abs(Math.Exp(x) - (e.getSum(n) * Math.Exp(intx))) + "\t");
                Console.WriteLine(e.reminderTerm(--n));
                n++;
               
            }
            

        }
    }
}
