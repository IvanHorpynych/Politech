#include "header.h"

int main(){
	double N = 100;
	printf("Building spline with N = %.f \n", N);
	SplineInterpolation(N);
	printf("Ok \n");

	return 0;
}