#include "main.h"
#define A 0.0
#define B 7.0
#define EPS 0.001

void PrintCoefficients(long double* arr, int n){
	printf("\n\tCoefficients:\n");
	for (int i = 0; i < n; i++)
		printf("%25.20f\n", arr[i]);
}

void WriteFile(long double(*Func)(long double, int, long double*), int n, long double* coef){
	FILE* file;
	fopen_s(&file, "XY.txt", "wt");
	long double h = (B - A) / 100.0;
	long double x, y;
	for (x = A; x <= B; x += h){
		y = Func(x, n, coef);
		fprintf(file, "%f\t%f\n", x, y);
	}
	fclose(file);
}

int main(){
	double leastSquaresDeviation = 0;
	long double (*poly)(long double, int);
	long double(*generalizedPoly)(long double, int,long double*);
	poly = PolynomialsLegendre;
	//poly = Polynomials;

	generalizedPoly = GeneralizedPolynomialsLegendre;
	if (poly == PolynomialsLegendre)
		generalizedPoly = GeneralizedPolynomialsLegendre;
	else if (poly == Polynomials)
		generalizedPoly = GeneralizedPolynomials;

	for (int i = 1;; i++){
		long double **matrix = CreateNoralEquations(i, A, B, poly);
		//PrintNoralEquations(matrix, i);
		long double* Coefficients = GaussianElimination(matrix, i);
		//PrintCoefficients(Coefficients, i+1);
		long double INT = RedinedCalculationIntegralSimpson(Func, generalizedPoly, A, B, i, Coefficients);
		//WriteFile(GeneralizedPolynomialsLegendre, i, Coefficients);
		leastSquaresDeviation = sqrt(INT / (B - A));
		printf("%d\t%30.30f\n", i, leastSquaresDeviation);
		if (leastSquaresDeviation < 0.6){
			WriteFile(generalizedPoly, i, Coefficients);
			break;
		}
	}
	return 0;
}