/************************************************************************
*file: OOP_lab3.c
*synopsis: all base functions which work with lists.
* These functions are declared in the include file "OOP_LAB3.h".
*related files: none
*author: Chernysh Andrey
*written: 20/11/2014
*last modified: 26/11/2014
************************************************************************/
#pragma warning (disable : 4996)
#include <stdlib.h>
#include <stdio.h>
#include "OOP_lab3.h"

int is_open(char *dba){
	FILE *f;
	if ((f = fopen(dba, "w+b")) == NULL){
		printf("\nfile %s didn't opened! ERROR !!!", dba);
		return -1;
	}
	else {
		printf("\nFile %s succesfully opened!", dba);
		return 0;
	}
	fclose(f);
}
int write2dba(char *dba, SCAN_INFO* scan){
	FILE *f_dba;
	SCAN_INFO b;
	int k = 0, i = 0;
	if ((f_dba = fopen(dba, "r+b")) == NULL){
		printf("\nfile didn't opened! ERROR !!!");
		return -1;
	}
	rewind(f_dba);
	fread(&k, sizeof(int), 1, f_dba);
	if (k == 0){
		k = 1;
		fwrite(&k, sizeof(int), 1, f_dba);
		fwrite(scan, sizeof(SCAN_INFO), 1, f_dba);
	}
	else{
		fseek(f_dba, sizeof(int), SEEK_SET);
		while (i < k + 1){
			fread(&b, sizeof(SCAN_INFO), 1, f_dba);
			if ((strcmp(b.manufacturer, scan->manufacturer) == 0) && (strcmp(b.model, scan->model) == 0))
				break;
			i++;
		}
		if (i >= k + 1){
			k++;
			rewind(f_dba);
			fwrite(&k, sizeof(int), 1, f_dba);
			fseek(f_dba, sizeof(SCAN_INFO)*(k - 1), SEEK_CUR);
			fwrite(scan, sizeof(SCAN_INFO), 1, f_dba);
		}
	}
	fclose(f_dba);
	return 0;
}
int read_from_dba(char *file_name){
	FILE *f;
	SCAN_INFO b;
	if ((f = fopen(file_name, "r+b")) == NULL){
		printf("\nfile didn't opened! ERROR !!!");
		return -1;
	}
	int i = 0, k;
	rewind(f);
	fread(&k, sizeof(int), 1, f);
	while (i < k){
		fread(&b, sizeof(SCAN_INFO), 1, f);
		printf("\n\tManufacturer : %s;\n\tModel : %s;\n\tYear : %d;\n\tPrice : %f;", b.manufacturer, b.model, b.year, b.price);
		printf(" \n\tOptical distribution : %d; \n\tx_size : %d;\n\ty_size : %d", b.optr, b.x_size, b.y_size);
		i++;
		printf("\n");
	}
	fclose(f);
	return 0;
}
SCAN_INFO* get_rec(char *csv_line){
	char* str = (char*)malloc(strlen(csv_line)*sizeof(char));
	SCAN_INFO* rec = (SCAN_INFO*)malloc(sizeof(SCAN_INFO));
	int i = 0, j = 0, k = 0;
	while (i != strlen(csv_line)){
		j = 0;
		while ((csv_line[i] != ';') && (i != strlen(csv_line) - 1)){
			str[j] = csv_line[i];
			j++;
			i++;
		}
		str[j] = '\0';
		switch (k){
		case 0: strcpy(rec->manufacturer, str);
		case 1: strcpy(rec->model, str);
		case 2: rec->year = atoi(str);
		case 3: rec->price = atof(str);
		case 4: rec->optr = atoi(str);
		case 5:	rec->x_size = atoi(str);
		case 6: rec->y_size = atoi(str);
		}
		k++;
		i++;
	}
	return rec;
}
int csv2dba(char *dba, char *csv){
	FILE *f_csv;
	char* str = (char*)malloc(127 * sizeof(char));
	if ((f_csv = fopen(csv, "r")) == NULL){
		printf("\nfile %s didn't opened! ERROR !!!", csv);
		return -1;
	}

	while (!feof(f_csv)){
		fgets(str, 123, f_csv);
		write2dba(dba, get_rec(str));
	}
	fclose(f_csv);
	return 0;
}
int make_index(char *dba, char *field_name, int type){
	FILE *f;
	SCAN_INFO temp;//struct for reading from file;
	if ((f = fopen(dba, "r+b")) == NULL){
		printf("\nfile didn't opened! ERROR !!!");
		return -1;
	}
	int i = 0, k;
	fread(&k, sizeof(int), 1, f);
	int *id = (int*)malloc((k)*sizeof(int));//array for indexes;
	for (int j = 0; j < k; j++)//making array with indexes;
		id[j] = j;
	char **str = (char**)malloc((k)*sizeof(char*));//array for model,manufacturer;
	int *tempint = (int*)malloc(k*sizeof(int));//array for year,x_size,y_size;
	float *tempfloat = (float*)malloc(k*sizeof(float));//array for price;
	while (i < k + 1){
		str[i] = (char*)malloc(128 * sizeof(char));
		fread(&temp, sizeof(SCAN_INFO), 1, f);
		if (field_name == "manufacturer")
			strcpy(str[i], temp.manufacturer);
		else if (field_name == "model")
			strcpy(str[i], temp.model);
		else if (field_name == "year")
			tempint[i] = temp.year;
		else if (field_name == "x_size")
			tempint[i] = temp.x_size;
		else if (field_name == "optr")
			tempint[i] = temp.optr;
		else if (field_name == "y_size")
			tempint[i] = temp.y_size;
		else if (field_name == "price")
			tempfloat[i] = temp.price;
		else return -1;
		i++;
	}
	char* tempch;//variable for changing strings;
	int tem;//variable for changing integer digits;
	for (int s = 0; s < k - 1; s++){
		for (int j = 0; j < k - 1; j++){
			if ((field_name == "manufacturer") || (field_name == "model")){
				if (strcmp(str[j], str[j + 1]) > 0){
					i = id[j];
					id[j] = id[j + 1];
					id[j + 1] = i;
					tempch = str[j];
					str[j] = str[j + 1];
					str[j + 1] = tempch;
				}
			}
			else if ((field_name == "x_size") || (field_name == "y_size") || (field_name == "year") || (field_name == "optr")){
				if (tempint[j] > tempint[j + 1]){
					tem = id[j];
					id[j] = id[j + 1];
					id[j + 1] = tem;
					tem = tempint[j];
					tempint[j] = tempint[j + 1];
					tempint[j + 1] = tem;
				}
			}
			else if (tempfloat[j] > tempfloat[j + 1]){
				tem = id[j];
				id[j] = id[j + 1];
				id[j + 1] = tem;
				tem = tempfloat[j];
				tempfloat[j] = tempfloat[j + 1];
				tempfloat[j + 1] = tem;
			}
		}
	}
	char *name = (char*)malloc(strlen(field_name)*sizeof(char));
	strcpy(name, field_name);
	strcat(name, ".idx");
	FILE *f_ind;

	if ((f_ind = fopen(name, "w+b")) == NULL){
		printf("\nfile %s didn't opened! ERROR !!!", name);
		return -1;
	}


	if (type == 1)
		printf("\n\t%s : ", name);
	for (int j = 0; j < k; j++){
		fwrite(&id[j], sizeof(int), 1, f_ind);
		if (type == 1){
			printf(" %d ", id[j]);
		}
	}
	fclose(f);
	fclose(f_ind);
}
RECORD_SET *get_recs_by_index(char *dba, char *indx_field){
	FILE *f, *f_idx;
	RECORD_SET *a, *b;
	b = (RECORD_SET*)malloc(sizeof(RECORD_SET));
	a = (RECORD_SET*)malloc(sizeof(RECORD_SET));
	int k, i = 0, j = 0;
	if ((f = fopen(dba, "r+b")) == NULL)
		return -1;
	if ((f_idx = fopen(indx_field, "r+b")) == NULL)
		return -1;
	fread(&k, sizeof(int), 1, f);
	a->rec_nmb = k;
	a->recs = (SCAN_INFO*)malloc(a->rec_nmb*(sizeof(SCAN_INFO)));
	b->rec_nmb = k;
	b->recs = (SCAN_INFO*)malloc(b->rec_nmb*(sizeof(SCAN_INFO)));
	while (i < k){
		fread(&a->recs[i], sizeof(SCAN_INFO), 1, f);
		i++;
	}
	i = 0;
	while (i < k ){
		fread(&j, sizeof(int), 1, f_idx);
		b->recs[i] = a->recs[j];
		i++;
	}
	return b;
	fclose(f);
	fclose(f_idx);
}
void reindex(char *dba, int type){
	make_index(dba, "manufacturer", type);
	make_index(dba, "year", type);
	make_index(dba, "model", type);
	make_index(dba, "x_size", type);
	make_index(dba, "y_size", type);
	make_index(dba, "price", type);
	make_index(dba, "optr", type);
}
int del_scan(char *dba, int n){
	FILE *f;
	if ((f = fopen(dba, "r+b")) == NULL)
		return -1;
	int k;
	fread(&k, sizeof(int), 1, f);
	if (n > k){ return -1; }
	else {
		if (k < 1) {

			return -1;
		}
	}
	SCAN_INFO temp;
	int i = 1;

	while ((n + i) != k + 1){
		fseek(f, sizeof(int)+(n + i)*sizeof(SCAN_INFO), SEEK_SET);
		fread(&temp, sizeof(SCAN_INFO), 1, f);
		fseek(f, sizeof(int)+((n + i - 1) * sizeof(SCAN_INFO)), SEEK_SET);
		fwrite(&temp, sizeof(SCAN_INFO), 1, f);
		i++;
	}

	k--;
	fseek(f, 0L, SEEK_SET);
	fwrite(&k, sizeof(int), 1, f);
	_chsize(f->_file, sizeof(int)+(k*(sizeof(SCAN_INFO))));
	fclose(f);
	reindex(dba,0);
}
int make_txt(char *dba, float price){
	FILE *f_dba, *f_txt;
	if ((f_dba = fopen(dba, "r+b")) == NULL)
	{
		return -1;
	}
	char *name = (char*)malloc(sizeof(char));
	printf("\nEnter name of .txt file : ");
	scanf("%s", name);
	if ((f_txt = fopen(name, "w+")) == NULL)
	{
		return -1;
	}

	SCAN_INFO temp;
	int k, i = 0;
	fread(&k, sizeof(int), 1, f_dba);
	while (i < k){
		fread(&temp, sizeof(SCAN_INFO), 1, f_dba);
		if (temp.price < price){
			fprintf(f_txt, "\n\tManufacturer : %s;\n\tModel : %s;\n\tYear : %d;\n\tPrice : %f;", temp.manufacturer, temp.model, temp.year, temp.price);
			fprintf(f_txt, " \n\tOptical distribution : %d; \n\tx_size : %d;\n\ty_size : %d", temp.optr, temp.x_size, temp.y_size);
		}
		fprintf(f_txt, "\n");
		i++;
	}
	fclose(f_dba);
	fclose(f_txt);
	return 0;
}

