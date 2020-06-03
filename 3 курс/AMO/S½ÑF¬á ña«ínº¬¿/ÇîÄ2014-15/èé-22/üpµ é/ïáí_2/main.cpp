#include "Function.h"

int main(){
	cout << "Newton-Rawson Method:\n";
	NewtonRawsonMethod(-1.7, -1.1, 16);
	cout << "Method of Successive Approximation:\n";
	methodOfSuccessiveApproximations(-1.7, -1.1, 16, 43);
	cout << "Newton-Rawson Method:\n";
	NewtonRawsonMethod(3.9, 4.2,-136);
	cout << "Method of Successive Approximation:\n";
	methodOfSuccessiveApproximations(3.9, 4.2,-136,-67);
}