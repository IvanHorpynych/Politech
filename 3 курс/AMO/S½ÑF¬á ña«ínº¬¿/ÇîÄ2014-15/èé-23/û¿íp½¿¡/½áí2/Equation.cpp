#include "Equation.h"

void Equation::SetSign(bool isPlus) {
    sign = isPlus ? 1.0 : -1.0 ;
}
double Equation::EquationLeft (double x) {
    return sign*(exp(x)-3*cos(2*x)+2*x+1);
}
double Equation::FirstDerivative (double x) {
    return sign*(exp(x)+6*sin(2*x)+2);
}
double Equation::SecondDerivative (double x) {
    return sign*(exp(x)+12*cos(2*x));
}