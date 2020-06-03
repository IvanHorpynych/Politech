#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h> 
#include <stdlib.h> 
#include <string.h> 
#include <Windows.h> 
#define MAX_LEN_ELEM 120

typedef struct doc_next
{
	char surname[30];
	char name[15];
	char spesiality[20];
	char surname_pat_first[30];
	char surname_pat_second[30];
	char surname_pat_third[30];
	struct patient *first;
	struct patient *second;
	struct patient *third;
	struct doc_next *next;
}Doctor;

typedef struct {
	Doctor* first;
}first_doc;


typedef struct human
{
	char surname[30];
	char name[15];
	unsigned int bDay;
	unsigned int bMonth;
	unsigned int bYear;
	char disease[100];
	char surname_doctor[30];
	struct Doctor *specialist;
	struct human *next;
}patient;
typedef struct {
	patient* first;
}first_pat;

void create_pat(first_pat *pat, first_doc *doc);

struct patient *get_pat(int *txt_line, char *file_name) {
	FILE *stream;
	patient *scan = (patient*)malloc(sizeof(patient));
	int i = 0, len;
	char *string = (char*)malloc(MAX_LEN_ELEM*sizeof(char)),
		*ptr = NULL;

	if (stream = fopen(file_name, "r")) {
		while (!feof(stream) && i < (*txt_line)) {
			fgets(string, MAX_LEN_ELEM, stream);
			i++;
		}
		fclose(stream);

		if (i != (*txt_line)) {
			free(string);
			return NULL;
		}

		len = strlen(string);
		len -= 1;
		string[len] = '\0';
		for (i = len; i >= 0; i--)
			if (string[i] == ';') string[i] = '\0';
		ptr = string;
		strcpy(scan->surname, ptr); ptr += strlen(ptr) + 1;
		strcpy(scan->name, ptr); ptr += strlen(ptr) + 1;
		scan->bDay = atoi(ptr); ptr += strlen(ptr) + 1;
		scan->bMonth = atoi(ptr); ptr += strlen(ptr) + 1;
		scan->bYear = atoi(ptr); ptr += strlen(ptr) + 1;
		strcpy(scan->disease, ptr); ptr += strlen(ptr) + 1;
		strcpy(scan->surname_doctor, ptr);
		if (!strcmp(scan->surname_doctor, "-"))
			scan->specialist = NULL;
		free(string);
		return scan;
	}
	free(string);
	return NULL;
}
void init_list_pat(first_pat *pat) {

	patient *scan, *pointer = NULL;
	int i = 1;

	scan = get_pat(&i, "patients.txt");
	while (scan != NULL) {


		if (pat->first == NULL) {
			pat->first = scan;
			pointer = scan;
			i++;
			scan = get_pat(&i, "patients.txt");
		}
		else {
			pointer->next = scan;
			pointer = scan;
			i++;
			scan = get_pat(&i, "patients.txt");
		}
	}
	pointer->next = NULL;
}
void show_pat(first_pat *pat)
{
	patient* temp = pat->first;
	int i = 1;
	printf("\n");
	while (temp)
	{
		printf("#: %d .", i);
		printf("  surname : %s\n", temp->surname);
		printf("  name : %s\n", temp->name);
		printf("  data : %d.%d.%d\n", temp->bDay, temp->bMonth, temp->bYear);
		printf("  disease : %s\n", temp->disease);
		printf("  surname_doctor : %s\n", temp->surname_doctor);
		printf("\n");
		temp = temp->next;
		i++;
	}
}

