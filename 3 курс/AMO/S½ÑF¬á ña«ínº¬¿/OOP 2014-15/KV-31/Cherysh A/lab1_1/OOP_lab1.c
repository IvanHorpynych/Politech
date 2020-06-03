/************************************************************************************
*file: OOP_lab1.c																	*
*Synopsis: all functions were made for work with strings.							*
*These functions are declared in the include file "OOP_lab1.h".						*
*You can test this program in the file "OOP_lab1_test.c".							*
*related files: OOP_lab1_test.c														*
*author: Chernysh Andrey															*
*written: 15/09/2014																*
*last modified: 01/10/2014															*
************************************************************************************/
#include <stdio.h>
#include <stdlib.h>
#include "OOP_lab1.h"

int substr(const char *string1, const char *string2){
	int index = 0;
	int i, j;
	i = 0; j = 0;
	while (1){
		if ((string1[i] == string2[j]) && (string2[j] != '\0')){
			i++;
			j++;
			continue;
		}
		else{
			if (string2[j] == '\0') { index = i - j + 1; return index; }
			else { i++; j = 0; }
		}
		if (string1[i] == '\0'){
			return 0;
		}
	}
}
int subseq(const char *string1, const char *string2){
	int i = 0, j = 0, max = 0, count = 0;
	while ((string1[i] != '\0')){
		j = 0;
		while ((string2[j] != '\0')){
			if ((string1[i] == string2[j]) && (string1[i + 1] == string2[j + 1] != '\0')){
				i++;
				count++;
			}
			j++;
		}
		if (count >= max){
			max = count; 
			count = 0;
		};
		i++;
		count = 0;
	}
	return max;
}
char ispal(const char *string){
	for (int i = 0; i < (strlen(string) / 2 + 0.5); i++){
		if (string[i] != string[strlen(string) - i - 1])
			return 0;
	}
	return 1;
}
char *makepal(const char *string)
{
	if (*string == NULL)
		return "NULL";
	if (ispal(string) == 1)
		return string;
	int i, j, N = strlen(string), count = N - 1;

	for (i = (N - 1); i >= (N - 1) / 2; i--){
		j = 0;
		while ((i + j < N) && (i - j > -1) && (string[i - j] == string[i + j]))
			j++;
		if ((i - j < count) && (i + j == N)){
			count = i - j;
		}

		j = 0;
		while ((i + j + 1 < N) && (i - j > -1) && (string[i - j] == string[i + j + 1]))
			j++;
		if ((i - j < count) && (i + j + 1 == N)){
			count = i - j;
		}
	}

	char *add = (char*)malloc(count + 2);
	for (i = 0; i <= count; i++)
		add[i] = string[count - i];
	add[i] = '\0';

	char *res = (char*)malloc(N + count + 1);
	strcpy(res, string);
	strcat(res, add);

	free(add);
	return res;
}
double* txt2double(const char *string, int *size){
	double *a = (double*)malloc((strlen(string) / 2)*sizeof(double));
	char *g = (char*)malloc(strlen(string) / 2);
	int i = 0, k = 0, j = 0;

	while (i<strlen(string)){
		while (string[i] == ';')
			i++;
		while ((string[i] != ';') && (i<strlen(string))){
			g[k] = string[i];
			k++;
			i++;
		}
		g[k] = '\0';

		a[j] = atof(g);

		if ((a[j] == 0.0) && (g[0] != '0')){
			*size = 0;
			free(a);
			return a;
		}
		j++;
		k = 0;
	}
	*size = j;

	return a;
}
