#include <iostream>
#include <iomanip>
#include <vector>
#include <math.h>
using namespace std;

#define a	-575
#define b	575
#define M4	5
#define EPS	1e-2
#define ROWS 3

typedef struct
{
	double result;
	double error;
}integration_result;


double f (double x)
{
	return 1/(1 + x*x);
}

double F (double x)
{
	return atan(x);
}



integration_result composite_simsons_rule (double eps)
{
	double h = sqrt(sqrt(180 * eps /(double) ((b - a) * M4)));
	long n = (b - a) / h;
	h = (b - a) / (double)n;

	double sum = f(a) + f(b);

	for (long long i = 1; i <= n - 1; i += 2)
		sum += 4 * f(a + i * h);

	for (long long i = 2; i <= n - 2; i += 2)
		sum += 2 * f(a + i * h);

	integration_result res;
	res.result = h * sum / (double)3;
	res.error = abs((F(b) - F(a)) - res.result);
	return res;
}



// Refined calculation
integration_result refined_calculation (long n)
{
	double h = (b - a) / (double)n;

	double sum = f(a) + f(b);

	for (long long i = 1; i <= n - 1; i += 2)
		sum += 4 * f(a + i * h);

	for (long long i = 2; i <= n - 2; i += 2)
		sum += 2 * f(a + i * h);

	integration_result res;
	res.result = h * sum / (double)3;
	res.error = abs((F(b) - F(a)) - res.result);
	return res;
}



int main()
{
	// 1
	integration_result Ih;
	vector<double> epses;
	cout << "Simson's rule :"<<endl;
	for (int i = 0; i < ROWS; i++)
	{
		double eps;
		if (!i)
			eps = EPS;
		else
			eps = Ih.error;

		Ih = composite_simsons_rule(eps);
		cout <<  eps << ",  "  << sqrt(12 * eps / ((b - a) * M4)) << ",  " << Ih.result << ",  " << Ih.error << endl;
		epses.push_back(Ih.error);
	}

	// 2
	integration_result In, I2n;
	cout << "Refined calculation :"<<endl;
	for (int i = 0; i < epses.size(); i++)
	{
		long n = 1 / sqrt(sqrt(epses[i]));
		do
		{
			In = refined_calculation(n);
			I2n = refined_calculation(2 * n);
			n *= 2;
		}
		while (abs(In.result - I2n.result) / (double)15 > epses[i]);
		

		cout << epses[i] << ",  "<< (b - a) / (double)n << ",  " << I2n.error << endl;
	}
	return 0;
}