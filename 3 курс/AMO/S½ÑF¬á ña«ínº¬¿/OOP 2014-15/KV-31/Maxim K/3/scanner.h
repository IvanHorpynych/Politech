/************************************************************************
*file: scanner.h
*author: Maxim K. E.
*group: KV-31, FPM
*written: 20/11/2014
*last modified: 9/12/2014
************************************************************************/
#include <stddef.h> // for  size_t
#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>
#include <string.h>
#include <io.h>

#ifndef _SCANNER_H
#define _SCANNER_H

typedef struct _tag{
	char manufacturer[60];
	int year;
	char model[60];
	float price;
	int x_size;
	int y_size;
	int optr;
}SCAN_INFO;
typedef struct{
	int rec_nmb;//number of records
	SCAN_INFO *recs;//records 
}RECORD_SET;


void scantodba(char *, SCAN_INFO *sc);
int create_file(char*, char*); //create file.dba from file.csv
int make_index(char *dba, char *field_name);
RECORD_SET * get_recs_by_index(char *dba, char *indx_field);
void reindex(char *dba); //sort all fields
int del_scan(char *dba, int*);

int dba_to_txt(char *dba, char *txt, float limit_price);
#endif