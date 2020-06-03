#pragma once
#include <stdio.h>
#include <math.h>

/*Виконує множення параметра Tochnist на величину Krok*/
void ZminaTochnosti(double& Tochnist, double Krok);

/*Знаходження наступного члена ряду за відомим попереднім.*/
double ZnahNastupnogo(double Poperednij, double X, int K);

/*Знаходження точки, в якій обчислюється значення функції.*/
double ZnahTochky(double a, double b);

/*Обчислення функції в точці x.*/
double ObchFunc(double x);

/*Обчислення xi*/
double ObchXi(double a,double b, double h, int i);

/*Обчислення h*/
double ObchH(double a, double b);
