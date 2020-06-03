#pragma once
#include <stdio.h>
#include <math.h>


/*Обчислює задану за варіантом функцію.*/
long double Obchyslennia_Funktsiji_Za_Variantom(long double x);

/*Обчислює четверту похідну функції, заданої за варіантом.*/
long double Obchyslennia_Chetvertoji_Pohidnoji_Funktsiji_Za_Variantom(long double x);

/*Обчислює максимум четвертої похідної на інтервалі.*/
long double Max_Chetvertoji_Pohidnoji(long double a, long double b);

/*Обчислює кроку інтегрування.*/
long double Obchyslennia_Kroku_Integruvannia(long double a, long double b, long double eps);