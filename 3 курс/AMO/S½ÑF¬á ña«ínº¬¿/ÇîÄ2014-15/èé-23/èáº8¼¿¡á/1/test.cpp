#include "lib.h"

int main(){
	double eps = 0.01, RemTerm = 0, h = 0, abserr = 0, result = 0;
	int series_len = 1;
	double a = -3.9, b = 14, x = 0;
		
	printf("TABLE_1");
	printf("\n      eps       n   Absolute Error   Remainder Term	Result\n\n");
	x = (a + b)/2;
	for(int i = 0; i <= 4; i++){
		RemTerm = Remainder_Term(eps, series_len, x, result);
		abserr = abs(result - exp(x));
		printf("%13e %3d %15e %15e %13f\n", eps, series_len, abserr, RemTerm, result);
		eps = eps/1000;
		series_len = 1;
		result = 0;
	}

	printf("\n\nTABLE_2(witheps = 1.0e-008) \n");
	printf("  x     Absolute Error	RemainderTerm	 Result\n\n");
	eps = 0.00000001;
	h = (b - a)/10;
	for(int i = 0; i <= 10; i++){
		x = a + h*i;
		RemTerm = Remainder_Term(eps, series_len, x, result);
		abserr = abs(result - exp(x));
		printf("%5.2f %15e %16e %16f\n", x, abserr, RemTerm, result);
		series_len = 1;
		result = 0;
	}
	getchar();	
	return 0;
}
