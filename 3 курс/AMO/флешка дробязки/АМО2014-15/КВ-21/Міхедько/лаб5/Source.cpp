

#include <iostream>
#include <math.h>
#include "approx_Legendre.h"


using namespace std;

double f(double x)
{
	return 0.1*x*x*log10(abs(sin(x/1.7))/2);
}

int main()
{
	double a=3;
	double b=10.5;
	double eps = 10e-3;

	cout<<"Calculating..."<<endl;
	AnswerLegendre(f,a,b,eps);
	//AnswerChebyshev(f,a,b,eps);
	
	cout<<"Ok"<<endl;

	return 0;
}
