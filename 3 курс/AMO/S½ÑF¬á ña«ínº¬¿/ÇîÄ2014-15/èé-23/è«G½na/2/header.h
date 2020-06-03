#include <cstdlib>												
#include <iostream>												
#include <math.h>

class BisSuccMeth {
private: 
	int mstep;
	int step;
	double x;

	double func(double x);
	int direvative(double x);
	double Iteration(double x0,  double lambda, double eps, double q);
    double Bisection(double a0, double b0, double eps);

public: 	
	BisSuccMeth():mstep(255),step(0),x(0.0){} ;	  
	~BisSuccMeth();
	
	void RunIteration( double a, double b, double m, double M);
    void RunBisection(double a, double b);
};