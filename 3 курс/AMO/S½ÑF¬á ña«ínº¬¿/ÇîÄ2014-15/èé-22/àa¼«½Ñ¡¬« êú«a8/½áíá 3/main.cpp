#include "function.h"

int main(){
	double tmp;
	double a[n][n]={{3,7,6,0},{13,49,20,15},{20,19,50,10},{17,7,17,17}};
	double b[n] = {63,353,437,225};
	double x[n] = {0,0,0,0};
	cout << "Gauss Method" << endl;
	Gauss(a, b, x);
	for (int i = 0;  i<n; i++){
		a[0][i] = a[0][i] - a[3][i];
		tmp = a[0][i];
		a[0][i] = a[3][i];
		a[3][i] = tmp;
	}
	b[0] = b[0] - b[3];
	tmp = b[3];
	b[3] = b[0];
	b[0] = tmp;
	cout << "Gauss-Seidel Method" << endl; 
	GaussSeidel(a,b,x);
	getchar();
}
