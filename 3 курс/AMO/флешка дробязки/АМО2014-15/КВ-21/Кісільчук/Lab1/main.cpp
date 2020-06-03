#include "main.h"

double absolute_x(double x,int* znak){
	*znak = 1;
	if (x < 0)
		x = -x;
	while (x >= 2 * M_PI)
		x -= 2 * M_PI;
	if (x >= M_PI)
		x = 2 * M_PI - x;
	if (x >= M_PI_2){
		x = M_PI - x;
		*znak = -1;
	}
	return x;
}

void task1(double a, double b){
	double absolut_cosx = cos(a);
	//double absolut_cosx = cos((a + b) / 2);
	int znak;
	//double x = absolute_x((a + b) / 2, &znak);
	double x = absolute_x(a, &znak);
	printf("eps\t\tn\tabsolute error\t\tremaining member\n");
	double Ui, R, sumU;
	int t;
	double Ui_r;
	if ((x >= 0) && (x < M_PI_4)){
		Ui_r = 1;
		t = -1;
	}
	else{
		x = M_PI_2 - x;
		Ui_r = x;
		t = 1;
	}
	int j;
	for (double eps = 1e-2; eps >= 1e-14; eps *= 1e-3){
		sumU = 0;
		Ui = Ui_r;
		for (j = 1;; j++){
			sumU += Ui;
			Ui = -(x*x) / (2 * j*(2 * j + t))*Ui;
			if (fabs(Ui) < eps){
				R = Ui;
				break;
			}
		}
		sumU *= znak;
		printf("%g\t\t%d\t%2.20f\t%2.20f\n", eps, j, fabs(absolut_cosx - sumU), fabs(R));
	}
}

void task2(const char* fname, double a, double b, int n){
	printf("xi\t  absolute error\t\tremaining member\n");
	ofstream csv(fname);
	double h = (b - a) / 10;
	double sumU, Ui, R, err, absolut_cosx;
	int znak;
	for (int i = 0; i <= 10; i++){
		double x = a + h*i;
		double xi = absolute_x(x,&znak);
		sumU = 0;
		int t;
		if ((xi >= 0) && (xi < M_PI_4)){
			Ui = 1;
			t = -1;
		}
		else{
			xi = M_PI_2 - xi;
			Ui = xi;
			t = 1;
		}
		for (int j = 1;; j++){
			sumU += Ui;
			Ui = -(xi*xi) / (2 * j*(2 * j + t))*Ui;
			if (j == n + 1){
				R = Ui;
				break;
			}
		}
		sumU *= znak;
		absolut_cosx = cos(x);
		err = fabs(absolut_cosx - sumU);
		printf("%-10.3g%-30.25f%2.25f\n", x, err, fabs(R));
		csv << x << ',' << err << '\n';
	}
	csv.close();
}

int main(){
	const double a = -3.3;
	const double b = 24.9;
	task1(a, b);
	int n = 3;
	printf("\n\n");
	task2("lab1.csv", a, b, n);
}