#pragma once
#pragma warning (disable:4996)

#include <stdio.h>
#include <math.h>
#include "Func.h"

/*���������� ��������� ����� ��� �����.*/
double ObchTochky(double a, double b);

/*��������� ����� ���� �� ���.*/
void Zsuv(double& a, double& b, double xn);

/*���������� ����������� �������� ������ �� ������ [A, B] �� ������� �������.*/
double* ObchKoren_bis(double A, double B, double e);