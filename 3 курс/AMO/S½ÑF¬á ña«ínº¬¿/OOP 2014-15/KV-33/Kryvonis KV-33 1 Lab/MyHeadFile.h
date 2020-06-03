/******************************************************************
*Laboratory work 1
*File: MyHeadFile.h
*Description:Header file which includes different library
*Author: Artem Kryvonis
*written: 08/09/2014
*last modified 13/09/2014
******************************************************************/
#ifndef _OOP_LAB1_H
#define _OOP_LAB1_H

#include <stdio.h>
#include <stdlib.h>

int substr(char*, char*); //������� �� ������� ������ �������� � ����� string1, � ����� ���������� �������, ����� string2. 
int subseq(char*, char*); //������� �� ������� �������� �������  ������ ������������� ������� ����� string1 � string2
char ispal(char*); //������� �� ������� 1, ���� string � ���������� � 0 � � ���������� �������
char* makepal(char*); //������� �� ������ �� �������� �������� �� ����� �������, ���������� ���� �� �������� ������� �� ����� �������� ����� ������� � ����� ����� � ������� �������� �� ����� � ����������
double* txt2double(char*, int*); //������� �� ������ ����� � ���������� ����� � ������� �������� �� �����

#endif;
