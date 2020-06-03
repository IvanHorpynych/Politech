#pragma once
#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <string.h>
#define MAX_LEN_ELEM 48
#define MAX_ELEM 100

typedef struct{
	char menufacturer[127]; // виробник
	char model[128]; // назва моделі
	int year; // рік виготовлення
	float price; // ціна
	int x_size; // горизонтальний розмір області сканеру
	int y_size; // вертикаьний розмір області сканеру
	int optr; // оптичне розширення
} SCAN_INFO;

typedef struct{
	int rec_nmb; //номер запису
	SCAN_INFO *recs; // запис
} RECORD_SET;

// Прототипи функцій
void init_file();
RECORD_SET* get_recs_by_index(char *dba, char *indx_field);
void reindex(char *dba);
int del_scan(char*dba, int n);
int price_scan(char *dba, char *txt, float prc);

void print_dba(char *dba);
void print_bin(char *bin);
void prit_rec_set(RECORD_SET *rec);