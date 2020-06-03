#pragma once
#include <stdio.h>
#include <math.h>
#include "Function.h"
#include "Legandre.h"
#include "Trapezoid.h"
#include <malloc.h>

/*
Виконує створення і першопочаткове заповнення матриці, що відпловідає нормальній системі.
Розмірність системи залежить від необхідного ступеню поліному m.
*/
double** StvorenniaNormalnojiSystemy(int m, double a, double b, double eps, int r);