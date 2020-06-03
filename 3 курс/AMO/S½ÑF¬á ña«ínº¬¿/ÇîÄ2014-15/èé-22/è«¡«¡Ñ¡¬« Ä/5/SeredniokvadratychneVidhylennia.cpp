#include "SeredniokvadratychneVidhylennia.h"

/*
Повертає значення середньоквадратичного відхилення функції pt2Func1(double)
і функції pt2Func2(double, int) на відрізку [a, b].
*/
double ObchyslenniaVidhylennia(double (*pt2Func1)(double),double (*pt2Func2)(double, int, double*),
							   double (*pt2IntFunc1)(double (*pt2Func1)(double),double (*pt2Func2)(double, int, double*), double, double, double, int, int, double*), 
							   double a, double b, int r, int m2, double* koef){
	/*
	if(a == b)
		return 0;
	int testn = 100;
	double testh = (b-a)/testn;
	double sum = 0.0;
	for(int i = 0; i <= testn; i++){
		sum = 0.0;
		for(int j = 0; j <= m2; j++){
			sum += (pt2Func1(a+i*testh)-pt2Func2(a+i*testh, m2, koef))*(pt2Func1(a+i*testh)-pt2Func2(a+i*testh, m2, koef));
		}
		//fprintf(f, "%.3f;", a);
	}
	return sqrt(sum/(testn+1));
	*/
	//for(int i = 0; i <= m2; i++)
	//	printf("%f\n", koef[i]);
	double aaa = PodvijnyjPererahunokVidRiznytsi(pt2Func1, pt2Func2, pt2IntFunc1, a, b, 0.01, r, m2, koef);
	return sqrt( (PodvijnyjPererahunokVidRiznytsi(pt2Func1, pt2Func2, pt2IntFunc1, a, b, 0.01, r, m2, koef))/(b - a) );
	
}

/*
Виконує пошук такого значення степені узагальненого многочлена, щоб значення
середньоквадратичного відхилення було менше за eps.
Повертає степінь.
*/
int ObchyslenniaStepeniUzagalnenogoMnogochlena(double (*pt2Func1)(double),double (*pt2Func2)(double, int, double*),
											   double (*pt2IntFunc1)(double (*pt2Func1)(double),double (*pt2Func2)(double, int, double*), double, double, double, int, int, double*), 
											   double a, double b, int r, double eps){
	for(int i = 1;;i++){
		double** TestMatr = StvorenniaNormalnojiSystemy(i, a, b, 0.01, 2);
		FILE* f = fopen("TestSyst.txt", "w+t");
		for(int j = 0; j <= i; j++){
			for(int k = 0; k <= i + 1; k++){
				fprintf(f, "%f\t",TestMatr[j][k]);
			}
			fprintf(f, "\n");
		}
		fclose(f);

		ObnulenniaNyzhniojiChastyny(TestMatr, i+1, i+2);
		f = fopen("TestSyst.txt", "at");
		fprintf(f, "\n\n\n");
		for(int j = 0; j <= i; j++){
			for(int k = 0; k <= i + 1; k++){
				fprintf(f, "%f\t",TestMatr[j][k]);
			}
			fprintf(f, "\n");
		}
		fclose(f);
		double* Koreni = ZvorotnijHid(TestMatr, i+1, i+2);
		f = fopen("TestSyst.txt", "at");
		fprintf(f, "\n\n\n");
		for(int j = 0; j <= i; j++){
			fprintf(f, "%f\t", Koreni[j]);
		}
		fprintf(f, "\n");
		fclose(f);
		double temp = ObchyslenniaVidhylennia(pt2Func1, pt2Func2, pt2IntFunc1, a, b, r, i, Koreni);

		if(eps > temp){
			printf("Coeficients:\n");
			for(int j = 0; j <=i; j++){
				printf("%f\t", Koreni[j]);
			}
			printf("\n");
			return i;
		}
	}
}