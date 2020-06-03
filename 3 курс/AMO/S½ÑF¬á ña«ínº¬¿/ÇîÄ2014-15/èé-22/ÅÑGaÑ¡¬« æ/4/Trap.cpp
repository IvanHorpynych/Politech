#include "Trap.h"

Trap::Trap()
{
	this->a = 1;
	this->b = 23;
	this->Pm=0;
	
	this->eps = 1E-3;
	//this->h = 1/pow(eps, 1/r);
	this->n=1/sqrt(eps);
	//solution();
}

Trap::~Trap()
{


}

void Trap::solution()
{
	//double  met=Method1();
	double  val=RealValue();

	std::cout<<"Error  "<<"        Step integ "<<"     Real val integ "<<" val error \n";
	std::cout<<eps<<"   "<<h<<"    "<< val<<"         "<< abs(Method1()-RealValue())<<"\n";

	std::cout<<"Error  "<<"        Step integ "<<"       val error \n";
	std::cout<<eps<<"   "<<h<<" "<<"   "<< abs(Method2()-RealValue())<<"\n";

}

double Trap::Func(double x)
{
	return (log(x)*log(x))/x;
	//return (1.2*tanh(x)*cos(x)*x)-Pm;
}

void Trap::a_b_in(int a,int b)
{
 this->a=a;
 this->b=b;
}

void Trap::Pm_in(double a)
{
	this->Pm=a;
}

double Trap::_der(int x)
{
	return pow(log(x),3)/3;
}

double Trap::RealValue()
{
	return this->_der(b) - this->_der(a);
}

double Trap::Integral(int num)
{
	double sum = a;
	_h_c(num);


	double toRet = this->Func(a);
	toRet = toRet/2;
	for(int i = 1; i < num; i++)
	{
		sum += h;
		toRet += this->Func(sum); 
	}
	toRet += this->Func(b)/2;

	return h*toRet;
}

void Trap::_h_c(int x)
{
	this->h = (b-a);
	this->h=h/x;
}

double Trap::Condition(int num)
{
	return abs(this->Integral(num) - this->Integral(num*2)) / (pow(2,r) - 1);
}

double Trap::Direvative2(int x)
{
	//return pow(x,5)/120 + sin(x) + pow(x,3)/6; ---
	return 2*log(x);
}

double Trap::Method1()
{
	double Max = Direvative2(a);

	for(int i = a; i <= b; i++)
	{
		if(Direvative2(i) >= Max)
			Max = Direvative2(i);
	}


	this->h = sqrt((eps*12)/((b-a)*Max));
	n = (b-a)/h;
	this->eps=abs(RealValue() - Integral(n));
	return eps; 
}

double Trap::Method2()
{
	n=1/sqrt(eps);

	while(Condition(this->n) > eps)
	{
		this->n = this->n * 2;
	}

	return Integral(n*2);
}












