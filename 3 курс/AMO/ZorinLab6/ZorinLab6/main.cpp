#include "methods.h"
using namespace std;



double func(double x)
{
	return 3*x*log10(x)*sin(x/1.7);
}

void main()
{
	ofstream tbl("tbl.csv");
	double a = 2, b = 25, x;
	int n = 100;
	double* A = new double[n];
	double* B = new double[n];
	double* C = new double[n];
	double* D = new double[n];
	double h = (b - a) / n;
	A = defineA(a, b, n);
	C = defineC(A, a, b, n);
	D = defineD(C, a, b, n);
	B = defineB(A, C, D, a, b, n);

	for (int i = 0; i < n; ++i)
	{
		x = a + i*h;
		cout << "x= " << x << ";  " << "f(x)= " << func(x) << ";  " << "ValueSpline= " << getInt(x, a, b, n, A, B, C, D) << ";" << endl;
		tbl << x << ";" << getInt(x, a, b, n, A, B, C, D) << ";" << endl;
	}


	return;
}