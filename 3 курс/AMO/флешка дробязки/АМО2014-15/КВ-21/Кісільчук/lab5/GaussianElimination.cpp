#include "GaussianElimination.h"

bool CheckMainElementsMatrix(long double** matrix, int n){
	for (int i = 0; i < n; i++){
		if (matrix[i][i] == 0){
			int j;
			for (j = 0; j < n; j++){
				if (j != i){
					if ((matrix[j][i] != 0) && (matrix[i][j] != 0)){
						long double *tmp = matrix[i];
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

long double** CopyMatrix(long double** matrix, int n){
	long double** res = new long double*[n];
	for (int i = 0; i < n; i++){
		res[i] = new long double[n + 1];
		for (int j = 0; j < n + 1; j++)
			res[i][j] = matrix[i][j];
	}
	return res;
}

void PrintMatrix(long double** matrix, int m){
	printf("\n");
	for (int i = 0; i < m + 1; i++){
		for (int j = 0; j < m + 2; j++)
			printf("%12.8f ", matrix[i][j]);
		printf("\n");
	}
}

long double* GaussianElimination(long double** matrix_, int m){
	int n = m + 1;
	long double** matrix = CopyMatrix(matrix_, n);
	long double* res = new long double[n];
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
		for (int j = i + 1; j < n; j++){
			for (int k = n; k >= i; k--){
				matrix[j][k] -= matrix[i][k] * matrix[j][i];
			}
		}
		//PrintMatrix(matrix, i);
	}
	/*
	зворотній хід
	*/
	for (int i = n - 1; i >= 0; i--){
		long double xi = 0.0;
		for (int j = i; j < n - 1; j++)
			xi += res[j + 1] * matrix[i][j + 1];
		res[i] = matrix[i][n] - xi;
	}
	return res;
}