#include "Func.h"
#include "Formula_Simpsona.h"
#include "Podvijnyj_pererahunok.h"

int main(){
	long double a = 0.0;
	long double b = 15.0;
	long double eps[5] = {0.01, 0.001, 0.0001, 0.00001, 0.000001};
	long double (*pt2Func)(long double)= Obchyslennia_Funktsiji_Za_Variantom;
	for(int i = 0; i < 5; i++){
		long double h = Obchyslennia_Kroku_Integruvannia(a, b, eps[i]);
		long double* Integral = ObchyslenniaIntegrala(a, b, h, pt2Func);
		long double ZalChlen = (b-a)*h*h*h*h*Obchyslennia_Chetvertoji_Pohidnoji_Funktsiji_Za_Variantom(a)/180.0;
		long double eps1 = abs(3.333333333333333333333333333333333333-Integral[0]);
		long double* Integral2 = Vykonannia_Drugoji_Chastyny(a, b, eps1, pt2Func);
		long double eps2 = abs(3.333333333333333333333333333333333333-Integral2[0]);
		printf("%d.1\t%.16f\t%d\t%.16f\n",i, Integral[0],(int)Integral[1], eps1);
		printf("%d.2\t%.16f\t%d\t%.16f\n",i, Integral2[0],(int)Integral2[1], eps2);
	}
	
	getchar();
	return 0;
}