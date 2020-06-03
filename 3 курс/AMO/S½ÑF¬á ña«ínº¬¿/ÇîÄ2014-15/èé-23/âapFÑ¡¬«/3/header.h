//HEADER.H
#include "stdio.h"
#include "stdlib.h"
#include "iostream"
#include "cmath"

#define n 4 //Кількість коренів у СЛАР

void GaussianElimination(double CoefficientOfMatrix[n][n + 1], double GaussianEliminationEquationRoot[n]);
void SeidelMethod(double SeidelCoefficient[n][n + 1], double SeidelEquationRoot[n], double &Error, double &eps);