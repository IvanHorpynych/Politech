#include <iostream>
#include <cmath>
#include <vector>

//using namespace System ;

class Row
{
public:
	Row();
   ~Row();

   int factor(int i);
   double getel(double prev,int k,double x);  //
   double EXP(double x,double eps);
   double EXP(double x);
   double Rn(double x,int n);

private:
	std::vector<double> arr;
	double x;
	double R;
	   int k;
    double E;
	   int n;
	   int n2;
	double u2;

};