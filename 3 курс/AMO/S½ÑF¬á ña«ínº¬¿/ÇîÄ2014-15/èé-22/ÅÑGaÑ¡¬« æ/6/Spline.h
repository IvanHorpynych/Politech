#include <iostream>
#include <fstream>
#include <iostream>
#include <math.h>

class Spline
{
public:
	Spline();
	~Spline();
	void SplineInterpolation(double N);
private:

	double MyFuction(double x);
	double* findAi(double N);
	double Fi(int i, double N);
	double* findCi(double N);
	double* findDi(double* C, double N);
	double* findBi(double* C, double* D, double N);
	double Splin(double x, double* A, double* B, double* C, double* D, double N);


	double a,b;

};