#include "stdio.h"
#include "stdlib.h"
#include "iostream"
#include "cmath"

int Approximation(double x, double min, double Max, double &eps, double &ApproximationPrecision, double &ApproximationEquationRoot);
int Raphson(double x, double min, double &eps, double &RaphsonPrecision, double &RaphsonEquationRoot);
double FirstDerivative(double a);
double SecondDerivative(double a);
void MinMaxDerivative(double a, double b, double &m, double &M, double &x);
void Sign(double a, double b, double &x);
