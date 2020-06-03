#pragma once
#include <stdio.h>
#include <math.h>

/*������ �������� ������� Systema ������� m*n �� �������� Stovpets ������� m. ������� ��������.*/
double* MnozhenniaMatrytsiNaStovpets(double** Systema, int n, int Stovpets);

/*������ �������� ��������� ������� Stovpets2 �� ������� Stovpets1.*/
void DodavanniaStovptsiv(double* Stovpets1, double* Stovpets2, int n);

/*������ �������� �������� ������� Stovpets2 �� ������� Stovpets1.*/
double* VidnimanniaStovptsiv(double* Stovpets1, double* Stovpets2, int n);

/*������ ���������� ������� Systema �� ������� �������� n �� �������, ���� ����� Xi ��������� ����� ���� ������.
������� ������� �������� �������.*/
double** PryvedenniaSystemy(double** Systema, int n);

/*��������� ���� ������ �������� ������� ������ ������� n.
������� ����.*/
double SumaModulivElementivRiadka(double* Riadok, int n);

/*��������� m-����� ������� Systema ��������� n*n. ������� m-�����.*/
double ZnahodzhenniaNormy(double** Systema, int n);

/*�������� ��������� ���������� ��������. ������� ��������� ����������.*/
double ObchyslenniaKonstantyZavershennia(double q, double eps);

/*������ �� ��������� ������� �������� �� ������� m. ������� ��������.*/
double* VydilenniaStovptsia(double** Systema, int n, int m);

/*�������� ������� ���������� �� ������� �������� Systema �� ��������� n*n, ��������� ����������� NablyzhenniaPoperednie.
������� ������ ���������.*/
double* ObchyslenniaNablyzhennia(double** Systema, int n, double* NablyzhenniaPoperednie);

/*����`��� ���� ������� ������ ��������. ������� ������ ������.*/
double* RozvjazanniaMetodomIteratsiji(double** Systema, int n, double eps);