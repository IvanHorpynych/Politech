#include "math.h"
#include <stdio.h>
#include <iostream>
#include <iomanip>
using namespace std;

int iteration(double m, double M, double x, int t)
{
	double x_n, l, q, eps;
	int i, c = 0, n = 4;
	l = 1 / M;
	q = 1 - m / M;
	cout << "\tEps" << "\tRoot's value\t" << "\tEstimation accuracy" << endl;
	for (i = 0, eps = 1e-2; i <= n; ++i, eps /= 1e3)
	{
		do {
			x_n = x;
			x = x_n - l*t*(log(2 + sin(x_n)) - x_n / 10 - 0.5);
			++c;
		} while (abs(x_n - x)>(1 - q)*eps / q);
		cout << setprecision(15) << "\t" << eps << "\t" << x << "\t" << abs(x_n - x) << endl;
	}
	cout << endl;
	return 0;
}
int bisection(double a, double b)
{
	double x, c1, eps;
	int i, c = 0, n = 4;
	cout << "\tEps" << "\tRoot's value\t" << "\tEstimation accuracy" << endl;
	for (i = 0, eps = 1e-2; i <= n; ++i, eps /= 1e3)
	{
		while (abs(b - a) >= 2 * eps) {
			c1 = (a + b) / 2;
			if (((log(2 + sin(a)) - a / 10 - 0.5)*(log(2 + sin(c1)) - c1 / 10 - 0.5))<0) b = c1;
			if (((log(2 + sin(b)) - b / 10 - 0.5)*(log(2 + sin(c1)) - c1 / 10 - 0.5))<0) a = c1;
			++c;
		}
		x = (a + b) / 2;
		cout << setprecision(15) << "\t" << eps << "\t" << x << "\t" << (b - a)*0.5 << endl;
	}
	cout << endl;
	return 0;
}
int comparison(double m, double M, double x2, int t, double a, double b)
{
	double c1, x_n, l, q, eps;
	int i, x, c2 = 0, c = 0, n = 4;
	l = 1 / M;
	q = 1 - m / M;
	cout << "\tEps" << "\tBisection method" << "\tIteration method" << endl;
	for (i = 0, eps = 1e-2; i <= n; ++i, eps /= 1e3)
	{
		while (abs(b - a) >= 2 * eps) {
			c1 = (a + b) / 2;
			if (((log(2 + sin(a)) - a / 10 - 0.5)*(log(2 + sin(c1)) - c1 / 10 - 0.5))<0) b = c1;
			if (((log(2 + sin(b)) - b / 10 - 0.5)*(log(2 + sin(c1)) - c1 / 10 - 0.5))<0) a = c1;
			++c;
		}
		x = (a + b) / 2;

		do {
			x_n = x2;
			x2 = x_n - l*t*(log(2 + sin(x_n)) - x_n / 10 - 0.5);
			++c2;
		} while (abs(x_n - x2)>(1 - q)*eps / q);

		cout << "\t" << eps << "\t\t" << c << "\t\t\t" << c2 << endl;
	}
	cout << endl;





	return 0;
}

int main()
{
	cout << "\t\tIteration method" << endl;
	cout << "\t\tTable 1" << endl;
	if (iteration(0.481, 0.632, -2.37, -1) != 0) {
		cout << "\tError.Not completed" << endl;
	}
	cout << "\t\tTable 2" << endl;
	if (iteration(0.366, 0.4, -0.48, 1) != 0) {
		cout << "\tError.Not completed" << endl;
	}
	cout << "\t\tTable 3" << endl;
	if (iteration(0.408, 0.667, 2.94, -1) != 0) {



		cout << "\tError.Not completed" << endl;
	}
	cout << "\t\tBisection method" << endl;
	cout << "\t\tTable 1" << endl;
	if (bisection(-3, -2) != 0) {
		cout << "\tError.Not completed" << endl;
	}
	cout << "\t\tTable 2" << endl;
	if (bisection(-1, 0) != 0) {
		cout << "\tError.Not completed" << endl;
	}
	cout << "\t\tTable 3" << endl;
	if (bisection(2.5, 3.5) != 0) {
		cout << "\tError.Not completed" << endl;
	}
	cout << "\t\tComparison Table" << endl;
	if (comparison(0.481, 0.632, -2.37, -1, -3, -2) != 0) {
		cout << "\tError.Not completed" << endl;
	}
	
	system("pause");
	return 0;
}
