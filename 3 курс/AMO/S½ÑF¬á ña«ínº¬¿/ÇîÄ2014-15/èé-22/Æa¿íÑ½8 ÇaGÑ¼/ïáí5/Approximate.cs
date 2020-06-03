using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lab5AMO
{
    class Approximate
    {
        private readonly Simpson _intCalc = new Simpson();
        private readonly Gauss _mSolver = new Gauss();
        public Func<double, double> InpFunction { get; set; }
        private int N { get; set; }
        public Approximate(Func<double, double> funcToApprox)
        {
            InpFunction = funcToApprox;
            N = 4;
        }
        
        private bool ErrorCalc(double eps, double a, double b, Func<double, double> f, Func<double, double> phi)
        {
            Func<double, double> underIntegralFunc = x =>
            {
                double fx = f(x);
                double phiMinusFx = fx - phi(x);
                return phiMinusFx * phiMinusFx;
            };

            var integralValue = _intCalc.RungeMethod(underIntegralFunc,1e-5,b,a);//(underIntegralFunc, 1e-5, b, a);
            double delta = Math.Sqrt(integralValue / (b - a));
            Console.WriteLine("delta = {0}", delta);
            return eps < delta;
        }

      

        public Func<double, double> Get_Phi(double[] coefficients)
        {
            var ChebyshevCooficients = new Func<double, double>[coefficients.Length];
            for (int i = 0; i < coefficients.Length; i++)
            {
                ChebyshevCooficients[i] = Chebyshev(i);
            }
            return x => coefficients.Select((t, i) => t * ChebyshevCooficients[i](x)).Sum();
        }

        public double[] Approximation(double eps, double a, double b)
        {
            double[] res;
            Func<double, double> phi;
            do
            {
                var indexMatr = new double[N, N + 1];
                //magic
                FillIndexMatr(indexMatr, InpFunction, b, a);

                res = _mSolver.GaussRoots(indexMatr);
                
                phi = Get_Phi(res);
                Console.WriteLine("n = {0}",N);
                WriteTable(phi,a,b,N);
                N += 5;
                
            } while (ErrorCalc(eps, a, b, InpFunction, phi));

            return res;
        }

        private void WriteTable(Func<double, double> phi, double a, double b, int size)
        {
            
            double x = a;
            string dest = String.Format("output{0}.csv", size);
            using (StreamWriter writer = File.CreateText(dest))
            {
                while (x <= b)
                {
                    writer.WriteLine(x +";" + phi(x));
                    //  writer.WriteLine(x);
                    x += 0.1;
                }
            }
        }

        private void FillIndexMatr(double[,] indexMatr, Func<double, double> f, double b, double a)
        {
            for (int i = 0; i < indexMatr.GetLength(0); i++)
            {
                for (int j = 0; j < indexMatr.GetLength(0); j++)
                {
                    indexMatr[i, j] = _intCalc.RungeMethod(x => Chebyshev(i)(x)*Chebyshev(j)(x), 
                                                            1e-5, b,a);
                }
                indexMatr[i, indexMatr.GetLength(0)] = _intCalc.RungeMethod(x => Chebyshev(i)(x)*f(x),
                                                                            1e-5, b, a);
            }
        }

        public Func<double, double> Chebyshev(int n)
        {
            if (n == 0)
            {
                return x => 1;
            }
            if (n == 1)
            {
                return x => x;
            }

            return x => 2 * x * Chebyshev(n - 1)(x) - Chebyshev(n - 2)(x);
        }
    }
}
