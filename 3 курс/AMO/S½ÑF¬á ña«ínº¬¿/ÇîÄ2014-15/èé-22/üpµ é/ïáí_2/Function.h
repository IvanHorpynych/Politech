#define _USE_MATH_DEFINES

#include <cstdlib>
#include <iostream>
#include <math.h>
#include <cmath>

using namespace std;

static int iter [5] ;
static int iter2 [5];

double f(double x);
double df(double x);
double ddf(double x);
void methodOfSuccessiveApproximations(double a, double b, double m, double M);
void NewtonRawsonMethod(double a, double b, double m);


