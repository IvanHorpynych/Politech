#include "header.h"

int main(){
	double N = 50;
	printf("Building spline with N = %.f \n", N);
	SplineInterpolation(N);
	printf("Ok \n");

	getchar();
	return 0;
}
