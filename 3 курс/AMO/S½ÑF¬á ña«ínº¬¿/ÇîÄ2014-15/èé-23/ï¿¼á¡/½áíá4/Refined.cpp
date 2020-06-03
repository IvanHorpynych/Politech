#include "Refined.h"
#include "const.h"

double Refined::Solve(double eps,double &fault)
{
	int n = ceil (1/sqrt(eps));
	int i;
	int N = n; 
	for (i=1; Rest(N) > eps; i++) 
		N=i*n;
	fault = abs(trapez.Exact() - trapez.Solve(find_step(2*N)));
	return find_step(N);
}
double Refined::Rest (int n) 
{
	return 1.0/3.0*abs(trapez.Solve(find_step(n))-trapez.Solve(find_step(2*n)));
}
double Refined::find_step (int n)
{
	double tmp = (UPPER_LIMIT-LOWER_LIMIT)/n;
	return (UPPER_LIMIT-LOWER_LIMIT)/n;
}