#include <stdlib.h>
#include <iostream>
#include <fstream>
#include <iomanip>
#include <cstdlib>
#include <math.h>
#include <cstdlib> 

using namespace std;

int main()
{
	const double a = 0.0, b = 1.0; 
	double u, x, eps, h, cosh_x; 
	int i, n;	
	ofstream reportFile;

	reportFile.open("Res.csv", ios::app);

	cout.precision(16);
	cout << "                               Table # 1\n";
	cout << "\n";
	cout << "  ....................................................................\n";
	cout.width(12);
	cout << "EPS" << " :";
	cout.width(6);
	cout << "N" << " :";
	cout.width(22);
	cout << "ABSOLUTE ERROR " << " :";
	cout.width(22);
	cout << "REMAINDER TERM" << " :\n";
	cout << "  ....................................................................\n";

	eps = 0.01;
	x = (b + a) / 2;
	while (eps > 0.00000000000001)
	{
		i = 0;
		u = 1.0;
		cosh_x = u;

		while ((2 * u / 3) > eps) 
		{
			i++;
			u = x*x*u / (2 * i*(2 * i - 1));
			cosh_x = cosh_x + u;
		}
		cout.width(12);
		cout << eps << " :"; 
		cout.width(6);
		cout << i << " :"; 
		cout.width(22);
		cout << fabs(cosh(x) - cosh_x) << " :"; 
		cout.width(22);
		cout << 2 * u / 3 << " :\n";
		eps = eps*0.001;
	}

	cout << "  ....................................................................\n";
	cout << "\n";
	cout << "                               Table # 2\n";
	cout << "\n";
	cout << "  ............................................................\n";
	cout.width(12);
	cout << "X(i)" << " :";
	cout.width(22);
	cout << "ABSOLUTE ERROR " << " :";
	cout.width(22);
	cout << "REMAINDER TERM" << " : \n";
	
	cout << "  ............................................................\n";
	
	eps = 0.00000001; 
	h = (b - a) / 10;
	for (int j = 0; j <= 10; j++)
	{
		x = a + h*j;

		i = 0;
		u = 1.0;
		cosh_x = u;

		while ((2 * u / 3) > eps) 
		{
			i++;
			u = x*x*u / (2 * i*(2 * i - 1));
			cosh_x = cosh_x + u;
		}
		printf("%13.1f :", x);
		cout.width(22);
		cout << fabs(cosh(x) - cosh_x) << " :"; 
		reportFile << x << ",";
		reportFile << fabs(cosh(x) - cosh_x) <<endl;
		cout.width(22);
		cout << 2 * u / 3 << ": \n"; 

	}
	cout << "  ............................................................\n";
	reportFile.close();
	system("pause");
	return 0;
}