#include "MnogochlenLezhandra.h"

/*
Повертає значення многочлена Лежандра n-ї степені в даній точці X.
L0 = 1
L1 = x
L(n+1) = (2n+1)/(n+1)xLn(x) - n/(n+1)L(n-1)(x)
*/
double ObchyslenniaMnogochlenaLezhandra(double x, int n){
	/*
	double LnMinus = 1.0;
	double Ln = x;
	double LnPlus;
	if(0 == n){
		return LnMinus;
	}
	if(1 == n){
		return Ln;
	}
	for(int i = 2; i <= n; i++){
		LnPlus = ((2*i - 1)*x*Ln - (i - 1)*LnMinus)/i;
		LnMinus = Ln;
		Ln = LnPlus;
	}
	return LnPlus;
	*/
	
	double ret = 1.0;
	for(int i = 1; i <= n; i++)
		ret *= x;
	return ret;
	
}

/*
Повертає значення узагальненого многочлена Лежандра n-ї степені
в даній точці x з коефіцієнтами koef.
*/
double ObchyslenniaUzagalnenogoMnogochlenaLezhandra(double x, int n, double* koef){
	double ret = 0;
	for(int i = 0; i <= n; i++){
		ret += koef[i]*ObchyslenniaMnogochlenaLezhandra(x, i);
	}
	return ret;
}