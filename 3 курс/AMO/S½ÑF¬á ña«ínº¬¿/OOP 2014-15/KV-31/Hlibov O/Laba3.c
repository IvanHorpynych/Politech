/************************************************************************
>file: Laba3.c
>author: Hlibov A.R
>group: KV-31, FPM
>written: 6/12/2014
>last modified: 9/12/2014
************************************************************************/

#include "Laba3.h"

void write_in_file(char *filename, SCAN_INFO *h){
	FILE *fdba = fopen(filename, "r+b");
	int d = 1, flag = 0, k;
	SCAN_INFO sc;
	fseek(fdba, 0, SEEK_END);
	if (ftell(fdba) == 0){
		fwrite(&d, sizeof(int), 1, fdba);
		fwrite(h, sizeof(SCAN_INFO), 1, fdba);
	}
	else{
		fseek(fdba, 0, SEEK_SET);
		fread(&d, 4, 1, fdba);
		k = d;
		do{
			fread(&sc, sizeof(SCAN_INFO), 1, fdba);
			if (strcmp(sc.manufacturer, h->manufacturer) == 0)
				flag = 1;
			else k--;
		} while ((k >= 1) && (flag == 0));
		if (k < 1){
			d += 1;
			fseek(fdba, 0, SEEK_SET);
			fwrite(&d, 4, 1, fdba);
			fseek(fdba, sizeof(SCAN_INFO)*(d - 1), SEEK_CUR);
			fwrite(h, sizeof(SCAN_INFO), 1, fdba);
		}
	}
	fclose(fdba);
}

void write_from_csv(char *filename1, char *filename2){
	SCAN_INFO struc;
	FILE *fcsv;
	if ((fcsv = fopen(filename1, "r")) == NULL) return;
	char *str = (char*)malloc(sizeof(SCAN_INFO)), *str1 = (char*)malloc(30), *c = ";";
	int flag = 1, k = 1;

	flag = fscanf(fcsv, "%s", str);
	strcat(str, ";");
	if (flag == 1){
		do {
			for (int i = 1; i <= 7; i++){
				strncpy(str1, str, strlen(str) - strlen(strstr(str, c)));
				str1[strlen(str) - strlen(strstr(str, c))] = '\0';
				switch (i){
				case 1:
						  for (int j = 0; j <= strlen(str1); j++)
							  struc.manufacturer[j] = str1[j];
						  break;
				case 2:
						  struc.year = atoi(str1);
						  break;
				case 3:
						  for (int j = 0; j <= strlen(str1); j++)
							  struc.model[j] = str1[j];
						  break;
				case 4:
						  struc.price = atof(str1);
						  break;
				case 5:
						  struc.x_size = atoi(str1);
						  break;
				case 6:
						  struc.y_size = atoi(str1);
						  break;
				case 7:
						  struc.optr = atoi(str1);
						  break;
				}
				str = strstr(str, c) + 1;
			}
			write_in_file(filename2, &struc);
			
			flag = fscanf(fcsv, "%s", str);
			strcat(str, ";");
		} while (flag != -1);
	}
}

