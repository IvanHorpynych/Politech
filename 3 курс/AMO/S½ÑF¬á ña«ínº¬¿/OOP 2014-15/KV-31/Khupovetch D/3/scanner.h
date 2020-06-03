/*
File: header.h
Author: Khupovetch Dmitry, KV-31
Written: 1.12.2014
*/

#pragma warning (disable : 4996)
#include <stdlib.h>
#include<stdio.h>
#include <malloc.h>
#include <string.h>

typedef struct {
	char manufacturer[127];
	int year;
	char model[128];
	double price;
	int x_size;
	int y_size;
	int optr;
} SCAN_INFO;

typedef struct 
{
	int rec_nmb;//number of records
	SCAN_INFO *recs;//records
}RECORD_SET;


void scanner_r();
SCAN_INFO get_rec(char *);
void scan_file();
int make_index(char *, char *);
RECORD_SET* get_recs_by_index(char *, char *);
void reindex(char *);
int del_scan(char *, int);
void comf_price(char *, float);
