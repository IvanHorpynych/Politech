#pragma once
#pragma warning (disable:4996)
#include <stdio.h>
#include <math.h>
#include "Func.h"

/*���������� q �� �������� ��������. ������� ������� ����������� ��
y' = exp(x) + 1/x + 10 */
double ObchQ(double a, double b);

/*���������� Alpha �� �������� ��������*/
double ObchAlpha(double a, double b);

/*���������� ����������.*/
double ObchNabl(double x, double a, double b);

/*���������� ��������� ����� ����������.*/
double ObchConstZav(double q, double e);

/*���������� ������� ���������� �� �������� [a, b].*/
double ObchPershNabl(double a, double b);

/*���������� ����������� �������� ������ �� �������� [a, b] �� ������� e.*/
double* ObchKoren_it(double a, double b, double e);

/*��������� ����� ������� ������*/
void VykPershChast(double eps, double KrZminy, int KilkZmin);