#include "scanners.h"

void print_dba(){
	FILE *f;
	SCAN_INFO scan_temp;
	int i, count;

	f = fopen("Scanners.dba", "rb");
	count = fgetc(f);               
	printf("%d\n", count);
	for (i = 0; i < count; i++){
		fread(&scan_temp, sizeof(scan_temp), 1, f);
		printf("%s\n", scan_temp.manufacturer);
		printf("%d\n", scan_temp.year);
		printf("%s\n", scan_temp.model);
		printf("%.2f\n", scan_temp.price);
		printf("%d\n", scan_temp.x_size);
		printf("%d\n", scan_temp.y_size);
		printf("%d\n", scan_temp.optr);
		printf("\n");
	}
	fclose(f);
}

void scanner_input(SCAN_INFO scan){
	FILE *f, *tmp;
	int count, new_count, i;
	SCAN_INFO scan_temp;
	f = fopen("Scanners.dba", "rb");
	count = fgetc(f);
	new_count = count;
	if (count != 0){
		for (i = 0; i < count; i++){
			fread(&scan_temp, sizeof(scan_temp), 1, f);
			if (!strcmp(scan_temp.manufacturer, scan.manufacturer) &&
				scan_temp.year == scan.year &&
				!strcmp(scan_temp.model, scan.model) &&
				scan_temp.price == scan.price &&
				scan_temp.x_size == scan.x_size &&
				scan_temp.y_size == scan.y_size &&
				scan_temp.optr == scan.optr) return;
		}
	}
	new_count++;
	tmp = fopen("tmp.dba", "w+b");
	fputc(new_count, tmp);
	rewind(f);
	fgetc(f);
	for (i = 0; i < count; i++){
		fread(&scan_temp, sizeof(scan_temp), 1, f);
		fwrite(&scan_temp, sizeof(scan_temp), 1, tmp);
	}
	fclose(f);
	f = fopen("Scanners.dba", "wb");
	rewind(tmp);
	fgetc(tmp);
	fputc(new_count, f);

	for (i = 0; i < count; i++){
		fread(&scan_temp, sizeof(scan_temp), 1, tmp);
		fwrite(&scan_temp, sizeof(scan_temp), 1, f);
	}


	fwrite(&scan, sizeof(scan), 1, f);

	fclose(f);
	fclose(tmp);
	remove("tmp.dba");
}

SCAN_INFO* get_rec(char *csv_line){
	SCAN_INFO *rec;
	int i = 0, j = 0, rec_f = 0, len;
	char *tmp;

	if (!(csv_line)) return NULL;
	rec = (SCAN_INFO*)malloc(sizeof(SCAN_INFO));
	len = strlen(csv_line);

	tmp = (char*)calloc(1, sizeof(char));
	for (; i <= len; i++){
		if (csv_line[i] != ';' && csv_line[i] != '\0'){
			tmp = (char*)realloc(tmp, sizeof(char)*(j + 2));
			tmp[j] = csv_line[i];
			j++;
		}
		else{
			tmp[j] = '\0';
			switch (rec_f){
			case 0: strcpy(rec->manufacturer, tmp);
					break;
			case 1: rec->year = atoi(tmp);
					break;
			case 2: strcpy(rec->model, tmp);
					break;
			case 3: rec->price = atof(tmp);
					break;
			case 4: rec->x_size = atoi(tmp);
					break;
			case 5: rec->y_size = atoi(tmp);
					break;
			case 6: rec->optr = atoi(tmp);
					break;
			}
			j = 0;
			free(tmp);
			tmp = (char*)calloc(1, sizeof(char));
			rec_f++;
		}
	}
	free(tmp);
	return rec;
}

void input_dba(){
	FILE *csv_f, *dba_f;
	int i, counter = 0;
	char buf[128];
	SCAN_INFO *tmp;

	csv_f = fopen("Scanners.csv", "r");
	if(csv_f == NULL) return;
	while (fgets(buf, sizeof(buf), csv_f)) counter++;

	dba_f = fopen("Scanners.dba", "wb");
	fputc(counter, dba_f);
	rewind(csv_f);
	while (fgets(buf, sizeof(buf), csv_f)){
		tmp = get_rec(buf);
		fwrite(tmp, sizeof(*tmp), 1, dba_f);
		}
	fclose(dba_f);
	fclose(csv_f);
}

typedef struct {
	int id;
	void *el;
}VECT;

int str_cmp(const void* a, const void* b)
{
	VECT *pa = (VECT*)a;
	VECT *pb = (VECT*)b;
	return strcmp((char*)pa->el, (char*)pb->el);
}

int int_cmp(const void* a, const void* b)
{
	VECT *pa = (VECT*)a;
	VECT*pb = (VECT*)b;
	int *pia = (int*)pa->el;
	int *pib = (int*)pb->el;
	return *pia - *pib;
}

int float_cmp(const void* a, const void* b)
{
	VECT *pa = (VECT*)a;
	VECT *pb = (VECT*)b;
	float *pfa = (float*)pa->el;
	float *pfb = (float*)pb->el;
	return *pfa - *pfb;
}

