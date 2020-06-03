//FUNCTION.CPP
#include "header.h"

void GaussMethod(double GaussCoefficient[n][n + 1], double GaussEquationRoot[n]){
	double main1 = 0, main2 = 0, main3 = 0, helper[n + 1];
	
	//під головною діагоналлю "0", в головній діагоналі "1"
	for(int i = 0; i < n; i++){
		if(GaussCoefficient[i][i] != 0){ 
			main1 = GaussCoefficient[i][i];
			for(int j = 0; j <= n; j++){
				GaussCoefficient[i][j] /= main1;	
			}
			for(int  k = i; k < n - 1; k++){
				main2 = GaussCoefficient[k + 1][i];
				for(int j = 0; j <= n; j++){
					helper[j] = GaussCoefficient[i][j] * main2;
					GaussCoefficient[k + 1][j] -= helper[j];
				}
			}
		}
		else{
			printf("Divide by Zero, Coefficient %d %d ", i, i);
			printf(" = Zero; \n" );
		}
	}

	//над головною діагоналлю "0"
	for(int  i = n - 1; i >= 0; i--){
		for(int ki = i; ki > 0; ki--){
			main3 = GaussCoefficient[ki - 1][i];
			for(int j = 0; j <= n; j++){
				helper[j] = GaussCoefficient[i][j] * main3;
				GaussCoefficient[ki - 1][j] -= helper[j];
			}	
		}
	}

	//корені рівняння
	for(int i = 0; i < n; i++){
		GaussEquationRoot[i] = GaussCoefficient[i][n];
	}
}

void SeidelMethod(double SeidelCoefficient[n][n + 1], double SeidelEquationRoot[n], double &Error, double &eps){
	double B[n], sum1 = 0, sum2 = 0, tmp = 0, q = 0, max = 0, sub = 0, NextEquation = 0, PrevEquation = 0;

	for(int i = 0; i < n; i++){
		tmp = SeidelCoefficient[i][i];
		B[i] = SeidelCoefficient[i][n] / tmp;
		SeidelEquationRoot[i] = B[i];
		for(int j = 0; j <= n; j++){
			SeidelCoefficient[i][j] /= tmp;
			if(j < n){
				max += SeidelCoefficient[i][j];
			}
		}
		if((max - 1) > q){
			q = max - 1;
		}
		max = 0;
		tmp = 0;
	}
	do{		
		for(int i = 0; i < n; i++){
			PrevEquation = SeidelEquationRoot[i];
			sum1 = sum2 = 0;
			for(int j = 0; j < i ; j++){
				sum1 += SeidelCoefficient[i][j] * SeidelEquationRoot[j];
			}
			for(int j = i+1; j < n; j++){
				sum2 += SeidelCoefficient[i][j] * SeidelEquationRoot[j];
			}
			SeidelEquationRoot[i] = B[i] - sum1 - sum2;
			NextEquation = SeidelEquationRoot[i];
			sub = abs(NextEquation - PrevEquation);
			Error = sub * q / (1 - q);
			if(Error < eps){
				break;
			}
		}
	}while(abs(Error) >= eps);
}