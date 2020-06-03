#ifndef _TRAPEZIUM_H
#define _TRAPEZIUM_H
#pragma once
#include "equation.h"

class Trapezium 
{
public:
	double Solve (double step); 
	double Exact ();
private: 
	Equation eq;
	double Rest (double x, double step);
};
#endif