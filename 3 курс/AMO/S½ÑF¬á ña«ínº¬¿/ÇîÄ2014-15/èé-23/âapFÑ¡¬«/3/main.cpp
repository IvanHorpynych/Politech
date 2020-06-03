#include "header.h"
int main(){
	
	double Matrix[n][n + 1] = {{10, 2, 0, 19, 44},{2, 24, 7, 14, 114},{10, 14, 29, 4, 108},{20, 13, 3, 8, 61}},
		ModernMatrix[n][n + 1] = { { 19, 1, -0.5, 1, 4 }, { 2, 24, 7, 14, 114 }, { 10, 14, 29, 4, 108 }, { 10, 2, 0, 19, 44 } },
		GEquationRoot[n], SEquationRoot[n], Error = 0, eps = 0.001;
	
	GaussianElimination(Matrix, GEquationRoot);
	printf("Gaussian Elimination: \n");
	for(int i = 0; i < 4; i++){
		printf("x%d = %f\n", i, GEquationRoot[i]);
	}
	printf("\n");
	
	SeidelMethod(ModernMatrix, SEquationRoot, Error, eps);
	printf("Seidel Method: \n");
	for(int i = 0; i < 4; i++){
		printf("x%d = %f\n", i, SEquationRoot[i]);
	}
	printf("\nError = %.8f \n", Error);
	printf("eps = %.8f \n", eps);

	system("pause");

	return 0;
}