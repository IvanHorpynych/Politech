#pragma once

#include <math.h>
#include <iostream>
#include <fstream>

using namespace std;

double f(double x)
{
	return 0.1*x*x*log10(abs(sin(x / 1.7)) / 2);
}

double g(double t, double(*f)(double), double a, double b)
{
	return f((b + a) / 2 + t*(b - a) / 2);
}

//многочлени Лежанра
double L(int n, double x)
{
	double Pn_next, Pn = x, Pn_1 = 1;

	if (n == 0) return Pn_1;
	if (n == 1) return Pn;

	int i = 1;
	while (i<n)
	{
		Pn_next = (1.0*(2 * i + 1) / (i + 1))*x*Pn - (1.0*i / (i + 1))*Pn_1;
		Pn_1 = Pn;
		Pn = Pn_next;
		++i;
	}

	return Pn;
}

//підінтегральна функція g(t)*Lk(t)
double g_L(double t, double(*f)(double), double a, double b, int k)
{
	return g(t, f, a, b)*L(k, t);
}

//інтегруємо функцію g(t)*Lk(t) на проміжку [-1;1] (узагальнена формула Сімпсона)
double Integral_g_L(double(*f)(double), double a, double b, int k, int n)
{
	double h = 2.0 / n; //h=(b-a)/n = (1-(-1))/n
	double I, I1 = 0, I2 = 0;
	double x = -1;

	I = g_L(-1, f, a, b, k) + g_L(1, f, a, b, k);

	for (int i = 0; i<(n - 2) / 2; ++i)
	{
		x += h;
		I1 += g_L(x, f, a, b, k);
		x += h;
		I2 += g_L(x, f, a, b, k);
	}

	x += h;
	I1 += g_L(x, f, a, b, k);
	return (h / 3)*(I + 4 * I1 + 2 * I2);
}

//подвійний перерахунок інтегралу для досягнення потрібної точності
double Integral_g_L_II(double(*f)(double), double a, double b, int k, double eps)
{
	int n = (int)(1 / sqrt(eps));
	if (n % 2 == 0) n += 2;
	else n += 1;
	double I_h1, I_h2;

	I_h2 = Integral_g_L(f, a, b, k, n);

	do {
		I_h1 = I_h2;
		n = n * 2;
		I_h2 = Integral_g_L(f, a, b, k, n);
	} while ((abs(I_h1 - I_h2) / 15) > eps);

	return I_h2;
}

//розраховуємо коефіцієнт Ак
double calculate_A(int k, double(*f)(double), double a, double b, double eps)
{
	return Integral_g_L_II(f, a, b, k, eps)*(2 * k + 1) / 2;
}

//коефіцієнти апроксимації многочленами Лежанра
double* apLegendre(double(*f)(double), double a, double b, int n)
{
	double* A = new double[n];
	double eps = 0.0000001;
	for (int i = 0; i<n; ++i)
	{
		A[i] = calculate_A(i, f, a, b, eps);
	}
	return A;
}

//Повертає значення многочлену в точці x
double Pm(double x, double a, double b, double* A, int n)
{
	double  t = (2 * x - (b + a)) / (b - a);
	double y = 0;
	for (int j = 0; j<n; ++j) y += A[j] * L(j, t);
	return y;
}

//Рахуємо і записуєм результат у файл
void AnswerLegendre(double(*f)(double), double a, double b, double eps)
{
	ofstream fout("result_l.txt");
	int k = (b - a) * 25;//кількість точок, які обчислюються на заданому інтервалі
	double step = (b - a) / k;
	double x, y;
	double* A;
	int n = 2; //перше наближення кількості многочленів n = 2+1 = 3
	double LSD; //Least Squares Deviation - середнє квадратичне відхилення

	do{
		n = n * 2;
		A = apLegendre(f, a, b, n);
		LSD = 0;
		x = a;
		for (int i = 0; i <= k; ++i)
		{
			y = f(x) - Pm(x, a, b, A, n);
			LSD += y*y;
			x += step;
		}
		LSD = sqrt(LSD) / (k + 1);
		cout << "LSD[" << n << "] = " << LSD << endl;
	} while (LSD>eps);

	cout << "N = " << n << endl;
	x = a;
	for (int i = 0; i <= k; ++i)
	{
		y = Pm(x, a, b, A, n);
		if (abs(x)<10e-6) fout << 0;
		else fout << x;
		fout << " ; ";
		if (abs(y)<10e-6) fout << 0;
		else fout << y;
		fout << endl;
		x += step;
	}

	fout.close();
	return;
}
