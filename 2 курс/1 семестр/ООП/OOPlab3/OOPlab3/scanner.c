#include "scanner.h"

/*    Порівняння структур типу SCAN_INFO.
Якщо рівні повертає 0.*/
int cmp_scan(SCAN_INFO *scan1, SCAN_INFO *scan2) {
	if (!strcmp(scan1->menufacturer, scan2->menufacturer) &&
		!strcmp(scan1->model, scan2->model) &&
		scan1->year == scan2->year &&
		scan1->price == scan2->price &&
		scan1->x_size == scan2->x_size &&
		scan1->y_size == scan2->y_size &&
		scan1->optr == scan2->optr)
		return 0;
	else
		return 1;
}

/* 1. Функція яка записує в бінарний файл дані про сканер.
Перший рядок - кількість елементів файлу
В інших рядках записані дані про сканери через ";".
Повертає кількість записаних значень. */
void bin_file(SCAN_INFO *scan) {
	FILE *stream;
	int *count = (int*)malloc(sizeof(int)), i;
	SCAN_INFO *scan_temp = (SCAN_INFO*)malloc(sizeof(SCAN_INFO));
	if (NULL == (stream = fopen("Scaners.dat", "rb+"))) {
		stream = fopen("Scaners.dat", "wb");
		(*count) = 1;
		fwrite(count, sizeof(int), 1, stream);
		fwrite(scan, sizeof(SCAN_INFO), 1, stream);
		free(scan_temp);
		free(count);
		fclose(stream);
		return;
	}
	fread(count, sizeof(int), 1, stream);
	if ((*count) != 0)
		for (i = 0; i < (*count); i++) {
			fread(scan_temp, sizeof(SCAN_INFO), 1, stream);
			if (!cmp_scan(scan, scan_temp)) {
				free(scan_temp);
				fclose(stream);
				return;
			}
		}
	(*count)++;
	rewind(stream);
	fwrite(count, sizeof(int), 1, stream);
	fseek(stream, 0, SEEK_END);
	fwrite(scan, sizeof(SCAN_INFO), 1, stream);

	free(scan_temp);
	fclose(stream);
}

/*    Функція витягує з файлу запис. csv_line - номер
запису. */
struct SCAN_INFO* get_rec(char *csv_line, char *file_name) {
	FILE *stream;
	SCAN_INFO *scan = (SCAN_INFO*)malloc(sizeof(SCAN_INFO));
	int i = 0, len;
	char *string = (char*)malloc(MAX_LEN_ELEM*sizeof(char)),
		*ptr = NULL;

	if (stream = fopen(file_name, "r")) {
		while (!feof(stream) && i < (*csv_line)) {
			fgets(string, MAX_LEN_ELEM, stream);
			i++;
		}
		fclose(stream);

		if (i != (*csv_line)) {
			free(string);
			return NULL;
		}

		len = strlen(string);
		len -= 1;
		string[len] = '\0';
		for (i = len; i >= 0; i--)
			if (string[i] == ';') string[i] = '\0';
		ptr = string;
		strcpy(scan->menufacturer, ptr); ptr += strlen(ptr) + 1;
		strcpy(scan->model, ptr); ptr += strlen(ptr) + 1;
		scan->year = atoi(ptr); ptr += strlen(ptr) + 1;
		scan->price = atof(ptr); ptr += strlen(ptr) + 1;
		scan->x_size = atoi(ptr); ptr += strlen(ptr) + 1;
		scan->y_size = atoi(ptr); ptr += strlen(ptr) + 1;
		scan->optr = atoi(ptr);
		free(string);
		return scan;
	}
	free(string);
	return NULL;
}

/* 2. Функція первісно переписує файл Scaners.csv в
файл Scaners.dat без повторень. */
void init_file() {
	SCAN_INFO *scan;
	int i = 1;

	remove("Scaners.dat");
	scan = get_rec(&i, "Scaners.csv");
	while (scan != NULL) {
		bin_file(scan);
		i++;
		scan = get_rec(&i, "Scaners.csv");
	}
}

