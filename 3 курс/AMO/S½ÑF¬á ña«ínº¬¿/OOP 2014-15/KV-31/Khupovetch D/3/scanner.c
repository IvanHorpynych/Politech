#include "scanner.h"
#include<string.h>

SCAN_INFO get_rec(char *csv_line)
{
	SCAN_INFO basa;
	int k = 1, j = 0, i;
	char *st = NULL;
	for (i = 0; i < strlen(csv_line); i++,  k++)
	{
		j = 0;
		switch (k)
		{
		case 1:
		{
				  while (csv_line[i] != ';')
				  {
					  basa.manufacturer[j] = csv_line[i];
					  i++;
					  j++;
				  }
				  basa.manufacturer[j] = '\0';
				  break;
		}

		case 2:
		{
				  while (csv_line[i] != ';')
				  {
					  basa.model[j] = csv_line[i];
					  i++;
					  j++;
				  }
				  basa.model[j] = '\0';
				  break;
		}
		case 3:
		{
				  st = (char*)malloc(50 * sizeof(char*));
				  while (csv_line[i] != ';')
				  {
					  st[j] = csv_line[i];
					  i++;
					  j++;
				  }
				  basa.year = atoi(st);
				  free(st);
				  st = NULL;
				  break;
		}
		case 4:
		{
				  st = (char*)malloc(50 * sizeof(char*));
				  while (csv_line[i] != ';')
				  {
					  st[j] = csv_line[i];
					  i++;
					  j++;
				  }
				  basa.price = atof(st);
				  free(st);
				  st = NULL;
				  break;
		}
		case 5:
		{
				  st = (char*)malloc(50 * sizeof(char*));
				  while (csv_line[i] != ';')
				  {
					  st[j] = csv_line[i];
					  i++;
					  j++;
				  }
				  basa.x_size = atoi(st);
				  free(st);
				  st = NULL;
				  break;
		}
		case 6:
		{
				  st = (char*)malloc(50 * sizeof(char*));
				  while (csv_line[i] != ';')
				  {
					  st[j] = csv_line[i];
					  i++;
					  j++;
				  }
				  basa.y_size = atoi(st);
				  free(st);
				  st = NULL;
				  break;
		}
		case 7:
		{
				  st = (char*)malloc(50 * sizeof(char*));
				  while (csv_line[i] != '\0')
				  {
					  st[j] = csv_line[i];
					  i++;
					  j++;
				  }
				  basa.optr = atoi(st);
				  free(st);
				  st = NULL;
				  break;
		}
		default:
		{
				   printf("error\n");
				   break;
		}
		}
	}
		return basa;
	}

int if_raven(const SCAN_INFO *bs1,const SCAN_INFO *bs2)
 {
	if ((strcmp(bs1->manufacturer,bs2->manufacturer)==0) 
	&& (strcmp(bs1->model, bs2->model) == 0) 
	&& (bs1->optr==bs2->optr)
	&& (bs1->price==bs2->price)
	&& (bs1->x_size==bs2->x_size)
	&& (bs1->y_size==bs2->y_size)
	&& (bs1->year == bs2->year))
		return 1;
	else
		return 0;
}

void scanner_inp(const SCAN_INFO scan)
{
	FILE * file_dba = NULL;
	SCAN_INFO help;
	int count, i;
	int boo = 1;
	if ((file_dba = fopen("Scaners.dba", "rb+")) == NULL)
	{
		if ((file_dba = fopen("Scaners.dba", "wb")) == NULL)
		{
			printf("File could not be opened");
		}
		//if file is not created
		else
		{
			count = 1;
			fwrite(&count, sizeof(int), 1, file_dba);
			fwrite(&scan, sizeof(SCAN_INFO), 1, file_dba);
			fclose(file_dba);
		}
	}
	else
	{
		fread(&count, sizeof(int), 1, file_dba);
		for (i = 0; i < count; i++)
		{
			fread(&help, sizeof(SCAN_INFO), 1, file_dba);
			if (if_raven(&help, &scan) == 1)
			{
				boo = 0;
				break;
			}
		}
		if (boo)
		{
			fseek(file_dba, 0, SEEK_END);
			fwrite(&scan, sizeof(SCAN_INFO), 1, file_dba);
			rewind(file_dba);
			count++;
			fwrite(&count, sizeof(int), 1, file_dba);
			fclose(file_dba);
		}
	}
}

