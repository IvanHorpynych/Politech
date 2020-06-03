#pragma once
#include <math.h>
//������������

int InitN(long double eps);

/*
�������� �����
*/
long double FindH(long double a, long double b, int n);

/*
�������� �� ������� Func
*/
long double IntegralSimpson(long double(*Func)(long double), long double a, long double h, int n);

/*
�������� �� ������� ���������� ��������
*/
long double IntegralSimpson(long double(*Func)(long double, int), long double a, long double h, int n, int n1, int n2);

/*
�������� �� ������� ������� Func1(long double) Func2(long double,int)
*/
long double IntegralSimpson(long double(*Func1)(long double), long double(*Func2)(long double, int), long double a, long double h, int n, int n2);

long double IntegralSimpson(long double(*Func1)(long double), long double(*Func2)(long double, int, long double*), long double a, long double h, int n, int n2);

/*
�������� ����������� ��� ������� ���������� ��������
*/
long double RedinedCalculationIntegralSimpson(long double(*Func)(long double, int), long double a, long double b, long double n1, long double n2);

long double RedinedCalculationIntegralSimpson(long double(*Func)(long double), long double a, long double b);

/*
�������� ����������� ��� ������� ������� Func1(long double) Func2(long double,int)
*/
long double RedinedCalculationIntegralSimpson(long double(*Func1)(long double), long double(*Func2)(long double, int),
	long double a, long double b, long double n2);

long double RedinedCalculationIntegralSimpson(long double(*Func1)(long double), long double(*Func2)(long double, int, long double*), long double a, long double b,
	long double n2, long double* coef);