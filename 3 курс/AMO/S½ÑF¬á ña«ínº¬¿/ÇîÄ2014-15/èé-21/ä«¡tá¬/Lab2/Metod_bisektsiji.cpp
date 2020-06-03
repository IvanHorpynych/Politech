#include "Metod_bisektsiji.h"

/*Обчислення наступної точки для зсуву.*/
double ObchTochky(double a, double b){
	return (a + b) / 2;
}


/*Виконання зсуву однієї із меж.*/
void Zsuv(double& a, double& b, double xn){
	if (0 > ObchFunc(a) * ObchFunc(xn)){
		b = xn;
		return;
	}
	a = xn;
}

/*Обчислення наближеного значення кореня на відрізку [A, B] із заданою точністю.*/
double* ObchKoren_bis(double A, double B, double e){
	int i = 0;
	while (B - A > 2 * e){
		double xn = ObchTochky(A, B);
		Zsuv(A, B, xn);
		i++;
	}
	double* fff = new double[2];
	fff[0] = (A+B)/2;
	fff[1] = i;
	//return {(A + B) / 2, i};
	return fff;
}