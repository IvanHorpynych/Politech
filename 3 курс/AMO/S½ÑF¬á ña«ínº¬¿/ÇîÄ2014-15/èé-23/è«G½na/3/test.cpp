#include "header.h"
int main(){
	
	double Matrix[n][n + 1] = {{3, 7, 6, 0, 63},{13, 49, 20, 15, 353},{20, 19, 50, 10, 437},{17, 7, 17, 17, 225}},
		ModernMatrix[n][n + 1] = {{61, 1, -2, 17, 181},{13, 49, 20, 15, 353},{20, 19, 50, 10, 437},{31, 2, 1, 41, 238}},
		GEquationRoot[n], SEquationRoot[n], Error = 0, eps = 0.001;
	GaussMethod(Matrix, GEquationRoot);
	printf("Gauss Method: \n");
	for(int i = 0; i < 4; i++){
		printf("x%d = %f\n", i, GEquationRoot[i]);
	}
	printf("\n");
	
	SeidelMethod(ModernMatrix, SEquationRoot, Error, eps);
	printf("Seidel Method: \n");
	for(int i = 0; i < 4; i++){
		printf("x%d = %f\n", i, SEquationRoot[i]);
	}
	printf("Error = %.8f \n", Error);
	printf("eps = %.8f \n", eps);

	return 0;
}