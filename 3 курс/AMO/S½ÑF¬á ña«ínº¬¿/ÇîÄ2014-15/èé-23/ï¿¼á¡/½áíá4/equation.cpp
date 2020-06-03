#include "equation.h"

double Equation::Integrand (double x)
{
	return x*(2*cos(x/2)-x/2*sin(x/2));
}
double Equation::AntiDerivative (double x)
{
	return x*x*cos(x/2);
}	
double Equation::SecondDerivative (double x)
{
	return 1/8*(x*x-24)*sin(x/2)-3/2*x*cos(x/2);
}