using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6AMO
{
    class Splines
    {
         private SplineInfo[] _splines;
        public struct SplineInfo
        {
            public double a, b, c, d, xi;
        }

        public Splines(double[] x, double[] y, int n)
        {
            BuildSpline(x,y);
        }


        public void BuildSpline(double[] x, double[] y)
        {
            if (x.Length != y.Length) throw new ArgumentException();

            _splines = new SplineInfo[x.Length];
            for (int i = 0; i < x.Length; ++i)
            {
                _splines[i].xi = x[i];
                _splines[i].a = y[i];
            }
            _splines[0].c = _splines[x.Length - 1].c = 0.0;

            var alpha = new double[x.Length - 1];
            var beta = new double[x.Length - 1];
            alpha[0] = beta[0] = 0.0;

            SweepMethod.ForwardSweep(x, y, alpha, beta);

            SweepMethod.BackwardSweep(alpha, beta, _splines);
            for (int i = x.Length - 1; i > 0; --i)
            {
                double hi = x[i] - x[i - 1];
                _splines[i].d = (_splines[i].c - _splines[i - 1].c) / hi;
                _splines[i].b = hi * (2.0 * _splines[i].c + _splines[i - 1].c) / 6.0 + (y[i] - y[i - 1]) / hi;
            }
        }



        public double Calc(double x)
        {
            if (_splines == null) { throw new NullReferenceException(); }

            SplineInfo s;

            if (x <= _splines[0].xi) s = _splines[0];
            else if (x >= _splines[_splines.Length - 1].xi) s = _splines[_splines.Length - 1];
            else
            {
                int l = 0;
                int r = _splines.Length - 1;
                while (l + 1 < r)
                {
                    int curr = l + (r - l)/2;
                    if (x <= _splines[curr].xi)
                    {
                        r = curr;
                    }
                    else
                    {
                        l = curr;
                    }
                }
                s = _splines[r];
            }

            double delta = x - s.xi;

            return s.a + delta*(s.b + delta*(s.c/2.0 + s.d*delta/6.0));
        }
    }
}
