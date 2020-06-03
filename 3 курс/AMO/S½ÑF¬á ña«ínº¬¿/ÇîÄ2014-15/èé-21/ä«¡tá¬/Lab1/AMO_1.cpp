#include "AMO_1.h"

/*Виконує множення параметра Tochnist на величину Krok*/
void ZminaTochnosti(double& Tochnist, double Krok){
	Tochnist *= Krok;
}

/*Знаходження наступного члена ряда за відомим попереднім.*/
double ZnahNastupnogo(double Poperednij, double X, int K){
	if (0 == K){
		return X;
	}	
	return Poperednij * (X * X) / (2 * K * (2 * K + 1));
}

/*Знаходження точки, в якій обчислюється значення функції.*/
double ZnahTochky(double a, double b){
	return (a + b) / 2;
}

/*Обчислення xi*/
double ObchXi(double a, double b, double h, int i){
	return (a+b)/2 + h*i;
}

/*Обчислення h*/
double ObchH(double a, double b){
	return (b - a) / 10;
}

