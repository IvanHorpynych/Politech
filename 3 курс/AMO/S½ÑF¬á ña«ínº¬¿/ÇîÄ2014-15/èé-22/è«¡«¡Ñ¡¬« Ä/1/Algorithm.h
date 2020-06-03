#pragma once

#include <iostream>
#include <math.h>

class Task1
{
	public:
		Task1(double a, double b);
		Task1(double xi, int RowLength);
		~Task1();
		
		double Rn();
		double dPartFunc(double eps);
		double AbsMistake();
		double AbsMistake(double eps);
		int factorial(int n);
		int ReturnLength();
		double ReturnLastElem();
		

		double dExp;
	private:
		double x; //double
		double wPart;
		double dPart;
		int RowLength;
		double LastElem;
		int as;
		
		//int RowLength;
		//int n;
};