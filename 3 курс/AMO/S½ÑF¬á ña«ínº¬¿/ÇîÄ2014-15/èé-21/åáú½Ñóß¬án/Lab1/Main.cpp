#include <stdlib.h>
#include <iostream>
#include <fstream>
#include <iomanip>
#include <math.h>
#include <cstdlib> 

using namespace std;

int main()
{
	const double a = -0.8, b = 1.9; 
	double u; 
	double x, eps, h;
	double ch_x;
	int i;
	ofstream reportFile;

	reportFile.open("results.csv", ios::app);
	cout.precision(16);
	cout << "1.1\n";
	cout.width(12);
	cout << "eps";
	cout.width(5);
	cout << "   n";
	cout.width(22);
	cout << "Absolute error";
	cout.width(28);
	cout << "Remaining member" << endl;
	cout << "  ---------------------------------------------------------------------\n";

	eps = 0.01;
	x = (b + a) / 2;
	while (eps > 0.00000000000001) // eps^-14
	{
		i = 0;
		u = 1.0;
		ch_x = u;

		while ((2 * u / 3) > eps) // cosh(x)
		{
			i++;
			u = x*x*u / (2 * i*(2 * i - 1));
			ch_x = ch_x + u;
		}
		cout.width(12);
		cout << eps << " ";
		cout.width(4);
		cout << i<< " ";
		cout.width(26);
		cout << fabs(cosh(x) - ch_x); // absolute error
		cout.width(26);
		cout << 2 * u / 3 << endl; // remainder term Rn
		eps = eps*0.001;
	}

	cout << "\n";
	cout << "1.2\n";
	cout.width(12);
	cout << "Xi";
	cout.width(28);
	cout << "  Absolute error  ";
	cout.width(28);
	cout << "Remaining member  " << endl;
	cout << "  --------------------------------------------------------------------\n";

	eps = 0.00000001; // eps^-8
	h = (b - a) / 10;
	for (int j = 0; j <= 10; j++)
	{
		x = a + h*j;

		i = 0;
		u = 1.0;
		ch_x = u;

		while ((2 * u / 3) > eps) // cosh(x)
		{
			i++;
			u = x*x*u / (2 * i*(2 * i - 1));
			ch_x = ch_x + u;
		}
		printf("%13.1f", x);
		cout.width(28);
		cout << fabs(cosh(x) - ch_x);
		reportFile << x << ",";
		reportFile << fabs(cosh(x) - ch_x) << endl;
		cout.width(28);
		cout << 2 * u / 3 << endl;
	}

	reportFile.close();
	system("pause");
	return 0;
}