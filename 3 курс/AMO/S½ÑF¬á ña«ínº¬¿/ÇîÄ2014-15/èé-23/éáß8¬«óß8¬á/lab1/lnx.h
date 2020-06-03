#include"stdio.h"
#include"stdlib.h"
#include<iostream>
#include <iomanip>
#define _USE_MATH_DEFINES 
#include <math.h>
#include <cmath>

using namespace std;

double lnX(double eps, int *series_len, double ex, double a1,double *Rn,double *n1)
{
	double result = 0,L = 0;
	int n = 1;
	double Ln = pow(a1,2*n-1)/(2*n-1); 
	while (Ln > (4*eps))
	{	
		L = L + Ln;	
		n++;
		Ln = pow(a1,2*n-1)/(2*n-1);
		
	}
	
	*Rn = Ln; 	
	*series_len = n; 
	result = ex*M_LN2 - 2*L - Ln;
	if (eps == 1e-008) *n1 = n;
	return result;

}
double lnx2(int n, double ex, double a1,double *Rn)
{
	double L = 0,result = 0,Ln = a1;
	*Rn = 0;

	for (int i = 2; i <= n; i++)
	{ 
		L = L + Ln;	
		Ln = pow(a1,2*i-1)/(2*i-1);
	}
	*Rn = Ln;
	result = ex*M_LN2 - 2*L - Ln;
	return result;
}
