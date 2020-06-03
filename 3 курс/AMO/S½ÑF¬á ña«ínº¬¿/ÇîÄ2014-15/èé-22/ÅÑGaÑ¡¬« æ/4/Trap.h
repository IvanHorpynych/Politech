#include <iostream>
#include <cmath>

class Trap
{
public:
	Trap();
	~Trap();
	void solution();
	
	double Method1();
	double Method2();
	  void a_b_in(int a,int b);
	  void Pm_in (double a);

private:
	

	double Direvative2(int x);

	double Condition(int num);
	double RealValue();
	double Integral(int num);
	double Func(double x);
	double _der(int x);
	  void _h_c(int x);


	double eps;
	double Pm;
	int n;
	int a;
	int b;
	int r;
	double h;
};