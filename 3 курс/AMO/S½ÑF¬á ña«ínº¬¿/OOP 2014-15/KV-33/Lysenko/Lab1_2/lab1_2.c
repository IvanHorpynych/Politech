/* file name: lab1_2.c
* author: Lysenko Vitaliy
* first written: 08/11/14
* last modified: 11/11/14
* related files: lab1_2.h
*/


#include "lab1_2.h"

#include <stdlib.h>
#include <stdio.h>
#include <string.h>

error_t argz_create_sep(const char *str, char sep, char **argz, size_t *argz_len)
{
	size_t i;

	if (str && argz && argz_len)
	{
		*argz = (char *)malloc((strlen(str) + 1) * sizeof(char));

		for (i = 0; i < strlen(str); ++i)
		{
			if (str[i] == sep)
				(*argz)[i] = '\0';
			else
				(*argz)[i] = str[i];
		}
		(*argz)[strlen(str)] = '\0';

		*argz_len = strlen(str) + 1;

		return OK;
	}

	return ENOMEM;
}

size_t argz_count(const char *argz, size_t argz_len)
{
	size_t i;
	size_t count = 0;

	for (i = 0; i < argz_len; ++i)
	{
		if (argz[i] == '\0')
			++count;
	}

	return count;
}

error_t argz_add(char **argz, size_t *argz_len, const char *str)
{
	size_t i;

	if (argz && argz_len && str)
	{
		*argz = (char *)realloc((void *)*argz, *argz_len + strlen(str) + 1);

		for (i = 0; i <= strlen(str); ++i)
			(*argz)[i + *argz_len] = str[i];

		(*argz_len) += strlen(str) + 1;

		return OK;
	}

	return ENOMEM;
}

void argz_delete(char **argz, size_t *argz_len, char *entry)
{
	long entryIndex;
	uint entrySize;
	long i;

	if (entry < *argz + *argz_len)
	{
		entryIndex = entry - *argz;
		entrySize = 0;
		i = entryIndex;
		while ((*argz)[i] != '\0')
		{
			++entrySize;
			++i;
		}
		++entrySize;
		++i;

		while (i < *argz_len)
		{
			(*argz)[i - entrySize] = (*argz)[i];
			++i;
		}

		*argz = (char *)realloc((void *)*argz, *argz_len - entrySize);

		(*argz_len) -= entrySize;
	}
}

error_t argz_insert(char **argz, size_t *argz_len, char *before, const char *entry)
{
	long i;
	long beforeIndex;
	char *buffer;

	if (argz && argz_len && before && entry)
	{
		beforeIndex = before - *argz;

		*argz = (char *)realloc((void *)*argz, *argz_len + strlen(entry) + 1);

		buffer = (char *)malloc((*argz_len - beforeIndex) * sizeof(char));

		for (i = beforeIndex; i < *argz_len; ++i)
			buffer[i - beforeIndex] = (*argz)[i];
		for (i = beforeIndex + strlen(entry) + 1; i < *argz_len + strlen(entry) + 1; ++i)
			(*argz)[i] = buffer[i - beforeIndex - strlen(entry) - 1];

		free((void *)buffer);

		for (i = 0; i <= strlen(entry); ++i)
			(*argz)[i + beforeIndex] = entry[i];

		(*argz_len) += strlen(entry) + 1;

		return OK;
	}

	return ENOMEM;
}

char *argz_next(char *argz, size_t argz_len, const char *entry)
{
	long entryIndex;
	long nextEntryIndex;

	if (!entry)
	{
		return argz;
	}


	entryIndex = entry - argz;

	nextEntryIndex = entryIndex;
	while (argz[nextEntryIndex] != '\0')
		++nextEntryIndex;
	++nextEntryIndex;

	if ((argz + nextEntryIndex) > (argz + argz_len))
		return NULL;
	else
		return argz + nextEntryIndex;
}

error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with)
{
	int i = 0, j = 0, str_len, wi_len, k, p1 = 0, p2 = 1, p3 = 0, p4 = 1;
	char *tmp;
	if ((*argz) == "" || with == "" || str == "")
		return ENOMEM;
	str_len = strlen(str);
	wi_len = strlen(with);
	while (i<*argz_len){
		if ((*argz)[i] == str[0]){
			for (j = 0; j<str_len && (*argz)[i + j] == str[j]; j++);
			if (j == str_len){
				tmp = (char*)malloc(sizeof(char)*(*argz_len));
				for (j = 0; j <= *argz_len - i - str_len; j++){
					tmp[j] = (*argz)[i + str_len + j];

				}
				if (i != 0 && (*argz)[i - 1] != '\0')
					p1 = 1;
				if (p1 == 1)
					(*argz)[i] = '\0';
				if ((*argz)[i + str_len] == '\0')
					p2 = 0;
				for (j = 0; j <= wi_len; j++)
					(*argz)[i + p1 + j] = with[j];
				if (p1 == 1)
					p4 = 0;

				for (k = 0; k <= *argz_len - i - str_len; k++)
					(*argz)[i + p2 + wi_len + 1 + k - p4] = tmp[k];
				if (p2 == 0)
					p3 = 1;
				*argz_len = *argz_len - str_len + wi_len + p1 + p2 + 1 - p3;
				return OK;

			}
			else i++;

		}
		else i++;


	}

	return ENOMEM;
}

void argz_print(const char *argz, size_t argz_len)
{
	int i;

	for (i = 0; i < argz_len; ++i)
	{
		if (argz[i] == '\0')
			printf("'\\0'");
		else
			printf("%c", argz[i]);
	}

	printf("\n");
}