void scanner_r()
{
	FILE * file_dba;
	SCAN_INFO help;
	int count, i;
	if ((file_dba = fopen("Scaners.dba", "rb")) == NULL)
		printf("File could not be opened");
	else
	{
		fread(&count, sizeof(int), 1, file_dba);
		printf("%d\n ", count);
		for (i = 0; i < count; i++)
		{
			fread(&help, sizeof(SCAN_INFO), 1, file_dba);
			printf("%s\n", help.manufacturer);
			/*printf("%s\n", help.model);
			printf("%d\n", help.year);
			printf("%f\n", help.price);
			printf("%d\n", help.optr);
			printf("%d\n", help.x_size);
			printf("%d\n", help.y_size);*/
			printf("\n");
		}
		fclose(file_dba);
	}
}

void scan_file()
{
	FILE *file_csv = NULL;
	char st[95];
	if ((file_csv = fopen("Scaners.csv", "r")) == NULL)
		printf("File could not be opened");
	else
	{
		while (!feof(file_csv))
		{
			fgets(st, sizeof(st), file_csv);
			scanner_inp(get_rec(st));
		}
		fclose(file_csv);
	}
}

int make_index(char *dba, char *field_name)
{
	FILE *file_dba;
	SCAN_INFO help;
	if ((file_dba = fopen(dba, "r+b")) == NULL)
	{
		printf("\nfile didn't opened! ERROR !!!");
		return -1;
	}
	int i = 0, size = 0;
	fread(&size, sizeof(int), 1, file_dba);
	int *array_ind = NULL;
	array_ind = (int*)malloc(size*sizeof(int));
	for (i = 0; i < size; i++)
	{
		array_ind[i] = i;
	}
	char **str = (char**)malloc((size)*sizeof(char*));//array for model,manufacturer;
	int *tempint = (int*)malloc(size*sizeof(int));//array for year,x_size,y_size;
	float *tempfloat = (float*)malloc(size*sizeof(float));//array for price;
	for (i = 0; i <= size; i++)
	{
		str[i] = (char*)malloc(128 * sizeof(char));
		fread(&help, sizeof(SCAN_INFO), 1, file_dba);
		if (field_name == "manufacturer")
			strcpy(str[i], help.manufacturer);
		else if (field_name == "model")
			strcpy(str[i], help.model);
		else if (field_name == "year")
			tempint[i] = help.year;
		else if (field_name == "x_size")
			tempint[i] = help.x_size;
		else if (field_name == "optr")
			tempint[i] = help.optr;
		else if (field_name == "y_size")
			tempint[i] = help.y_size;
		else if (field_name == "price")
			tempfloat[i] = help.price;
		else 
			return -1;
	}
	char *cell_str; //variable for string  
	int cell_int;   //variable for integer
	float cell_float; //variable for float
	int boo = 1, k;
	i = 0;
	while (boo) //sorting with flag 
	{
		boo = 0;
		for (i = 0; i < size - 1; i++)
		{
			if ((field_name == "manufacturer") || (field_name == "model"))
			{
				if (strcmp(str[i], str[i + 1]) > 0)
				{
					cell_str = str[i + 1];
					str[i + 1] = str[i];
					str[i] = cell_str;
					k = array_ind[i + 1];
					array_ind[i + 1] = array_ind[i];
					array_ind[i] = k;
					boo = 1;
				}
			}
			else if ((field_name == "year") || (field_name == "x_size") || (field_name == "y_size") || (field_name == "optr"))
			{
				if (tempint[i] > tempint[i + 1])
				{
					cell_int = tempint[i + 1];
					tempint[i + 1] = tempint[i];
					tempint[i] = cell_int;
					k = array_ind[i + 1];
					array_ind[i + 1] = array_ind[i];
					array_ind[i] = k;
					boo = 1;
				}
			}
			else if (field_name == "price")
			{
				if (tempfloat[i] > tempfloat[i + 1])
				{
					cell_float = tempfloat[i + 1];
					tempfloat[i + 1] = tempfloat[i];
					tempfloat[i] = cell_float;
					k = array_ind[i + 1];
					array_ind[i + 1] = array_ind[i];
					array_ind[i] = k;
					boo = 1;
				}
			}
		}
	}
	char *name_file = (char*)malloc((strlen(field_name)+4)*sizeof(char));
	strcpy(name_file, field_name);
	strcat(name_file, ".idx");
	FILE *file_idx;
	file_idx = fopen(name_file, "w");
	for (i = 0; i < size; i++)
	{
		fprintf(file_idx, "%d", array_ind[i]);
		//printf("%d ", array_ind[i]);
	}
	fclose(file_dba);
	fclose(file_idx);
	return 0;
}

