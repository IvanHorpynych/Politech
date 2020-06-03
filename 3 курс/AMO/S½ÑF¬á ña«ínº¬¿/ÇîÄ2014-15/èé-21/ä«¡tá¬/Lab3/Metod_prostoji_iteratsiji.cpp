#include "Metod_prostoji_iteratsiji.h"

/*Виконує множення матриці Systema розміром n*n на стовпець Stovpets розміром n. Повертає стовпець.*/
double* MnozhenniaMatrytsiNaStovpets(double** Systema, int n, double* Stovpets){
	double* Rezultat = new double[n];
	for (int i = 0; i < n; i++){
		Rezultat[i] = 0;
		for (int j = 0; j < n; j++){
			Rezultat[i] += Systema[i][j] * Stovpets[j];
		}
	}
	return Rezultat;
}

/*Виконує почленне додавання стовпця Stovpets2 до стовпця Stovpets1.*/
void DodavanniaStovptsiv(double* Stovpets1, double* Stovpets2, int n){
	for (int i = 0; i < n; i++){
		Stovpets1[i] += Stovpets2[i];
	}
}

/*Виконує почленне віднімання стовпця Stovpets2 від стовпця Stovpets1. Повертає різницю.*/
double* VidnimanniaStovptsiv(double* Stovpets1, double* Stovpets2, int n){
	double* Rezultat = new double[n+1];
	for (int i = 0; i < n; i++){
		Rezultat[i] = Stovpets1[i] - Stovpets2[i];
	}
	return Rezultat;
}

/*Виконує приведення системи Systema із кількістю невідомих n до вигляду, коли кожен Xi виражений через інші невідомі.
Повертає матрицю заданого вигляду.*/
double** PryvedenniaSystemy(double** Systema, int n){
	double** Rezultat = new double*[n];
	for (int i = 0; i < n; i++){
		Rezultat[i] = new double[n + 1];
		for (int j = 0; j < n; j++){
			if (i == j){
				Rezultat[i][j] = 0;
				continue;
			}
			Rezultat[i][j] = -(Systema[i][j] / Systema[i][i]);
		}
		Rezultat[i][n] = Systema[i][n] / Systema[i][i];
	}
	return Rezultat;
}

/*Знаходить суму модулів елементів вектора заданої довжини n.
Повертає суму.*/
double SumaModulivElementivRiadka(double* Riadok, int n){
	double Suma = 0;
	for (int i = 0; i < n; i++){
		Suma += abs(Riadok[i]);
	}
	return Suma;
}

/*Знаходить m-норму матриці Systema розмірності n*n. Повертає m-норму.*/
double ZnahodzhenniaNormy(double** Systema, int n){
	double Rezultat = SumaModulivElementivRiadka(Systema[0], n);
	for (int i = 1; i < n; i++){
		double N = SumaModulivElementivRiadka(Systema[i], n);
		if (N > Rezultat){
			Rezultat = N;
		}
	}
	return Rezultat;
}

/*Обчислює константу завершення ітерацій. Повертає константу завершення.*/
double ObchyslenniaKonstantyZavershennia(double q, double eps){
	return (1 - q)*eps / q;
}

/*Виділяє із приведеної системи з числом рівнянь n стовпець із номером m. Повертає стовпець.*/
double* VydilenniaStovptsia(double** Systema, int n, int m){
	double* Rezultat = new double[n];
	for (int i = 0; i < n; i++){
		Rezultat[i] = Systema[i][m];
	}
	return Rezultat;
}

/*Обчислює чергове наближення за відомими матрицею Systema із розмірністю n*n, попереднім наближенням NablyzhenniaPoperednie.
Повертає вектор наближень.*/
double* ObchyslenniaNablyzhennia(double** Systema, int n, double* NablyzhenniaPoperednie){
	double* Rezultat = MnozhenniaMatrytsiNaStovpets(Systema, n, NablyzhenniaPoperednie);
	double* Beta = VydilenniaStovptsia(Systema, n, n);
	DodavanniaStovptsiv(Rezultat, Beta, n);
	return Rezultat;
}

/*Розв`язує СЛАР методом простої ітерації. Повертає вектор рішення.*/
double* RozvjazanniaMetodomIteratsiji(double** Systema, int n, double eps){
	double** PryvedenaSystema = PryvedenniaSystemy(Systema, n);
	double* NablyzhenniaPoperednie = VydilenniaStovptsia(PryvedenaSystema, n, n);
	double Norma = ZnahodzhenniaNormy(PryvedenaSystema, n);
	double KonstantaZavershennia = ObchyslenniaKonstantyZavershennia(Norma, eps);
	double* NablyzhenniaNastupne = ObchyslenniaNablyzhennia(PryvedenaSystema, n, NablyzhenniaPoperednie);
	double* RiznytsiaNablyzhen = VidnimanniaStovptsiv(NablyzhenniaNastupne, NablyzhenniaPoperednie, n);
	while (SumaModulivElementivRiadka(RiznytsiaNablyzhen, n) > KonstantaZavershennia){
		delete[] NablyzhenniaPoperednie;
		NablyzhenniaPoperednie = NablyzhenniaNastupne;
		NablyzhenniaNastupne = ObchyslenniaNablyzhennia(PryvedenaSystema, n, NablyzhenniaPoperednie);
		RiznytsiaNablyzhen = VidnimanniaStovptsiv(NablyzhenniaNastupne, NablyzhenniaPoperednie, n);
	}
	delete[] NablyzhenniaPoperednie;
	return NablyzhenniaNastupne;
}