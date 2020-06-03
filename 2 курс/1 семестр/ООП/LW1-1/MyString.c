#include "MyString.h"
#include <locale.h>


int substr (const char *string1, const char *string2) {
	char *sub_prt = strstr(string1, string2);
	int sub_border = -1;
	if (sub_prt != NULL) sub_border = sub_prt-string1;
	return sub_border + 1;
}

int subseq (const char *string1, const char *string2) {
	int current_length = 0, max_length = 0;
	unsigned int length1 = strlen(string1), length2 = strlen(string2);
	unsigned int i, j, i0, j0;
	for (i = 0; i < length1; i++) {
		for (j = 0; j < length2; j++) {
			if (string2[j] == string1[i]) {
				j0 = j; i0 = i;
				while (i0 <= length1 && j0 <= length2 && string1[i0] == string2[j0]) {
					current_length++;
					i0++; j0++;
				}
				if (current_length > max_length) max_length = current_length;
				current_length = 0;
			}
		}
	}
	return max_length; 
}

char ispal (const char *string) {
	int res = 1;
	unsigned int length = strlen(string)-1;
	unsigned int i = 0;
	while (i <= length/2 && res == 1) {
		if (string[i] != string[length-i]) res = 0;
		i++;
	}
	return res;
}

char* makepal(const char *string) {
	setlocale(LC_ALL, "RUSSIAN");
	char *reñ_string = (char*)calloc(strlen(string), sizeof(char));
	char *palynd_part = (char*)calloc(strlen(string), sizeof(char));
	char *tail = (char*)calloc(strlen(string), sizeof(char));
	unsigned int border = 0, i;
	char buf;
	if (ispal(string) == 1) return (string);
	reñ_string = strcpy(reñ_string, string);
	palynd_part = strcpy(palynd_part, string);
	while (ispal(palynd_part) != 1) {
		border++;
		for (i = 0; i <= border - 1; i++) tail[i] = string[i];
		for (i = border; i <= strlen(string); i++) palynd_part[i - border] = string[i];
	}
	for (i = 0; i <= (strlen(tail) - 1) / 2; i++) {
		buf = tail[i];
		tail[i] = tail[strlen(tail) - 1 - i];
		tail[strlen(tail) - 1 - i] = buf;
	}
	reñ_string = strcat(reñ_string, tail);
	return reñ_string;
	free(reñ_string); free(palynd_part); free(tail);
}

double* txt2double(const char *string, int *size) {
	char *current_numb = (char*)calloc(strlen(string), sizeof(char));
	double *result_vect = (double*)calloc(1, sizeof(double));
	unsigned int length = strlen(string)-1;
	unsigned int i = 0, j, k = 0;
	*size = 1;
	while (i <= length) {
		j = 0;
		while (i <= length && string[i] != ';') {
			current_numb[j] = string[i];
			j++; i++;
		}
		while (j <= length) { current_numb[j] = '0'; j++; }
		i++; // Jumping over ';'
		if (k > 0) result_vect = realloc(result_vect, _msize(result_vect)+sizeof(double));
		result_vect[k] = atof(current_numb);
		if (result_vect[k] == 0.0 && current_numb[0] != '0') *size = 0;
		k++;
	}
	if (*size != 0) *size = k;
	return (result_vect);
	free(result_vect); free(current_numb);
}