#include "Func.h"

/*Обчислює задану за варіантом функцію.*/
long double Obchyslennia_Funktsiji_Za_Variantom(long double x){
	return 1.0 / (sqrt(3.0 * x + 4.0));
}

/*Обчислює четверту похідну функції, заданої за варіантом.*/
long double Obchyslennia_Chetvertoji_Pohidnoji_Funktsiji_Za_Variantom(long double x){
	return 8505.0 / (16.0 * pow((double)(3.0 * x + 4.0), (double)(9.0/2.0)));
}

/*Обчислює максимум четвертої похідної на інтервалі.*/
long double Max_Chetvertoji_Pohidnoji(long double a, long double b){
	return Obchyslennia_Chetvertoji_Pohidnoji_Funktsiji_Za_Variantom(a);
}

/*Обчислює кроку інтегрування.*/
long double Obchyslennia_Kroku_Integruvannia(long double a, long double b, long double eps){
	long double M4 = Max_Chetvertoji_Pohidnoji(a, b) * 2.0;
	long double h = sqrt(sqrt((180.0 * eps) / ((b - a)*M4))) / 2.0;
	int n = (b - a)/h;
	if(n % 2 == 0){
		n += 2;
	}else{
		n += 1;
	}
	h = (b-a)/n;
	return h;
}