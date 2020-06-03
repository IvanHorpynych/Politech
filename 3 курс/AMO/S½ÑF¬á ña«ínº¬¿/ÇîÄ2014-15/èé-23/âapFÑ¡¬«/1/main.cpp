#include "MySin.h"
#include <iostream>
#define _USE_MATH_DEFINES
#include <math.h>
using namespace std;
int main()
{
	MySinus msin;
	double a = 51.33, b = -0.55;
	double x = a;
	double h = (b - a) / 10;
	double reduct_x;
	int my_len_N;
	msin.setParam(x, 1e-2);
	msin.calc();
	cout << "________eps_______N_____Absolute_error_____Remainder_term___ " << endl;
	for (double eps = 1e-2; eps >= 1e-15; eps = eps*1e-3){
		msin.increaseEps(eps);
		cout.setf(ios_base::scientific, ios_base::floatfield);
		cout.precision(0);
		cout << "| " << eps;
		if (eps == 1e-8) {
			my_len_N = msin.getN();
		}
		cout << " | " << msin.getN();
		cout.setf(ios_base::fixed, ios_base::floatfield);
		cout.precision(15);
		cout << " | " << msin.getAbsoluteError();
		cout << " | " << msin.getRemaindTerm() << " |" << endl;
	}
	cout << " ----------------------------------------------------------- " << endl;
	cout << endl;
	msin.setStatusN(my_len_N);
	cout << " __________X____(reduct)_|_Absolute_error___|_Remainder_term___ " << endl;
	for (int i = 0; i <= 10; i++){
		x = a + h*i;
		reduct_x = fabs(remainder(x, M_PI_2));
		msin.setParam(x, 1e-8);
		msin.calc();
		cout << "| ";
		cout.setf(ios_base::fixed, ios_base::floatfield);
		cout.width(10);
		cout.precision(5);
		cout << x << " (";
		cout.width(9);
		cout.precision(7);
		cout << reduct_x << ")";
		cout.setf(ios_base::scientific, ios_base::floatfield);
		cout.precision(9);
		cout << " | " << msin.getAbsoluteError();
		cout << " | " << msin.getRemaindTerm() << " |" << endl;
	}
	cout << " --------------------------------------------------------------- " << endl;
	getchar();
	return 0;
}