#include <iostream>
#include <stdio.h>
#include "methods.h"
#include "math.h"
#define N 4

using namespace std;

double fx(double x) 
{
	return (exp(x)+log(x)-10*x+3);
}

double minus_fx(double x) 
{
	return -(exp(x) + log(x) - 10 * x + 3);
}

int main() 
{
	double x;
	double	eps;
	double a, b;
	int i;
	Method *method = new Method();
	Result result, result2;
	
	cout<<"\t\tIteration Method"<<endl;
	cout<<endl;
	x = 0.3;
	cout<<"\tEps\t"<<"\t"<<"   Root's value\t"<<"\tEstimation accuracy"<<endl;
	for(i = 0, eps = 1e-2; i <= N; ++i, eps /= 1e3) {
		result = method->Iteration(0.1, 5, x, eps, minus_fx);
		printf("%.14lf \t %.15lf \t %.15lf\n", eps, result.x, result.clarRoot);
	}
	cout<<endl;


	x = 3.4;
	cout<<"\tEps\t"<<"\t"<<"   Root's value\t"<<"\tEstimation accuracy"<<endl;
	for(i = 0, eps = 1e-2; i <= N; ++i, eps /= 1e3) {
		result = method->Iteration(3.0, 40, x, eps, fx);
		printf("%.14lf \t %.15lf \t %.15lf\n", eps, result.x, result.clarRoot);
	}
	cout<<endl;
	


	cout<<"\t\tBisection Method"<<endl;
	cout<<endl;
	a = 0;
	b = 0.5;
	cout<<"\tEps\t"<<"\t"<<"   Root's value\t"<<"\tEstimation accuracy"<<endl;
	for(i = 0, eps = 1e-2; i <= N; ++i, eps /= 1e3) {
		result = method->Bisection(a, b, eps, fx);
		printf("%.14lf \t %.15lf \t %.15lf\n", eps, result.x, result.clarRoot);
	}
	cout<<endl;

	a = 3;
	b = 3.5;
	cout<<"\tEps\t"<<"\t"<<"   Root's value\t"<<"\tEstimation accuracy"<<endl;
	for(i = 0, eps = 1e-2; i <= N; ++i, eps /= 1e3) {
		result = method->Bisection(a, b, eps, minus_fx);
		printf("%.14lf \t %.15lf \t %.15lf\n", eps, result.x, result.clarRoot);
	}
	cout<<endl;



	cout<<"\tComparison Table "<<endl;
	cout<<endl;
	x = 0.3;
	a = 0;
	b = 0.5;
	cout<<"\tEps\t"<<"\t"<<"Iteration method\t"<<"Bisection method"<<endl;
	for(i = 0, eps = 1e-2; i <= N; ++i, eps /= 1e3) {
		result = method->Iteration(0.1, 5, x, eps, minus_fx);
		result2 = method->Bisection(a, b, eps, fx);
		printf("%.14lf \t %d \t\t\t %d\n", eps, result.count, result2.count);
	}

	return 0;
}