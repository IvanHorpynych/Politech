#ifndef _SOLVER_H
#define _SOLVER_H
#pragma once

#include "Equation.h"
#include "constants.h"

class Solver
{
    Equation equation;
    int nLastIterations;
    double lastPrecision;
    double GetMinDerivative(double lowerLimit, double upperLimit);
public:
    double IterationMethod(double lowerLimit, double upperLimit, double eps);
    double NewtonMethod(double lowerLimit, double upperLimit, double eps);
    int GetLastIterationsNum();
    double GetLastPrecision();
};
#endif