/* 3. Функція створює індексний файл для заданого dba-файлу.
Індексний файл містить номера записів dba-файлу, відсортовані
в порядку незменшення по field_name.*/
int make_index(char *dba, char *field_name) {
	FILE *stream;
	SCAN_INFO *scan;
	char *string = (char*)malloc(MAX_LEN_ELEM * sizeof(char));
	int i, j, count, ms[MAX_ELEM], count_el, val;
	float ftemp[MAX_ELEM], fval;
	int	itemp[MAX_ELEM];
	char **stemp = (char**)malloc(MAX_ELEM * sizeof(char*));

	count_el = 1;
	while (NULL != (scan = get_rec(&count_el, "Scanner.dba"))) {
		if (!strcmp(field_name, "manufacturer")) {
			stemp[count_el - 1] = (char*)malloc(127 * sizeof(char));
			strcpy(stemp[count_el - 1], scan->menufacturer);
		}
		else if (!strcmp(field_name, "model")) {
			stemp[count_el - 1] = (char*)malloc(128 * sizeof(char));
			strcpy(stemp[count_el - 1], scan->model);
		}
		else if (!strcmp(field_name, "year")) itemp[count_el - 1] = scan->year;
		else if (!strcmp(field_name, "price")) ftemp[count_el - 1] = scan->price;
		else if (!strcmp(field_name, "x_size")) itemp[count_el - 1] = scan->x_size;
		else if (!strcmp(field_name, "y_size")) itemp[count_el - 1] = scan->y_size;
		else if (!strcmp(field_name, "optr")) itemp[count_el - 1] = scan->optr;
		count_el++;
	}
	count_el -= 2;

	//sorting
	for (i = 0; i < count_el; i++)
		ms[i] = i;

	if (field_name[0] == 'm') {
		for (i = 0; i < count_el - 1; i++) {
			count = i;
			for (j = i + 1; j < count_el; j++)
				if (strcmp(stemp[j], stemp[count]) < 0)
					count = j;
			strcpy(string, stemp[i]);
			strcpy(stemp[i], stemp[count]);
			strcpy(stemp[count], string);
			val = ms[i]; ms[i] = ms[count]; ms[count] = val;
		}
		for (i = 0; i < count_el + 1; i++)
			free(stemp[i]);
	}
	else if (field_name[0] == 'p')
		for (i = 0; i < count_el - 1; i++) {
			count = i;
			for (j = i + 1; j < count_el; j++)
				if (ftemp[j] < ftemp[count])
					count = j;
			fval = ftemp[i]; ftemp[i] = ftemp[count]; ftemp[count] = fval;
			val = ms[i]; ms[i] = ms[count]; ms[count] = val;
		}
	else
		for (i = 0; i < count_el - 1; i++) {
			count = i;
			for (j = i + 1; j < count_el; j++)
				if (itemp[j] < itemp[count])
					count = j;
			val = itemp[i]; itemp[i] = itemp[count]; itemp[count] = val;
			val = ms[i]; ms[i] = ms[count]; ms[count] = val;
		}

	strcpy(string, field_name);
	strcat(string, ".idx");
	if (stream = fopen(string, "w")) {
		i = 0;
		if (i < count_el) fprintf(stream, " %d", ms[i]); i++;
		for (; i < count_el; i++)
			fprintf(stream, " %d", ms[i]);
		fclose(stream);
	}

	free(stemp);
	free(string);
	return count_el;
}

/* 4. Функція повертає показник на RECORD_SET* на набір записів,
отриманих за допомогою індексного файлу.*/
RECORD_SET* get_recs_by_index(char *dba, char *indx_field) {
	RECORD_SET **set_record;
	FILE *stream;
	int val_mas[MAX_ELEM], n, i;
	char *idx = (char*)malloc(20 * sizeof(char));

	strcpy(idx, indx_field);
	strcat(idx, ".idx");

	if ((stream = fopen(idx, "r")) != 0) {
		n = 0;
		while (!feof(stream)) {
			fscanf(stream, "%d", &val_mas[n]);
			n++;
		}
		fclose(stream);

		set_record = (RECORD_SET**)malloc(n * sizeof(RECORD_SET*));
		for (i = 0; i < n; i++) {
			set_record[i] = (RECORD_SET*)malloc(sizeof(RECORD_SET));
			set_record[i]->recs = (SCAN_INFO*)malloc(sizeof(SCAN_INFO));
		}

		for (i = 0; i < n; i++) {
			set_record[i]->rec_nmb = val_mas[i];
			val_mas[i]++;
			set_record[i]->recs = get_rec(&val_mas[i], "Scanner.dba");
		}
		free(idx);
		return set_record;
	}
	free(idx);
	return NULL;
}

