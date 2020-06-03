#include "PeretvorenniaZminnyh.h"

/*
Повертає значення x = (b+a)/2 + t*(b-a)/2.
*/
double VidTDoX(double t, double a, double b){
	return ((b + a) + t*(b - a))/2;
}

/*
Повертає значення t = (2*x-b-a)/(b-a).
*/
double VidXDoT(double x, double a, double b){
	return (2*x-b-a)/(b-a);
}