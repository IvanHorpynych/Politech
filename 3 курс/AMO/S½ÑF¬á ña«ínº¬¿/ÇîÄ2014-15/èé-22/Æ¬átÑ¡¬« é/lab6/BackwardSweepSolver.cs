namespace Lab6
{
    class BackwardSweepSolver
    {
        public static void BackwardSweep(double[] alpha, double[] beta, CubicSplinesApproximation.SplineInfo[] splines)
        {
            for (int i = alpha.Length - 1; i > 0; --i)
            {
                splines[i].c = alpha[i] * splines[i + 1].c + beta[i];
            }
        }

        public static void ForwardSweep(double[] x, double[] y, double[] alpha, double[] beta)
        {
            for (int i = 1; i < x.Length - 1; ++i)
            {
                double hi = x[i] - x[i - 1];
                double hi1 = x[i + 1] - x[i];
                double A = hi;
                double C = 2.0 * (hi + hi1);
                double B = hi1;
                double F = 6.0 * ((y[i + 1] - y[i]) / hi1 - (y[i] - y[i - 1]) / hi);
                double z = (A * alpha[i - 1] + C);
                alpha[i] = -B / z;
                beta[i] = (F - A * beta[i - 1]) / z;
            }
        }
    }
}
