#include "Metod_prostoji_iteratsiji.h"

/*������ �������� ������� Systema ������� n*n �� �������� Stovpets ������� n. ������� ��������.*/
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

/*������ �������� ��������� ������� Stovpets2 �� ������� Stovpets1.*/
void DodavanniaStovptsiv(double* Stovpets1, double* Stovpets2, int n){
	for (int i = 0; i < n; i++){
		Stovpets1[i] += Stovpets2[i];
	}
}

/*������ �������� �������� ������� Stovpets2 �� ������� Stovpets1. ������� ������.*/
double* VidnimanniaStovptsiv(double* Stovpets1, double* Stovpets2, int n){
	double* Rezultat = new double[n+1];
	for (int i = 0; i < n; i++){
		Rezultat[i] = Stovpets1[i] - Stovpets2[i];
	}
	return Rezultat;
}

/*������ ���������� ������� Systema �� ������� �������� n �� �������, ���� ����� Xi ��������� ����� ���� ������.
������� ������� �������� �������.*/
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

/*��������� ���� ������ �������� ������� ������ ������� n.
������� ����.*/
double SumaModulivElementivRiadka(double* Riadok, int n){
	double Suma = 0;
	for (int i = 0; i < n; i++){
		Suma += abs(Riadok[i]);
	}
	return Suma;
}

/*��������� m-����� ������� Systema ��������� n*n. ������� m-�����.*/
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

/*�������� ��������� ���������� ��������. ������� ��������� ����������.*/
double ObchyslenniaKonstantyZavershennia(double q, double eps){
	return (1 - q)*eps / q;
}

/*������ �� ��������� ������� � ������ ������ n �������� �� ������� m. ������� ��������.*/
double* VydilenniaStovptsia(double** Systema, int n, int m){
	double* Rezultat = new double[n];
	for (int i = 0; i < n; i++){
		Rezultat[i] = Systema[i][m];
	}
	return Rezultat;
}

/*�������� ������� ���������� �� ������� �������� Systema �� ��������� n*n, ��������� ����������� NablyzhenniaPoperednie.
������� ������ ���������.*/
double* ObchyslenniaNablyzhennia(double** Systema, int n, double* NablyzhenniaPoperednie){
	double* Rezultat = MnozhenniaMatrytsiNaStovpets(Systema, n, NablyzhenniaPoperednie);
	double* Beta = VydilenniaStovptsia(Systema, n, n);
	DodavanniaStovptsiv(Rezultat, Beta, n);
	return Rezultat;
}

/*����`��� ���� ������� ������ ��������. ������� ������ ������.*/
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