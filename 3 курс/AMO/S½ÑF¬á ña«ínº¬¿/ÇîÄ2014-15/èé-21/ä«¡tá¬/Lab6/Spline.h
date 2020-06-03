#pragma once
#include <stdio.h>
#include <math.h>
#include <malloc.h>
#include "Metod_jedynogo_podilu.h"

/*Обчислює коефіцієнт a.
Ai = fi.*/
double ObchyslenniaA(double (*pt2Func)(double), double x);

/*Виконує обчислення H.
H = Xi - (Xi_)*/
double ObchyslenniaH(double Xi, double Xi_);

/*Виконує обчислення Hi.
Hi = Masx[i] - Masx[i-1]*/
double ObchyslenniaHi(double* MasX, int i);

/*Обчислює і-й вільний член.*/
double ObchyslenniaVilnogoChlena(double (*pt2Func)(double), double* MasX, int i);

/*Виконує побудову системи рівнянь для пошуку коефіцієнтів Сі.*/
double** PobudovaSystemyRivnian(double (*pt2Func)(double), double* MasX, int n);

/*Виконує обчислення масиву коефіцієнтів Сі методом єдиного поділу.*/
double* ObchyslenniaMasyvuC(double** Systema, int n);

/*Виконує обчислення масиву коефіцієнтів Ai.*/
double* ObchyslenniaMasyvuA(double (*pt2Func)(double), double* MasX, int n);

/*Обчислює коефіцієнт D.*/
double ObchyslenniaD(double Ci, double Ci_, double Hi);

/*Виконує обчислення масиву коефіцієнтів Di.*/
double* ObchyslenniaMasyvuD(double* MasX, double* MasC, int n);

/*Обчислює коефіцієнт B.*/
double ObchyslenniaB(double Ci, double Di, double Fi, double Fi_, double Hi);

/*Виконує обчислення масиву коефіцієнтів Bi.*/
double* ObchyslenniaMasyvuB(double (*pt2Func)(double), double* MasX, double* MasC, double* MasD, int n);

/*Виконує обчислення Si.*/
double ObchyslenniaS(double Ai, double Bi, double Ci, double Di, double Xi, double X);

/*Виконує обчислення натурального кубічного сплайна.
Кількість часткових проміжків - n.
Кількість підпроміжків - m.*/
void KubichnyjSplajn(double (*pt2Func)(double), double a, double b, int n, int m);