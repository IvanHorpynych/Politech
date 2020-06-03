#pragma once
#include <math.h>


double min_abs(double (*f)(double),double a,double b, int steps)
{
	if (b<a)
	{
		double tmp = a;
		a = b;
		b = tmp;
	}

	double x = a,m,min;
	double delta = fabs(b-a)/steps;

	min = fabs(f(x));
	x+=delta;

	while(x<=b)
	{
		m = fabs(f(x));
		if (min>m) min = m;
		x+=delta;
	}
	return min;
}


double iteration(double (*f)(double), double (*f_1)(double),
				 double a, double b, double eps,
				 int max_iteration, int *number_of_iterations,int *st)
 
 
{
	
	(*st) = 0;
	(*number_of_iterations) = 0;

	if (f(a)*f(b)>0)
	{
		(*st) = 1;
		return 0.0;
	}
	if (f(a)==f(b))
	{
		(*st) = 2; 
		return a;
	}
	if (f(a)==0)
	{
		(*st) = 3; 
		return a;
	}
	if (f(b)==0)
	{
		(*st) = 4; 
		return b;
	}

	


	if (a>b) 
	{
		double temp = a;
		a = b;
		b = temp;
	}

	
	double min_f_1,max_f_1,lamb,q,x,x1;
	int alpha;

	(f_1((a + b)/2)>0)? (alpha=1):(alpha=-1);
		
	min_f_1 = alpha*f_1(a);
	max_f_1 = alpha*f_1(b);

	if (min_f_1 > max_f_1) 
	{
		double temp = min_f_1;
		min_f_1 = max_f_1;
		max_f_1 = temp;
	}

	lamb = 1/max_f_1;
	q = 1 - min_f_1/max_f_1;

	x = (a + b)/2; 
	do {
		x1 = x;
		x = x1 - lamb*alpha*f(x1);
		++(*number_of_iterations);

	}while ( ( fabs(x - x1)>(((1-q)/q))*eps)&&( (*number_of_iterations)<=max_iteration) ) ;

	if ((*number_of_iterations)>max_iteration) (*st) = 5; 

	return x;
}


double chord(double (*f)(double), double (*f_1)(double), double (*f_2)(double),
				 double a, double b, double eps,
				 int max_iteration, int *number_of_iterations,int *st)

{
	
	(*st) = 0;
	(*number_of_iterations) = 0;

	if (f(a)*f(b)>0)
	{
		(*st) = 1; 
		return 0.0;
	}
	if (f(a)==f(b))
	{
		(*st) = 2; 
		return a;
	}
	if (f(a)==0)
	{
		(*st) = 3; 
		return a;
	}
	if (f(b)==0)
	{
		(*st) = 4; 
		return b;
	}


	if (a>b) 
	{
		double temp = a;
		a = b;
		b = temp;
	}

	
	double m1,c,x,x1;
	int alpha;

	(f_2((a + b)/2)>0)? (alpha=1):(alpha=-1);

	m1 = min_abs(f_1,a,b,10000);
		
	if (alpha*f(a)>0)
	{
		c=a;
		x=b;
	}
	else
	{
		c=b;
		x=a;
	}

	do {
		x1 = x;
		x = x1 - alpha*f(x1)*(x1 - c)/(alpha*f(x1) - alpha*f(c));
		++(*number_of_iterations);

	}while ( ((fabs(f(x))/m1) >= eps )&&( (*number_of_iterations)<=max_iteration) ) ;

	if ((*number_of_iterations)>max_iteration) (*st) = 5; 

	return x;
}
