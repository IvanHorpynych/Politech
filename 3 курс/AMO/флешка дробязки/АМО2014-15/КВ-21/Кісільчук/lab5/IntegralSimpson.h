#pragma once
#include <math.h>
//Інтегрування

int InitN(long double eps);

/*
Значення кроку
*/
long double FindH(long double a, long double b, int n);

/*
інтеграл від функції Func
*/
long double IntegralSimpson(long double(*Func)(long double), long double a, long double h, int n);

/*
Інтеграл від добутку многочленів Лежандра
*/
long double IntegralSimpson(long double(*Func)(long double, int), long double a, long double h, int n, int n1, int n2);

/*
Інтеграл від добутку функцій Func1(long double) Func2(long double,int)
*/
long double IntegralSimpson(long double(*Func1)(long double), long double(*Func2)(long double, int), long double a, long double h, int n, int n2);

long double IntegralSimpson(long double(*Func1)(long double), long double(*Func2)(long double, int, long double*), long double a, long double h, int n, int n2);

/*
Подвійний перерахунок для добутку многочленів Лежандра
*/
long double RedinedCalculationIntegralSimpson(long double(*Func)(long double, int), long double a, long double b, long double n1, long double n2);

long double RedinedCalculationIntegralSimpson(long double(*Func)(long double), long double a, long double b);

/*
Подвійний перерахунок для добутку функцій Func1(long double) Func2(long double,int)
*/
long double RedinedCalculationIntegralSimpson(long double(*Func1)(long double), long double(*Func2)(long double, int),
	long double a, long double b, long double n2);

long double RedinedCalculationIntegralSimpson(long double(*Func1)(long double), long double(*Func2)(long double, int, long double*), long double a, long double b,
	long double n2, long double* coef);