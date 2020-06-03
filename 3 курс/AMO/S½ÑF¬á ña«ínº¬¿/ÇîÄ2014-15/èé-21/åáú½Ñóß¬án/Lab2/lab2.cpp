#include "lab2.h"
#include <math.h>
#include <stdlib.h>
#include <stdio.h>

double derivative(double x) {
    double y;
    y = 0.6*x*x - 4*x*x*x + sin(x);
    return fabs(y);
}

double my_function(double x){
    return 0.2*x*x*x - x*x*x*x - cos(x) + 1.5;
}

double fi_function(double x, double lamda){
    return x - lamda*my_function(x);
}

int iteration(double x_curr, double x_next, double eps, double q){
    double l_part, r_part;
    l_part = fabs(x_next - x_curr);  // |X(n-1) - Xn|  
    r_part = (1 - q) / q*eps;
    if (l_part <= r_part){
	    return 1;
    }
    return 0;
}

int chord(double x_curr, double eps, double m1){
    double verific;
    verific = fabs(my_function(x_curr)) / m1;  // nablugennya
    if (verific <= eps){
	    return 1;
	}
    return 0;
}

void computation(){
    double m1[2], M1[2], q[2], lamda[2];
    const double a[2] = { -1, 1 }, b[2] = { -0.5, 1.5 };
    double x_curr, x_next;
    int n_iterat[2][4], n_chord[4];
    double eps;

    double c[2];
    double x_beg[2];

    for (int i = 0; i < 2; i++){
	    double tmp = 0;
	    M1[i] = derivative(a[i]);
		tmp = derivative(b[i]);
		if (tmp > M1[i]){  // if m > M
			m1[i] = M1[i];
			M1[i] = tmp;
		}
		else{
			m1[i] = tmp;
		}
		q[i] = 1 - m1[i] / M1[i];
	}

	lamda[0] = 1 / M1[0];
	lamda[1] = -1 / M1[1];

	printf("First (Integration) Method \n");
	printf("\n_____Eps___________Equating Root__________Correctness______\n");
	eps = 1e-2;
	x_curr = 0;
	for (int i = 1; i <= 4; i++){
		for (int j = 0; j < 2; j++){
			int n = 0;
			double mark_error = 0;
			x_next = (a[j] + b[j]) / 2;
			do{
				x_curr = x_next;
				x_next = fi_function(x_curr, lamda[j]);
				n++;
			} while (iteration(x_curr, x_next, eps, q[j]) == 0);
			mark_error = fabs(x_next - x_curr)*q[j] / (1 - q[j]);
			printf("               | %19.15f | %19.15f | \n", x_curr, mark_error);
			if (j == 0){
				n_iterat[0][i - 1] = n;
				printf(" %e |                     |                     |\n", eps);
			}
		}
		printf("_______________|_____________________|_____________________|\n");
		eps = eps*1e-3;
	}

	printf("\n");

	printf("Secant (Chord) Method\n");
	printf("\n_____Eps___________Equating Root__________Correctness______\n");
	c[0] = a[0];
	x_beg[0] = b[0];
	c[1] = b[1];
	x_beg[1] = a[1];
	eps = 1e-2;
	for (int i = 1; i <= 4; i++){
		for (int j = 0; j < 2; j++){
			int n = 0;
			double mark_error = 0;
			x_next = x_beg[j];
			do {
				x_curr = x_next;
				x_next = x_curr - my_function(x_curr) / (my_function(x_curr) - my_function(c[j]))*(x_curr - c[j]);
				n++;
			} while (chord(x_curr, eps, m1[j]) == 0);

			mark_error = fabs(my_function(x_curr)) / m1[j];
			printf("               | %19.15f | %19.15f | \n", x_curr, mark_error);
			if (j == 0){
				n_iterat[1][i - 1] = n;
				printf(" %e |                     |                     |\n", eps);
			}
		}
		printf("_______________|_____________________|_____________________|\n");
		eps = eps*1e-3;
	}

	printf("\n");

	printf("______eps________Iter____Chord__\n");
	eps = 1e-2;
	for (int i = 0; i< 4; i++){
		printf(" %e | %5d |%6d |\n", eps, n_iterat[0][i], n_iterat[1][i]);
		eps = eps*1e-3;
	}
	printf("_______________|_______|_______|\n");
}
