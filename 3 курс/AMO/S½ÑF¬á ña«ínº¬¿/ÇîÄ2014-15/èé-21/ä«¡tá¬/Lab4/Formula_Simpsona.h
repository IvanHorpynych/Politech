#pragma once
#include <stdio.h>
#include <math.h>

/*Обчислення наступного x за номером k, довжиною проміжка h і початкм відрізка a.*/
long double ObchyslenniaX(long double a, int k, long double h);

/*Обчислює Sigma1 і Sigma2.*/
long double* ObchyslenniaSigma(long double a, long double b, long double h, int n, long double(*pt2Func)(long double));

long double* ObchyslenniaIntegrala(long double a, long double b, long double h, long double(*pt2Func)(long double));