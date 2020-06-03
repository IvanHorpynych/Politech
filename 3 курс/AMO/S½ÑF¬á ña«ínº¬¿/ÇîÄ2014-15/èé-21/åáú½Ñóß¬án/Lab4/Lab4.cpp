#include "header.h"

double Integrand(double x){
	return sqrt(2 * x + 1) / 2;
}

double Primitive(double x){
	return (2 * x + 1) * sqrt(2 * x + 1) / 6;
}

double MetodTrapezium(double &step){
	double I = (Integrand(a) + Integrand(b))/2 , x = a, h = 0;
	int n = (b - a) / step;
	n++;
	step = (b - a)/(double)n;
	for(int i = 0; i < n-1; i++){
		x += step;
		I += Integrand(x);
	}
	return I * step;
}

double RefinedCalculation(double eps, double &step){
	int n = (int)(1 / sqrt(eps));
	double I_1 = 0, I_2 = 0;
	step = (b - a) / (double)n;
	I_2 = MetodTrapezium(step);
	do{
		I_1 = I_2;
		step = step / 2;
		I_2 = MetodTrapezium(step);
	}while((fabs(I_1 - I_2) / 3) > eps);
	return I_2;
}