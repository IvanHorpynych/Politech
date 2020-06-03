#include <stdio.h>
#include <math.h>
#include "lab2.h"

double func(double x)
{
	return exp(x) - 3 * cos(2 * x) + 2 * x + 1;
}

double derivative(double x)
{
	return fabs(exp(x) + 6 * sin(2 * x) + 2);
}

double derivative1(double x)
{
	return exp(x) + 6 * sin(2 * x) + 2;
}
double derivative_2(double x)
{
	return exp(x) + 12 * cos(2 * x);
}

double fi_func(double x, double lamda)
{
	return x - lamda*func(x);
}

int check_iteration(double x_current, double x_next, double eps, double q)
{
	if (fabs(x_next - x_current) <= (1 - q) / q*eps)
		return 1;
	return 0;
}

int check_newton(double x_current, double eps, double m1)
{
	if (fabs(func(x_current)) / m1 <= eps)
		return 1;
	return 0;
}

void solution()
{
	double m1[3], M1[3], q[3], lamda[3], x_beg[3];
	const double a[3] = { -2.0, -1.0, 0.0 }, b[3] = { -1.5, -0.81, 0.5 };
	double x_current, x_next, eps;
	int numb[2][4];

	for (int i = 0; i < 3; i++)
	{
		double tmp = 0;
		M1[i] = derivative(a[i]);
		tmp = derivative(b[i]);
		if (tmp > M1[i])
		{
			m1[i] = M1[i];
			M1[i] = tmp;
		}
		else
			m1[i] = tmp;
		q[i] = 1 - m1[i] / M1[i];
	}

	lamda[0] = 1 / M1[0];
	lamda[1] = -1 / M1[1];
	lamda[2] = 1 / M1[2];

	printf("Method of iteration\n");
	printf(" eps\t\t x\t\t\t precision\n");
	eps = 1e-2;
	x_current = 0;
	for (int i = 1; i <= 4; i++)
	{
		for (int j = 0; j < 3; j++)
		{
			int n = 0;
			double error = 0;
			x_next = (a[j] + b[j]) / 2;
			do
			{
				x_current = x_next;
				x_next = fi_func(x_current, lamda[j]);
				n++;
			} while (check_iteration(x_current, x_next, eps, q[j]) == 0);
			error = fabs(x_next - x_current)*q[j] / (1 - q[j]);
			if (j == 0)
			{
				numb[0][i - 1] = n;
				printf(" %e\t%18.15f\t%18.15f\n", eps, x_current, error);
			}
			else
				printf("\t\t%18.15f\t%18.15f\n", x_current, error);
		}
		printf("\n");
		eps = eps*1e-3;
	}

	printf("Newton's method\n");
	printf(" eps\t\t x\t\t\t precision\n");

	x_beg[0] = a[0];
	x_beg[1] = b[1];
	x_beg[2] = b[2];

	eps = 1e-2;
	for (int i = 1; i <= 4; i++)
	{
		for (int j = 0; j < 3; j++)
		{
			int n = 0;
			double error = 0;
			x_next = x_beg[j];
			do
			{
				x_current = x_next;
				x_next = x_current - func(x_current) / derivative1(x_current);
				n++;
			} while (check_newton(x_current, eps, m1[j]) == 0);

			error = fabs(func(x_current)) / m1[j];
			if (j == 0)
			{
				numb[1][i - 1] = n;
				printf(" %e\t%18.15f\t%18.15f\n", eps, x_current, error);
			}
			else
				printf("\t\t%18.15f\t%18.15f\n", x_current, error);
		}
		printf("\n");
		eps = eps*1e-3;
	}

	printf(" eps\t\t1 method\t2 method\n");
	eps = 1e-2;
	for (int i = 0; i < 4; i++)
	{
		printf(" %e\t %5d\t\t%6d\n", eps, numb[0][i], numb[1][i]);
		eps = eps*1e-3;
	}

}
