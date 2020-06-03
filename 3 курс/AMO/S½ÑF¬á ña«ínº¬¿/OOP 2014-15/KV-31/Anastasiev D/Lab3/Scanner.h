/************************************************************************
*file: Scanner.h
*purpose: file
*author: Anastasiev D.
*written: 24/10/2014
*last modified: 21/11/2014
*************************************************************************/
#ifndef _SCANNER_H
#define _SCANNER_H

#include <io.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

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


void inputscanner(SCAN_INFO scan);
void first_input();
int make_index(char *dba, char *field_name);
RECORD_SET * get_recs_by_index(char *dba , char *indx_field );
void reindex(char *dba);
int del_scan(char *dba, int n);
void read_price_txt(char *dba);
#endif