#include "header.h"

double MyFunction(double a){
	return 5 * a * a * a- 6 * exp(a) + 15;
}
//1 похідна
double FirstDerivative(double a){
	return 15 * a *a - 6 * exp(a);
}

//2 похідна
double SecondDerivative(double a){
	return 30 *a - 6 * exp(a);
}

void MinMaxDerivative(double a, double b, double &min, double &Max, double &x){
	if (FirstDerivative(a) < FirstDerivative(b)){
		min = FirstDerivative(a);
		Max = FirstDerivative(b);
		x = b;
	}
	else{
		min = FirstDerivative(b);
		Max = FirstDerivative(a);
		x = a;
	}
}

void Sign(double a, double b, double &x){
	while ((SecondDerivative(a) == 0) || (SecondDerivative(b) == 0)){
		printf("Border has been changed! \n");
		a += 0.01;
		b -= 0.01;
	}
	if (SecondDerivative(a)*MyFunction(a) > 0){
		x = a;
	}
	else{
		if (SecondDerivative(b)*MyFunction(b) > 0){
			x = b;
		}
		else{
			printf("Can't find correct start argument for Raphson Method");
		}
	}
}

//метод ітерацій
int Approximation(double x, double min, double Max, double &eps, double &ApproximationPrecision, double &ApproximationEquationRoot){
	double lambda = 1 / Max, q = (1 - min) / Max;
	int n = 1;
	double x0 = x, x1 = 0, difference = 0, condition = abs((1 - q) / q * eps);//умова

	x1 = x0 - lambda * MyFunction(x0);
	difference = abs(x1 - x0);
	while (difference > condition){
		x1 = x0 - lambda * MyFunction(x0);
		difference = abs(x1 - x0);
		x0 = x1;
		n++;
	}
	ApproximationEquationRoot = x1;
	ApproximationPrecision = abs(q * (difference) / (1 - q));
	return n;
}

//метод дотичних
int Raphson(double x, double min, double &eps, double &RaphsonPrecision, double &RaphsonEquationRoot){
	int n = 1;
	double m1 = min, x0 = x, x1 = 0;

	x1 = x0 - MyFunction(x0) / FirstDerivative(x0);
	while (abs(MyFunction(x1) / m1) > eps){
		x1 = x0 - MyFunction(x0) / FirstDerivative(x0);
		x0 = x1;
		n++;
	}
	RaphsonEquationRoot = x1;
	RaphsonPrecision = abs(MyFunction(x1) / m1);
	return n;
}
