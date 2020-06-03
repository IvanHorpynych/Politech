
#include "scanner.h"
#pragma warning(disable : 4996)


void scantodba(char* filename, SCAN_INFO *sc)
{
	FILE *f_scan = NULL;
	int k, flag = 0;
	long count, dcount = 0;
	SCAN_INFO ch;
	f_scan = fopen(filename, "r+b");
	fseek(f_scan, 0, SEEK_END);
	count = ftell(f_scan);
	if (count <= 0) {
		k = 1;
		fseek(f_scan, 0, SEEK_SET);
		fwrite(&k, sizeof(int), 1, f_scan);
		fwrite(sc, sizeof(SCAN_INFO), 1, f_scan);
	}
	else { 
		fseek(f_scan, sizeof(int), SEEK_SET);
		while (dcount <= count) {
			fread(&ch, sizeof(SCAN_INFO), 1, f_scan);
			if (strcmp(sc->model, ch.model) == 0){ flag = 1; }
			dcount += sizeof(SCAN_INFO);
		}
		if (flag == 0) {
			fseek(f_scan, 0, SEEK_SET);
			fread(&k, sizeof(int), 1, f_scan);
			fseek(f_scan, 0, SEEK_SET);
			k++;
			fwrite(&k, sizeof(int), 1, f_scan);
			fseek(f_scan, sizeof(int)+(k - 1)*sizeof(SCAN_INFO), SEEK_SET);
			fwrite(sc, sizeof(SCAN_INFO), 1, f_scan);
		}
	}
	fclose(f_scan);
}


int create_file(char* filename_csv, char* filename_dba){
	FILE *f_csv;
	FILE *f_dba;
	char *strdob;
	char *str;
	int countf = 0;
	int i, k, j;
	SCAN_INFO ch;

	if ((f_dba = fopen("Scaners.dba", "wb")) == NULL) return -1;
	fclose(f_dba);
	strdob = (char*)malloc(400 * sizeof(char));

	if ((f_csv = fopen("Scaners.csv", "r")) == NULL) return -1;

	str = (char*)malloc(400 * sizeof(char));
	while (fscanf(f_csv, "%s", str) == 1){
		strcat(str, ";");
		for (i = 0, j = 0; i < strlen(str); i++){
			if (str[i] != ';')
				strdob[i - j] = str[i];
			else{
				++countf;
				strdob[i - j] = '\0';
				switch (countf){
				case 1:
					for (k = 0; k <= strlen(strdob); ++k)
						ch.manufacturer[k] = strdob[k];
					break;
				case 2:
					ch.year = atoi(strdob);
					break;
				case 3:
					for (k = 0; k <= strlen(strdob); ++k)
						ch.model[k] = strdob[k];
					break;
				case 4:
					ch.price = atof(strdob);
					break;
				case 5:
					ch.x_size = atoi(strdob);
					break;
				case 6:
					ch.y_size = atoi(strdob);
					break;
				case 7:
					ch.optr = atof(strdob);
					break;

				default:
					break;
				};
				j = i + 1;
			};
		};
		scantodba("Scaners.dba", &ch);
		countf = 0;
	};

	fclose(f_csv);

	return 0;
};


