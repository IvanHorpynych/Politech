#pragma once
#include <stdio.h>
#include <math.h>
#include <malloc.h>
#include "Metod_jedynogo_podilu.h"

/*�������� ���������� a.
Ai = fi.*/
double ObchyslenniaA(double (*pt2Func)(double), double x);

/*������ ���������� H.
H = Xi - (Xi_)*/
double ObchyslenniaH(double Xi, double Xi_);

/*������ ���������� Hi.
Hi = Masx[i] - Masx[i-1]*/
double ObchyslenniaHi(double* MasX, int i);

/*�������� �-� ������ ����.*/
double ObchyslenniaVilnogoChlena(double (*pt2Func)(double), double* MasX, int i);

/*������ �������� ������� ������ ��� ������ ����������� ѳ.*/
double** PobudovaSystemyRivnian(double (*pt2Func)(double), double* MasX, int n);

/*������ ���������� ������ ����������� ѳ ������� ������� �����.*/
double* ObchyslenniaMasyvuC(double** Systema, int n);

/*������ ���������� ������ ����������� Ai.*/
double* ObchyslenniaMasyvuA(double (*pt2Func)(double), double* MasX, int n);

/*�������� ���������� D.*/
double ObchyslenniaD(double Ci, double Ci_, double Hi);

/*������ ���������� ������ ����������� Di.*/
double* ObchyslenniaMasyvuD(double* MasX, double* MasC, int n);

/*�������� ���������� B.*/
double ObchyslenniaB(double Ci, double Di, double Fi, double Fi_, double Hi);

/*������ ���������� ������ ����������� Bi.*/
double* ObchyslenniaMasyvuB(double (*pt2Func)(double), double* MasX, double* MasC, double* MasD, int n);

/*������ ���������� Si.*/
double ObchyslenniaS(double Ai, double Bi, double Ci, double Di, double Xi, double X);

/*������ ���������� ������������ �������� �������.
ʳ������ ��������� ������� - n.
ʳ������ ��������� - m.*/
void KubichnyjSplajn(double (*pt2Func)(double), double a, double b, int n, int m);