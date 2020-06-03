#include "Formula_Simpsona.h"

/*Обчислення наступного x за номером k, довжиною проміжка h і початкм відрізка a.*/
long double ObchyslenniaX(long double a, int k, long double h){
	return a + k*h;
}

/*Обчислює Sigma1 і Sigma2.*/
long double* ObchyslenniaSigma(long double a, long double b, long double h, int n, long double(*pt2Func)(long double)){
	long double* Sigma = new long double[2];
	Sigma[0] = Sigma[1] = 0;
	for (int i = 1; i < n - 1;){
		long double x = ObchyslenniaX(a, i, h);
		Sigma[0] += (*pt2Func)(x);
		x = ObchyslenniaX(a, (i + 1), h);
		Sigma[1] += (*pt2Func)(x);
		i += 2;
	}
	long double x = ObchyslenniaX(a, (n - 1), h);
	Sigma[0] += (*pt2Func)(x);
	return Sigma;
}

long double* ObchyslenniaIntegrala(long double a, long double b, long double h, long double(*pt2Func)(long double)){
	int n = (b - a)/h;
	if(n % 2 == 0){
		n += 2;
	}else{
		n += 1;
	}
	h = (b-a)/n;
	long double* Sigma = ObchyslenniaSigma(a, b, h, n, pt2Func);
	long double* Integral = new long double[2];
	Integral[0] = (h / 3.0)*(pt2Func(a) + pt2Func(b) + 4.0 * Sigma[0] + 2 * Sigma[1]);
	Integral[1] = n;
	return Integral;
}