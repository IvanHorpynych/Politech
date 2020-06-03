#pragma once
#include <stdio.h>
#include <math.h>

/*
�������� ���������� ���� n ��� ���������� ��������� �� ������� ������� eps.
*/
int ObchyslenniaPochatkovogoKroku(int r, double eps);

/*
�������� ������� ������������� ������ h �� ����� ������� a �� b � ����� n.
*/
double ObchyslenniaElementarnogoVidrizka(double a, double b, int n);

/*
�������� �������� �� ������� pt2Func �� ������ [a, b] � �������� ������������� ������ h � ������ n.
*/
double ObchyslenniaIntegrala(double (*pt2Func)(double), double a, double b, double h, int n);

/*
������� ��������� ����������� �������� �������� �� ������� pt2Func
�� ��������� ������� ������������ pt2IntFunc �� ������ [a, b] �� ������� eps.
������� �� ���������, � �������.
*/
double PodvijnyjPererahunok(double (*pt2Func)(double), double (*pt2IntFunc)(double (*pt2Func)(double), double, double, double, int), 
							double a, double b, double eps, int r);

/*
�������� �������� �� ������� ������� pt2Func1(double, int) � pt2Func2(double, int) �� ������ [a, b] � �������� ������������� ������ h � ������ n.
*/
double ObchyslenniaIntegralaVidDobutku1(double (*pt2Func1)(double, int), double (*pt2Func2)(double, int), double a, double b, double h, int n, int m1, int m2);

/*
������� ��������� ����������� �������� �������� �� ������� ������� pt2Func1(double, int) � pt2Func2(double, int)
�� ��������� ������� ������������ pt2IntFunc1 �� ������ [a, b] �� ������� eps.
������� �� ���������, � �������.
*/
double PodvijnyjPererahunokVidDobutku1(double (*pt2Func1)(double, int),double (*pt2Func2)(double, int),
									  double (*pt2IntFunc1)(double (*pt2Func1)(double, int),double (*pt2Func2)(double, int), double, double, double, int, int, int), 
									  double a, double b, double eps, int r, int m1, int m2);

/*
�������� �������� �� ������� ������� pt2Func1(double) � pt2Func2(double, int) �� ������ [a, b] � �������� ������������� ������ h � ������ n.
*/
double ObchyslenniaIntegralaVidDobutku2(double (*pt2Func1)(double), double (*pt2Func2)(double, int), double a, double b, double h, int n, int m2);

/*
������� ��������� ����������� �������� �������� �� ������� ������� pt2Func1(double) � pt2Func2(double, int)
�� ��������� ������� ������������ pt2IntFunc1 �� ������ [a, b] �� ������� eps.
������� �� ���������, � �������.
*/
double PodvijnyjPererahunokVidDobutku2(double (*pt2Func1)(double),double (*pt2Func2)(double, int),
									   double (*pt2IntFunc1)(double (*pt2Func1)(double),double (*pt2Func2)(double, int), double, double, double, int, int), 
									  double a, double b, double eps, int r, int m2);

//**************************************************************************
//**************************************************************************
/*
�������� �������� �� ������ ������� pt2Func1(double) � pt2Func2(double, int) �� ������ [a, b] � �������� ������������� ������ h � ������ n.
*/
double ObchyslenniaIntegralaVidRiznytsi(double (*pt2Func1)(double), double (*pt2Func2)(double, int, double*), double a, double b, double h, int n, int m2, double*koef);

/*
������� ��������� ����������� �������� �������� �� ������ ������� pt2Func1(double) � pt2Func2(double, int, double*)
�� ��������� ������� ������������ pt2IntFunc1 �� ������ [a, b] �� ������� eps.
������� �� ���������, � �������.
*/
double PodvijnyjPererahunokVidRiznytsi(double (*pt2Func1)(double),double (*pt2Func2)(double, int, double*),
									   double (*pt2IntFunc1)(double (*pt2Func1)(double),double (*pt2Func2)(double, int, double*), double, double, double, int, int, double*), 
									  double a, double b, double eps, int r, int m2, double* koef);