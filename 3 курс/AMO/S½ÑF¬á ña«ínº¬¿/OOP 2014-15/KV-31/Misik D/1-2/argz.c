/******************************************************************************************
	name:					argz.c
	description:			this file contains function "argz_create_sep", that makes from 
							string array of string, where ellements of array are ellements 
							of string which separated by sep. every ellement of array
							contains symbol '='. argz_len get value of full argz length.
							function type is error_t - if we get argz, that contains at 
							least one ellement returns 'OK', else - 'ENOMEM'. argz_len 
							got value of index of last null symbol + 1. 
							function "PosChr" returns index of first entering 
							in string parameter sep. if string doesn't contain parameter 
							sep then function returns constant INT_MAX (see <limits.h>).
							function "argz_count" count number of ellements in array argz.
							function "argz_delete" delete first entering of ellement 
							entry in argz. it also change value of argz_len.
							function "argz_insert" insert in argz string entry if
							this string contains symbol '=' before element before. it
							also change value of argz_len. function return 'OK', if
							string entry was inserted in argz, else return 'ENOMEM'
							function "argz_next" returns pointer to ellement of argz
							next to entry, if argz contains ellement entry. If entry = 0, then
							function returns pointer to argz. if argz doesn't contains
							ellement after entry function returns 0
							function "argz_print" prints argz, argz_len and number of
							ellements in argz.
	author:					Dima
	date of creation:		04.09.2014
	written:				06.09.2014
	date of last modified:	10.09.2014
******************************************************************************************/

#include <stdio.h>
#include <stdlib.h>
#include <limits.h>
#include <string.h>
#include "argz.h"

typedef enum { false, true } bool;

int PosChr(const char *string, int sep) {														//i - index of char sep.
	unsigned i;
	for (i = 0; i < strlen(string); i++) if (string[i] == sep) return i;
	return INT_MAX;
}

error_t argz_create_sep(const char *string, int sep, char **argz, size_t *argz_len) {			//str - helpful string
	char *STR = (char *)malloc(sizeof(char)* (strlen(string) + 2)); 
	*argz = (char *)malloc(sizeof(char));
	unsigned i, POS, LEN = strlen(string);
	for (i = 0; i < strlen(string); i++) STR[i] = string[i];
	STR[strlen(string)] = sep;
	STR[strlen(string) + 1] = '\0';
	while ((POS = PosChr(STR, sep)) != INT_MAX) if (PosChr(STR, '=') < POS) {
		*argz = (char *)realloc(*argz, sizeof(char)* (POS + 2 + (*argz_len)));
		for (i = 0; i < POS; i++) (*argz)[(*argz_len)++] = STR[i];	
		(*argz)[(*argz_len)++] = '\0';
		for (i = 0; i < LEN - POS; i++) STR[i] = STR[i + POS + 1];
		STR[LEN - POS] = '\0';
		LEN = strlen(STR);
	} else {
		for (i = 0; i < LEN - POS; i++) STR[i] = STR[i + POS + 1];
		STR[LEN - POS] = '\0';
		LEN = strlen(STR);
	}
	free(STR);
	if (*argz_len == 0) return ENOMEM;
	else return OK;
}

size_t argz_count(const char *argz, size_t arg_len) {
	int i = 0;
	size_t COUNT = 0;
	for (i = 0; i < arg_len; i++) if (argz[i] == '\0') COUNT++;
	return COUNT;
}

error_t argz_add(char **argz, size_t *argz_len, const char *str) {
	int i;
	*argz = (char *)realloc(*argz, sizeof(char)* ((*argz_len) + strlen(str) + 1));
	if (PosChr(str, '=') != INT_MAX) for (i = 0; i <= strlen(str); i++) (*argz)[(*argz_len)++] = str[i];
	else return ENOMEM;
	return OK;
}

void argz_delete(char **argz, size_t *argz_len, char *entry) {
	int NEW_LEN = 0;
	int i, SP;
	char *STR = (char *)malloc(sizeof(char)* (*argz_len));
	SP = i = 0;
	do {
		int j, NEW_SP = SP;
		bool flag = true;
		if ((*argz)[i] == '\0') {
			for (j = 0; j <= i - SP; j++) if ((*argz)[j + SP] != entry[j]) {
				flag = false;
				break;
			}
			NEW_SP = i + 1;
		}
		if (flag == false) for (j = SP; j < i + 1; j++) STR[NEW_LEN++] = (*argz)[j];
		SP = NEW_SP;
	} while (++i < *argz_len);
	free(*argz);
	*argz = STR = (char *)realloc(STR, sizeof(char)* (NEW_LEN + 1));
	*argz_len = NEW_LEN;
}

error_t argz_insert(char **argz, size_t *argz_len, char *before, const char *entry) {
	int SP, i, SP_WRD;
	i = SP_WRD = 0;
	SP = -1;
	do if ((*argz)[i] == '\0') {
		int j;
		bool flag = true;
		for (j = 0; j <= i - SP_WRD; j++) if ((*argz)[j + SP_WRD] != before[j]) flag = false;
		if (flag == true) SP = SP_WRD;
		else SP_WRD = i + 1;
	}
	while (++i < *argz_len && SP == -1);
	if (SP != -1 && PosChr(entry, '=') != INT_MAX) {
		int i;
		*argz = (char *)realloc(*argz, sizeof(char)* ((*argz_len) + strlen(entry) + 1));
		for (i = *argz_len - 1; i > SP - 1; i--) (*argz)[i + strlen(entry) + 1] = (*argz)[i];
		*argz_len += strlen(entry) + 1;
		for (i = SP; i < SP + strlen(entry) + 1; i++) (*argz)[i] = entry[i - SP];
	} else return ENOMEM;
	return OK;
}

char *argz_next(char *argz, size_t argz_len, const char *entry) {
	if (entry == 0) return argz;
	else {
		int i, SP, SP_WRD;
		i = SP_WRD = 0;
		SP = -1;
		while (entry[i++] != '\0');
		if (&(entry[i - 1]) != &(argz[argz_len - 1])) return &(entry[i]);
		else return 0;
	}
}

void argz_print(const char *argz, size_t argz_len) {
	unsigned i;
	for (i = 0; i < argz_len; i++) printf("%c", argz[i]);
	printf("\nLength = %d\n", argz_len);
	printf("Count of argz are %d\n\n", argz_count(argz, argz_len));
}

error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with) {
	argz_insert(argz, argz_len, with, str);
	argz_delete(argz, argz_len, with);
	return OK;
}