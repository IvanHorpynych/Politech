#pragma once
#include "Formula_Simpsona.h"
#include "Func.h"

/*Обчислює початковий крок*/
long double Obchyslennia_Pochatkovogo_Kroku(int r, long double eps);

/*Обчислює Rn.*/
long double Obchyslennia_Zalyshkovogo_Chlena(int r, long double IntegralN, long double Integral2n);

/*Обчислює інтеграл.*/
long double ObchyslenniaIntegrala_pp(long double a, long double b, int n, long double eps, long double(*pt2Func)(long double));

long double* Vykonannia_Drugoji_Chastyny(long double a, long double b, long double eps, long double(*pt2Func)(long double));