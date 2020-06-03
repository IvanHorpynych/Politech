#ifndef LAB2_H_
#define LAB2_H_
#include <stdio.h>
double derivative(double x);
double my_function(double x);
double fi_function(double x, double lamda);
int verification_iteration (double x_curr, double x_next, double eps, double q);
int verification_chord (double x_curr, double eps, double m1);
void comput();

#endif /* LAB2_H_ */
