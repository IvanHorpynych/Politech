#include "Trapezoid.h"

/*
Обирає початковий крок n для обчислення інтеграла із заданою точністю eps.
*/
int ObchyslenniaPochatkovogoKroku(int r, double eps){
	double Znam = pow (eps, (double)(1/(double)r));
	return 1/pow(eps, (double)(1/(double)r)) + 1;
}

/*
Обчислює довжини елементарного відрізка h на основі границь a та b і кроку n.
*/
double ObchyslenniaElementarnogoVidrizka(double a, double b, int n){
	return (b - a)/n;
}

/*
Обчислює інтеграл від функції pt2Func на відрізку [a, b] з довжиною елементарного відрізка h і кроком n.
*/
double ObchyslenniaIntegrala(double (*pt2Func)(double), double a, double b, double h, int n){
	double ret = 0.0;
	ret += ((pt2Func)(a) + (pt2Func)(b))/2.0;
	for(int i = 1; i < n; i++){
		ret += (*pt2Func)(a + h*i);
	}
	return ret*h;
}

/*
Методом подвійного перерахунку обчислює інтеграл від функції pt2Func
за допомогою формули інтегрування pt2IntFunc на відрізку [a, b] із точністю eps.
Точність не абсолютна, а відносна.
*/
double PodvijnyjPererahunok(double (*pt2Func)(double), double (*pt2IntFunc)(double (*pt2Func)(double), double, double, double, int), 
							double a, double b, double eps, int r){
	int n = ObchyslenniaPochatkovogoKroku(r, eps);
	double h = ObchyslenniaElementarnogoVidrizka(a, b, n);
	double IntN;
	double Int2N = pt2IntFunc(pt2Func, a, b, h, n);
	double Rn;
	do{
		IntN = Int2N;//= pt2IntFunc(pt2Func, a, b, h, n);
		n *= 2;
		h = ObchyslenniaElementarnogoVidrizka(a, b, n);
		Int2N = pt2IntFunc(pt2Func, a, b, h, n);
		Rn = 1/(pow(2.0, (double)r) - 1)*abs(IntN - Int2N)/abs(Int2N);
	}while(Rn > eps);
	return Int2N;
}

//**************************************************************************
//**************************************************************************
/*
Обчислює інтеграл від добутку функцій pt2Func1(double, int) і pt2Func2(double, int) на відрізку [a, b] з довжиною елементарного відрізка h і кроком n.
*/
double ObchyslenniaIntegralaVidDobutku1(double (*pt2Func1)(double, int), double (*pt2Func2)(double, int), double a, double b, double h, int n, int m1, int m2){
	double ret = 0.0;
	ret += (pt2Func1(a, m1)*pt2Func2(a, m2) + pt2Func1(b, m1)*pt2Func2(b, m2))/2.0;
	for(int i = 1; i < n; i++){
		ret += pt2Func1(a + h*i, m1)*pt2Func2(a + h*i, m2);
	}
	return ret*h;
}

/*
Методом подвійного перерахунку обчислює інтеграл від добутку функцій pt2Func1(double, int) і pt2Func2(double, int)
за допомогою формули інтегрування pt2IntFunc1 на відрізку [a, b] із точністю eps.
Точність не абсолютна, а відносна.
*/
double PodvijnyjPererahunokVidDobutku1(double (*pt2Func1)(double, int),double (*pt2Func2)(double, int),
									  double (*pt2IntFunc1)(double (*pt2Func1)(double, int),double (*pt2Func2)(double, int), double, double, double, int, int, int), 
									  double a, double b, double eps, int r, int m1, int m2){
	int n = ObchyslenniaPochatkovogoKroku(r, eps);
	double h = ObchyslenniaElementarnogoVidrizka(a, b, n);
	double IntN;
	double Int2N = pt2IntFunc1(pt2Func1,pt2Func2, a, b, h, n, m1, m2);
	double Rn;
	do{
		IntN = Int2N;//= pt2IntFunc(pt2Func, a, b, h, n);
		n *= 2;
		h = ObchyslenniaElementarnogoVidrizka(a, b, n);
		Int2N = pt2IntFunc1(pt2Func1,pt2Func2, a, b, h, n, m1, m2);
		Rn = 1/(pow(2.0, (double)r) - 1)*abs(IntN - Int2N)/abs(Int2N);
	}while(Rn > eps);
	return Int2N;
}
//**************************************************************************
//**************************************************************************
/*
Обчислює інтеграл від добутку функцій pt2Func1(double) і pt2Func2(double, int) на відрізку [a, b] з довжиною елементарного відрізка h і кроком n.
*/
double ObchyslenniaIntegralaVidDobutku2(double (*pt2Func1)(double), double (*pt2Func2)(double, int), double a, double b, double h, int n, int m2){
	double ret = 0.0;
	ret += (pt2Func1(a)*pt2Func2(a, m2) + pt2Func1(b)*pt2Func2(b, m2))/2.0;
	for(int i = 1; i < n; i++){
		ret += pt2Func1(a + h*i)*pt2Func2(a + h*i, m2);
	}
	return ret*h;
}

