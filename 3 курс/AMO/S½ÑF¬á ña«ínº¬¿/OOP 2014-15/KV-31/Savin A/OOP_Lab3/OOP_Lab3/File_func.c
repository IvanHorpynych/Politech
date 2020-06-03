/************************************************************************
*file: File_func.h
*author: Savin A.D.
*group: KV-31, FPM
*written: 08/11/2014
*last modified: 08/11/2014
************************************************************************/

#include "File_func.h"
#pragma warning(disable : 4996)

void scantodba(SCAN_INFO *sc)
{
	FILE *scaners = NULL;
	scaners = fopen("D:/Projects/OOP_Lab3/Scaners.dba", "r+b");
	int NumberOfRecords;
	size_t check;
	fseek(scaners, 0, SEEK_SET);
	check = fread(&NumberOfRecords, sizeof(int), 1, scaners);
	if (check <= 0) { //если файл пустой
		NumberOfRecords = 1;
		fseek(scaners, 0, SEEK_SET);
		fwrite(&NumberOfRecords, sizeof(int), 1, scaners);
		fwrite(sc, sizeof(SCAN_INFO), 1, scaners);
	}
	else { //если файл не пустой
		fseek(scaners, 0, SEEK_SET);
		fread(&NumberOfRecords, sizeof(int), 1, scaners);
		fseek(scaners, 0, SEEK_SET);
		NumberOfRecords++;
		fwrite(&NumberOfRecords, sizeof(int), 1, scaners);
		fseek(scaners, sizeof(int) + (NumberOfRecords - 1)*sizeof(SCAN_INFO), SEEK_SET);
		fwrite(sc, sizeof(SCAN_INFO), 1, scaners);
	}
	fclose(scaners);
}

void csvtodba(FILE *source, FILE *reciever)
{
	reciever = fopen("D:/Projects/OOP_Lab3/Scaners.dba", "w+b");
	fclose(reciever);
	source = fopen("D:/Projects/OOP_Lab3/Scaners.csv", "r+");
	char ch;
	char s[255];
	int i = 0;
	SCAN_INFO *sci = (SCAN_INFO *)malloc(sizeof(SCAN_INFO));
	while ((ch = getc(source)) != EOF){ //считываем строку из csv во вспомогательную
		while (ch != '\n') {
			s[i] = ch;
			i++;
			ch = getc(source);
		}
		s[i] = ';';
		i = 0;
		int j = 0;
		char hlpstr[255];
		while (s[i] != ';'){ //дойдя до точки с запятой заполняем то, или иное поле
			hlpstr[j] = s[i];
			i++;
			j++;
		}
		i++;
		hlpstr[j] = '\0';
		j = 0;
		strcpy(sci->manufacturer, hlpstr);
		while (s[i] != ';'){
			hlpstr[j] = s[i];
			i++;
			j++;
		}
		i++;
		hlpstr[j] = '\0';
		j = 0;
		strcpy(sci->model, hlpstr);
		while (s[i] != ';'){
			hlpstr[j] = s[i];
			i++;
			j++;
		}
		i++;
		hlpstr[j] = '\0';
		j = 0;
		sci->year = atoi(hlpstr);
		while (s[i] != ';'){
			hlpstr[j] = s[i];
			i++;
			j++;
		}
		i++;
		hlpstr[j] = '\0';
		j = 0;
		sci->x_size = atoi(hlpstr);
		while (s[i] != ';'){
			hlpstr[j] = s[i];
			i++;
			j++;
		}
		i++;
		hlpstr[j] = '\0';
		j = 0;
		sci->y_size = atoi(hlpstr);
		while (s[i] != ';'){
			hlpstr[j] = s[i];
			i++;
			j++;
		}
		i++;
		hlpstr[j] = '\0';
		j = 0;
		sci->optr = atoi(hlpstr);
		while (s[i] != ';'){
			hlpstr[j] = s[i];
			i++;
			j++;
		}
		i++;
		hlpstr[j] = '\0';
		j = 0;
		sci->price = atof(hlpstr);
		scantodba(sci);
		i = 0;
	}
	fclose(source);
	fclose(reciever);
	free(sci);
}

