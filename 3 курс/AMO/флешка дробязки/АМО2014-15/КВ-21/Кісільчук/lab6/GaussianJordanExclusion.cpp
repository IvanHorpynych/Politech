#include "GaussianJordanExclusion.h"

bool CheckMainElementsMatrix(double** matrix, int n){
	for (int i = 0; i < n; i++){
		if (matrix[i][i] == 0){
			int j;
			for (j = 0; j < n; j++){
				if (j != i){
					if ((matrix[j][i] != 0) && (matrix[i][j] != 0)){
						double *tmp = matrix[i];
						matrix[i] = matrix[j];
						matrix[j] = tmp;
						break;
					}
				}
			}
			if (j == n)
				return false;
		}
	}
	return true;
}

double** CopyMatrix(double** matrix, int n){
	double** res = new double*[n];
	for (int i = 0; i < n; i++){
		res[i] = new double[n + 1];
		for (int j = 0; j < n + 1; j++)
			res[i][j] = matrix[i][j];
	}
	return res;
}

void PrintMatrix(double** matrix, int n){
	printf("\n");
	for (int i = 0; i < n+1; i++){
		for (int j = 0; j < n + 2; j++)
			printf("%6.2f", matrix[i][j]);
		printf("\n");
	}
}

double* GaussianJordanExclusion(double** matrix_, int n){
	double** matrix = CopyMatrix(matrix_, n);
	double* res = new double[n+1];
	if (!CheckMainElementsMatrix(matrix, n))
		return NULL;

	for (int i = 0; i < n; i++){
		if (matrix[i][i] == 0)
			return NULL;
		/*
		Ділимо на головний елемент
		*/
		for (int j = n; j >= i; j--){
			matrix[i][j] = matrix[i][j] / matrix[i][i];
		}

		/*
		віднімаємо від інших рядків
		*/
		for (int j = 0; j < n; j++){
			if (j == i)
				continue;
			for (int k = n; k >= i; k--){
				matrix[j][k] -= matrix[i][k] * matrix[j][i];
			}
		}
		//PrintMatrix(matrix, i);
	}
	for (int i = 0; i < n; i++)
		res[i] = matrix[i][n];
	return res;
}