int make_index(char *dba, char *field_name){
	VECT *vect;
	FILE *tmp, *f;
	int count, i;
	SCAN_INFO scan;
	int *p_i;
	float *p_f;
	char *name;

	f = fopen(dba, "rb");

	if (f == NULL) {
		fclose(f);
		return 0;
	}

	count = fgetc(f);
	vect = (VECT*)malloc(sizeof(VECT)*count);
	for (i = 0; i < count; i++){
		fread(&scan, sizeof(scan), 1, f);
		if (!strcmp("manufacturer", field_name)){
			vect[i].el = (void*)malloc(sizeof(char)*(strlen(scan.manufacturer) + 1));
			strcpy((char*)vect[i].el, scan.manufacturer);
		}
		if (!strcmp("year", field_name)){
			vect[i].el = (void*)malloc(sizeof(int));
			p_i = (int*)vect[i].el;
			*p_i = scan.year;
		}
		if (!strcmp("model", field_name)){
			vect[i].el = (void*)malloc(sizeof(char)*(strlen(scan.model) + 1));
			strcpy((char*)vect[i].el, scan.model);
		}
		if (!strcmp("price", field_name)){
			vect[i].el = (void*)malloc(sizeof(float));
			p_f = (float*)vect[i].el;
			*p_f = scan.price;
		}
		if (!strcmp("x_size", field_name)){
			vect[i].el = (void*)malloc(sizeof(int));
			p_i = (int*)vect[i].el;
			*p_i = scan.x_size;
		}
		if (!strcmp("y_size", field_name)){
			vect[i].el = (void*)malloc(sizeof(int));
			p_i = (int*)vect[i].el;
			*p_i = scan.y_size;
		}
		if (!strcmp("optr", field_name)){
			vect[i].el = (void*)malloc(sizeof(int));
			p_i = (int*)vect[i].el;
			*p_i = scan.optr;
		}
		vect[i].id = i;
	}
	fclose(f);
	if (!strcmp("manufacturer", field_name) || !strcmp("model", field_name)) qsort(vect, count, sizeof(VECT), str_cmp);
	else if (!strcmp("price", field_name)) qsort(vect, count, sizeof(VECT), float_cmp);
		 else qsort(vect, count, sizeof(VECT), int_cmp);

	name = (char*)malloc(sizeof(char)*(strlen(field_name) + 5));
	strcpy(name, field_name);
	strcat(name, ".idx");
	f = fopen(name, "wb");
	for (i = 0; i < count; i++){
		fputc(vect[i].id, f);
		free(vect[i].el);
	}
	free(vect);
	fclose(f);

	return 1;
}

RECORD_SET* get_recs_by_index(char *dba, char *field_name){
	FILE *f, *idx;
	RECORD_SET *rec_set;
	int count, i, j;
	char *name;

	f = fopen(dba, "rb");
	make_index(dba, field_name);
	name = (char*)malloc(sizeof(char)*(strlen(field_name) + 5));
	strcpy(name, field_name);
	strcat(name, ".idx");
	idx = fopen(name, "rb");

	if (f == NULL){
		fclose(f);
		fclose(idx);
		return NULL;
	}

	count = fgetc(f);
	rec_set = (RECORD_SET*)malloc(sizeof(RECORD_SET));
	rec_set->recs = (SCAN_INFO*)malloc(sizeof(SCAN_INFO)*count);

	rewind(f);
	for (i = 0; i < count; i++){
		int rec_id = fgetc(idx);
		fgetc(f);
		for (j = 0; j <= rec_id; j++) fread(&rec_set->recs[i], sizeof(rec_set->recs[i]), 1, f);
		rewind(f);
	}
	rec_set->rec_nmb = count;
	fclose(f);
	fclose(idx);
	return rec_set;
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

int del_scan(char *dba, int n){
	FILE *f, *tmp;
	SCAN_INFO scan;
	int count, i;

	f = fopen(dba, "rb");
	if (!f) return 0;

	count = fgetc(f);
//	if (count < n - 1) return 0;

	tmp = fopen("tmp.dba", "w+b");
	fputc(--count, tmp);
	for (i = 0; i < count; i++){
		fread(&scan, sizeof(scan), 1, f);
		if (i == n) fread(&scan, sizeof(scan), 1, f);
		fwrite(&scan, sizeof(scan), 1, tmp);
	}
	fclose(f);
	f = fopen(dba, "wb");
	rewind(tmp);
	fputc(fgetc(tmp), f);
	for (i = 0; i < count; i++){
		fread(&scan, sizeof(scan), 1, tmp);
		fwrite(&scan, sizeof(scan), 1, f);
	}
	fclose(f);
	fclose(tmp);
	remove("tmp.dba");
	reindex(dba);

	return 1;
}

void input_txt_by_value(float price){
	FILE *txt, *dba;
	RECORD_SET *rec_set;
	int i;

	dba = fopen("Scanners.dba", "rb");
	if (!dba) return;

	rec_set = get_recs_by_index("Scanners.dba", "price");
	txt = fopen("Scanners_price.txt", "w");
	for (i = 0; i < rec_set->rec_nmb; i++){
		if (rec_set->recs[i].price > price) break;
		fprintf(txt, "%s;%d;%s;%.2f;%d;%d;%d\n", rec_set->recs[i].manufacturer, rec_set->recs[i].year,
			rec_set->recs[i].model, rec_set->recs[i].price, rec_set->recs[i].x_size, rec_set->recs[i].y_size, rec_set->recs[i].optr);
	}

	fclose(txt);
}