int make_index(char *dba, char *field_name)
{
	FILE *index = NULL;
	FILE *source = NULL;
	char hlpstr1[255][255];
	char hlpstr2[255][255];
	int m1[255];
	int m2[255];
	float fm1[255];
	float fm2[255];
	int NumberOfRecords;
	char field[255] = "D:/Projects/OOP_Lab3/";
	strcat(field, field_name);
	strcat(field, ".idx");
	int i = 0;
	SCAN_INFO *sci = (SCAN_INFO *)malloc(sizeof(SCAN_INFO));
	source = fopen(dba, "rb");
	index = fopen(field, "w");
	fseek(source, 0, SEEK_SET);
	fread(&NumberOfRecords, sizeof(int), 1, source);
	for (i = 0; i < NumberOfRecords; i++) {
		fseek(source, sizeof(int)+i*sizeof(SCAN_INFO), SEEK_SET);
		fread(sci, sizeof(SCAN_INFO), 1, source);//считываем запись
		if (field_name == "manufacturer") {//в зависимости от того по какому полю нам надо отсортировать заполняем два вспомогательных массива
			strcpy(hlpstr1[i], sci->manufacturer);
			strcpy(hlpstr2[i], sci->manufacturer);
		}
		if (field_name == "model") {
			strcpy(hlpstr1[i], sci->model);
			strcpy(hlpstr2[i], sci->model);
		}
		if (field_name == "year") {
			m1[i] = sci->year;
			m2[i] = sci->year;
		}
		if (field_name == "x_size") {
			m1[i] = sci->x_size;
			m2[i] = sci->x_size;
		}
		if (field_name == "y_size") {
			m1[i] = sci->y_size;
			m2[i] = sci->y_size;
		}
		if (field_name == "optr") {
			m1[i] = sci->optr;
			m2[i] = sci->optr;
		}
		if (field_name == "price") {
			fm1[i] = sci->price;
			fm2[i] = sci->price;
		}
	}
	int j = 0;
	if ((field_name == "manufacturer") || (field_name == "model")){//один из двух массивов сортируем
		for (i = 0; i < NumberOfRecords; i++) {
			for (j = 0; j < NumberOfRecords - i - 1; j++) {
				if (strcmp(hlpstr1[j],hlpstr1[j+1]) > 0) {
					char tmp[255];
					strcpy(tmp, hlpstr1[j]);
					strcpy(hlpstr1[j], hlpstr1[j + 1]);
					strcpy(hlpstr1[j + 1], tmp);
				}
			}
		}
		j = 0; i = 0;
		fseek(index, 0, SEEK_SET);
		while (j < NumberOfRecords) {//сравнивая отсортированный массив с исходным заполняем csv индексами исходного при совпадении
			while (i < NumberOfRecords) {
				if ((strcmp(hlpstr1[j],hlpstr2[i]) == 0) && (strcmp(hlpstr1[j+1],hlpstr2[i]) != 0)) {
					fprintf(index, "%d", i);
				}
				i++;
			}
			i = 0;
			j++;
		}
	}
	if ((field_name == "year") ||
		(field_name == "x_size") ||
		(field_name == "y_size") ||
		(field_name == "optr")) {
		for (i = 0; i < NumberOfRecords; i++) {
			for (j = 0; j < NumberOfRecords - i - 1; j++) {
				if (m1[j] > m1[j + 1]) {
					int tmp;
					tmp = m1[j];
					m1[j] = m1[j + 1];
					m1[j + 1] = tmp;
				}
			}
		}
		j = 0; i = 0;
		fseek(index, 0, SEEK_SET);
		while (j < NumberOfRecords) {
			while (i < NumberOfRecords) {
				if ((m1[j] == m2[i]) && (m1[j+1] != m2[i])) {
					fprintf(index, "%d", i);
				}
				i++;
			}
			i = 0;
			j++;
		}
	}
	if (field_name == "price"){
		for (i = 0; i < NumberOfRecords; i++) {
			for (j = 0; j < NumberOfRecords - i - 1; j++) {
				if (fm1[j] > fm1[j + 1]) {
					float tmp;
					tmp = fm1[j];
					fm1[j] = fm1[j + 1];
					fm1[j + 1] = tmp;
				}
			}
		}
		j = 0; i = 0;
		fseek(index, 0, SEEK_SET);
		while (j < NumberOfRecords) {
			while (i < NumberOfRecords) {
				if ((fm1[j] == fm2[i]) && (fm1[j+1] != fm2[i])) {
					fprintf(index, "%d", i);
				}
				i++;
			}
			i = 0;
			j++;
		}
	}
	fclose(index);
	fclose(source);
	free(sci);
	return 0;
}

