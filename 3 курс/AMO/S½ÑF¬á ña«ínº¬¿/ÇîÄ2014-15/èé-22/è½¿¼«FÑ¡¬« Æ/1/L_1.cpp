#include <cstdlib>												
#include <iostream>												
#include <math.h>
using namespace std;

int sign; // Positive or Negative X indicator

//Ranges
double a = -3.3;
double b = 24.9;

double x;
int k, n_len;

double tmp, U;


//Determinating X sign
void SignX(){
	sign = 1;
	double pi = 3.1415926535897932384626433832795;

	if (x < 0){
		x = -x;
	}

	while (x > pi * 2){
		x -= pi * 2;
	}

	if (x >= pi){
		x -= pi;
		sign = -1;
	}
}

//1st TASK
void FirstCos(){
	tmp = 0;
	double eps = 1E-2;
	U = 1;
	k = 1;

	x = (b - a) / 2;
	//x = a;
	printf("1st task: x := %4.1f    cos( x ) := %4.6f\n\n", x, cos(x));
	printf("%-10s%-10s%-15s%-20s%-20s", "EPS", "| n", "| cos(x[i])", "| Absolute error", "| Remainder term");
	printf("\n------------------------------------------------------------------------\n");

	double real = x;

	SignX();

	while (eps > 1E-14){
		while (abs(U) > eps){
			tmp += U;
			U *= -x * x / (2 * k * (2 * k - 1));
			k++;
		}

		// Storing n count for eps = 10^(-8)
		if (eps == 1E-8){
			n_len = k - 1;
		}

		printf("%.0e%5s", eps, "|");
		printf("%2d%8s ", k - 1, "|");
		printf("%f%7s", sign * tmp, "| ");
		printf("%.5e%7s ", abs(sign * tmp - cos(real)), "|");
		printf("%.5e\n", abs(U));

		eps /= 1E3; // Changing by 10^(-3)
	}
	printf("\n");
}

//2nd TASK
void SecondCos(){
	printf("2nd task:\n\n");
	printf("%-7s%-15s%-14s%-20s%-20s", "x[i]", "| cos(x[i])", "| f", "| Absolute error", "| Remainder term");
	printf("\n----------------------------------------------------------------------------\n");

	for (int i = 0; i <= 10; i++){
		double h = (b - a) / 10;
		x = a + h * i;
		double Xi = x;

		printf("%-.2f%2s", x, " ");
		printf("%9f%7s", cos(x), " ");

		SignX();
		tmp = 0;
		U = 1;

		for (int n = 1; n <= n_len; n++){
			tmp += U;
			U *= -x * x / (2 * n * (2 * n - 1));
		}

		printf("%9f%7s", sign * tmp, " ");
		printf("%.5e%7s ", abs(sign * tmp - cos(Xi)), " ");
		printf("%.5e\n", abs(U));
	}
	printf("\n");
}
int main(){

	//1st TASK
	FirstCos();

	//2nd TASK
	SecondCos();

	system("pause");
	return 0;
}
