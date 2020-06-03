#include "spline.h"

double H(double xi, double xi_){
	return xi - xi_;
}

double Hi(double* ArrayX, int i){
	return H(ArrayX[i], ArrayX[i - 1]);
}

double FreeTerm(double(*Function)(double), double* ArrayX, int i){
	return 6 * ((Function(ArrayX[i + 1]) - Function(ArrayX[i])) / Hi(ArrayX, i + 1) - (Function(ArrayX[i]) - Function(ArrayX[i - 1])) / Hi(ArrayX, i));
}

double** CreateSystem(double(*Function)(double), double *ArrayX, int n){
	double** System = new double*[n + 1];
	for (int i = 0; i <= n; i++){
		System[i] = new double[n + 2];
		for (int j = 0; j <= n + 1; j++)
			System[i][j] = 0.0;
	}
	System[0][0] = 1;
	System[n][n] = 1;
	for (int i = 1; i < n; i++){
		System[i][i - 1] = Hi(ArrayX, i);
		System[i][i] = 2.0 * (Hi(ArrayX, i) + Hi(ArrayX, i + 1));
		System[i][i + 1] = Hi(ArrayX, i + 1);
		System[i][n + 1] = FreeTerm(Function, ArrayX, i);
		//printf("%-2d: %5.2f\n", i, System[i][n + 1]);
	}
	return System;
}

double* ÑalculationArrayC(double** System, int n){
	return GaussianJordanExclusion(System, n + 1);
}

double* ÑalculationArrayA(double(*Function)(double), double* ArrayX, int n){
	double* ret = new double[n + 1];
	for (int i = 0; i <= n; i++){
		ret[i] = Function(ArrayX[i]);
	}
	return ret;
}

double* ÑalculationArrayD(double* ArrayC, double* ArrayX, int n){
	double* ret = new double[n + 1];
	ret[0] = 0.0;
	for (int i = 1; i <= n; i++){
		ret[i] = (ArrayC[i] - ArrayC[i - 1]) / Hi(ArrayX, i);
	}
	return ret;
}

double* ÑalculationArrayB(double(*Function)(double), double* ArrayC, double* ArrayD, double* ArrayX, int n){
	double* ret = new double[n + 1];
	ret[0] = 0.0;
	for (int i = 1; i <= n; i++){
		ret[i] = (Hi(ArrayX, i)*ArrayC[i]) / 2.0 - (Hi(ArrayX, i)*Hi(ArrayX, i)*ArrayD[i]) / 6.0 + (Function(ArrayX[i]) - Function(ArrayX[i - 1])) / Hi(ArrayX, i);
	}
	return ret;
}

void PrintArray(double* arr, int n){
	for (int i = 0; i <= n; i++)
		printf("%6.2f", arr[i]);
	printf("\n");
}

void ÑalculationArrayS(double(*Function)(double), double*  ArrayA, double* ArrayB, double*  ArrayC,
	double* ArrayD, double* ArrayX, int  n){
	FILE *file;
	int m = 10;
	fopen_s(&file, "res.txt", "wt");
	if (file != NULL){
		for (int i = 1; i <= n; i++){
			double h = (ArrayX[i] - ArrayX[i - 1]) / (double)m;
			for (int j = 0; j <= m; j++){
				double xi = ArrayX[i - 1] + h*j;
				double si = ArrayA[i] + ArrayB[i] * (xi - ArrayX[i]) + 
					ArrayC[i]*(xi - ArrayX[i])*(xi - ArrayX[i]) / 2.0 + 
					ArrayD[i]*(xi - ArrayX[i])*(xi - ArrayX[i])*(xi - ArrayX[i]) / 6.0;
				fprintf(file, "%.16f\t%.16f\n", xi, si);
			}
		}
		fclose(file);
	}

}

void CubicSpline(double(*Function)(double), double a, double b, int n){
	double h = (b - a) / (double)n;
	double* ArrayX = new double[n + 1];
	for (int i = 0; i < n + 1; i++){
		ArrayX[i] = a + h*i;
		//printf("%-2d: %5.2f\n", i, ArrayX[i]);
	}
	double** System = CreateSystem(Function, ArrayX, n);
	PrintMatrix(System, n);
	double* ArrayC = ÑalculationArrayC(System, n);
	PrintArray(ArrayC, n);
	double* ArrayA = ÑalculationArrayA(Function, ArrayX, n);
	PrintArray(ArrayA, n);
	double* ArrayD = ÑalculationArrayD(ArrayC, ArrayX, n);
	PrintArray(ArrayD, n);
	double* ArrayB = ÑalculationArrayB(Function, ArrayC, ArrayD, ArrayX, n);
	PrintArray(ArrayB, n);
	ÑalculationArrayS(Function, ArrayA, ArrayB, ArrayC, ArrayD, ArrayX, n);
}
