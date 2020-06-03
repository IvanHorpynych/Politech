#pragma once
#include <stdio.h>
#include <math.h>
#include "Metod_iteratsiji.h"
#include "Metod_bisektsiji.h"
/*Обчислення похідної функції в заданії точці.
y = exp(x) + ln(x) - 10*x + 3
y' = exp(x) + 1/x + 10 */
double ObchPohidn(double x);

/*Обчислення другої похідної.
y'' = exp(x) - 1/(x^2) */
double ObchDrugPohidn(double x);

/*Обчислення функції в заданій точці.*/
double ObchFunc(double x);

/*Виконання першої частини роботи*/
void VykPershChast(double eps, double KrZminy, int KilkZmin);

void VykDrugChast(double eps, double KrZminy, int KilkZmin);