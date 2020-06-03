#include "NormalSystem.h"

/*
Створює нормальну систему. Розмір залежить від степені поліному m.
*/
long double** CreateNoralEquations(int m,long double a,long double b,long double(*FuncPolynom)(long double,int)){
	long double** matrix;
	matrix = new long double*[m+1];
	for (int i = 0; i <= m; i++){
		matrix[i] = new long double[m + 2];
		for (int j = 0; j <= m; j++)
			matrix[i][j] = RedinedCalculationIntegralSimpson(FuncPolynom, a, b, i, j);
		matrix[i][m + 1] = RedinedCalculationIntegralSimpson(Func, FuncPolynom, a, b, i);
	}
	return matrix;
}

/*
Виводить на екран нормальну систему степеня m.
*/
void PrintNoralEquations(long double** matrix, int m){
	FILE *file;
	fopen_s(&file, "system.txt", "wt");
	if (file != NULL){
		for (int i = 0; i < m + 1; i++){
			for (int j = 0; j < m + 1; j++)
				fprintf(file,"%12.3f ", matrix[i][j]);
			fprintf(file,"\n");
		}
		fprintf(file, "\n");
		for (int i = 0; i < m + 1; i++)
			fprintf(file, "%12.3f;", matrix[i][m + 1]);
		fclose(file);
	}
	for (int i = 0; i < m + 1; i++){
		for (int j = 0; j < m + 2; j++)
			printf("%12.3f ", matrix[i][j]);
		printf("\n");
	}
}