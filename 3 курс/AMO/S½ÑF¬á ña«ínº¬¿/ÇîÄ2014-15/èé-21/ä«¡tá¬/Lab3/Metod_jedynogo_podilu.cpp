#include "Metod_jedynogo_podilu.h"

/*Перевіряє рядок, заданий масивом чисел на наявність головного елемента, заданого другим параметром-індексом, не рівного нулю.
Повертає True в разі наявності такого елемента. Повертає False в разі відсутності такого елемента.*/
bool PerevirkaGolovnogo(double* Rivniannia, int Index){
	if (0 == Rivniannia[Index]){
		return false;
	}
	return true;
}

/*Множить рядок, починаючи з позиції Index до позиції n на величину Mnozhnyk.*/
void MnozhenniaRiadka(double* Rivniannia, int Index, int n, double Mnozhnyk){
	for (int i = Index; i <= n; i++){
		Rivniannia[i] *= Mnozhnyk;
	}
}

/*Почленно віднімає рядок Vidjemnyk від рядка Zmenshuvane. Довжина рядка задається n.*/
void VidnimanniaPochlenne(double* Zmenshuvane, double* Vidjemnyk, int n){
	for (int i = 0; i < n; i++){
		Zmenshuvane[i] -= Vidjemnyk[i];
	}
}

/*Виконує пошук першого рівняння з ненульовим головним елементом, заданим позицією Index,
починаючи з позиції Nomer, у системі рівнянь Systema з числом рівнянь n.
Повертає ноиер рівняння.*/
int PoshukRivnianniaZNenuliovymGolovnymElementom(int Index, int Nomer, double** Systema, int n){
	for (int i = Nomer; i < n; i++){
		if (true == PerevirkaGolovnogo(Systema[i], Index)){
			return i;
		}
	}
	return -1;
}

/*Виконує почленний обмін вмістом двох рядків Riadok1 і Riadok2 з довжиною n.*/
void ObminVmistom(double* Riadok1, double* Riadok2, int n){
	double Komirka;
	for (int i = 0; i < n; i++){
		Komirka = Riadok1[i];
		Riadok1[i] = Riadok2[i];
		Riadok2[i] = Komirka;
	}
}

/*Виконує обнулення стовпця системи рівнянь Systema нижче головного елемента, що стоїть на позиції Index.
В системі KilkistRivnian рівнянь.*/
void ObnulenniaStovptsia(double** Systema, int Index, int KilkistRivnian, int n){
	/*Ділення чергового рядка на величину головного члена.*/
	MnozhenniaRiadka(Systema[Index], Index, KilkistRivnian, (1 / Systema[Index][Index]));
	double* TymchasovyjRiadok = new double[n + 1];
	for (int i = Index + 1; i < KilkistRivnian; i++){
		memcpy(TymchasovyjRiadok, Systema[Index], n*sizeof(double));
		MnozhenniaRiadka(TymchasovyjRiadok, Index, n - 1, Systema[i][Index]);
		VidnimanniaPochlenne(Systema[i], TymchasovyjRiadok, n);
	}
	delete[] TymchasovyjRiadok;
};


/*Виконує обнулення частини системи рівнянь під головною діагоналлю.*/
void ObnulenniaNyzhniojiChastyny(double** Systema, int KilkistRivnian, int n){
	for (int i = 0; i < KilkistRivnian; i++){
		ObnulenniaStovptsia(Systema, i, KilkistRivnian, n);
	}
}

/*Виконує зворотній хід метода Гаусса.*/
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