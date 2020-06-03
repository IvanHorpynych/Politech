#include "stdio.h"
#include "stdlib.h"
#include "iostream"
#include "cmath"
using namespace std;

#define n 4

void GaussMetod(double Gauss_Coeff[n][n + 1], double GEquation_Root[n])
{
	double tmp1 = 0, tmp2 = 0, tmp3 = 0, xx[n + 1];
	int i, j, k;
	
	for (i = 0; i < n; i++)
	{
		tmp1 = Gauss_Coeff[i][i];
		for (j = 0; j <= n; j++)
			Gauss_Coeff[i][j] /= tmp1;
		for (k = i; k < n - 1; k++)
		{
			tmp2 = Gauss_Coeff[k + 1][i];
			for (j = 0; j <= n; j++)
			{
				xx[j] = Gauss_Coeff[i][j] * tmp2;
				Gauss_Coeff[k + 1][j] -= xx[j];
			}
		}
	}
	
	for (i = n - 1; i >= 0; i--)
	{
		for (k = i; k > 0; k--)
		{
			tmp3 = Gauss_Coeff[k - 1][i];
			for (j = 0; j <= n; j++)
			{
				xx[j] = Gauss_Coeff[i][j] * tmp3;
				Gauss_Coeff[k - 1][j] -= xx[j];
			}
		}
	}

	//roots
	for (int i = 0; i < n; i++){
		GEquation_Root[i] = Gauss_Coeff[i][n];
	}
}

void ZeidelMetod(double Zeidel_Coeff[n][n + 1], double Zeidel_Equation_Root[n], double &Error, double &eps)
{
	double B[n],
		sum1 = 0, sum2 = 0, tmp = 0,
		q = 0, max = 0, sub = 0,
		Next_Equation = 0, Prev_Equation = 0;
	int i, j;

	for (i = 0; i < n; i++)
	{
		tmp = Zeidel_Coeff[i][i];
		B[i] = Zeidel_Coeff[i][n] / tmp;
		Zeidel_Equation_Root[i] = B[i];
		for (j = 0; j <= n; j++)
		{
			Zeidel_Coeff[i][j] /= tmp;
			if (j < n)
				max += Zeidel_Coeff[i][j];
		}
		if ((max - 1) > q)
			q = max - 1;

		max = 0;
		tmp = 0;
	}

	do
	{
		for (i = 0; i < n; i++)
		{
			Prev_Equation = Zeidel_Equation_Root[i];
			sum1 = sum2 = 0;
			for (j = 0; j < i; j++)
				sum1 += Zeidel_Coeff[i][j] * Zeidel_Equation_Root[j];
			for (j = i + 1; j < n; j++)
				sum2 += Zeidel_Coeff[i][j] * Zeidel_Equation_Root[j];
			Zeidel_Equation_Root[i] = B[i] - sum1 - sum2;
			Next_Equation = Zeidel_Equation_Root[i];
			sub = abs(Next_Equation - Prev_Equation);
			Error = sub * q / (1 - q);
			if (Error < eps)
				break;
		}
	} while (abs(Error) >= eps);

}

int main()
{
	double Matrix[n][n + 1] = {
		{ 9, 11, 11, 12, 214 },
		{ 12, 24, 2, 9, 186 },
		{ 20, 4, 41, 16, 470 },
		{ 7, 13, 9, 3, 140 }
	}, 
		ModernMatrix[n][n + 1] = {
			{ 9, -2, 2, 2, 74 },
			{ 9, 24, 2, 12, 186 },
			{ 16, 4, 41, 20, 470 },
			{ 4, 3, 1, -15, -54, }
	},
		Error = 0, eps = 0.001,
		G_Equation_Root[n], Z_Equation_Root[n];
	
	
	GaussMetod(Matrix, G_Equation_Root);
	printf("                   Method Gaussa: \n");
	for (int i = 0; i < 4; i++){
		printf("x%d = %f\n", i, G_Equation_Root[i]);
	}
	printf("\n");

	ZeidelMetod(ModernMatrix, Z_Equation_Root, Error, eps);
	printf("                    Seidel Method: \n");
	for (int i = 0; i < 4; i++){
		printf("x%d = %f \n", i, Z_Equation_Root[i]);
	}
	printf("\nError = %.8f \n", Error);
	printf("eps   = %.8f \n", eps);

	system("pause");

	return 0;
}
