#include "sinhx.h"
#include <iostream>

Sinh::Sinh() {}
Sinh::~Sinh() {}

Result Sinh::AccuracyValue(double x, double eps) {
	double U, result = 0;
	int k;
	double lib_sin = sinh(x);
	Result Res;
	
	U = x;
	for(k = 1; abs(U) >= eps; ++k) 
		{
			result += U;
			U *= x*x/(2*k * (2*k + 1));
		}
	
	
	Res.absEr = abs(result - lib_sin);
	Res.n = k;
	Res.remT = U;
	Res.f_x = result;
	return Res;
}

Result Sinh::AbsoluteError(double x, int n) {
	double U, result = 0;
	double lib_sin = sinh(x);
	int k = 1;
	Result Res;

	Res.remT = 0;
	
	if(n > 0) 
	{
		U = x;
		x *= x;
		
			while(--n) 
			{
				result += U;
				U *= x/(2*k * (2*k + 1));
				k++;
			}
		
		
		Res.remT = U;
	}
	Res.absEr = abs(result - lib_sin);
	Res.n = k;
	Res.f_x = result;

	return Res;
}


