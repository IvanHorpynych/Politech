/***********************************************************************
*file: str_work.c
*synopsis: The string functions operate with strings: 
*check for containing any substring or check for palyndromicity;
*they also change the strings by adding some characters.
*These functions are declared in the include file "string.h".
*related files: none
*author: volontyr alexandr
*written: 03/09/2014
*last modified: 06/09/2014
************************************************************************/


#include <stdio.h>
#include <malloc.h>
#include <string.h>
#include <stdlib.h>
#include "string.h"

typedef enum { false, true } bool;

/*
The put_str function gets a string from a keyboard and returns it.
*/
char* put_str(){
	int i;
	char *string = (char*)malloc(sizeof(char)* 255);
	string = (char*)realloc(string, sizeof(char)* 2);
	string[0] = getchar();
	i = 1;
	while ((string[i] = getchar()) != '\n') {
		++i;
		string = (char*)realloc(string, (i + 1)*sizeof(char));
	}
	string[i] = '\0';
	return string;
}

/*
The substr function searches string2 in string1 and returns a number of string2 position
in string1 or returns -1 if string1 doesn't contain string2.
*/
int substr(const char *string1, const char *string2) {
	int i, j, result = -1;
	bool flag;
	for (i = 0; string1[i] != '\0'; i++) {
		if (string1[i] == string2[0]) {
			flag = true;
			for (j = 1; string2[j] != '\0'; j++) {
				if (string1[i + j] != string2[j]) {
					flag = false;
					break;
				}
			}
			if (flag == true) {
				result = i;
				break;
			}
		}
	}
	return result;
}

/*
The subseq function returns maximal length of common subsequence among such strings as string1 and string2. 
*/
int subseq(const char *string1, const char *string2) {
	int k, max_len = 0;
	for (int i = 0; string1[i] != '\0'; i++) {
		for (int j = 0; string2[j] != '\0'; j++) {
			if (string1[i] == string2[j]) {
				k = 1;
				while ((string1[k + i] == string2[k + j]) && (string1[k + i] != '\0' || string2[k+j] != '\0')) {
					k++;
				}
				if (k > max_len) { 
					max_len = k; 
				}
			}
		}
		
	}
	return max_len;
}

/*
The ispal function checks if string is palyndrom. It retruns 1 if it's true or 0 if it's false.
*/
char ispal(const char *string) {
	char result = 1;
	int length = strlen(string) - 1;
	for (int i = 0; i <= length/2; i++) {
		if (string[i] != string[length - i]) {
			result = 0;
			break;
		}
	}
	return result;
}

/*
The makepal function makes a palyndrom of string adding to it minimal number of characters.
The function returns a pointer to this string.
*/
char* makepal(char **string) {
	int i = strlen(*string) - 1, length = strlen(*string) - 1, pos = -1;
	char *str = (char*)malloc(sizeof(char)* i);
	if (ispal(*string) == 1) {
		return *string;
	}
	str[0] = (*string)[length];
	while (i > 0) {
		i--;
		str[length - i] = (*string)[i];
		str[length - i + 1] = '\0';
		if (ispal(str) == 1) {
			pos = i - 1;
		}
	}
	if (pos == -1) {
		pos = length - 1;
	}
	*string = (char*)realloc(*string, sizeof(char)*(length + pos + 1));
	for (i = pos; i >= 0; i--) {
		(*string)[length + pos - i + 1] = (*string)[i];
	}
	(*string)[length + pos + 2] = '\0';
	return *string;
}

/*
The txt2double function converts separated numbers with semicolons located in string and adds these
numbers to an array. Parameter size takes a value of the array length if convertation is successful or 0 if it isn't.
The function returns a pointer to the array of numbers.
*/
double* txt2double(const char *string, int **size) {
	int num_elem = 0;
	for (int i = 0; string[i] != '\0'; i++) {
		if (string[i] == ';') {
			num_elem++;
		}
	}
	num_elem++;
	double *arr = (double*)malloc((num_elem)*sizeof(double));
	char *str = (char*)malloc(sizeof(char)*(strlen(string)));
	int k = 0, j = 0;
	for (int unsigned i = 0; i <= strlen(string); i++) {
		if (string[i] != ';' && string[i] != '\0') {
			str[k] = string[i];
			k++;
		}
		else {
			str[k] = '\0';
			if ((atof(str) != 0.0) || (atof(str) == 0.0 && *str == '0')) {
					arr[j] = atof(str);
					free(str);
					str = (char*)malloc(sizeof(char)*(strlen(string)));
			}
			else {
				*size = 0;
				break;
			}
			j++;
			k = 0;
		}
	}
	free(str);
	if (*size != 0) { 
		*size = &num_elem;
	}
	return arr;
}