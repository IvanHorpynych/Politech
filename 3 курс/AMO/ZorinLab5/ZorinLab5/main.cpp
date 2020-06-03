#include "main.h"

double func(double x)
{
	return 1/x - 0.1*x*x*sin(2*x);
}

double lejandr(int n, double x)
{
	double Pn1, Pn = x, Pn_1 = 1;
	if (n == 0) return Pn_1;
	int i = 1;
	while (i < n)
	{
		Pn1 = ((2 * n + 1) / (n + 1))*x*Pn - (n / (n + 1))*Pn_1;
		Pn_1 = Pn;
		Pn = Pn1;
		++i;
	}
	return Pn;
}

double* constA(double a, double b, double eps, int N, function<double(double)> f)
{
	double ** A = new double*[N + 1];
	int i, j;
	for (i = 0; i < N + 1; i++)
	{
		A[i] = new double[N + 2];
	}

	for (i = 0; i < N + 1; i++)
	{
		for (j = 0; j < N + 1; j++)
			A[i][j] = integrate(a, b, eps, [&](double x) {return lejandr(i, x) * lejandr(j, x); });
		A[i][N + 1] = integrate(a, b, eps, [&](double x) {return lejandr(i, x) * f(x); });
	}
	auto res = gaussian_elimination(A, N + 1);
	return res;
}

double value(double*A, double x, int N)
{
	double Pn1, Pn = x, Pn_1 = 1, sum;

	sum = Pn_1*A[0];
	if (N == 0) return sum;
	sum += Pn*A[1];
	int i = 1;
	while (i < N + 1)
	{
		Pn1 = ((2 * N + 1) / (N + 1))*x*Pn - (N / (N + 1))*Pn_1;
		Pn_1 = Pn;
		Pn = Pn1;
		sum += Pn*A[i + 1];
		++i;
	}
	return sum;
}

int main()
{
	ofstream table("table.csv");
	
	for (int n = 10; n < 100; n++)
	{
		
		double* A = constA( 2, 11, 0.00001, n, func);
		if (sqrt((integrate(2, 11, 0.00001, [&](double x) {return (func(x) - value(A,x,n)) * (func(x) - value(A,x,n)); })) / 9) < 0.01)
		{
			for (double x = 2; x <= 11; x += 0.5)
			{
				cout << x << ";   " << value(A,x,n) << ";   "<<endl;
				table << x << ";" << value(A,x,n) << ";"<<endl;
			}
			cout << endl;
			cout << "Polin. order=" << n << endl;
			break;
		}
	}
	
		return 0;
	
}
