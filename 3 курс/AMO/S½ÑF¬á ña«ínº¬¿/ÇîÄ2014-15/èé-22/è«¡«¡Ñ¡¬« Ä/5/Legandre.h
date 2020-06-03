#pragma once
#include <stdio.h>

/*
Повертає значення многочлена Лежандра n-ї степені в даній точці X.
L0 = 1
L1 = x
L(n+1) = (2n+1)/(n+1)xLn(x) - n/(n+1)L(n-1)(x)
*/
double ObchyslenniaMnogochlenaLezhandra(double x, int n);

/*
Повертає значення узагальненого многочлена Лежандра n-ї степені
в даній точці x з коефіцієнтами koef.
*/
double ObchyslenniaUzagalnenogoMnogochlenaLezhandra(double x, int n, double* koef);