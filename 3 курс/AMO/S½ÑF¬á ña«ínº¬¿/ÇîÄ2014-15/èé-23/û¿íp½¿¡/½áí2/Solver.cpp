#include "Solver.h"
#include <math.h>
#include <stdlib.h>

double Solver::It



erationMethod(double lowerLimit, double upperLimit, double eps) {
    double minDerivativeValue = equation.FirstDerivative(lowerLimit),
    maxDerivativeValue = equation.FirstDerivative(upperLimit);
    if ((minDerivativeValue < 0) && (maxDerivativeValue < 0))
    {
        minDerivativeValue = -minDerivativeValue;
        maxDerivativeValue = -maxDerivativeValue;
        equation.SetSign(false);
    }
    if (minDerivativeValue > maxDerivativeValue) {
        double t = minDerivativeValue; minDerivativeValue = maxDerivativeValue; maxDerivativeValue = t;
    }
    double q = 1 - minDerivativeValue/maxDerivativeValue;
    double lam = 1/maxDerivativeValue;
    
    double minDelta = (1-q)/q*eps;
    double Xk = (lowerLimit + upperLimit) / 2,
    Xprev = Xk;
    nLastIterations = 0;
    do
    {
        nLastIterations++;
        Xprev = Xk;
        Xk = Xprev - lam * equation.EquationLeft(Xprev);
    } while (fabs(Xk - Xprev) > minDelta);
    lastPrecision = fabs(Xk - Xprev);
    return Xk;
}

double Solver::NewtonMethod(double lowerLimit, double upperLimit, double eps) {
    double Xn;
    nLastIterations = 0; //f(x) и f''(x) имеют разные знаки и при lowerlimit, и при upperlimit
    double minDeriv = GetMinDerivative (lowerLimit, upperLimit);
    if (equation.EquationLeft(lowerLimit)*equation.SecondDerivative(lowerLimit) > 0)
        Xn = lowerLimit;
    else if (equation.EquationLeft(upperLimit)*equation.SecondDerivative(upperLimit) > 0)
        Xn = upperLimit;
    do
    {
        nLastIterations++;
        Xn = Xn - equation.EquationLeft(Xn)/equation.FirstDerivative(Xn);
    } while (fabs(equation.EquationLeft(Xn))/minDeriv > eps);
    lastPrecision = fabs(equation.EquationLeft(Xn)/minDeriv);
    return Xn;
}

double Solver::GetMinDerivative(double lowerLimit, double upperLimit) {
    double min = equation.FirstDerivative(lowerLimit);
    for (double arg = lowerLimit + searchDerivativeStep;
         arg <= upperLimit;
         arg += searchDerivativeStep)
        if (equation.FirstDerivative(arg) < min)
            min = equation.FirstDerivative(arg);
    return min;
}

int Solver::GetLastIterationsNum() {
    return nLastIterations;
}

double Solver::GetLastPrecision() {
    return lastPrecision;
}