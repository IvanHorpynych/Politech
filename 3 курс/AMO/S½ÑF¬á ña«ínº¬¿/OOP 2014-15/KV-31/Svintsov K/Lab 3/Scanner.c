/***********************************************************************
*file: scanner.c
*synopsis: Includes functions for operating with text,binary files
* These functions are declared in the include file "scanner.h".
*related files: none
*author: Kyrylo Svintsov
*written: 26.11.2014
*last modified: 26.11.2014
************************************************************************/

#include"Scanner.h"
#include<string.h>

//prototypes of function which are used in qsort()
int cmpfunc_i(const void *, const void *);
int cmpfunc_f(const void *, const void *);
int cmpfunc_s(const void *, const void *);

/*-----------------------------------------------------------*
Name: get_rec
Usage: get_rec(string_for_transform)

Synopsis: fill in members of structure "rec" the following 
transformation of each element in string parametr.
Return value: SCAN_INFO (structure`s type)
*--------------------------------------------------------- */
SCAN_INFO get_rec(char *csv_line){
	SCAN_INFO rec;
	int k, j, i;
	char *add_ptr = NULL;
	k = 1;
	for (i = 0; i < strlen(csv_line); i++, k++){
		j = 0;
		if (k < 3){
			if (k == 1){
				do{
					rec.manufacturer[j] = csv_line[i];
					i++;
					j++;
				} while (csv_line[i] != ';');
				rec.manufacturer[j] = '\0';
			}
			else{
				do{
					rec.model[j] = csv_line[i];
					i++;
					j++;
				} while (csv_line[i] != ';');
				rec.model[j] = '\0';
			}
		}
		else{
			do{
				add_ptr = (char *)realloc(add_ptr, (j+1)*(sizeof(char)));
				if (add_ptr){
					add_ptr[j] = csv_line[i];
					i++;
					j++;
				}
				else
					exit(1);
			} while ((csv_line[i] != ';') && (csv_line[i] != '\0'));
			add_ptr[j] = '\0';
			switch (k){

			case 3:{
					   rec.year = atoi(add_ptr);
					   break;
			}
			case 4:{
					   rec.price = atof(add_ptr);
					   break;
			}
			case 5:{
					   rec.x_size = atoi(add_ptr);
					   break;
			}
			case 6:{
					   rec.y_size = atoi(add_ptr);
					   break;
			}
			case 7:{
					   rec.optr = atoi(add_ptr);
					   break;
			}
			default:{
						printf("Smth went wrong");
						break;
			}

			}

		}
	}
	return rec;
}

/*-----------------------------------------------------------*
Name: isEqual
Usage: isEqual(rec1,rec2)
Synopsis: Check two records for equal memmbers
Return value: 1 if equal or 0 if not
*--------------------------------------------------------- */
char isEqual(const SCAN_INFO a, const SCAN_INFO b){
	if ((strcmp(a.manufacturer, b.manufacturer) == 0) && (strcmp(a.model, b.model) == 0) && (a.optr == b.optr)
		&& (a.price == b.price) && (a.x_size == b.x_size) && (a.year == b.year) && (a.y_size == b.y_size))
		return 1;
	else return 0;
}

/*-----------------------------------------------------------*
Name: scanner_inp
Usage: scanner_inp(rec1)
Synopsis: Write to file "Scaners.dba" record from parametr
At the start of file contains the counter of records
If file also have the record in parametr,it doesn`t write it
Return value: void function
*--------------------------------------------------------- */
void scanner_inp(const SCAN_INFO scan){
	FILE * fPTR = NULL;
	SCAN_INFO cmp;
	int counter,i;
	char flag = 0;
	if ((fPTR = fopen("Scaners.dba", "rb+")) == NULL){
		if ((fPTR = fopen("Scaners.dba", "wb")) == NULL){
			printf("File could not be opened");
		}
		//if file is not created
		else{
			counter = 1;
			fwrite(&counter, sizeof(int), 1, fPTR);
			fwrite(&scan, sizeof(SCAN_INFO), 1, fPTR);
			fclose(fPTR);
		}
	}
	else{
		fread(&counter, sizeof(int), 1, fPTR);
		for (i = 1; i <= counter; i++){
			fread(&cmp, sizeof(SCAN_INFO), 1, fPTR);
			if (isEqual(cmp, scan) == 1){
				flag = 1;
				break;
			}
		}
		if (flag == 0){
			fseek(fPTR, 0, SEEK_END);
			fwrite(&scan, sizeof(SCAN_INFO), 1, fPTR);
			rewind(fPTR);
			counter++;
			fwrite(&counter, sizeof(int), 1, fPTR);
			fclose(fPTR);
		}	
	}
	
}

/*-----------------------------------------------------------*
Name: scanner_r
Usage: scanner_r()
Synopsis:read the records from file "Scaners.dba" and bring to
screen a counter and record`s member"price"
Return value: void function
*--------------------------------------------------------- */
void scanner_r(){
	FILE * fPTR = NULL;
	SCAN_INFO res;
	int counter,i;
	if ((fPTR = fopen("Scaners.dba", "rb")) == NULL)
		printf("File could not be opened");
	else{
		fread(&counter, sizeof(int), 1, fPTR);
		printf("%d ", counter);
		for (i = 1; i <= counter; i++){
			fread(&res, sizeof(SCAN_INFO), 1, fPTR);
			printf("%.2f\t", res.price);
		}
		
		fclose(fPTR);
	}
}

