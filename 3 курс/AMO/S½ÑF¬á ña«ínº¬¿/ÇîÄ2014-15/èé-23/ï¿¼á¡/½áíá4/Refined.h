#ifndef _REFINED_H
#define _REFINED_H
#pragma once
#include "Trapezium.h"

class Refined
{
public:
	double Solve (double eps, double &fault); //return step 
private:
	Trapezium trapez;
	double find_step (int n);
	double Rest (int n);
};
#endif