struct Doctor *get_doc(int *txt_line, char *file_name) {
	FILE *stream;
	Doctor *scan = (Doctor*)malloc(sizeof(Doctor));
	int i = 0, len;
	char *string = (char*)malloc(MAX_LEN_ELEM*sizeof(char)),
		*ptr = NULL;
	if (stream = fopen(file_name, "r")) {
		while (!feof(stream) && i < (*txt_line)) {
			fgets(string, MAX_LEN_ELEM, stream);
			i++;
		}
		fclose(stream);

		if (i != (*txt_line)) {
			free(string);
			return NULL;
		}

		len = strlen(string);
		len -= 1;
		string[len] = '\0';
		for (i = len; i >= 0; i--)
			if (string[i] == ';') string[i] = '\0';
		ptr = string;
		strcpy(scan->surname, ptr); ptr += strlen(ptr) + 1;
		strcpy(scan->name, ptr); ptr += strlen(ptr) + 1;
		strcpy(scan->spesiality, ptr);ptr += strlen(ptr) + 1;
		strcpy(scan->surname_pat_first, ptr);
		
			scan->first = NULL;
		ptr += strlen(ptr) + 1;
		strcpy(scan->surname_pat_second, ptr);
	
			scan->second = NULL;
		ptr += strlen(ptr) + 1;
		strcpy(scan->surname_pat_third, ptr);
	
			scan->third = NULL;
		return scan;
	}
	free(string);
	return NULL;
}

void init_list_doc(first_doc *doc) {
	Doctor *scan, *pointer = NULL;
	int i = 1;

	scan = get_doc(&i, "doctors.txt");
	while (scan != NULL) {

		if (doc->first == NULL) {
			doc->first = scan;
			pointer = scan;
			i++;
			scan = get_doc(&i, "doctors.txt");
		}
		else {
			pointer->next = scan;
			pointer = scan;
			i++;
			scan = get_doc(&i, "doctors.txt");
		}
	}
	pointer->next = NULL;
}
void show_doc(first_doc *doc)
{
	Doctor* temp = doc->first;
	int i = 1;
	printf("\n");
	while (temp)
	{
		printf("#: %d .", i);
		printf("  surname : %s\n", temp->surname);
		printf("  name : %s\n", temp->name);
		printf("  spesiality : %s\n", temp->spesiality);
		printf("  surname_pat_first : %s\n", temp->surname_pat_first);
		printf("  surname_pat_second : %s\n", temp->surname_pat_second);
		printf("  surname_pat_third : %s\n", temp->surname_pat_third);
		printf("\n");
		temp = temp->next;
		i++;
	}
}
char* dec(char *str) {
	int i;
	for (i = 0; MAX_LEN_ELEM; i++) {
		if (str[i] == '\n')
		{
			str[i] = '\0';
			return str;
		}
	}
}




void init_conect_pat_to_doc(first_pat *pat, first_doc *doc) {
	patient *point_1 = pat->first;
	Doctor *point_2;
	char *simb = (char*)malloc(sizeof(char));
	*simb = "-";
	while (point_1 != NULL) {
		point_2 = doc->first;
		while (point_2 != NULL) {
			if (strcmp(point_1->surname_doctor, simb) && !strcmp(point_1->surname_doctor, point_2->surname) && (point_1->specialist != point_2))
				if (point_2->first == NULL) {
					point_2->first = point_1;
					point_1->specialist = point_2;
					strcpy(point_2->surname_pat_first, point_1->surname);
				}
				else if (point_2->second == NULL) {
					point_2->second = point_1;
					point_1->specialist = point_2;
					strcpy(point_2->surname_pat_second, point_1->surname);
				}

				else if (point_2->third == NULL) {
					point_2->third = point_1;
					point_1->specialist = point_2;
					strcpy(point_2->surname_pat_third, point_1->surname);
				}
				else {};
				point_2 = point_2->next;
		}
		point_1 = point_1->next;
	}
	return;
}

void statistic_pat(first_pat *pat, first_doc *doc) {
	int i = 1;
	patient *point_1 = pat->first;
	Doctor *point_2;
	while (point_1 != NULL) {
		if (strcmp(point_1->surname_doctor, "-")) {
			point_2 = point_1->specialist;
			printf("# %d:\n", i);
			printf("surname patient : %s\n", point_1->surname);  printf("surname doctor : %s\n", point_2->surname);
			point_1 = point_1->next;
			i++;
		}
		else {
			printf("# %d:\n", i);
			printf("surname patient : %s\n", point_1->surname); printf("surname doctor: ---\n");
			point_1 = point_1->next;
			i++;
		}

	}
}

