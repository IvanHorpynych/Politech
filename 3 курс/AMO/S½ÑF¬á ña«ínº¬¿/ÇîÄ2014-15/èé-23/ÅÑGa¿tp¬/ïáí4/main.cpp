#include <iostream>
#include "DefiniteIntegral.h"
#include<conio.h>
using namespace std;
int main(int argc, char **argv) {
	DefiniteIntegral DIntegral;
	const int n = 4;
	long double eps = 1e-2;
	long double delta[n];
	DIntegral.SetBorder(1, 15);
	cout.setf(ios_base::fixed, ios_base::floatfield);
	cout.precision(15);
	cout << "_______Eps________________Step_______________Value_____________Getting_Error__" << endl;
	for (int i = 0; i < n; i++){
		DIntegral.SetEps(eps);
		DIntegral.IntegrationSimpsonRule();
		delta[i] = DIntegral.GetAbsError();
		cout << eps << " | " << DIntegral.GetIntegrationStep() << " | " << DIntegral.GetExactValueIntegral() << " | "
			<< delta[i] << endl;
		eps = eps*1e-3;
	} cout << endl;
	cout << "_______delta_____________Step_____________Getting_Error____" << endl;
	for (int i = 0; i < n; i++){
		DIntegral.SetEps(delta[i]);
		DIntegral.IntegrationSimpsonRule();
		cout << delta[i] << " | " << DIntegral.GetIntegrationStep() << " | " << DIntegral.GetAbsError() << " | " << endl;
	}
	_getch();
	return 0;
}