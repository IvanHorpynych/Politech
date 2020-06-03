#pragma once
#pragma warning (disable:4996)

#include <stdio.h>
#include <math.h>
#include "Func.h"

/*Обчислення наступноъ точки для зсуву.*/
double ObchTochky(double a, double b);

/*Виконання зсуву однієї із меж.*/
void Zsuv(double& a, double& b, double xn);

/*Обчислення наближеного значення кореня на відрізку [A, B] із заданою точністю.*/
double* ObchKoren_bis(double A, double B, double e);