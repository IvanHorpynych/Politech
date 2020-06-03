/************************************************************************
*file: Lab3.h
*author: Gnedoj D. O.
*group: KV-31, FPM
*written: 10/10/2014
*last modified: 15/10/2014
************************************************************************/
#include <stdlib.h>
#include <stdio.h>
#include <math.h> 
#include <io.h>
#include <stddef.h> // for  size_t
#include <string.h>

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
//for make_index on fields with type of int
typedef struct numbered_integer{
	int position;
	int value;
}numbered_integer;
//for make_index on fields with type of float
typedef struct numbered_float{
	int position;
	float value;
}numbered_float;
//for make_index on fields with type of char*
typedef struct numbered_string{
	int position;
	char value[60];
}numbered_string;

int save_to_dat(char*, SCAN_INFO*); //add struckt to file.dba
int create_file(char*, char*); //create file.dba from file.csv
int make_index(char *dba, char *field_name); //sort and create file.idx
RECORD_SET * get_recs_by_index(char *dba , char *indx_field );
void reindex(char *dba); //sort all fields
int del_scan(char *dba, int n); //dell struckt from file.dba
int from_dba_to_txt(char *dba, char *txt, float limit_price); //send information from .dba to .txt

#endif