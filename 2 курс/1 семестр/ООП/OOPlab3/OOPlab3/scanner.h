#pragma once
#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <string.h>
#define MAX_LEN_ELEM 48
#define MAX_ELEM 100

typedef struct{
	char menufacturer[127]; // ��������
	char model[128]; // ����� �����
	int year; // �� ������������
	float price; // ����
	int x_size; // �������������� ����� ������ �������
	int y_size; // ����������� ����� ������ �������
	int optr; // ������� ����������
} SCAN_INFO;

typedef struct{
	int rec_nmb; //����� ������
	SCAN_INFO *recs; // �����
} RECORD_SET;

// ��������� �������
void init_file();
RECORD_SET* get_recs_by_index(char *dba, char *indx_field);
void reindex(char *dba);
int del_scan(char*dba, int n);
int price_scan(char *dba, char *txt, float prc);

void print_dba(char *dba);
void print_bin(char *bin);
void prit_rec_set(RECORD_SET *rec);