RECORD_SET * get_recs_by_index(char *dba, char *indx_field)
{
	RECORD_SET *help = (RECORD_SET *)malloc(sizeof(RECORD_SET));
	SCAN_INFO helpsci;
	make_index(dba, indx_field);
	int m;
	char field[255] = "D:/Projects/OOP_Lab3/";
	strcat(field, indx_field);
	strcat(field, ".idx");
	FILE *index = fopen(field, "r");
	FILE *source = fopen(dba, "r+b");
	fseek(source, 0, SEEK_SET);
	fread(&m, sizeof(int), 1, source); //считываем стартовое число элементов
	help->rec_nmb = m;
	SCAN_INFO *helpmas = (SCAN_INFO *)malloc(m*sizeof(SCAN_INFO));
	fseek(index, 0, SEEK_SET);
	char ch;
	char helpstr[255];
	int i = 0;
	while ((ch = getc(index)) != EOF){ //заполянем массив индексами из индексного файла
		helpstr[i] = ch;
		i++;
	} 
	helpstr[i] = '\0';
	for (i = 0; i < m; i++) { //заполняем вспомогательный массив структур по индексам
		int hlp;
		char chlp;
		chlp = helpstr[i];
		hlp = atoi(&chlp);
		fseek(source, sizeof(int) + hlp*(sizeof(SCAN_INFO)), SEEK_SET);
		fread(&helpsci, sizeof(SCAN_INFO), 1, source);
		helpmas[i] = helpsci;
	}
	fclose(index);
	fclose(source);
	help->recs = helpmas;
	return help; //возвращаем вспомогательный массив
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

int del_scan(char *dba, int n)
{
	FILE *source = fopen(dba, "r+b");
	SCAN_INFO *msci;
	int m;
	fseek(source, 0, SEEK_SET);
	fread(&m, sizeof(int), 1, source);
	msci = (SCAN_INFO *)malloc(m*sizeof(SCAN_INFO));
	fseek(source, sizeof(int), SEEK_SET);
	fread(msci, sizeof(SCAN_INFO), m, source); //считываем весь файл в буфер
	fclose(source);
	source = fopen(dba, "w+b");
	int h = m - 1;
	fseek(source, 0, SEEK_SET);
	fwrite(&h, sizeof(int), 1, source); //записываем новое число элементов
	for (int i = 0; i < m; i++) { //записываем элементы в очищенный исходный файл, без элемента с указанным номером
		if (i == n) continue;
		if (i < n) {
			fseek(source, sizeof(int)+i*sizeof(SCAN_INFO), SEEK_SET);
			fwrite(&msci[i], sizeof(SCAN_INFO), 1, source);
		}
		if (i > n) {
			fseek(source, sizeof(int)+(i - 1)*sizeof(SCAN_INFO), SEEK_SET);
			fwrite(&msci[i], sizeof(SCAN_INFO), 1, source);
		}
	}
	fclose(source);
	reindex(dba);
	free(msci);
	return 0;
}

void showbyprice(char *dba, int price)
{
	FILE *source = fopen(dba, "r+b");
	FILE *reciever = fopen("D:/Projects/OOP_Lab3/Prices.txt", "w");
	SCAN_INFO sci;
	int n;
	fseek(source, 0, SEEK_SET);
	fread(&n, sizeof(int), 1, source);
	for (int i = 0; i < n; i++){ //считываем структуру
		fseek(source, sizeof(int)+i*sizeof(SCAN_INFO), SEEK_SET);
		fread(&sci, sizeof(SCAN_INFO), 1, source);
		if (sci.price <= price) { //и сравниваем её цену с данной
			fprintf(reciever, "Firm - %s, model - %s, made in %d, has size %dx%d, optical resolution - %d and costs %.2f.\n", sci.manufacturer, sci.model, sci.year, sci.x_size, sci.y_size, sci.optr, sci.price);
		}
	}
	fclose(source);
	fclose(reciever);
}