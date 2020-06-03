#include "Spline.h"

/*Обчислює коефіцієнт a.
Ai = fi.*/
double ObchyslenniaA(double (*pt2Func)(double), double x){
	return pt2Func(x);
}

/*Виконує обчислення H.
H = Xi - (Xi_)*/
double ObchyslenniaH(double Xi, double Xi_){
	return Xi - Xi_;
}

/*Виконує обчислення Hi.
Hi = Masx[i] - Masx[i-1]*/
double ObchyslenniaHi(double* MasX, int i){
	return ObchyslenniaH(MasX[i], MasX[i-1]);
}

/*Обчислює і-й вільний член.*/
double ObchyslenniaVilnogoChlena(double (*pt2Func)(double), double* MasX, int i){
	return 6*( (pt2Func(MasX[i+1]) - pt2Func(MasX[i]))/ObchyslenniaHi(MasX, i+1) - (pt2Func(MasX[i]) - pt2Func(MasX[i-1]))/ObchyslenniaHi(MasX, i) );
}

/*Виконує побудову системи рівнянь для пошуку коефіцієнтів Сі.*/
double** PobudovaSystemyRivnian(double (*pt2Func)(double), double* MasX, int n){
	//створення і першочергове заповнення нулями
	double** Systema = (double**)malloc((n + 1) * sizeof(double*));
	for(int i = 0; i <= n; i++){
		Systema[i] = (double*)malloc((n + 2) * sizeof(double));
		for(int j = 0; j <= n + 1; j++){
			Systema[i][j] = 0.0;
		}
	}
	Systema[0][0] = 1;
	Systema[n][n] = 1;
	//заповнення коефіцієнтами
	for(int i = 1; i <= n - 1; i++){
		Systema[i][i-1] =  ObchyslenniaHi(MasX, i);
		Systema[i][i] = 2.0*(ObchyslenniaHi(MasX, i)+(ObchyslenniaHi(MasX, i+1)));
		Systema[i][i+1] = ObchyslenniaHi(MasX, i+1);
		Systema[i][n+1] = ObchyslenniaVilnogoChlena(pt2Func, MasX, i);
	}
	return Systema;
}

/*Виконує обчислення масиву коефіцієнтів Сі методом єдиного поділу.*/
double* ObchyslenniaMasyvuC(double** Systema, int n){
	ObnulenniaNyzhniojiChastyny(Systema, n+1, n+2);
	double* ret = ZvorotnijHid(Systema, n+1, n+2);
	return ret;
}

/*Виконує обчислення масиву коефіцієнтів Ai.*/
double* ObchyslenniaMasyvuA(double (*pt2Func)(double), double* MasX, int n){
	double* ret = (double*)malloc( (n+1)*sizeof(double));
	for(int i = 0; i <= n; i++){
		ret[i] = ObchyslenniaA(pt2Func, MasX[i]);
	}
	return ret;
}

/*Обчислює коефіцієнт D.*/
double ObchyslenniaD(double Ci, double Ci_, double Hi){
	return (Ci - Ci_)/Hi;
}

/*Виконує обчислення масиву коефіцієнтів Di.*/
double* ObchyslenniaMasyvuD(double* MasX, double* MasC, int n){
	double* ret = (double*)malloc( (n+1)*sizeof(double));
	ret[0] = 0;
	for(int i = 1; i <= n; i++){
		double Chys = MasC[i] - MasC[i-1];
		double Znam = ObchyslenniaHi(MasX, i);
		ret[i] = Chys/Znam;
	}
	return ret;
}

/*Обчислює коефіцієнт B.*/
double ObchyslenniaB(double Ci, double Di, double Fi, double Fi_, double Hi){
	return Hi*Ci/2.0 - Hi*Hi*Di/6.0 + (Fi - Fi_)/Hi;
}

/*Виконує обчислення масиву коефіцієнтів Bi.*/
double* ObchyslenniaMasyvuB(double (*pt2Func)(double), double* MasX, double* MasC, double* MasD, int n){
	double* ret = (double*)malloc( (n+1)*sizeof(double));
	ret[0] = 0;
	for(int i = 1; i <= n; i++){
		double Ci = MasC[i];
		double Di = MasD[i];
		double Fi = pt2Func(MasX[i]);
		double Fi_= pt2Func(MasX[i-1]);
		double Hi = ObchyslenniaHi(MasX, i);
		ret[i] = ObchyslenniaB(Ci, Di, Fi, Fi_, Hi);
	}
	return ret;
}

/*Виконує обчислення Si.*/
double ObchyslenniaS(double Ai, double Bi, double Ci, double Di, double Xi, double X){
	return Ai + Bi*(X - Xi) + Ci*(X - Xi)*(X - Xi)/2.0 + Di*(X - Xi)*(X - Xi)*(X - Xi)/6.0;
}

/*Виконує обчислення натурального кубічного сплайна.
Кількість часткових проміжків - n.
Кількість підпроміжків - m.*/
void KubichnyjSplajn(double (*pt2Func)(double), double a, double b, int n, int m){
	//створення і заповнення масиву вузлів
	double h = (b - a)/n;
	double* MasX = (double*)malloc( (n+1)*sizeof(double));
	for(int i = 0; i <= n; i++){
		MasX[i] = a + h*i;
	}
	//обчислення масивів коефіцієнтів для часткових проміжків
	double** Systema = PobudovaSystemyRivnian(pt2Func, MasX, n);
	double* MasC = ObchyslenniaMasyvuC(Systema, n);
	double* MasA = ObchyslenniaMasyvuA(pt2Func, MasX, n);
	double* MasD = ObchyslenniaMasyvuD(MasX, MasC, n);
	double* MasB = ObchyslenniaMasyvuB(pt2Func, MasX, MasC, MasD, n);
	FILE* f = fopen("Res.txt", "wt");
	fprintf(f, "Xij\t\t\tFi\t\t\tSi\n");
	for(int i = 1; i <= n; i++){
		double h = (MasX[i] - MasX[i-1])/m;
		for(int j = 0; j <= m; j++){
			double Xij = MasX[i-1] + h*j;
			double Fij = pt2Func(Xij);
			double Sij = ObchyslenniaS(MasA[i], MasB[i], MasC[i], MasD[i], MasX[i], Xij);
			fprintf(f, "%.16f\t%.16f\t%.16f\n", Xij, Fij, Sij);
		}
	}
	fclose(f);
}