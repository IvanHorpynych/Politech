#pragma once
#include <stdio.h>
#include <math.h>

/*
Обчислює початковий крок n для обчислення інтеграла із заданою точністю eps.
*/
int ObchyslenniaPochatkovogoKroku(int r, double eps);

/*
Обчислює довжини елементарного відрізка h на основі границь a та b і кроку n.
*/
double ObchyslenniaElementarnogoVidrizka(double a, double b, int n);

/*
Обчислює інтеграл від функції pt2Func на відрізку [a, b] з довжиною елементарного відрізка h і кроком n.
*/
double ObchyslenniaIntegrala(double (*pt2Func)(double), double a, double b, double h, int n);

/*
Методом подвійного перерахунку обчислює інтеграл від функції pt2Func
за допомогою формули інтегрування pt2IntFunc на відрізку [a, b] із точністю eps.
Точність не абсолютна, а відносна.
*/
double PodvijnyjPererahunok(double (*pt2Func)(double), double (*pt2IntFunc)(double (*pt2Func)(double), double, double, double, int), 
							double a, double b, double eps, int r);

/*
Обчислює інтеграл від добутку функцій pt2Func1(double, int) і pt2Func2(double, int) на відрізку [a, b] з довжиною елементарного відрізка h і кроком n.
*/
double ObchyslenniaIntegralaVidDobutku1(double (*pt2Func1)(double, int), double (*pt2Func2)(double, int), double a, double b, double h, int n, int m1, int m2);

/*
Методом подвійного перерахунку обчислює інтеграл від добутку функцій pt2Func1(double, int) і pt2Func2(double, int)
за допомогою формули інтегрування pt2IntFunc1 на відрізку [a, b] із точністю eps.
Точність не абсолютна, а відносна.
*/
double PodvijnyjPererahunokVidDobutku1(double (*pt2Func1)(double, int),double (*pt2Func2)(double, int),
									  double (*pt2IntFunc1)(double (*pt2Func1)(double, int),double (*pt2Func2)(double, int), double, double, double, int, int, int), 
									  double a, double b, double eps, int r, int m1, int m2);

/*
Обчислює інтеграл від добутку функцій pt2Func1(double) і pt2Func2(double, int) на відрізку [a, b] з довжиною елементарного відрізка h і кроком n.
*/
double ObchyslenniaIntegralaVidDobutku2(double (*pt2Func1)(double), double (*pt2Func2)(double, int), double a, double b, double h, int n, int m2);

/*
Методом подвійного перерахунку обчислює інтеграл від добутку функцій pt2Func1(double) і pt2Func2(double, int)
за допомогою формули інтегрування pt2IntFunc1 на відрізку [a, b] із точністю eps.
Точність не абсолютна, а відносна.
*/
double PodvijnyjPererahunokVidDobutku2(double (*pt2Func1)(double),double (*pt2Func2)(double, int),
									   double (*pt2IntFunc1)(double (*pt2Func1)(double),double (*pt2Func2)(double, int), double, double, double, int, int), 
									  double a, double b, double eps, int r, int m2);

//**************************************************************************
//**************************************************************************
/*
Обчислює інтеграл від різниці функцій pt2Func1(double) і pt2Func2(double, int) на відрізку [a, b] з довжиною елементарного відрізка h і кроком n.
*/
double ObchyslenniaIntegralaVidRiznytsi(double (*pt2Func1)(double), double (*pt2Func2)(double, int, double*), double a, double b, double h, int n, int m2, double*koef);

/*
Методом подвійного перерахунку обчислює інтеграл від різниці функцій pt2Func1(double) і pt2Func2(double, int, double*)
за допомогою формули інтегрування pt2IntFunc1 на відрізку [a, b] із точністю eps.
Точність не абсолютна, а відносна.
*/
double PodvijnyjPererahunokVidRiznytsi(double (*pt2Func1)(double),double (*pt2Func2)(double, int, double*),
									   double (*pt2IntFunc1)(double (*pt2Func1)(double),double (*pt2Func2)(double, int, double*), double, double, double, int, int, double*), 
									  double a, double b, double eps, int r, int m2, double* koef);