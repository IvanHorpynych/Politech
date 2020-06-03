/************************************************************************
*file: OOP_lab3.h
*purpose: declarations for file functions, types, constants
*author: Andrey Chernysh
*written: 20/11/2014
*last modified: 26/11/2014
*************************************************************************/
#ifndef _OOP_LAB3_H
#define _OOP_LAB3_H
#include <stdio.h>
#include <stdlib.h>

typedef struct{
	char manufacturer[127];// ������������
	int year;	 // �� ������������
	char model[128];// ������������ �����
	float price;// ����
	int x_size;// �������������� ����� ������ ����������
	int y_size;// ������������ ����� ������ ����������
	int optr; // �������� ������
} SCAN_INFO;

typedef struct{
	int rec_nmb;//number of records
	SCAN_INFO *recs;//records 
}RECORD_SET;

int is_open(char *dba);
int write2dba(char *dba, SCAN_INFO* scan);
int read_from_dba(char *file_name);
SCAN_INFO* get_rec(char *csv_line);
int csv2dba(char *dba, char *csv);
int make_index(char *dba, char *field_name, int type);
RECORD_SET *get_recs_by_index(char *dba, char *indx_field);
void reindex(char *dba, int type);
int del_scan(char *dba, int n);
int make_txt(char *dba, float price);

#endif 