void create_pat(first_pat *pat, first_doc *doc) {
	patient *buf = (patient*)malloc(sizeof(patient));
	patient *point;
	char *str[MAX_LEN_ELEM];
	point = pat->first;
	while (point->next != NULL) point = point->next;
	printf("Write surname:");
	fgets(str, 30, stdin);
	fgets(str, 30, stdin);
	dec(str); strcpy(buf->surname, str);
	printf("Write name:");
	fgets(str, 15, stdin);
	dec(str); strcpy(buf->name, str);
	printf("Write disease:");
	fgets(str, 90, stdin);
	dec(str); strcpy(buf->disease, str);
	printf("Write surname doctor:");
	fgets(str, 100, stdin);
	dec(str); strcpy(buf->surname_doctor, str);
	printf("Write day:");
	scanf("%d", &(buf->bDay));
	printf("Write month:");
	scanf("%d", &(buf->bMonth));
	printf("Write year:");
	scanf("%d", &(buf->bYear));
	buf->specialist = NULL;
	buf->next = NULL;
	point->next = buf;


}

void init_conect_doc_to_pat(first_pat *pat, first_doc *doc) {
	Doctor *point_1 = doc->first;
	patient *point_2;
	while (point_1 != NULL) {
		point_2 = pat->first;
		while (point_2 != NULL) {
			if ((strcmp(point_1->surname_pat_first, "-")) && (!strcmp(point_1->surname_pat_first, point_2->surname)) && (point_1->first != point_2)) 
				{
				
					point_1->first = point_2;
					point_2->specialist = point_1;
					strcpy(point_2->surname_doctor, point_1->surname);
				}
				else
					if ((strcmp(point_1->surname_pat_second, "-")) && (!strcmp(point_1->surname_pat_second, point_2->surname)) && (point_1->second != point_2))
						 {
							point_1->second = point_2;
							point_2->specialist = point_1;
							strcpy(point_2->surname_doctor, point_1->surname);
						}

						else
							if ((strcmp(point_1->surname_pat_third, "-")) && (!strcmp(point_1->surname_pat_third, point_2->surname)) && (point_1->third != point_2))
								{
									point_1->third = point_2;
									point_2->specialist = point_1;
									strcpy(point_2->surname_doctor, point_1->surname);
								}
					point_2 = point_2->next;
			}
		point_1 = point_1->next;
		}
	return;

	}

void statistic_doc(first_pat *pat, first_doc *doc) {
	int i = 1;
	patient *point_2;
	Doctor *point_1 = doc->first;
	while (point_1 != NULL) {
		printf("# %d:\n", i);
		if (strcmp(point_1->surname_pat_first, "-")) {
			point_2 = point_1->first;
			printf("surname doctor : %s\n", point_1->surname);  printf("surname patient 1: %s\n", point_2->surname);
		}
		else { printf("surname doctor : %s\n", point_1->surname); printf("surname patient 1: ---\n"); }
			
			
		if (strcmp(point_1->surname_pat_second, "-")) {
			point_2 = point_1->second;
			printf("surname doctor : %s\n", point_1->surname); printf("surname patient 2: %s\n", point_2->surname);
		} else { printf("surname doctor : %s\n", point_1->surname); printf("surname patient 2: ---\n"); }

		if (strcmp(point_1->surname_pat_third, "-")) {
			point_2 = point_1->third;
			printf("surname doctor : %s\n", point_1->surname); printf("surname patient 3: %s\n", point_2->surname);
		}
		else { printf("surname doctor : %s\n", point_1->surname); printf("surname patient 3: ---\n"); }

			point_1 = point_1->next;
			i++;
		}

	}


