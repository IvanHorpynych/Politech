#pragma once
#include <stdio.h>
#include <math.h>
#include "Funktsija.h"
#include "MnogochlenLezhandra.h"
#include "FormulaTrapetsij.h"
#include <malloc.h>

/*
������ ��������� � �������������� ���������� �������, �� �������� ��������� ������.
��������� ������� �������� �� ����������� ������� ������� m.
*/
double** StvorenniaNormalnojiSystemy(int m, double a, double b, double eps, int r);