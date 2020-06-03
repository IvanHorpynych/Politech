#pragma once
#include <stdio.h>
#include <math.h>
#include "Metod_iteratsiji.h"
#include "Metod_bisektsiji.h"
/*���������� ������� ������� � ����� �����.
y = exp(x) + ln(x) - 10*x + 3
y' = exp(x) + 1/x + 10 */
double ObchPohidn(double x);

/*���������� ����� �������.
y'' = exp(x) - 1/(x^2) */
double ObchDrugPohidn(double x);

/*���������� ������� � ������ �����.*/
double ObchFunc(double x);

/*��������� ����� ������� ������*/
void VykPershChast(double eps, double KrZminy, int KilkZmin);

void VykDrugChast(double eps, double KrZminy, int KilkZmin);