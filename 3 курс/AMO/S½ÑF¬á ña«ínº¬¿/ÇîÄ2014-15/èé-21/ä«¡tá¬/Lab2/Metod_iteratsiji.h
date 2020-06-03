#pragma once
#pragma warning (disable:4996)
#include <stdio.h>
#include <math.h>
#include "Func.h"

/*Обчислення q на заданому інтервалі. Похідна функції визначається як
y' = exp(x) + 1/x + 10 */
double ObchQ(double a, double b);

/*Обчислення Alpha на заданому інтервалі*/
double ObchAlpha(double a, double b);

/*Обчислення наближення.*/
double ObchNabl(double x, double a, double b);

/*Обчислення конствнти умови завершення.*/
double ObchConstZav(double q, double e);

/*Обчислення першого наближення на інтервалі [a, b].*/
double ObchPershNabl(double a, double b);

/*Обчислення наближеного значення кореня на інтервалі [a, b] із точністю e.*/
double* ObchKoren_it(double a, double b, double e);

/*Виконання першої частини роботи*/
void VykPershChast(double eps, double KrZminy, int KilkZmin);