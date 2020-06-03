#include <iostream>
#include <fstream>
#include "sinhx.h"

using namespace std;

int main() {
	ofstream tbl("table.csv");
	const double a = -9.8;
	const double b = 13.9;
	const double h = (b-a)/10;
	double eps;
	double x = (a+b)/2;
	Sinh *sinhx = new Sinh();
	Result result;

	cout << "\t\t TABLE 1\t" << endl;
	cout<<'|'<<" Eps\t"<< '|' <<"  n\t"<< '|' <<" Absolute Error "<< '|' <<" Remainder term"<<endl;
	cout << "-------------------------------------------------"<<endl;
	
	for(eps = 1e-2; eps >= 1e-14; eps *= 1e-3) 
	{
		result = sinhx->AccuracyValue(x, eps);
		cout<< '|' <<eps<<"\t"<< '|' <<"  "<<result.n<<"\t"<< '|' <<" "<<result.absEr<<"\t "<< '|' <<result.remT<<endl;
	}
	cout<<"\n"<<endl;

	int n = sinhx->AccuracyValue(x, 1e-8).n;
	cout << "\t\t TABLE 2\t" << endl;
	cout<< '|' <<"  Xi\t"<< '|' <<" Absolute Error"<< '|' <<" Remainder term"<<endl;
	cout << "----------------------------------------" << endl;
	for (int i = 0; i <= 10; ++i) 
	{
		x = a + h*i;
		result = sinhx->AbsoluteError(x, n);
		tbl << x << ';' << result.absEr << ';' << endl;
		cout<< '|' <<x<<"\t"<< '|' <<" "<<result.absEr<<"\t"<< '|' <<result.remT<<endl;
	}
	system("PAUSE");
	return 0;
}