using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lab5
{
    class Approximation
    {
        public static int n; // Matrix size;
        private double[,] Matrix;
        private double[] RightPart;
        private double a, b;  // Limits of integration
        private double eps;
        public double delta;
        public Func<double, double> func { get; set; }
        public Approximation(double _a , double _b, double _eps, Func<double, double> f)
        {
            Matrix = new double[n, n];
            Matrix.Initialize();
            RightPart = new double[n];
            RightPart.Initialize();
            func = f;
            eps = _eps;
            a = _a;
            b = _b;
        }
        private void FillIndexMatr()
        {
            var intCalc = new SimpsonMethod();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Matrix[i, j] = intCalc.DoCalculations(x => ChebishevCoeff(i)(x) * ChebishevCoeff(j)(x),
                                                            0.01, b, a);
                }
                RightPart[i] = intCalc.DoCalculations(x => ChebishevCoeff(i)(x) * this.func(x),
                                                                            0.01, b, a);
            }
        }
        public Func<double, double> ChebishevCoeff(int n)
        {
            if (n == 0)
            {
                return x => 1;
            }
            if (n == 1)
            {
                return x => x;
            }

            return x => 2 * x * ChebishevCoeff(n - 1)(x) - ChebishevCoeff(n - 2)(x);
        }

        public double GetDelta (double a, double b, Func<double, double> phi)
        {
            var intCalc = new SimpsonMethod();
            Func<double, double> underIntegralFunc = x =>
            {
                double fx = func(x);
                double phiMinFx = phi(x) - fx;
                return phiMinFx * phiMinFx;
            };

            double delta = Math.Sqrt(intCalc.DoCalculations(underIntegralFunc, 1e-3, b, a) / (b - a));

            return delta;
        }

        public Func<double, double> GetPhi(double[] AnswersOfSLE)
        {
            var ChebCoeffsVect = new Func<double, double>[AnswersOfSLE.Length];
            for (int i = 0; i < AnswersOfSLE.Length; i++)
            {
                ChebCoeffsVect[i] = ChebishevCoeff(i);
            }
            return x => AnswersOfSLE.Select((t, i) => t * ChebCoeffsVect[i](x)).Sum();
        }

        private void WriteCordinateFile(double a, double b, Func<double, double> phi)
        {
            double y = 0.0, x = a;
            string Dest = "D:\\АМО\\Amo_Lab5_A\\Amo_Lab5_A";
            Dest += n.ToString() + ".csv";
            using (StreamWriter writer = File.CreateText(Dest))
            {
                while (x <= b)
                {
                    y = phi(x);
                    writer.WriteLine(y + ";" + x);
                    x += 0.1;
                }
            }
        }

        public void Approximate()
        {
            FillIndexMatr();
            var SLESolver = new ComletePivoting(Matrix, RightPart, n);
            SLESolver.SolveMatrix();
            Func<double, double> phi;
            phi = GetPhi(SLESolver.Answer);
            delta = GetDelta(a, b, phi);
            WriteCordinateFile(a, b, phi);
        }
    }
}
