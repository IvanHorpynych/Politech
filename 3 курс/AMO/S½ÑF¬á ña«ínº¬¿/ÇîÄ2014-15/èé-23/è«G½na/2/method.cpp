#include "header.h"


BisSuccMeth::~BisSuccMeth() { /*empty */}

 double BisSuccMeth::func(double x)
{
	if (x != 0)
		return ( 3 / (2 + cos(x)) - (x / 4) );

	return 1E-14;
}

 int BisSuccMeth::direvative(double x)
{
	if (x != 0){
		double res = ((3 * sin(x)) / pow(cos(x) + 2, 2)) - 1/4; 

	   if (res > 0)
	     return 1; 
	   if (res < 0)
	     return -1;
	 }
  return 1E-14;
}

 double BisSuccMeth::Iteration(double x0,  double lambda, double eps, double q) 
 {
    x = x0;
	step = 1;
	double err = 0.0;
	double x1 = x - lambda * func(x) * direvative(x);

	  while ((abs(x1 - x) > eps) && (step <= mstep)){
		   ++step;
		   x = x1;
		   x1 = x - lambda * func(x) * direvative(x);
	  }
	 err = (q / (1 - q) * (x1 - x));
	 x = x1;
		 
	return err;
 }

 double BisSuccMeth::Bisection(double a0, double b0, double eps){
    double a = a0, b = b0, x1 = (b + a) / 2, err = 0.0;
    step=1;

	   if (func(x1) * func(b) < 0)
		  a = x1;

	   if (func(x1) * func(a) < 0)
		  b = x1;

	   while ((abs(b - a) >  eps) /*&& ((b-a)) != (b0 - a0) / pow2*/){
		  if (func(x1) * func(b) < 0)
			a = x1;

		  if (func(x1) * func(a) < 0)
			b = x1;
			x1 = (b + a) / 2;
			step++;
	   }

	   err = (b - a) / 2;
	   x = x1;
   return err;
}

 void BisSuccMeth::RunIteration( double a, double b, double m, double M){
	double eps = 1E-2;
	double lambda = 1 / M;
	double q = 1 - m / M;
	step = 0;
    double err = 0.0;

		printf("\nMethod of Successive:\n");
		printf("%-10s%-11s%-12s%-10s %" , "EPS", "| root", "| accuracy", "| Steps |");
		printf("\n");

		for (int i = 0; i < 5; i++){
			x = 0.0; 
			// Execution of the iteration method implementation
			err = Iteration(a, lambda,/* (1 - q) / q * */eps, q);
			// Output
			printf("%.0e%5s", eps, "|");
			printf("%2f%2s ", x, "|");
			printf("%f%4s", err, "| ");
			printf("%.4d%3s ", step, "|");
			printf("\n");
			eps *= 1E-3;
		}
}

 void BisSuccMeth::RunBisection(double a, double b){
	 double eps = 1E-2, err =0.0;
     step = 0;

	    printf("\nBisection method:\n");
	    printf("%-10s%-11s%-12s%-10s %", "EPS", "| root", "| accuracy", "| Steps |");
	    printf("\n");

		for (int i = 0; i < 5; i++){
    		  x = 0.0;
		  // Execution of the iteration method implementation
			  err = Bisection(a, b, eps);	
		 // Output
			 printf("%.0e%5s", eps, "|");
			 printf("%2f%2s ", x, "|");
     		 printf("%.0e%4s", err, "| ");
			 printf("%.4d%3s ", step, "|");
			 printf("\n");
			  eps *= 1E-3;
		}

}