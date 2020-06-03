using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    class Trapezium 
    {
        double LOWER_LIMIT;
        double UPPER_LIMIT;
        double step;
        public Trapezium(double low, double up, double getstep)
        {
            LOWER_LIMIT = low;
            UPPER_LIMIT = up;
            step = getstep;
        }
       public double Solve (Func<double,double> integrated)  
        {
	        double iter = LOWER_LIMIT;
	        double end = UPPER_LIMIT - step;
	        double res = integrated(iter)/2;
            for (; iter <= end; iter += step)
                res += integrated(iter);
            res = step * ( integrated(iter)/ 2 + res);
	        return res;
        }
    }
}
