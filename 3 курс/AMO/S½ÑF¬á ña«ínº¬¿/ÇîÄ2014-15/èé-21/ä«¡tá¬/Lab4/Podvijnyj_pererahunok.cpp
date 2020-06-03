#include "Podvijnyj_pererahunok.h"
#include <math.h>

/*Обчислює початковий крок*/
long double Obchyslennia_Pochatkovogo_Kroku(int r, long double eps){
	long double a = sqrt(sqrt(eps));
	double b = 1.0/a;
	return (1.0/a);
}

/*Обчислює Rn.*/
long double Obchyslennia_Zalyshkovogo_Chlena(int r, long double IntegralN, long double Integral2n){
	long double r2 = 1.0;
	for(int i = 1; i <= r; i++)
		r2 *= 2;
	return (1.0 / (r2 - 1.0))*abs(IntegralN - Integral2n);
}


long double* Vykonannia_Drugoji_Chastyny(long double a, long double b, long double eps, long double(*pt2Func)(long double)){
	int n = Obchyslennia_Pochatkovogo_Kroku(4, eps);
	/*if( n%2 == 0){
		n += 2;
	}else{
		n += 3;
	}*/
	long double h = (b - a) / n;
	long double* Integral_Poperednij = ObchyslenniaIntegrala(a, b, h, pt2Func);
	n *= 2;
	h = (b - a) / n;
	long double* Integral_Nastupnyj = ObchyslenniaIntegrala(a, b, h, pt2Func);
	long double Rn = Obchyslennia_Zalyshkovogo_Chlena(4, Integral_Poperednij[0], Integral_Nastupnyj[0]);
	while (Rn > eps){
		Integral_Poperednij[0] = Integral_Nastupnyj[0];
		n *= 2;
		h = (b - a) / n;
		Integral_Nastupnyj = ObchyslenniaIntegrala(a, b, h, pt2Func);
		Rn = Obchyslennia_Zalyshkovogo_Chlena(4, Integral_Poperednij[0], Integral_Nastupnyj[0]);
	}
	long double* V = new long double[2];
	V[0] = Integral_Nastupnyj[0];
	V[1] = n;
	return V;
}