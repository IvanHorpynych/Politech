/***********************************************************************
*file: "lab11_Header.h"
*purpose: declarations for laba11.c functions, types, constants
*Author: Anastasiev D. V.
*written 18/09/2014
*last modified: 18/09/2014
***********************************************************************/



#ifndef _HEADER_H
#define _HEADER_H


#include <stdio.h>
#include <string.h>
#include <stdlib.h>

//������� ���������� ������ �������� � �����, ���� ������ ������� ������ - �������� -1
int substr(const char *, const char *);

//������� ���������� ������������ ��������������������� ���� ����� 
int subseq(const char *, const char *);

//������� �������� �� ���������
char ispal( const char *);

/*������� ���������� ��������� �������� �� ��������� ����� ����������� 
���������� ������������ ���������� �������� � ����� ���������� ����� */
char* makepal(const char *);
//������� ���������� ��������� �� ������ ������������� �� ����� ��������� � ������
double* txt2double(const char * , int *);


#endif