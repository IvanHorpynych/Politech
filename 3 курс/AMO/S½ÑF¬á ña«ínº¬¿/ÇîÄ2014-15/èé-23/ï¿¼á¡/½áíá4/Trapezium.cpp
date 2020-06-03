#include "Trapezium.h"
#include "const.h"


double Trapezium::Exact ()
{
	return eq.AntiDerivative(UPPER_LIMIT) - eq.AntiDerivative(LOWER_LIMIT); 
}
double Trapezium::Solve (double step)
{
	double iter = LOWER_LIMIT;
	double end = UPPER_LIMIT - step;
	double res = eq.Integrand(iter)/2;
	for ( ; iter <= end; iter+=step) 
		res += eq.Integrand(iter); 
	res = step*(eq.Integrand(iter)/2+res);
	res+= Rest((UPPER_LIMIT+LOWER_LIMIT)/2, step);
	return res;
}
double Trapezium::Rest (double x, double step)
{
	return -(UPPER_LIMIT-LOWER_LIMIT)*step*step/12*eq.SecondDerivative(x);
}