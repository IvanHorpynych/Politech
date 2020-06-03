#pragma once
#include <stdio.h>
#include <math.h>

/*���������� ���������� x �� ������� k, �������� ������� h � ������� ������ a.*/
long double ObchyslenniaX(long double a, int k, long double h);

/*�������� Sigma1 � Sigma2.*/
long double* ObchyslenniaSigma(long double a, long double b, long double h, int n, long double(*pt2Func)(long double));

long double* ObchyslenniaIntegrala(long double a, long double b, long double h, long double(*pt2Func)(long double));