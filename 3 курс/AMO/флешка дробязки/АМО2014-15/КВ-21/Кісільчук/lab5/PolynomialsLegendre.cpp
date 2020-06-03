#include "PolynomialsLegendre.h"

/*
Поліном Лежандра
*/
long double PolynomialsLegendre(long double x, int n){
	if (n == 0)
		return 1.0;
	if (n == 1)
		return x;
	long double prev = 1.0;
	long double elem = x;
	long double next;
	for (int i = 1; i < n; i++){
		next = ((2 * i + 1)*x*elem - i*prev) / (long double)(i+1);
		prev = elem;
		elem = next;
	}
	return next;
}

long double Polynomials(long double x, int n){
	long double ret = 1;
	for (int i = 0; i < n; i++)
		ret *= x;
	return ret;
}

/*
Повертає значення узагальненого полінома Лежандра n-ї степені
*/
long double GeneralizedPolynomialsLegendre(long double x, int n, long double* Coefficients){
	long double ret = 0;
	for (int i = 0; i <= n; i++){
		ret += Coefficients[i] * PolynomialsLegendre(x, i);
	}
	return ret;
}

long double GeneralizedPolynomials(long double x, int n, long double* Coefficients){
	long double ret = 0;
	for (int i = 0; i <= n; i++){
		ret += Coefficients[i] * Polynomials(x, i);
	}
	return ret;
}
