#include "header.h"

int main()
{
	double eps = 0.01, I_value = Primitive(b) - Primitive(a), delta = 0,
		IH = 2, n = 0, step = 0, stepref = 0, M2 = 0.5;

	printf("RESULT = %.16f \n \n", I_value);
	
	for(int i = 0; i < 4; i++){
		step = sqrt((12*eps)/(M2*(b - a)));
		IH = MetodTrapezium(step);
		stepref = step;
		eps *= 0.01;
		delta = fabs(I_value - IH);
		
		printf(" eps    %.16f  \n step   %.16f result     %.16f \n delta  %.16f new_result %.16f\n", eps, step, IH,  delta,  RefinedCalculation(delta, stepref));
		printf("\n");
	}

	system("pause");
	return 0;
}