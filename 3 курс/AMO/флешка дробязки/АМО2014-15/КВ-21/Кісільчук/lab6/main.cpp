#include "main.h"
#define A 0.0
#define B 7.0
#define N 80

double Func(double x){
	return (x*x - sin(x))*cos(2.5*x);
}

int main(){
	CubicSpline(Func, A, B, N);
	return 0;
}