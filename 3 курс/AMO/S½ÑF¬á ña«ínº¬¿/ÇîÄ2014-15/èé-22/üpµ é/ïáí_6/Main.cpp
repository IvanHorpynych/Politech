#include <iostream>
#include <fstream>
#include <conio.h>
#define _USE_MATH_DEFINES
#include <math.h>
#include "Spline.h"
using namespace std;
//Возвращает значение заданной функции в точке _X
double Fx(double _X)
{
	return (double)(0.1 * _X * _X * sin(3 * _X) * pow(M_E,pow(_X, 1/3)));; 
}
//Возвращает значение сплайна
void Results(double A, double B, int n, int m)
{
	CubSpline Spline;
	double ResSpline;
	double* XVector = new double [n];
	double* FxVector = new double [n];

	double h = (B - A)/n;
	double X = A;
	int i = 0;
	while (X + i*h <= B)
	{
		XVector[i] = X + i*h;
		FxVector[i] = Fx(X + i*h);
		i++;
	}

	Spline.BuildSpline(XVector, FxVector,n);
	cout.precision(5);
	cout<<fixed;
	ofstream f("results.txt");
	cout<<"X	Fx	Spline\n";
	f<<"X	Fx	Spline\n";

	X = A;
	h = (B - A)/m;
	for (int i = 0; i < m; i++)
	{
		ResSpline = Spline.ReturnSplineValue(X);
		cout<<X<<"	"<<Fx(X)<<"	"<<ResSpline<<endl;
		f<<X<<"	"<<ResSpline<<endl;
		X += h;
	}

	f.close();
}
//Главная программа
int main()
{
	double A = 0;
	double B = 6;
	int    n = 50;
	int	   m = 100;
	Results(A, B, n, m);
	_getch();
}