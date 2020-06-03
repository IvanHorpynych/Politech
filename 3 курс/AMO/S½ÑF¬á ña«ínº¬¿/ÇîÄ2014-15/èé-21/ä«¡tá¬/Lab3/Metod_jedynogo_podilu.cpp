#include "Metod_jedynogo_podilu.h"

/*�������� �����, ������� ������� ����� �� �������� ��������� ��������, �������� ������ ����������-��������, �� ������ ����.
������� True � ��� �������� ������ ��������. ������� False � ��� ��������� ������ ��������.*/
bool PerevirkaGolovnogo(double* Rivniannia, int Index){
	if (0 == Rivniannia[Index]){
		return false;
	}
	return true;
}

/*������� �����, ��������� � ������� Index �� ������� n �� �������� Mnozhnyk.*/
void MnozhenniaRiadka(double* Rivniannia, int Index, int n, double Mnozhnyk){
	for (int i = Index; i <= n; i++){
		Rivniannia[i] *= Mnozhnyk;
	}
}

/*�������� ����� ����� Vidjemnyk �� ����� Zmenshuvane. ������� ����� �������� n.*/
void VidnimanniaPochlenne(double* Zmenshuvane, double* Vidjemnyk, int n){
	for (int i = 0; i < n; i++){
		Zmenshuvane[i] -= Vidjemnyk[i];
	}
}

/*������ ����� ������� ������� � ���������� �������� ���������, ������� �������� Index,
��������� � ������� Nomer, � ������ ������ Systema � ������ ������ n.
������� ����� �������.*/
int PoshukRivnianniaZNenuliovymGolovnymElementom(int Index, int Nomer, double** Systema, int n){
	for (int i = Nomer; i < n; i++){
		if (true == PerevirkaGolovnogo(Systema[i], Index)){
			return i;
		}
	}
	return -1;
}

/*������ ��������� ���� ������ ���� ����� Riadok1 � Riadok2 � �������� n.*/
void ObminVmistom(double* Riadok1, double* Riadok2, int n){
	double Komirka;
	for (int i = 0; i < n; i++){
		Komirka = Riadok1[i];
		Riadok1[i] = Riadok2[i];
		Riadok2[i] = Komirka;
	}
}

/*������ ��������� ������� ������� ������ Systema ����� ��������� ��������, �� ����� �� ������� Index.
� ������ KilkistRivnian ������.*/
void ObnulenniaStovptsia(double** Systema, int Index, int KilkistRivnian, int n){
	/*ĳ����� ��������� ����� �� �������� ��������� �����.*/
	MnozhenniaRiadka(Systema[Index], Index, KilkistRivnian, (1 / Systema[Index][Index]));
	double* TymchasovyjRiadok = new double[n + 1];
	for (int i = Index + 1; i < KilkistRivnian; i++){
		memcpy(TymchasovyjRiadok, Systema[Index], n*sizeof(double));
		MnozhenniaRiadka(TymchasovyjRiadok, Index, n - 1, Systema[i][Index]);
		VidnimanniaPochlenne(Systema[i], TymchasovyjRiadok, n);
	}
	delete[] TymchasovyjRiadok;
};


/*������ ��������� ������� ������� ������ �� �������� ���������.*/
void ObnulenniaNyzhniojiChastyny(double** Systema, int KilkistRivnian, int n){
	for (int i = 0; i < KilkistRivnian; i++){
		ObnulenniaStovptsia(Systema, i, KilkistRivnian, n);
	}
}

/*������ �������� ��� ������ ������.*/
double* ZvorotnijHid(double** Systema, int KilkistRivnian, int n){
	double* Koreni = new double[KilkistRivnian];
	Koreni[KilkistRivnian - 1] = Systema[KilkistRivnian - 1][n - 1];
	for (int i = 2; i <= KilkistRivnian; i++){
		for (int j = KilkistRivnian - i; j >= 0; j--){
			Systema[j][n - i] *= Koreni[n - i];
			Systema[j][n - 1] -= Systema[j][n - i];
			Systema[j][n - i] = 0;
		}
		Koreni[KilkistRivnian - i] = Systema[KilkistRivnian - i][n - 1];
	}
	return Koreni;
}