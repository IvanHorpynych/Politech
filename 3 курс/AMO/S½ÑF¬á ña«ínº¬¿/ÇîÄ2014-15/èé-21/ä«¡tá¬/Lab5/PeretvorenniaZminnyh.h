#pragma once
#include <stdio.h>

/*
Повертає значення x = (b+a)/2 + t*(b-a)/2.
*/
double VidTDoX(double t, double a, double b);

/*
Повертає значення t = (2*x-b-a)/(b-a).
*/
double VidXDoT(double x, double a, double b);