#include "header.h"

int main(){
	double Border[2][2] = { { -2, -1.5 }, { 4, 4.5 } }; //межі областей
	double eps = 0.01, x = 0, a = 0, b = 0, min = 0, Max = 0, ApproximationPrecision = 0,//наближена точність
		ApproximationEquationRoot = 0,//наближений корінь
		RaphsonPrecision = 0,//точність
		RaphsonEquationRoot = 0;//корінь
	int ApproximationIterationCount = 0, RaphsonIterationCount = 0;


	printf("TABLE_1");
	printf("\n       EPS       RESULT          PRECISION \n");
	for (int j = 0; j < 4; j++){
		for (int i = 0; i < 2; i++){
			a = Border[i][0];
			b = Border[i][1];
			MinMaxDerivative(a, b, min, Max, x);
			Approximation(x, min, Max, eps, ApproximationPrecision, ApproximationEquationRoot);
			printf("%15e %15.12f %15.12f \n", eps, ApproximationEquationRoot, ApproximationPrecision);
			ApproximationPrecision = 0;
			ApproximationEquationRoot = 0;
			Max = 0;
			min = 0;
			x = 0;
		}
		printf("\n");
		eps = eps / 100;
	}
	eps = 0.01;

	printf("\n\nTABLE_2");
	printf("\n       EPS       RESULT          PRECISION \n");
	for (int j = 0; j < 4; j++){
		for (int i = 0; i < 2; i++){
			a = Border[i][0];
			b = Border[i][1];
			MinMaxDerivative(a, b, min, Max, x);
			Sign(a, b, x);
			Raphson(x, min, eps, RaphsonPrecision, RaphsonEquationRoot);
			printf("%15e %15.12f %15.12f \n", eps, RaphsonEquationRoot, RaphsonPrecision);
			RaphsonPrecision = 0;
			RaphsonEquationRoot = 0;
			min = 0;
			x = 0;
		}
		printf("\n");
		eps = eps / 100;
	}
	eps = 0.01;

	printf("\n\nTABLE_3");
	printf("\n     EPS        Approx_iteration   Raphson_iteration\n");
	for (int j = 0; j < 4; j++){
		a = Border[0][0];
		b = Border[0][1];
		MinMaxDerivative(a, b, min, Max, x);
		ApproximationIterationCount = Approximation(x, min, Max, eps, ApproximationPrecision, ApproximationEquationRoot);
		Sign(a, b, x);
		RaphsonIterationCount = Raphson(x, min, eps, RaphsonPrecision, RaphsonEquationRoot);
		printf("%14e %10d %16d \n", eps, ApproximationIterationCount, RaphsonIterationCount);
		ApproximationPrecision = 0;
		ApproximationEquationRoot = 0;
		RaphsonPrecision = 0;
		RaphsonEquationRoot = 0;
		Max = 0;
		min = 0;
		x = 0;
		printf("\n");
		eps = eps / 100;
	}

	system("pause");
	return 0;
}