/*
Методом подвійного перерахунку обчислює інтеграл від добутку функцій pt2Func1(double) і pt2Func2(double, int)
за допомогою формули інтегрування pt2IntFunc1 на відрізку [a, b] із точністю eps.
Точність не абсолютна, а відносна.
*/
double PodvijnyjPererahunokVidDobutku2(double (*pt2Func1)(double),double (*pt2Func2)(double, int),
									  double (*pt2IntFunc1)(double (*pt2Func1)(double),double (*pt2Func2)(double, int), double, double, double, int, int), 
									  double a, double b, double eps, int r, int m2){
	int n = ObchyslenniaPochatkovogoKroku(r, eps);
	double h = ObchyslenniaElementarnogoVidrizka(a, b, n);
	double IntN;
	double Int2N = pt2IntFunc1(pt2Func1,pt2Func2, a, b, h, n, m2);
	double Rn;
	do{
		IntN = Int2N;//= pt2IntFunc(pt2Func, a, b, h, n);
		n *= 2;
		h = ObchyslenniaElementarnogoVidrizka(a, b, n);
		Int2N = pt2IntFunc1(pt2Func1,pt2Func2, a, b, h, n, m2);
		Rn = 1/(pow(2.0, (double)r) - 1)*abs(IntN - Int2N)/abs(Int2N);
	}while(Rn > eps);
	return Int2N;
}

//**************************************************************************
//**************************************************************************
/*
Обчислює інтеграл від різниці функцій pt2Func1(double) і pt2Func2(double, int) на відрізку [a, b] з довжиною елементарного відрізка h і кроком n.
*/
double ObchyslenniaIntegralaVidRiznytsi(double (*pt2Func1)(double), double (*pt2Func2)(double, int, double*), double a, double b, double h, int n, int m2, double* koef){
	double ret = 0.0;
	ret += (( pt2Func1(a)-pt2Func2(a, m2, koef)) *( pt2Func1(a)-pt2Func2(a, m2, koef)) + (pt2Func1(b)-pt2Func2(b, m2, koef))*(pt2Func1(b)-pt2Func2(b, m2, koef)))/2.0;
	for(int i = 1; i < n; i++){
		ret += (pt2Func1(a + h*i)-pt2Func2(a + h*i, m2, koef))*(pt2Func1(a + h*i)-pt2Func2(a + h*i, m2, koef));
	}
	return ret*h;
}

/*
Методом подвійного перерахунку обчислює інтеграл від різниці функцій pt2Func1(double) і pt2Func2(double, int)
за допомогою формули інтегрування pt2IntFunc1 на відрізку [a, b] із точністю eps.
Точність не абсолютна, а відносна.
*/
double PodvijnyjPererahunokVidRiznytsi(double (*pt2Func1)(double),double (*pt2Func2)(double, int, double*),
									   double (*pt2IntFunc1)(double (*pt2Func1)(double),double (*pt2Func2)(double, int, double*), double, double, double, int, int, double*), 
									   double a, double b, double eps, int r, int m2, double* koef){
	int n = ObchyslenniaPochatkovogoKroku(r, eps);
	double h = ObchyslenniaElementarnogoVidrizka(a, b, n);
	double IntN;
	double Int2N = pt2IntFunc1(pt2Func1,pt2Func2, a, b, h, n, m2, koef);
	double Rn;
	do{
		IntN = Int2N;//= pt2IntFunc(pt2Func, a, b, h, n);
		n *= 2;
		h = ObchyslenniaElementarnogoVidrizka(a, b, n);
		Int2N = pt2IntFunc1(pt2Func1,pt2Func2, a, b, h, n, m2, koef);
		Rn = 1/(pow(2.0, (double)r) - 1)*abs(IntN - Int2N)/abs(Int2N);
	}while(Rn > eps);
	return Int2N;
}