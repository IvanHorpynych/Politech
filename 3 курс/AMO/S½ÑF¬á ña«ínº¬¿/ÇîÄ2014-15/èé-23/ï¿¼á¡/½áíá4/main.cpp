#include <stdio.h>
#include "const.h"
#include "Refined.h"
#include "Trapezium.h"

int main () 
{
	Trapezium tr; 
	Refined ref;
	double exact = tr.Exact();
	double tmp = tr.Solve(STEP);
	double delta = abs (exact - tr.Solve(STEP));
	double fault;
	double step = ref.Solve(delta, fault);
	printf ("Using composite trapezium rule\n");
	printf ("EPS = %g Step = %g Exact = %2.10f Delta = %2.14f\n",EPS,STEP, exact, delta);
	printf ("Delta = %g Step = %2.15f Fault = %2.15f\n",delta,step,fault);

}