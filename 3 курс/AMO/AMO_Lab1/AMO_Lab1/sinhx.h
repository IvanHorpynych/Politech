#pragma once
#define _USE_MATH_DEFINES

#include <cmath>
#include "Result.h"

class Sinh 
{
private:
	//enum Func {	SIN, COS };
	int sign;
	//Func f;
	

public:
	Sinh();
	~Sinh();
	Result AccuracyValue(double x, double eps);
	Result AbsoluteError(double x, int n);
};

