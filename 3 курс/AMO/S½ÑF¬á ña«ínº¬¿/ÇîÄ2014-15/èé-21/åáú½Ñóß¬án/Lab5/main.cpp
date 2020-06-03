#include "header.h"

int main()
{
	double a = 3;
	double b = 10.5;
	double eps = 10e-3;

	cout << "Calculating..." << endl;
	AnswerLegendre(f, a, b, eps);
	
	cout << "Ok" << endl;
	system("pause");

	return 0;
}
