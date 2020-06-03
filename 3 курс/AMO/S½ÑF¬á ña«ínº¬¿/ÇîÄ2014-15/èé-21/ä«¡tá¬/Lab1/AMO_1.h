#pragma once
#include <stdio.h>
#include <math.h>

/*������ �������� ��������� Tochnist �� �������� Krok*/
void ZminaTochnosti(double& Tochnist, double Krok);

/*����������� ���������� ����� ���� �� ������ ���������.*/
double ZnahNastupnogo(double Poperednij, double X, int K);

/*����������� �����, � ��� ������������ �������� �������.*/
double ZnahTochky(double a, double b);

/*���������� ������� � ����� x.*/
double ObchFunc(double x);

/*���������� xi*/
double ObchXi(double a,double b, double h, int i);

/*���������� h*/
double ObchH(double a, double b);