void create_doc(first_pat *pat, first_doc *doc) {
	Doctor *buf = malloc(sizeof(Doctor));
	Doctor *point;
	char str[MAX_LEN_ELEM];
	point = doc->first;
	while (point->next != NULL) point = point->next;

	printf("Write name:");
	fgets(str, 15, stdin);
	fgets(str, 15, stdin);
	dec(str); strcpy(buf->name, str);

	printf("Write surname:");
	fgets(str, 30, stdin);
	dec(str); strcpy(buf->surname, str);

	printf("Write spesiality:");
	fgets(str, 20, stdin);
	dec(str); strcpy(buf->spesiality, str);

	buf->first = NULL;
	printf("Write surname_pat_first:");
	fgets(str, 30, stdin);
	dec(str); strcpy(buf->surname_pat_first, str);
	
	buf->second = NULL;
	printf("Write surname_pat_second:");
	fgets(str, 30, stdin);
	dec(str); strcpy(buf->surname_pat_second, str);
	
	buf->third = NULL;
	printf("Write surname_pat_third:");
	fgets(str, 30, stdin);
	dec(str); strcpy(buf->surname_pat_third, str);
	

	buf->next = NULL;
	point->next = buf;

}

void create_files_doc(first_doc *doc){
	FILE *stream;
	Doctor *temp = doc->first;

	stream = fopen("doctors.txt", "w");
	while (temp) {
		fprintf(stream, "%s;", temp->surname);
		fprintf(stream, "%s;", temp->name);
		fprintf(stream, "%s;", temp->spesiality);
		if (temp-> first) fprintf(stream, "%s;", temp->first);
		else fprintf(stream, "-;");
		if (temp->second) fprintf(stream, "%s;", temp->second);
		else fprintf(stream, "-;");
		if (temp->third) fprintf(stream, "%s;", temp->third);
		else fprintf(stream, "-;");
		if (temp->next) fprintf(stream, "\n");
		temp = temp->next;
	}
	fclose(stream);
}

void create_files_pat(first_pat *pat) {
	FILE *stream;
	patient *temp = pat->first;

	stream = fopen("patients.txt", "w");
	while (temp) {
		fprintf(stream, "%s;", temp->surname);
		fprintf(stream, "%s;", temp->name);
		fprintf(stream, "%d;", temp->bDay);
		fprintf(stream, "%d;", temp->bMonth);
		fprintf(stream, "%d;", temp->bYear);
		fprintf(stream, "%s;", temp->disease);
		if (temp->surname_doctor) fprintf(stream, "%s;", temp->surname_doctor);
		else fprintf(stream, "-;");
		if (temp->next) fprintf(stream, "\n");
		temp = temp->next;
	}
	fclose(stream);
}

int main() {
	first_pat *head_pat = (first_pat*)malloc(sizeof(first_pat));
	head_pat->first = NULL;

	first_doc *head_doc = (first_doc*)malloc(sizeof(first_doc));
	head_doc->first = NULL;
	init_list_pat(head_pat);
	init_list_doc(head_doc);
	
	int command, status = 0;
	do {
		printf("Commands:\n 0-Exit\n 1- print patients\n 2-print doctor\n 3-add patient\n 4-add doctor\n 5-Doctors statistics\n 6-patients statistics\n 7-write file about doctors\n 8-write file about patients\n");
		printf("Enter your command ");
		scanf("%i", &command);


		switch (command) {
		case 0:
			status = 1;
			break;
		case 1:
			printf("Patients:\n");
			show_pat(head_pat); 
			break;
		case 2:
			printf("Doctors:\n");
			show_doc(head_doc);
			break;
		case 3:
			printf("New Patient:\n");
			create_pat(head_pat, head_doc);
			break;
		case 4:
			printf("New Doctor:\n");
			fflush(stdin);
			create_doc(head_pat, head_doc);
			break;
		case 5:
			printf("Doctors statistics:\n");
			init_conect_doc_to_pat(head_pat, head_doc);
			statistic_doc(head_pat, head_doc);
			break;
			printf("patients statistics:\n");
		case 6:
			init_conect_pat_to_doc(head_pat, head_doc);
			statistic_pat(head_pat, head_doc);
			break;
		case 7:
			create_files_doc(head_doc);
			break;
		case 8:
			create_files_pat(head_pat);
			break;
		}
	} while (status == 0);
	
	
	return 0;
}
	
	
	
	
	
	
	
	
	
	
