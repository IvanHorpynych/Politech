#include <iostream>
#include <math.h>
#include "Approx_Legendre.h"

using namespace std;

double f(double x){
	return 0.7*x*x*sin(x);
}

int main(){
	double a = 2;
	double b = 17;
	double eps = 10e-2;
	cout << "Calculating..." << endl;
	AnswerLegendre(f, a, b, eps);
	cout << "Ok" << endl;
	getchar();
	return 0;
}
