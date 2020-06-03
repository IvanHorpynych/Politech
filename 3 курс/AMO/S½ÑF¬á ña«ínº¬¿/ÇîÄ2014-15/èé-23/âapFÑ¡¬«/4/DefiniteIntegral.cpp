#include "DefiniteIntegral.h"
#include <cmath>
using namespace std;
DefiniteIntegral::DefiniteIntegral()
{
	eps = 1e2;
	a = 1;
	b = 15;
	n = 0;
	result = 0;
	ifcomput = false;
	h = 0;
	M4 = derivative4(a); // для заданої функції похідні всіх порядків крім нульового є монотонно спадною
	//функцією на проміжку[1; 15]
}
DefiniteIntegral::~DefiniteIntegral()
{
}
long double DefiniteIntegral::integrand(long double x)
{
	return pow((x*x - 1), 2) / pow(x, 3);
}
long double DefiniteIntegral::antiderivative(long double border)
{
	return (pow(border, 4) - 1) / (2 * border*border) - 2 * log(border);
}
long double DefiniteIntegral::derivative4(long double x)
{
	return 360 / pow(x, 7) - 48 / pow(x, 5);
}
long double DefiniteIntegral::ruleSimpson(int _n)
{
	long double integral = 0;
	long double x = a;
	if (_n % 2 == 1){
		_n++;
	}
	h = (b - a) / _n;
	for (int i = 1; i <= _n-1; i++){
		if (i % 2 == 1)
			integral += 4 * (integrand(x += h));
		if (i % 2 == 0)
			integral += 2 * (integrand(x += h));
	}
	integral = integral + integrand(a) + integrand(b);
	return (integral*h / 3);
}

long double DefiniteIntegral::GetExactValueIntegral()
{
	return(antiderivative(b) - antiderivative(a));
}
long double DefiniteIntegral::IntegrationSimpsonRule()
{
	h = pow(180 * eps / ((b - a)*M4), 0.25);
	n = (b - a) / h;
	n++;
	if (n % 2 == 1){
		n++;
	}
	ifcomput = true;
	return(result = ruleSimpson(n));
}
long double DefiniteIntegral::IntegrationRefinedCalculation()
{
	int n = 1 / pow(eps, 0.25);
	long double remainder_term;
	long double In, I2n;
	if (n % 2 == 1){
		n++;
	} I2n =	ruleSimpson(n);
	do{
		In = I2n;
		n = 2 * n;
		I2n = ruleSimpson(n);
		remainder_term = 1 / 15 * fabs(In - I2n);
	} while (remainder_term > eps);
	ifcomput = true;
	return (result = I2n);
}
long double DefiniteIntegral::GetAbsError()
{
	if (ifcomput){
		return fabs(GetExactValueIntegral() - result);
	}
	return 0;
}
long double DefiniteIntegral::GetIntegrationStep()
{
	return(h);
}
int DefiniteIntegral::SetBorder(long double _a, long double _b)
{
	a = _a;
	b = _b;
	return 0;
}
long double DefiniteIntegral::SetEps(long double _eps)
{
	return (eps = _eps);
}