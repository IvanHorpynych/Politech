//#include "header.h"
#include "Spline.h"

int main(){
	double N = 600;
	printf("Building spline with N = %.f \n", N);

	Spline spl;
	spl.SplineInterpolation(N);
	printf("Ok \n");
	return 0;
}