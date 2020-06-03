#ifndef _EQUATION_H
#define _EQUATION_H
#pragma once
#include <math.h>

class Equation
{
    double sign;
public:
    Equation() : sign(1.0) {}
    double EquationLeft(double x);
    double FirstDerivative(double x);
    double SecondDerivative(double x);
    void SetSign(bool isPlus);
};
#endif