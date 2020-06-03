#include "stdio.h"
#include "stdlib.h"
#include "iostream"
#include "cmath"

#define a 0
#define b 15

double Integrand(double x);
double Primitive(double x);
double MetodTrapezium(double &step);
double RefinedCalculation(double eps, double &step);