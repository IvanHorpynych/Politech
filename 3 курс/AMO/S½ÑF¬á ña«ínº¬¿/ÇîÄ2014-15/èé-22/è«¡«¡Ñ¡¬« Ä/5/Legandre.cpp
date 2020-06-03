#include "Legandre.h"

double ObchyslenniaMnogochlenaLezhandra(double x, int n)
{
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
������� �������� ������������� ���������� �������� n-� ������
� ���� ����� x � ������������� koef.
*/
double ObchyslenniaUzagalnenogoMnogochlenaLezhandra(double x, int n, double* koef){
	double ret = 0;
	for(int i = 0; i <= n; i++){
		ret += koef[i]*ObchyslenniaMnogochlenaLezhandra(x, i);
	}
	return ret;
}