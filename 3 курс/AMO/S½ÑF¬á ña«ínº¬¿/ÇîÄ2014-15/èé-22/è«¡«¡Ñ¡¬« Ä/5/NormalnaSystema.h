#pragma once
#include <stdio.h>
#include <math.h>
#include "Function.h"
#include "Legandre.h"
#include "Trapezoid.h"
#include <malloc.h>

/*
������ ��������� � �������������� ���������� �������, �� �������� ��������� ������.
��������� ������� �������� �� ����������� ������� ������� m.
*/
double** StvorenniaNormalnojiSystemy(int m, double a, double b, double eps, int r);