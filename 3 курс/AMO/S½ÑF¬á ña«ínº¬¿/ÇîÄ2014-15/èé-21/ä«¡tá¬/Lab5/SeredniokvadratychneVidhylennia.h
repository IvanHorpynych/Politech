#pragma once
#include "FormulaTrapetsij.h"
#include "NormalnaSystema.h"
#include "Metod_jedynogo_podilu.h"

/*
������� �������� ��������������������� ��������� ������� pt2Func1(double)
� ������� pt2Func2(double, int) �� ������ [a, b].
*/
double ObchyslenniaVidhylennia(double (*pt2Func1)(double),double (*pt2Func2)(double, int, double*),
							   double (*pt2IntFunc1)(double (*pt2Func1)(double),double (*pt2Func2)(double, int, double*), double, double, double, int, int, double*), 
							   double a, double b, int r, int m2, double* koef);

/*
������ ����� ������ �������� ������ ������������� ����������, ��� ��������
��������������������� ��������� ���� ����� �� eps.
������� ������.
*/
int ObchyslenniaStepeniUzagalnenogoMnogochlena(double (*pt2Func1)(double),double (*pt2Func2)(double, int, double*),
											   double (*pt2IntFunc1)(double (*pt2Func1)(double),double (*pt2Func2)(double, int, double*), double, double, double, int, int, double*), 
											   double a, double b, int r, double eps);