void reindex(char *dba)
{
	make_index(dba, "manufacturer");
	make_index(dba, "year");
	make_index(dba, "model");
	make_index(dba, "x_size");
	make_index(dba, "y_size");
	make_index(dba, "price");
	make_index(dba, "optr");
}

RECORD_SET *get_recs_by_index(char *dba, char *indx_field)
{
	FILE *file_dba;
	FILE *file_idx;
	/*char f_name[25];
	strcpy(f_name, indx_field);
	strcat(f_name, ".idx");*/
	file_dba = fopen(dba, "rb");
	RECORD_SET *rec_s = NULL;
	rec_s = malloc(sizeof(RECORD_SET));
	fread(&(rec_s->rec_nmb), sizeof(int), 1, file_dba);
	rec_s->recs = calloc(rec_s->rec_nmb, sizeof(SCAN_INFO));
	int i, j, index=0;
	char c[1];
	SCAN_INFO rec;
	file_idx = fopen(indx_field, "r");
	for (i = 0; i < rec_s->rec_nmb; i++)
	{
		fscanf(file_idx, "%c", &c);
		index = atoi(c);
		//search by index in dba file
		for (j = 0; j <= index; j++)
			fread(&rec, sizeof(SCAN_INFO), 1, file_dba);
		rec_s->recs[i] = rec;
		printf("%d\n", rec_s->recs[i].year);
		fseek(file_dba, sizeof(int), SEEK_SET);
	}
	fclose(file_dba);
	fclose(file_idx);
	return rec_s;
}

int del_scan(char *dba, int n)
{
	FILE *file_dba;
	scanner_r();
	file_dba = fopen(dba, "rb+");
	int count;
	SCAN_INFO help;
	int i;
	fread(&count, sizeof(int), 1, file_dba);
	for (i = 1; i <= count - n - 1; i++)
	{
		fseek(file_dba, (n + i)*sizeof(SCAN_INFO)+sizeof(int), SEEK_SET);
		fread(&help, sizeof(SCAN_INFO), 1, file_dba);
		fseek(file_dba, (n + i - 1)*sizeof(SCAN_INFO)+sizeof(int), SEEK_SET);
		fwrite(&help, sizeof(SCAN_INFO), 1, file_dba);
	}
	rewind(file_dba);
	count--;
	fwrite(&count, sizeof(int), 1, file_dba);
	printf("-------------------------------\n");
	fclose(file_dba);
	reindex(dba);
	scanner_r();
	return 1;
}
	

void comf_price(char *dba, float price)
{
	FILE *file_dba;
	FILE *file_txt;
	file_dba = fopen(dba, "r");
	int count, i;
	SCAN_INFO help;
	fread(&count, sizeof(int), 1, file_dba);
	file_txt = fopen("comfi.txt", "w");
	fprintf(file_txt, "%s", "Printer`s model with a suitable price:");
	for (i = 0; i < count; i++)
	{
		fread(&help, sizeof(SCAN_INFO), 1, file_dba);
		if (help.price <= price)
			fprintf(file_txt, "%s,", help.model);
	}
	fclose(file_dba);
	fclose(file_txt);
}
