#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <malloc.h>
/* SUBSTR ���������� ������ �������� � ������ string1, � �������� ���������� ���������, ������ string2 */
int substr (const char *, const char *);
/* SUBSEQ ���������� ���������� �����  ����� ��������������������� �������� ����� */
int subseq (const char *, const char *);
/* ISPAL ���������� 1, ���� string �������� ����������� � 0 � � ��������� ������ */
char ispal (const char *);
/* MAKEPAL ����������� ������ � ��������� �������� � ��� ���������� ����� ��������  */
char* makepal (const char *);
/* TXT2DOUBLE ��������� ��� ����� � ������������ ������� � ���������� ��������� �� ���� */
double* txt2double (const char *, int *);