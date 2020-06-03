/*
File: string_func.c
Korshun.A.S
*/

#include <stddef.h> // for  size_t
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <malloc.h>

#ifndef STRING_FUNC_H
#define STRING_FUNC_H

int substr(const char *string1, const char *string2); //���������� ������ ��������, �� �������� string2 ���������� � string1
int subseq(const char *string1, const char *string2); //���������� ����� ������ �������� ������ ��������� string1 and string2
char ispal(const char *string); //���������� 0, ���� ����� ����������� � 1, ���� ��� ���������
char* makepal(const char *); //������� ���������
double* txt2double(const char *, int *size);

#endif 