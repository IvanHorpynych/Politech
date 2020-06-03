/*
File: string_func_2.c
Purpose: declarations of functions used in main program.
Author: Savin A.D.
Group: KV-31, FPM
Written: 25.09.2014
*/

#include "string_func_2.h"

argz_create_sep(const char *string, int sep, char **argz, size_t *argz_len)
{
	int i = 0;
	*argz_len = strlen(string) + 1;
	char *helpstr;
	*argz = malloc(*argz_len*sizeof(char));
	helpstr = *argz;
	for (i = 0; i < *argz_len; i++) {
		if (string[i] != sep) helpstr[i] = string[i]; else
			helpstr[i] = '\0';
	}
	*argz = helpstr;
	return OK;
}


size_t argz_count(const char *argz, size_t arg_len)
{
	int i = 0;
	int count = 0, h = 0;
	for (i = 0; i < arg_len; i++)
	if (argz[i] == '\0') count++; else h++;
	if (h == 0) return h; else
	return count;
}

error_t argz_add(char **argz, size_t *argz_len, const char *str)
{
	int i = 0;
	char *helpstr;
	helpstr = *argz;
	realloc(helpstr, *argz_len + strlen(str) + 1);
	for (i = 0; i < strlen(str) + 1; i++)
		helpstr[*argz_len + i] = str[i];
	*argz_len = *argz_len + strlen(str) + 1;
	*argz = helpstr;
	return OK;
}

void argz_delete(char **argz, size_t *argz_len, char *entry)
{
	int i = 0, argzcount = 0, entrycount = 0, flag = 1, poscount = 0, j = 0;
	int k = 0;
	char *helpstr;
	helpstr = *argz;
	for (i = 0; i < *argz_len; i++) //Searching for similar element
	{
		if ((helpstr[i] != '\0') && (helpstr[i] == entry[j])) {
			argzcount++;
			entrycount++;
			j++;
		}
		else if (helpstr[i] != '\0') {
			argzcount++;
			j++;
		}
		else
		if ((helpstr[i] == '\0') && (argzcount == entrycount) && (helpstr[i] == entry[j])) {
			flag = 0;
			break;
		}
		else {
			argzcount = 0;
			entrycount = 0;
			j = 0;
			poscount++;
		}
	}
	j = 0;
	if (flag == 0) {
		while (k < poscount){
			if (helpstr[j] == '\0') k++;
			j++;
		}
		int c = 0;
		for (c = 0; c < *argz_len + (i - j) + 1; c++)
			helpstr[i] = argz[0][i];
		for (c = 0; c < *argz_len - i - 1; c++){
			helpstr[c + j] = helpstr[c + i + 1];
		}
		*argz = helpstr;
		*argz_len = *argz_len - (i - j) - 1;
	}
}

error_t argz_insert(char **argz, size_t *argz_len, char *before, const char *entry)
{
	int i = 0, argzcount = 0, entrycount = 0, flag = 1, poscount = 0, j = 0;
	int k = 0;
	char *helpstr;
	char *helpstr2;
	helpstr2 = malloc(*argz_len*sizeof(char));
	helpstr = *argz;
	for (i = 0; i < *argz_len; i++) //Searching for similar element
	{
		if ((helpstr[i] != '\0') && (helpstr[i] == before[j])) {
			argzcount++;
			entrycount++;
			j++;
		}
		else if (helpstr[i] != '\0') {
			argzcount++;
			j++;
		}
		else
		if ((helpstr[i] == '\0') && (argzcount == entrycount) && (helpstr[i] == before[j])) {
			flag = 0;
			break;
		}
		else {
			argzcount = 0;
			entrycount = 0;
			j = 0;
			poscount++;
		}
	}
	j = 0;
	if (flag == 0) {
		while (k < poscount){
			if (helpstr[j] == '\0') k++;
			j++;
		}
		int c = 0;
		for (c = 0; c < *argz_len - j; c++)
			helpstr2[c] = helpstr[c + j];
		for (c = 0; c < strlen(entry) + 1; c++)
			helpstr[c + j] = entry[c];
		for (c = 0; c < *argz_len - j; c++)
			helpstr[c + j + strlen(entry) + 1] = helpstr2[c];
		*argz_len = *argz_len + strlen(entry) + 1;
		*argz = helpstr;
	}
}

char * argz_next(char *argz, size_t argz_len, const char *entry)
{
	int i = 0, argzcount = 0, entrycount = 0, flag = 1, poscount = 0, j = 0;
	int k = 0;
	char *helpstr;
	helpstr = argz;
	for (i = 0; i < argz_len; i++) //Searching for similar element
	{
		if ((helpstr[i] != '\0') && (helpstr[i] == entry[j])) {
			argzcount++;
			entrycount++;
			j++;
		}
		else if (helpstr[i] != '\0') {
			argzcount++;
			j++;
		}
		else
		if ((helpstr[i] == '\0') && (argzcount == entrycount) && (helpstr[i] == entry[j])) {
			flag = 0;
			break;
		}
		else {
			argzcount = 0;
			entrycount = 0;
			j = 0;
			poscount++;
		}
	}
		if (flag == 0)return argz + i + 1; else
			return 0;
}

error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with)
{
	int i = 0, argzcount = 0, entrycount = 0, flag = 1, poscount = 0, j = 0;
	int k = 0;
	char *helpstr = (char *)malloc(*argz_len);
	int len = *argz_len;
	for (i = 0; i < len; i++)
		helpstr[i] = argz[0][i];
	/* for (i = 0; i < len; i++) //Searching for similar element
	{
		if ((helpstr[i] != '\0') && (helpstr[i] == str[j])) {
			argzcount++;
			entrycount++;
			j++;
		}
		else if (helpstr[i] != '\0') {
			argzcount++;
			j++;
		}
		else
		if ((helpstr[i] == '\0') && (argzcount == entrycount) && (helpstr[i] == str[j])) {
			flag = 0;
			break;
		}
		else {
			argzcount = 0;
			entrycount = 0;
			j = 0;
			poscount++;
		}
	} */
	/* if (flag == 0) {
		if (argz_next(helpstr, len, str) >= helpstr + len)
		argz_add(&helpstr, &len, with);  */
		argz_insert(&helpstr, &len, argz_next(helpstr, len, str), with);
		argz_delete(&helpstr, &len, str);
		*argz = helpstr;
		*argz_len = len;
		return OK;
}

void argz_print(const char *argz, size_t argz_len)
{
	int i = 0;
	for (i = 0; i < argz_len; i++)
		printf("%c", argz[i]);
	printf("\n");
}