/*-----------------------------------------------------------*
Name: scan_file
Usage: scan_file()
Synopsis: Gets strings from file "Scaners.csv" and send then to function
scanner_inp() as a parametr
Return value: void function
*--------------------------------------------------------- */
void scan_file(){
	FILE *fPTR = NULL;
	char out_s[300];
	if ((fPTR = fopen("Scaners.csv", "r"))==NULL)
		printf("File could not be opened");
	else{
		while (!feof(fPTR)){
			fgets(out_s, sizeof(out_s), fPTR);
			scanner_inp(get_rec(out_s));
		}
		fclose(fPTR);
	}
}

int cmpfunc_i(const void * a, const void * b)
{
	return (*(int*)a - *(int*)b);
}

int cmpfunc_f(const void * a, const void * b)
{
	return (*(float*)a - *(float*)b);
}

int cmpfunc_s(const void * a, const void * b)
{
	return strcmp(*(char**)a, *(char**)b);
}

/*-----------------------------------------------------------*
Name: make_index
Usage: make_index(dba_name,"year")
Synopsis: creates the special arrays for sorting the members of records
then by copy of secondary array find the indexes and write them to idx file
Return value: 0 if function succesfully ended
*--------------------------------------------------------- */
int make_index(char *dba, char *field_name){
	FILE *fPTR;
	SCAN_INFO rec;
	if ((fPTR = fopen(dba, "rb")) == NULL)
		exit(1);
	else{
		int n, i;
		fread(&n, sizeof(int), 1, fPTR);
		if ((strcmp(field_name, "manufacturer") == 0) || (strcmp(field_name, "model") == 0)){
			char **ptr1 = calloc(n, sizeof(char*));
			char **ptr2 = calloc(n, sizeof(char*));
			for (i = 0; i < n; i++){
				ptr2[i] = calloc(127, sizeof(char));
				ptr1[i] = calloc(127, sizeof(char));
				fread(&rec, sizeof(SCAN_INFO), 1, fPTR);
				if (strcmp(field_name, "manufacturer") == 0){
					strcpy(ptr1[i], rec.manufacturer);
					strcpy(ptr2[i], rec.manufacturer);
				}
				else{
					strcpy(ptr1[i], rec.model);
					strcpy(ptr2[i], rec.model);
				}
			}
			fclose(fPTR);
			//sort
			qsort(ptr2, n, sizeof(char*), cmpfunc_s);
			//searching of indexes and writing down them to file
			char f_name[25];
			strcpy(f_name, field_name);
			strcat(f_name, ".idx");
			if ((fPTR = fopen(f_name, "w")) == NULL)
				exit(1);
			else{
				int j;
				for (i = 0; i < n; i++){
					j = 0;
					while (strcmp(ptr2[i], ptr1[j]) != 0)
						j++;
					fprintf(fPTR, "%d ", j);
				}
				fclose(fPTR);
			}
		}
		else{
			if (strcmp(field_name, "price") == 0){
				float *ptr1 = calloc(n, sizeof(float));
				float *ptr2 = calloc(n, sizeof(float));
				for (i = 0; i < n; i++){
					fread(&rec, sizeof(SCAN_INFO), 1, fPTR);
					ptr1[i] = ptr2[i] = rec.price;
				}
				fclose(fPTR);
				//sort
				qsort(ptr2, n, sizeof(float), cmpfunc_f);
				//searching of indexes and writing down them to file
				char f_name[25];
				strcpy(f_name, field_name);
				strcat(f_name, ".idx");
				//searching of indexes and writing down them to file
				if ((fPTR = fopen(f_name, "w")) == NULL)
					exit(1);
				else{
					int j;
					for (i = 0; i < n; i++){
						j = 0;
						while (ptr2[i] != ptr1[j])
							j++;
						fprintf(fPTR, "%d ", j);
					}
					free(ptr1);
					free(ptr2);
					fclose(fPTR);
				}
			}
			else{
				int *ptr1 = calloc(n, sizeof(int));
				int *ptr2 = calloc(n, sizeof(int));
				for (i = 0; i < n; i++){
					fread(&rec, sizeof(SCAN_INFO), 1, fPTR);
					if (strcmp(field_name, "year") == 0)
						ptr1[i] = ptr2[i] = rec.year;
					else{
						if (strcmp(field_name, "x_size") == 0)
							ptr1[i] = ptr2[i] = rec.x_size;
						else{
							if (strcmp(field_name, "y_size") == 0)
								ptr1[i] = ptr2[i] = rec.y_size;
							else{
								if (strcmp(field_name, "optr") == 0)
									ptr1[i] = ptr2[i] = rec.optr;
								else{
									exit(1);
								}
							}
						}
					}
				}
					fclose(fPTR);
					//sort
					qsort(ptr2, n, sizeof(int), cmpfunc_i);
					//searching of indexes and writing down them to file
					char f_name[25];
					strcpy(f_name, field_name);
					strcat(f_name, ".idx");
					//searching of indexes and writing down them to file
					if ((fPTR = fopen(f_name, "w")) == NULL)
						exit(1);
					else{
						int j;
						for (i = 0; i < n; i++){
							j = 0;
							while (ptr2[i] != ptr1[j])
								j++;
							fprintf(fPTR, "%d ", j);
						}
						fclose(fPTR);
					}
				}
			}
		}
	return 0;
};