int make_index(char *dba, char *field_name){

	char *filename = (char*)malloc(strlen(field_name) + 4);
	strcpy(filename, field_name);
	strcat(filename, ".idx");
	FILE *fidx = fopen(filename, "wb"), *fdba = fopen(dba, "rb");
	SCAN_INFO struc;
	int d, type = 0;
	fread(&d, 4, 1, fdba);
	fseek(fdba, sizeof(int), SEEK_SET);
	field_names *mass = (field_names*)malloc(d*sizeof(field_names));

	if (strcmp(field_name, "manufacturer") == 0)
		type = 1;
	if (strcmp(field_name, "year") == 0)
		fseek(fdba, 128, SEEK_CUR);
	if (strcmp(field_name, "model") == 0){
		fseek(fdba, 132, SEEK_CUR);
		type = 1;
	}
	if (strcmp(field_name, "price") == 0){
		fseek(fdba, 260, SEEK_CUR);
		type = 2;
	}
	if (strcmp(field_name, "x_size") == 0)
		fseek(fdba, 264, SEEK_CUR);
	if (strcmp(field_name, "y_size") == 0)
		fseek(fdba, 268, SEEK_CUR);
	if (strcmp(field_name, "optr") == 0)
		fseek(fdba, 272, SEEK_CUR);
	
	for (int i = 0; i < d; i++){
		switch (type){
		case 0:
			fread(&mass[i].int_value, 4, 1, fdba);
			break;
		case 1:
			fread(&mass[i].field, 128, 1, fdba);
			break;
		case 2:
			fread(&mass[i].float_value, 4, 1, fdba);
			break;
		}

		mass[i].index = i;
		if (type == 1)
			fseek(fdba, sizeof(SCAN_INFO)-128, SEEK_CUR);
		else
			fseek(fdba, sizeof(SCAN_INFO)-4, SEEK_CUR);
	}
	switch (type){
	case 0:
		for (int i = 0; i < d; i++) {
			for (int j = 0; j < d - i - 1; j++) {
				if (mass[j].int_value >= mass[j + 1].int_value){
					field_names tmp = mass[j];
					mass[j] = mass[j + 1];
					mass[j + 1] = tmp;
				}
			}
		}
		break;
	case 1:
		for (int i = 0; i < d; i++) {
			for (int j = 0; j < d - i - 1; j++) {
				if (strcmp(mass[j].field, mass[j+1].field) >= 0){
					field_names tmp = mass[j];
					mass[j] = mass[j + 1];
					mass[j + 1] = tmp;
				}
			}
		}
		break;
	case 2:
		for (int i = 0; i < d; i++) {
			for (int j = 0; j < d - i - 1; j++) {
				if (mass[j].float_value >= mass[j + 1].float_value){
					field_names tmp = mass[j];
					mass[j] = mass[j + 1];
					mass[j + 1] = tmp;
				}
			}
		}
		break;
	}

	for (int i = 0; i < d; i++)
		fwrite(&mass[i].index, sizeof(int), 1, fidx);
	fclose(fdba);
	fclose(fidx);
}

RECORD_SET * get_recs_by_index(char *dba, char *indx_field){
	char *field_name = (char*)malloc(strlen(indx_field));
	strcpy(field_name, indx_field);
	field_name[strlen(indx_field) - 4] = '\0';

	make_index(dba, field_name);
	FILE *fdba;
	FILE *fidx;
	int d, j;
	if ((fdba = fopen(dba, "rb")) == NULL) return NULL;
	if ((fidx = fopen(indx_field, "rb")) == NULL) return NULL;
	fseek(fdba, 0L, SEEK_SET);
	fread(&d, sizeof(int), 1, fdba);
	RECORD_SET *mass;
	SCAN_INFO struc;

	mass = (RECORD_SET*)malloc(sizeof(RECORD_SET));
	mass->rec_nmb = d;
	mass->recs = (SCAN_INFO*)malloc(d*sizeof(SCAN_INFO));

	for (int i = 0; i < d; i++){	
		fread(&j, sizeof(int), 1, fidx);
		fseek(fdba, sizeof(int)+j*sizeof(SCAN_INFO), SEEK_SET);
		fread(&struc, sizeof(SCAN_INFO), 1, fdba);
		mass->recs[i] = struc;
	}
	fclose(fdba);
	fclose(fidx);
	return mass;
}

void reindex(char *dba){
	make_index(dba, "manufacturer");
	make_index(dba, "year");
	make_index(dba, "model");
	make_index(dba, "price");
	make_index(dba, "x_size");
	make_index(dba, "y_size");
	make_index(dba, "optr");
}

void del_from_dba(char *filename, int number){
	RECORD_SET *buffer = get_recs_by_index("scanners.dba", "manufacturer.idx");
	FILE *fdba = fopen(filename, "wb");
	int count = buffer->rec_nmb - 1;
	fwrite(&count, sizeof(int), 1, fdba);
	SCAN_INFO struc;

	for (int i = 0; i < buffer->rec_nmb; i++){
		struc = buffer->recs[i];
		if (i != number)
			fwrite(&buffer->recs[i], sizeof(SCAN_INFO), 1, fdba);	
	}
	fclose(fdba);
	reindex("scanners.dba");
}

int write_to_txt(char *filename, int max_price){
	RECORD_SET *buffer = get_recs_by_index("scanners.dba", "price.idx");
	FILE *ftxt;
	if ((ftxt = fopen(filename, "wb")) == NULL) return -1;

	int i = 0;
	while ((buffer->recs[i].price <= max_price) && (i < buffer->rec_nmb)){
		int k = buffer->recs[i].price;
		fprintf(ftxt, "%s;%d;%s;%.2f;%d;%d;%d;\n", buffer->recs[i].manufacturer, buffer->recs[i].year, buffer->recs[i].model,
			buffer->recs[i].price, buffer->recs[i].x_size, buffer->recs[i].y_size, buffer->recs[i].optr);
		i++;
	}

	fclose(ftxt);
	return 1;
}
