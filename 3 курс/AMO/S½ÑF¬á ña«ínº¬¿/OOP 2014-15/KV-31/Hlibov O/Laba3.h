/************************************************************************
>file: Laba3.h
>author: Hlibov A.R
>group: KV-31, FPM
>written: 6/12/2014
>last modified: 9/12/2014
************************************************************************/
#include <stdlib.h>
#include <stdio.h>
#include <math.h> 
#include <io.h>
#include <stddef.h> 
#include <string.h>

#ifndef _SCANNER_H
#define _SCANNER_H

typedef struct {
	char manufacturer[128];// изготовитель
	int year;	 // год изготовления
	char model[128];// наименование модели
	float price;// цена
	int x_size;// горизонтальный размер области сканирования
	int y_size;// вертикальный размер области сканирования
	int optr; // оптическое разрешение
} SCAN_INFO;

typedef struct field_names{
	int index;
	float float_value;
	int int_value;
	char field[128];
}field_names;

typedef struct{
	int rec_nmb;//number of records
	SCAN_INFO *recs;//records 
}RECORD_SET;

void write_in_file(char *filename, SCAN_INFO *h);
void write_from_csv(char *filename1, char *filename2);
int make_index(char *dba, char *field_name);
RECORD_SET * get_recs_by_index(char *dba, char *indx_field);
void reindex(char *dba);
void del_from_dba(char *filename, int number);
int write_to_txt(char *filename, int max_price);

#endif