/* 5. Функція створює індексні файли для всіх полів структури SCAN_INFO.*/
void reindex(char *dba) {
	make_index(dba, "manufacturer");
	make_index(dba, "model");
	make_index(dba, "year");
	make_index(dba, "price");
	make_index(dba, "x_size");
	make_index(dba, "y_size");
	make_index(dba, "optr");
}

/* 6. Функція видаляє з dba-файлу запис з номером n.
Після видалення викликає функцію reindex().*/
int del_scan(char *dba, int n) {
	FILE *stream;
	SCAN_INFO **temp_scan;
	int count, i;
	temp_scan = (SCAN_INFO**)malloc(MAX_ELEM * sizeof(SCAN_INFO*));

	if (stream = fopen(dba, "r")) {
		count = 1;
		while (!feof(stream)) {
			temp_scan[count - 1] = (SCAN_INFO*)malloc(MAX_LEN_ELEM * sizeof(SCAN_INFO));
			fgets(temp_scan[count - 1], MAX_LEN_ELEM, stream);
			count++;
		}
		count -= 2;

		stream = freopen(dba, "w", stream);
		for (i = 0; i < n - 1 && i < count; i++)
			fputs(temp_scan[i], stream);
		for (i = n; i < count; i++)
			fputs(temp_scan[i], stream);

		fclose(stream);
		for (i = 0; i >= count; i++)
			free(temp_scan[i]);
		free(temp_scan);
		reindex(dba);
		return count - 1;
	}
	free(temp_scan);
	return 0;
}

/* 7. Функція читає з dba-файлу дані про сканер і записує в *.txt файл
Ціна яких не перевищує задану.*/
int price_scan(char *dba, char *txt, float prc) {
	FILE *txtfile, *stream;
	char *string = (char*)malloc(MAX_LEN_ELEM * sizeof(char)),
		*temp;
	int i;
	float val;
	txtfile = fopen(txt, "w");

	if (!(stream = fopen(dba, "r"))) return 1;

	while (!feof(stream)) {
		fgets(string, MAX_LEN_ELEM, stream);
		if (feof(stream)) break;
		string[strlen(string) - 1] = '\0';
		temp = string;
		for (i = 0; i < 3; i++)
			temp = strstr(temp, ";") + 1;
		i = 0;
		while (temp[i] != ';') i++;
		temp[i] = '\0';
		val = atof(temp);
		temp[i] = ';';
		if (val <= prc)
			fprintf(txtfile, "%s\n", string);
	}

	fclose(stream);
	fclose(txtfile);
	free(string);
	return 0;
}


//    Функції для друкування вмісту файлі та структур.
void print_dba(char *dba) {
	SCAN_INFO *scan;
	int i;
	char *string = (char*)malloc(MAX_LEN_ELEM*sizeof(char));

	printf("file - %s:\n", dba);
	i = 1;
	scan = get_rec(&i, dba);
	while (scan != NULL) {
		printf("%s %s %d %g %d %d %d\n", scan->menufacturer, scan->model,
			scan->year, scan->price, scan->x_size, scan->y_size, scan->optr);
		i++;
		scan = get_rec(&i, dba);
	}
	printf("\n");
}

void print_bin(char *bin) {
	FILE *stream;
	SCAN_INFO *scan = (SCAN_INFO*)malloc(sizeof(SCAN_INFO));
	int count;

	printf("file - %s:\n", bin);
	if (stream = fopen(bin, "rb")) {
		fread(&count, sizeof(int), 1, stream);
		while (count != 0) {
			fread(scan, sizeof(SCAN_INFO), 1, stream);
			printf("%s %s %d %g %d %d %d\n", scan->menufacturer, scan->model,
				scan->year, scan->price, scan->x_size, scan->y_size, scan->optr);
			count--;
		}
		printf("\n");
		fclose(stream);
	}
}

void print_rec_set(RECORD_SET **rec) {
	int i, count = 0;
	SCAN_INFO *scan;
	count = 1;
	scan = get_rec(&count, "Scanner.dba");
	while (scan != NULL) {
		count++;
		scan = get_rec(&count, "Scanner.dba");
	}
	count -= 2;

	for (i = 0; i < count; i++) {
		printf("%d - ", rec[i]->rec_nmb);
		printf("%s %s %d %g %d %d %d\n", rec[i]->recs->menufacturer, rec[i]->recs->model,
			rec[i]->recs->year, rec[i]->recs->price, rec[i]->recs->x_size, rec[i]->recs->y_size, rec[i]->recs->optr);
	}
	printf("\n");
}

