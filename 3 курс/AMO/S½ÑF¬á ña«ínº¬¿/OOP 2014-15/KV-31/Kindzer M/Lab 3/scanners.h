#ifndef _LAB3_
#define _LAB3_
#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <string.h>
#include <stdlib.h>

typedef struct {
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


void scanner_input(SCAN_INFO scan);
SCAN_INFO* get_rec(char *csv_line);
void input_dba();
int make_index(char *dba, char *field_name);
RECORD_SET* get_recs_by_index(char *dba, char *indx_field);
void reindex(char *dba);
int del_scan(char *dba, int n);
void input_txt_by_value(float price);
void print_dba();

#endif	