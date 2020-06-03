/************************************************************************
*file: svanner.h
*synopsis: declarations of functions, types, constants
*author: Kyrylo Svintsov
*written: 19.11.2014
*last modified: 19.11.2014
************************************************************************/

#ifndef SCANNER_H
#define SCANNER_H

#include <stdlib.h>
#include<stdio.h>
#include <conio.h>
#include <malloc.h>
#include<io.h>

typedef struct {
	char manufacturer[127];// maker
	int year; 
	char model[128];// model`s name
	float price;
	int x_size;
	int y_size;
	int optr; // optic resolution
} SCAN_INFO;

typedef struct{
	int rec_nmb;//number of records
	SCAN_INFO *recs;//records
}RECORD_SET;


void scanner_r();
SCAN_INFO get_rec(char *);
void scan_file();
int make_index(char *, char *);
RECORD_SET * get_recs_by_index(char *, char *);
void reindex(char *);
int del_scan(char , int );
void comf_price(char *, float);


#endif