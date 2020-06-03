#include "AMO_1.h"

/*������ �������� ��������� Tochnist �� �������� Krok*/
void ZminaTochnosti(double& Tochnist, double Krok){
	Tochnist *= Krok;
}

/*����������� ���������� ����� ���� �� ������ ���������.*/
double ZnahNastupnogo(double Poperednij, double X, int K){
	if (0 == K){
		return X;
	}	
	return Poperednij * (X * X) / (2 * K * (2 * K + 1));
}

/*����������� �����, � ��� ������������ �������� �������.*/
double ZnahTochky(double a, double b){
	return (a + b) / 2;
}

/*���������� xi*/
double ObchXi(double a, double b, double h, int i){
	return (a+b)/2 + h*i;
}

/*���������� h*/
double ObchH(double a, double b){
	return (b - a) / 10;
}

