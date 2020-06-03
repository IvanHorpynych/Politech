#pragma once
#include <stdio.h>
#include <malloc.h>
#include <string.h>

/*�������� �����, ������� ������� ����� �� �������� ��������� ��������, �������� ������ ����������-��������, ������ ����.
������� True � ��� �������� ������ ��������. ������� False � ��� ��������� ������ ��������.*/
bool PerevirkaGolovnogo(double* Rivniannia, int Index);

/*������� �����, ��������� � ������� Index �� ������� n �� �������� Mnozhnyk.*/
void MnozhenniaRiadka(double* Rivniannia, int Index, int n, double Mnozhnyk);

/*�������� ����� ����� Vidjemnyk �� ����� Zmenshuvane. ������� ����� �������� n.*/
void VidnimanniaPochlenne(double* Zmenshuvane, double* Vidjemnyk, int n);

/*������ ����� ������� ������� � ���������� �������� ���������, ������� �������� Index,
��������� � ������� Nomer, � ������ ������ Systema � ������ ������ n.
������� ����� �������.*/
int PoshukRivnianniaZNenuliovymGolovnymElementom(int Index, int Nomer, double** Systema, int n);

/*������ ��������� ���� ������ ���� ����� Riadok1 � Riadok2 � �������� n.*/
void ObminVmistom(double* Riadok1, double* Riadok2, int n);

/*������ ��������� ������� ������� ������ Systema ����� ��������� ��������, �� ����� �� ������� Index.*/
void ObnulenniaStovptsia(double** Systema, int Index, int KilkistRivnian, int n);

/*������ ��������� ������� ������� ������ �� �������� ���������.*/
void ObnulenniaNyzhniojiChastyny(double** Systema, int KilkistRivnian, int n);

/*������ �������� ������� �� ������� Index ������� ������ Systema �� ������ �������.*/
void MnozhenniaStovptsia(double** Systema, int Index, double Mnozhnyk);

/*������ �������� �� �������� �����, �� ����� �� ������� n, ��� �������� �����, �� ������ �� ��������� Index � ���������, �� ����� �� ������� n.*/
void VidnimanniaVidOstanniogo(double* Rivniannia, int Index, int n);

/*������ �������� ��� ������ ������.*/
double* ZvorotnijHid(double** Systema, int KilkistRivnian, int n);