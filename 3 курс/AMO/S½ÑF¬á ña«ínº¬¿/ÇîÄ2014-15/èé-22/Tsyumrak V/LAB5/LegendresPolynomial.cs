using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMO_Lab5
{
    delegate double Function(double x);

    class LegendresPolynomial
    {
        private List<Function> members; // P[0], P[1] ... P[n]

        public LegendresPolynomial()
        {
            members = new List<Function>();
            members.Add(x => 1.0);  // P[0](x) = 1
            members.Add(x => x);    // P[1](x) = x
        }

        // evaluates and returns next P
        private Function next()
        {
            int n = members.Count - 1; // for P[n + 1]
            // P[n+1](x) = ((2n+1)/(n+1)) * x * P[n](x) - n/(n+1) * P[n-1](x)
            members.Add(x => ((2 * n + 1) * x * P(n)(x) - n * P(n - 1)(x)) / (n + 1));
            return members.Last();
        }

        // returns n-th member of the series
        public Function P(int n)
        {
            if (n < 0) Console.WriteLine("Error while getting P(n): n < 0.");
            if (n < members.Count) return members[n];
            else
            {
                next();
                return this.P(n);
            }
        }
    }
}
