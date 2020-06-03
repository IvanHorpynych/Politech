//FUNCTION.CPP
#include "header.h"

void GaussianElimination(double CoefficientOfMatrix[n][n + 1], double GaussianEliminationEquationRoot[n]){
	double main1 = 0, main2 = 0, main3 = 0, helper[n + 1];

	for (int i = 0; i < n; i++){
		if (CoefficientOfMatrix[i][i] != 0){
			main1 = CoefficientOfMatrix[i][i];
			for (int j = 0; j <= n; j++){
				CoefficientOfMatrix[i][j] /= main1;
			}
			for (int k = i; k < n - 1; k++){
				main2 = CoefficientOfMatrix[k + 1][i];
				for (int j = 0; j <= n; j++){
					helper[j] = CoefficientOfMatrix[i][j] * main2;
					CoefficientOfMatrix[k + 1][j] -= helper[j];
				}
			}
		}
		else{
			printf("Divide by Zero, Coefficient %d %d ", i, i);
			printf(" = Zero; \n");
		}
	}

	for (int j = 0; j < n; j++) // p
		for (int i = j + 1; i < n; i++) // st
		{
		CoefficientOfMatrix[j][i] = -CoefficientOfMatrix[j][i];
		}

	for (int i = 0; i < n; i++)
		GaussianEliminationEquationRoot[i] = 1;
	 
	for (int j = 0; j < n; j++) // p
	{
		GaussianEliminationEquationRoot[n - j - 1] = CoefficientOfMatrix[n - j - 1][n];
		for (int i = 0; i < j; i++) // st
		{
			GaussianEliminationEquationRoot[n - j - 1] += GaussianEliminationEquationRoot[n - i - 1] * CoefficientOfMatrix[n - j - 1][n - i - 1];
		}
	}
}

void SeidelMethod(double SeidelCoefficient[n][n + 1], double SeidelEquationRoot[n], double &Error, double &eps){
	double B[n], sum1 = 0, sum2 = 0, tmp = 0, q = 0, max = 0, sub = 0, NextEquation = 0, PrevEquation = 0;

	for (int i = 0; i < n; i++){
		tmp = SeidelCoefficient[i][i];
		B[i] = SeidelCoefficient[i][n] / tmp;
		SeidelEquationRoot[i] = B[i];
		for (int j = 0; j <= n; j++){
			SeidelCoefficient[i][j] /= tmp;
			if (j < n){
				max += SeidelCoefficient[i][j];
			}
		}
		if ((max - 1) > q){
			q = max - 1;
		}
		max = 0;
		tmp = 0;
	}
	do{
		for (int i = 0; i < n; i++){
			PrevEquation = SeidelEquationRoot[i];
			sum1 = sum2 = 0;
			for (int j = 0; j < i; j++){
				sum1 += SeidelCoefficient[i][j] * SeidelEquationRoot[j];
			}
			for (int j = i + 1; j < n; j++){
				sum2 += SeidelCoefficient[i][j] * SeidelEquationRoot[j];
			}
			SeidelEquationRoot[i] = B[i] - sum1 - sum2;
			NextEquation = SeidelEquationRoot[i];
			sub = abs(NextEquation - PrevEquation);
			Error = sub * q / (1 - q);
			if (Error < eps){
				break;
			}
		}
	} while (abs(Error) >= eps);
}