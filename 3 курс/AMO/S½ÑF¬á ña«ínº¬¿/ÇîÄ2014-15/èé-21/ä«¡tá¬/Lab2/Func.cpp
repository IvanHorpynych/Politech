#include "Func.h"
/*Обчислення похідної функції.
y = exp(x) + ln(x) - 10*x + 3
y' = exp(x) + 1/x + 10 */
double ObchPohidn(double x){
	return exp(x) + 1 / x - 10;
}

/*Обчислення функції в заданій точці.
y = exp(x) + ln(x) - 10*x + 3 */
double ObchFunc(double x){
	return exp(x) + log(x) - 10 * x + 3;
}

/*Обчислення другої похідної.
y'' = exp(x) - 1/(x^2) */
double ObchDrugPohidn(double x){
	return exp(x) - 1 / (x*x);
}

/*Виконання першої частини роботи*/
void VykPershChast(double eps, double KrZminy, int KilkZmin){
	double MasKoren[3][2] = { { 0.01, 0.1 }, { 0.3, 0.4 }, { 3.3, 3.4 } };
	FILE* f = fopen("AMO2_iteratsija.txt", "wt");
	FILE* f1 =fopen("AMO2_bisektsija.txt", "wt");
	fprintf(f, "eps\t\t\tзначення\t\tоцінка точності\n");
	fprintf(f1, "eps\t\t\tзначення\t\tоцінка точності\n");
	for (int i = 1; i <= KilkZmin; i++){
		eps *= KrZminy;
		for (int j = 0; j < 3; j++){
			double* rishi = ObchKoren_it(MasKoren[j][0], MasKoren[j][1], eps);
			double xi = rishi[0];
			double OcTochn = ObchConstZav(ObchQ(MasKoren[j][0], MasKoren[j][1]), eps);
			fprintf(f, "%.16f\t%.16f\t%.16f\n", eps, xi, OcTochn);
			rishi = ObchKoren_bis(MasKoren[j][0], MasKoren[j][1], eps);
			xi = rishi[0];
			//OcTochn = ObchConstZav(ObchQ(MsasKoren[j][0], MasKoren[j][1]), eps);
			fprintf(f1, "%.16f\t%.16f\t%.16f\n", eps, xi, 2*eps);
		}
	}
	fcloseall();
}

void VykDrugChast(double eps, double KrZminy, int KilkZmin){
	double A = 0.01;
	double B = 0.1;
	FILE* f = fopen("AMO2_porivniannia.txt", "wt");
	fprintf(f,"eps\t\tметод І\t\tметод Б");
	for (int i = 1; i <= KilkZmin; i++){
		eps *= KrZminy;
			double* rish_it = ObchKoren_it(A, B, eps);
			double* rish_bis = ObchKoren_bis(A, B, eps);
			fprintf(f, "%.16f\t%.16f\t%.16f\n", eps, rish_it[1], rish_bis[1]);
			//xi = ObchKoren_bis(MasKoren[j][0], MasKoren[j][1], eps);
			//OcTochn = ObchConstZav(ObchQ(MsasKoren[j][0], MasKoren[j][1]), eps);
			//fprintf(f1, "%.16f\t%.16f\t%.16f\n", eps, xi, 2*eps);
	}
	fcloseall();
}