int make_index(char *dba, char *field_name){
	FILE *f;
	FILE *f_index;
	int i = 0, j, k, dcon;
	int *index;
	char **str;
	int *strint;
	float *strfloat;
	char *name;

	char* tempch;//variable for changing strings;
	int pem;//variable for changing integer digits;
	float pn;
	SCAN_INFO temp;//struct for reading from file;
	if ((f = fopen("Scaners.dba", "r+b")) == NULL){
		printf("\nfile didn't opened! ERROR !!!");
		return -1;
	}
	fread(&k, sizeof(int), 1, f);
	index = (int*)malloc((k)*sizeof(int));//array for indexes;
	for (j = 0; j < k; j++)//making array with indexes;
		index[j] = j;
	str = (char**)malloc((k)*sizeof(char*));//array for model,manufacturer;
	strint = (int*)malloc(k*sizeof(int));//array for year,x_size,y_size;
	strfloat = (float*)malloc(k*sizeof(float));//array for price;
	while (i < k + 1){
		str[i] = (char*)malloc(128 * sizeof(char));
		fread(&temp, sizeof(SCAN_INFO), 1, f);
		if (field_name == "manufacturer")
			strcpy(str[i], temp.manufacturer);
		else if (field_name == "model")
			strcpy(str[i], temp.model);
		else if (field_name == "year")
			strint[i] = temp.year;
		else if (field_name == "x_size")
			strint[i] = temp.x_size;
		else if (field_name == "optr")
			strint[i] = temp.optr;
		else if (field_name == "y_size")
			strint[i] = temp.y_size;
		else if (field_name == "price")
			strfloat[i] = temp.price;
		else return -1;
		i++;
	}
	for (dcon = 0; dcon < k - 1; dcon++){
		for (j = 0; j < k - 1; j++){
			if ((field_name == "manufacturer") || (field_name == "model")){

				if (strlen(str[j])>strlen(str[j + 1])){
					i = index[j];
					index[j] = index[j + 1];
					index[j + 1] = i;
					tempch = str[j];
					str[j] = str[j + 1];
					str[j + 1] = tempch;
				}
			}
			else if (field_name == "price"){
				if (strfloat[j] > strfloat[j + 1]){
					pn = index[j];
					index[j] = index[j + 1];
					index[j + 1] = pn;
					pn = strfloat[j];
					strfloat[j] = strfloat[j + 1];
					strfloat[j + 1] = pn;
				}

			}
			else
			if (strint[j] > strint[j + 1]){
				pem = index[j];
				index[j] = index[j + 1];
				index[j + 1] = pem;
				pem = strint[j];
				strint[j] = strint[j + 1];
				strint[j + 1] = pem;

			}
		}
	}
	name = (char*)malloc(strlen(field_name)*sizeof(char));
	strcpy(name, field_name);
	strcat(name, ".idx");
	if ((f_index = fopen(name, "w+b")) == NULL){
		printf("\nfile %s didn't opened! ERROR !!!", name);
		return -1;
	}
	printf("\n\t%s : ", name);
	for (j = 0; j < k; j++){
		fwrite(&index[j], sizeof(int), 1, f_index);

		printf(" %d ", index[j]);
	}
	fclose(f);
	fclose(f_index);
}
RECORD_SET * get_recs_by_index(char *dba, char *indx_field){
	FILE *f_dba;
	FILE *f_idx;
	int ke, i, j;
	SCAN_INFO *ch;
	RECORD_SET mass;
	if ((f_dba = fopen(dba, "rb")) == NULL) return -1;
	if ((f_idx = fopen(indx_field, "rb")) == NULL) return -1;
	fseek(f_dba, 0L, SEEK_SET);
	fread(&ke, sizeof(int), 1, f_dba);

	mass.rec_nmb = ke;
	mass.recs = (SCAN_INFO*)malloc(ke*sizeof(SCAN_INFO));
	ch = (SCAN_INFO*)malloc(ke*sizeof(SCAN_INFO));
	for (i = 0; i < ke; i++){

		fseek(f_dba, sizeof(int)+i*sizeof(SCAN_INFO), SEEK_SET);
		fread(&ch[i], sizeof(SCAN_INFO), 1, f_dba);
				printf("%s\n", ch[i].model);
	};
	mass.recs = ch;
	return &mass;
	fclose(f_dba);
	fclose(f_idx);
	return &mass;
};
int del_scan(char *dba, int n){
	int k, i;
	FILE *f_dba;
	SCAN_INFO ch;
	if ((f_dba = fopen(dba, "r+b")) == NULL)
		return -1;

	fread(&k, sizeof(int), 1, f_dba);
	if (n > k){ return -1; }
	else {
		if (k < 1) {

			return -1;
		}
	}

	i = 1;
	while ((n + i) != k + 1){
		fseek(f_dba, sizeof(int)+(n + i)*sizeof(SCAN_INFO), SEEK_SET);
		fread(&ch, sizeof(SCAN_INFO), 1, f_dba);
		fseek(f_dba, sizeof(int)+((n + i - 1) * sizeof(SCAN_INFO)), SEEK_SET);
		fwrite(&ch, sizeof(SCAN_INFO), 1, f_dba);
		i++;
	}
	k--;
	fseek(f_dba, 0, SEEK_SET);
	fwrite(&k, sizeof(int), 1, f_dba);
	
	fclose(f_dba);
	
}


void reindex(char *dba)
{
	make_index(dba, "manufacturer");
	make_index(dba, "year");
	make_index(dba, "model");
	make_index(dba, "x_size");
	make_index(dba, "y_size");
	make_index(dba, "optr");
	make_index(dba, "price");
}
int dba_to_txt(char *dba, char *txt, float limit_price){
	FILE *f_txt;
	FILE *f_dba;
	int k, i;
	SCAN_INFO ch;
	if ((f_txt = fopen(txt, "wb")) == NULL) return -1;
	if ((f_dba = fopen(dba, "rb")) == NULL) return -1;

	fseek(f_dba, 0, SEEK_SET);
	fread(&k, sizeof(int), 1, f_dba);
	for (i = 0; i < k; i++){

		fread(&ch, sizeof(SCAN_INFO), 1, f_dba);
		if (ch.price < limit_price){
			fprintf(f_txt, "%s/n;%d;%s;%.2f;%d;%d;%d;", ch.manufacturer, ch.year, ch.model, ch.price, ch.x_size, ch.y_size, ch.optr);

		};
	};
	fclose(f_txt);
	fclose(f_dba);
	return 0;
}