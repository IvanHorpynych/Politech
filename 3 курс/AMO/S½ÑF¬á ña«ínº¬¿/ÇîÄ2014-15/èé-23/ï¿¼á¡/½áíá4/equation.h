#ifndef _EQUATION_H
#define _EQUATION_H
#pragma once
#include <math.h>

class Equation
{
public:
	double Integrand (double x);
	double SecondDerivative(double x);
	double AntiDerivative (double x);
};
#endif