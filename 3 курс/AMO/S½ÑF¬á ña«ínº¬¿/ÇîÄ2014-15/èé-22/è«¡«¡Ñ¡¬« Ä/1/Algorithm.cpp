#include "Algorithm.h"


Task1::Task1(double a, double b)
{
	this->x = (b + a)/2;
	x = a;
	this->wPart = exp((int)x);
	this->dPart = x - (int)x;	
}

Task1::Task1(double xi, int RowLength)
{
	//this->elem = pow(x,n)/n; //factorial
	//this->x = (b + a)/2;
	this->x = xi;
	//this->x = 1.4;
	//this->wPart = exp(int(x));
	modf(x,&wPart);
	this->dPart = x - (int)x;
	this->RowLength = RowLength;
	//dExp = dPartFunc(eps);
	//this->eps = epsilon;
}


Task1::~Task1()
{

}

double Task1::dPartFunc(double eps)
{
	double S = 1;
	double U = 1;
	this->RowLength = 1;
	//this->elem = this->wPart;
	//float cur = 0;


	//while(cur < exp(this->x))
	//while(this->elem < exp(this->x))
	while(U >= eps)
	{
		U = U*(this->dPart/this->RowLength);
		S = S + U;
		LastElem = U;
		//this->elem = this->elem + S;
		//cur = cur + S;
		this->RowLength++;
	}
	//this->RowLength++;

	return S;
}

double Task1::ReturnLastElem()
{
	return LastElem;
}

double Task1::Rn()
{
	int n = RowLength;

	double e1 = 1 - (double)abs(x) / (n + 2);
    double e2 = 1 / e1;
    double e3 = (double)pow(abs(x), n + 1);
    double e4 = (factorial(n + 1));
    return e3 / e4 * e2;

}

double Task1::AbsMistake()
{
	return abs(dExp - exp(dPart)); 
}

double Task1::AbsMistake(double eps)
{
	double S = 0;
	double U = 1;
	
	//while(this->elem < exp(this->x))
	for(int i = 1; i <= RowLength; i++)
	{
		S = S + U;
		U = U*(this->dPart/i);
		LastElem = U;
	}

	return abs(S - exp(dPart));
}

int Task1::ReturnLength()
{
	return this->RowLength;
}

int Task1::factorial(int n)
{
	int result = 1;

	if( n == 0 )
		return 1;

	for(int i = 1; i <= n; i++)
	{
		result = result*i;
	}

	return result;
}
