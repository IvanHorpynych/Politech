#include "Function.h" 

double f(double x){
	x = 5*x*x*x - 6 * exp(x) + 15;
	return x;
}
double df(double x){
	x = 15*x*x - 6 * exp(x);
	return x;
}
double ddf(double x){
	x = 30*x - 6 * exp(x);
	return x;
}
void NewtonRawsonMethod(double a, double b, double m){
	double x0, x = 0, E = 0.01; int k;
	if (((ddf(a)<0) && (f(a)<0)) || ((f(a)>0) && (ddf(a)>0))){
		x = a;
		x0 = a;
	}else{
		x = b;
		x0 = b;
	}
	cout << "  E:" << "     " << "  x:" << "     " << "Steps:" << "    " << "  Error:" << "\n";
	cout << "  ";
	for (int i=0;i<5;i++){
		k = 0;
		while (true){ 
			x = x - f(x)/df(x);
			if (abs(f(x)/m)<=E){
				cout << E << "     " << x << "     " << k << "     "<< abs(f(x)/m) <<endl;
				break;
			}else k++;
		}
		x = x0;
		iter[i] = k;
		E = E/1000;
	}
}
void methodOfSuccessiveApproximations(double a, double b, double m, double M){
	double q = 1 - m/M;
	double x,xn, E= 0.01, err;
	int k;
	x = a;
	cout << "  E:" << "     " << "  x:" << "     " << "Steps:" << "    " << "  Error:" << "\n";
	cout << "  ";
	for (int i=0;i<5;i++){
		k = 0;
		while (true){ 
			xn = x - f(x)/M;
			err = abs(((xn - x) * q)/(1 - q));
			if (err<=E){
				cout << E << "     " << x << "     " << k << "     "<<  err <<endl;
				break;
			}else{ 
				k++;
				x = xn;
			}
		}
		x = a;
		iter2[i] = k;
		E = E/1000;
	}
}