#include "AMO_1.h"
#pragma warning (disable:4996)
int main(){
	double a = -9.8; //ліва межа
	double b = 13.9; //права межа
	double eps = 0.01; //задана точність
	double krok = 0.001; //крок зміни точності
	double suma = 0.0; //сума ряда
	double chlen = 0; //поточний член ряда
	int n = 0; //поточна довжина ряда
	double x; //точка, в якій обчислюється значення функції
	double TochnZn; //точне значення функції (в даному випадку sh(x))
	int n2; //фіксована довжина ряда
	double h;
	int i;
	FILE* FTabl1;
	FTabl1 = fopen("Tabl1.txt", "w+t");

	//виконання знаходження точки обчислення функції для першої частини роботи
	x = ZnahTochky(a, b);
	//виконання обчислення точного значення функції
	TochnZn = sinh(x);
	fprintf(FTabl1, "eps                Залишковий член    Похибка             Довжина\n");
	//основний цикл першої частини роботи
	while (eps > 1.0e-015){
		while (abs(suma - TochnZn) > eps){
			chlen = ZnahNastupnogo(chlen, x, n);
			suma += chlen;
			n++;
		}
		if (1.0e-008 == eps){
			n2 = n;
		}
		fprintf(FTabl1, "%.16f %.16f %.16f %d\n",eps, chlen, (suma - TochnZn), n);
		ZminaTochnosti(eps, krok);
		/*
		printf("ObchZn =\t %.16f\n", suma);
		printf("ZalChlen =\t %.16f\n", chlen);
		printf("Pohybka =\t %.16f\n", (suma - TochnZn));
		printf("DovzhRiadu =\t %d\n", n);
		*/
	}
	fprintf(FTabl1, "\n\n\nXi         Абсолютна похибка      Залишковий член\n");
	/*Виконання частини другої*/
	eps = 1.0e-008;
	h = ObchH(a, b);
	suma = 0;
	for (int i = 0; i <= 10; i++){
		suma = 0;
		x = ObchXi(a,b, h, i);
		for (int k = 0; k <= n2; k++){
			chlen = ZnahNastupnogo(chlen, x, k);
			suma += chlen;
		}
		TochnZn = sinh(x);
		fprintf(FTabl1, "%.16f\t%.16f\t%.16f\n", x, (suma-TochnZn), chlen);
		//printf("AbsZn  =\t %.16f\n", TochnZn);
		//printf("ObchZn =\t %.16f\n", suma);
	}





	//getchar();

	fcloseall();
	return 0;
}