/*
File: File_func.h
Synopsis: contains functions for working with binary file
Author: Savin A.D.
Group: KV-31, FPM
Written: 08.11.2014
*/


#ifndef File_func_H
#define File_func_H
#include <stddef.h> // for  size_t
#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>
#include <string.h>
#include <io.h>


typedef struct {
	char manufacturer[127];// изготовитель
	int year;	 // год изготовления
	char model[128];// наименование модели
	float price;// цена
	int x_size;// горизонтальный размер области сканирования
	int y_size;// вертикальный размер области сканирования
	int optr; // оптическое разрешение
} SCAN_INFO;

typedef struct{
	int rec_nmb;//number of records
	SCAN_INFO *recs;//records 
} RECORD_SET;


void scantodba(SCAN_INFO *sc);
void csvtodba(FILE *source, FILE *reciever);
int make_index(char *dba, char *field_name);
RECORD_SET * get_recs_by_index(char *dba, char *indx_field);
void reindex(char *dba);
int del_scan(char *dba, int n);
void showbyprice(char *dba, int price);
#endif /* File_func_H */