/*-----------------------------------------------------------*
Name: get_recs_by_index
Usage: get_recs_by_index(dba_name,"year")
Synopsis:creates the array of records. 
Return value: pointer to RECORD_SET
*--------------------------------------------------------- */
RECORD_SET *get_recs_by_index(char *dba, char *indx_field){
	FILE *fPTR_d;
	FILE *fPTR_i;
	char f_name[25];
	strcpy(f_name, indx_field);
	strcat(f_name, ".idx");
	if ((fPTR_d = fopen(dba, "rb")) == NULL)
		exit(1);
	else{
		RECORD_SET *rec_s=NULL;
		rec_s = malloc(sizeof(RECORD_SET));
		if (!rec_s)
			exit(1);
		else{
			fread(&(rec_s->rec_nmb), sizeof(int), 1, fPTR_d);
			rec_s->recs = calloc(rec_s->rec_nmb, sizeof(SCAN_INFO));
			int i,j,index;
			SCAN_INFO rec;
			if ((fPTR_i = fopen(f_name, "r")) == NULL)
				exit(1);
			else{
				for (i = 0; i < rec_s->rec_nmb; i++){
					fscanf(fPTR_i, "%d", &index);
					//search by index in dba file
					for (j = 0; j <= index; j++)
						fread(&rec, sizeof(SCAN_INFO), 1, fPTR_d);

					rec_s->recs[i] = rec;
					fseek(fPTR_d, sizeof(int), SEEK_SET);
				}
			}
		}
		fclose(fPTR_d);
		fclose(fPTR_i);

		return rec_s;
	}
}

/*-----------------------------------------------------------*
Name: reindex
Usage: reindex(dba_name)
Synopsis:creates idx file for all members of type SCAN_INFO
Return value: void function
*--------------------------------------------------------- */
void reindex(char *dba){
	make_index(dba, "manufacturer");
	make_index(dba, "model");
	make_index(dba, "year");
	make_index(dba, "x_size");
	make_index(dba, "y_size");
	make_index(dba, "optr");
	make_index(dba, "price");
}


/*-----------------------------------------------------------*
Name: del_scan
Usage: del_scan(dba_name,2)
Synopsis:deletes the pointed record from dba file
Return value: return 1 if function succesfulle ended or 0 if not
*--------------------------------------------------------- */
int del_scan(char *dba, int n){
	FILE *fPTR;
	if ((fPTR=fopen(dba, "rb+")) == NULL)
		return 0;
	else{
		int counter;
		SCAN_INFO rec;
		int i;
		fread(&counter, sizeof(int), 1, fPTR);
		for (i = 1; i <= counter - n - 1; i++){
			fseek(fPTR, (n+i)*sizeof(SCAN_INFO)+sizeof(int), SEEK_SET);
			fread(&rec, sizeof(SCAN_INFO), 1, fPTR);
			fseek(fPTR, (n+i-1)*sizeof(SCAN_INFO)+sizeof(int), SEEK_SET);
			fwrite(&rec, sizeof(SCAN_INFO), 1, fPTR);
		}
		rewind(fPTR);
		counter--;
		fwrite(&counter, sizeof(int), 1, fPTR);
		_chsize(_fileno(fPTR), counter*sizeof(SCAN_INFO)+sizeof(int));
		fclose(fPTR);
		reindex(dba);
	}
	return 1;
}

/*-----------------------------------------------------------*
Name: comf_price
Usage: comf_price(dba_name,value)
Synopsis: reads from dba file the infromation about prices and
write down to txt file the models with acceptable price
Return value: void fucntion
*--------------------------------------------------------- */
void comf_price(char *dba, float price){
	FILE *fPTR_d;
	FILE *fPTR_t;
	if ((fPTR_d = fopen(dba, "rb")) == NULL)
		exit(1);
	else{
		int counter, i;
		SCAN_INFO rec;
		fread(&counter, sizeof(int), 1, fPTR_d);
		if ((fPTR_t = fopen("pr.txt", "w")) == NULL)
			exit(1);
		else{
			fprintf(fPTR_t, "%s", "Models with acceptable price:");
			for (i = 0; i < counter; i++){
				fread(&rec, sizeof(SCAN_INFO), 1, fPTR_d);
				if (rec.price <= price)
					fprintf(fPTR_t, "%s,", rec.model);
			}
			fputc('.', fPTR_t);
			fclose(fPTR_d);
			fclose(fPTR_t);
		}
	}
}