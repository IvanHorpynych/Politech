#include "IntegralSimpson.h"
#define EPS_I 1e-13

/*
Початкове значення n
*/
int InitN(long double eps){
	return (int)pow(eps, (long double )- 0.25);
}

/*
Значення кроку
*/
long double FindH(long double a, long double b, int n){
	return (b - a) / n;
}

/*
інтеграл від функції Func
*/
long double IntegralSimpson(long double(*Func)(long double), long double a, long double h, int n){
	long double sigma1 = 0, sigma2 = 0, x = a;
	long double res = 0;
	for (int i = 1; i < n; i++){
		x += h;
		if (i % 2 != 0)
			sigma1 += (*Func)(x);
		else
			sigma2 += (*Func)(x);
	}
	return (h / 3)*((*Func)(a)+(*Func)(a + h*n) + 4 * sigma1 + 2 * sigma2);
}

/*
Інтеграл від добутку многочленів Лежандра
*/
long double IntegralSimpson(long double(*Func)(long double, int), long double a, long double h, int n, int n1, int n2){
	long double sigma1 = 0, sigma2 = 0, x = a;
	long double res = 0;
	for (int i = 1; i < n; i++){
		x += h;
		if (i % 2 != 0)
			sigma1 += (*Func)(x, n1)*(*Func)(x, n2);
		else
			sigma2 += (*Func)(x, n1)*(*Func)(x, n2);
	}
	return (h / 3)*((*Func)(a, n1)*(*Func)(a, n2) + (*Func)(a + n*h, n1)*(*Func)(a + n*h, n2) + 4 * sigma1 + 2 * sigma2);
}

/*
Інтеграл від добутку функцій Func1(long double) Func2(long double,int)
*/
long double IntegralSimpson(long double(*Func1)(long double), long double(*Func2)(long double, int), long double a, long double h, int n, int n2){
	long double sigma1 = 0, sigma2 = 0, x = a;
	long double res = 0;
	for (int i = 1; i < n; i++){
		x += h;
		if (i % 2 != 0)
			sigma1 += (*Func1)(x)*(*Func2)(x, n2);
		else
			sigma2 += (*Func1)(x)*(*Func2)(x, n2);
	}
	return (h / 3)*((*Func1)(a)*(*Func2)(a, n2) + (*Func1)(a + n*h)*(*Func2)(a + n*h, n2) + 4 * sigma1 + 2 * sigma2);
}

/*
Інтеграл від квадрату різниці функцій Func1(long double) Func2(long double,int)
long double GeneralizedPolynomialsLegendre(long double x, int n, long double* Coefficients);
*/
long double IntegralSimpson(long double(*Func1)(long double), long double(*Func2)(long double,int,long double*), long double a, long double h, int n, int n2,long double* coef){
	long double sigma1 = 0, sigma2 = 0, x = a;
	long double res = 0;
	for (int i = 1; i < n; i++){
		x += h;
		if (i % 2 != 0)
			sigma1 += pow((*Func1)(x)-(*Func2)(x, n2, coef),2);
		else
			sigma2 += pow((*Func1)(x)-(*Func2)(x, n2, coef),2);
	}
	return (h / 3)*(pow((*Func1)(a)-(*Func2)(a, n2, coef), 2) + pow((*Func1)(a + n*h) - (*Func2)(a + n*h, n2, coef),2) + 4 * sigma1 + 2 * sigma2);
}

/*
Подвійний перерахунок для добутку многочленів Лежандра
*/
long double RedinedCalculationIntegralSimpson(long double(*Func)(long double, int), long double a, long double b, long double n1, long double n2){
	int n = InitN(EPS_I);
	long double h = FindH(a, b, n);
	long double IntN;
	long double Int2N = IntegralSimpson(Func, a, h, n, n1, n2);
	do{
		IntN = Int2N;
		n *= 2;
		h = FindH(a, b, n);
		Int2N = IntegralSimpson(Func, a, h, n, n1, n2);
	} while (1 / (pow(2.0, 4) - 1)*abs(IntN - Int2N) / abs(Int2N) > EPS_I);
	return Int2N;
}

/*
Подвійний перерахунок для функції
*/
long double RedinedCalculationIntegralSimpson(long double(*Func)(long double), long double a, long double b){
	int n = InitN(EPS_I);
	long double h = FindH(a, b, n);
	long double IntN;
	long double Int2N = IntegralSimpson(Func, a, h, n);
	do{
		IntN = Int2N;
		n *= 2;
		h = FindH(a, b, n);
		Int2N = IntegralSimpson(Func, a, h, n);
	} while (1 / (pow(2.0, 4) - 1)*abs(IntN - Int2N) / abs(Int2N) > EPS_I);
	return Int2N;
}

/*
Подвійний перерахунок для добутку функцій Func1(long double) Func2(long double,int)
*/
long double RedinedCalculationIntegralSimpson(long double(*Func1)(long double), long double(*Func2)(long double, int), long double a, long double b, long double n2){
	int n = InitN(EPS_I);
	long double h = FindH(a, b, n);
	long double IntN;
	long double Int2N = IntegralSimpson(Func1, Func2, a, h, n, n2);
	do{
		IntN = Int2N;
		n *= 2;
		h = FindH(a, b, n);
		Int2N = IntegralSimpson(Func1, Func2, a, h, n, n2);
	} while (1 / (pow(2.0, 4) - 1)*abs(IntN - Int2N) / abs(Int2N) > EPS_I);
	return Int2N;
}

/*
Подвійний перерахунок для різниці функцій Func1(long double) Func2(long double,int,long double*)
*/
long double RedinedCalculationIntegralSimpson(long double(*Func1)(long double), long double(*Func2)(long double, int,long double*), long double a, long double b, 
			long double n2,long double* coef){
	int n = InitN(EPS_I);
	long double h = FindH(a, b, n);
	long double IntN;
	long double Int2N = IntegralSimpson(Func1, Func2,a,h,n,n2,coef);
	do{
		IntN = Int2N;
		n *= 2;
		h = FindH(a, b, n);
		Int2N = IntegralSimpson(Func1, Func2, a, h, n, n2, coef);
	} while (1 / (pow(2.0, 4) - 1)*abs(IntN - Int2N) / abs(Int2N) > EPS_I);
	return Int2N;
}