#pragma once
#include "FormulaTrapetsij.h"
#include "NormalnaSystema.h"
#include "Metod_jedynogo_podilu.h"

/*
Повертає значення середньоквадратичного відхилення функції pt2Func1(double)
і функції pt2Func2(double, int) на відрізку [a, b].
*/
double ObchyslenniaVidhylennia(double (*pt2Func1)(double),double (*pt2Func2)(double, int, double*),
							   double (*pt2IntFunc1)(double (*pt2Func1)(double),double (*pt2Func2)(double, int, double*), double, double, double, int, int, double*), 
							   double a, double b, int r, int m2, double* koef);

/*
Виконує пошук такого значення степені узагальненого многочлена, щоб значення
середньоквадратичного відхилення було менше за eps.
Повертає степінь.
*/
int ObchyslenniaStepeniUzagalnenogoMnogochlena(double (*pt2Func1)(double),double (*pt2Func2)(double, int, double*),
											   double (*pt2IntFunc1)(double (*pt2Func1)(double),double (*pt2Func2)(double, int, double*), double, double, double, int, int, double*), 
											   double a, double b, int r, double eps);