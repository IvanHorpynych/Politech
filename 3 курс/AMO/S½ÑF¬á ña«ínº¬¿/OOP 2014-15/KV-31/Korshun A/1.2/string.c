/*
File: func.c
Author: Korshun/a/s
Group: KV-31, FPM
Written: 15.11.2014
*/
#include "Header.h"

error_t argz_create_sep(const char *string, int sep, char **argz, size_t *argz_len)
{
	int i = 0;
	*argz_len = strlen(string) ;
	char *helpstr;
	helpstr = malloc(*argz_len*sizeof(char));
	for (i = 0; i < *argz_len; i++) {
		if (string[i] != sep) helpstr[i] = string[i]; else
			helpstr[i] = '\0';
	}
	*argz = helpstr;
	return OK;
}

size_t argz_count(const char *argz, size_t arg_len)
{
	int count = 1;
	for (int i = 0; i < arg_len; i++)
	{
		if (argz[i] == '\0')
		{
			count++;
		}
	}
	return count;
}

error_t argz_add(char **argz, size_t *argz_len, const char *str)
{
	int i = 0;
	char *hpstr;
	hpstr = malloc((*argz_len + strlen(str))*sizeof(char));
	hpstr = *argz;
	for (i = 0; i < strlen(str) ; i++)
		hpstr[*argz_len + i] = str[i];
	*argz_len = *argz_len + strlen(str) ;
	*argz = hpstr;
	return OK;
}

void argz_delete(char **argz, size_t *argz_len, char *entry)
{
	int i, j, index, flag = 0;
	index = 0;
	//search for element in argz
	for (i = 0; ((i < (*argz_len)) && (flag == 0)); i++){
		j = 0;
		while ((*argz)[i] == entry[j]){
			if (j == 0)
				index = i;
			if ((entry[j] == '\0') && ((*argz)[i] == '\0')){
				flag = 1;
				break;
			}
			i++;
			j++;
		}
	}
	//process of deleting
	if (flag == 1){
		for (j = 0; j < strlen(entry) + 1; j++){
			for (i = index; i < (*argz_len - 1); i++){
				(*argz)[i] = (*argz)[i + 1];
			}
		}
		(*argz) = (char *)realloc(*argz, ((*argz_len) - strlen(entry))*sizeof(char));
		if (*argz == NULL)
			printf("Unsuccessful deleting\n");
		else{
			printf("Successful delete\n");
			*argz_len = (*argz_len) - strlen(entry) - 1;
		}
	}
	else
		printf("Cannot find the chosen string for delete \n");
}

error_t argz_insert(char **argz, size_t *argz_len, char *before, const char *entry)
{
	puts(entry);
	puts(before);
	int count = argz_count(*argz, *argz_len);
	char *pos, *chrptr, *vec;
	chrptr = malloc(*argz_len*sizeof(char));
	chrptr = *argz;
	vec = malloc((*argz_len - strlen(before) + 1) *sizeof(char));
	int j = 0, k = 0, i = 0, p = 0;
	for (i = 0; i < count; i++)
	{
		while ((chrptr[j] != '\0') && (j < *argz_len))
		{
			vec[k] = chrptr[j];
			k++;
			j++;
		}
		pos = strstr(vec, before);
		if (pos != NULL)
		{
			if (strlen(before) != k)
			{
				printf("ERROR\n");
				return;
			}
			break;
		}
		j++;  k = 0;
	}
	if (i == count)
	{
		printf("Entry not fined\n");
		return;
	}
	*argz_len += strlen(entry) + 1;
	char *help = malloc(*argz_len*sizeof(char));
	k = 0;  i = 0;
	while (i < *argz_len)
	{
		if ((i < j - strlen(before)) || (i > j - strlen(before) + strlen(entry)))
		{
			help[i] = chrptr[p];
			p++;
		}
		else
		{
			help[i] = entry[k];
			k++;
		}
		i++;
	}
	*argz = help;
}

char* argz_next(char *argz, size_t argz_len, const char *entry)
{
	puts(entry);
	char *res = 0;
	if (!entry)
	{
		res = argz;
		return res;
	}
	char *pos, *chrptr;
	chrptr = malloc(argz_len*sizeof(char));
	int count = argz_count(argz, argz_len), j = 0, k = 0, boo = 0, d = 0;
	for (int i = 1; i < count; i++)
	{
		while ((argz[j] != '\0') && (j < argz_len))
		{
			chrptr[k] = argz[j];
			k++;
			j++;
		}
		pos = strstr(chrptr, entry);
		if (boo)
		{
			res = chrptr;
			break;
		}
		if (pos != NULL)
		{
			d = j + 1;
			boo = 1;
		}
		j++;
		k = 0;
	}
	free(chrptr);
	chrptr = NULL;
	chrptr = malloc((j - d)*sizeof(char));
	k = 0;
	for (int i = d; i <= j; i++)
	{
		chrptr[k] = argz[i];
		k++;
	}
	res = chrptr;
	return res;
}

error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with)
{   
	puts(str);
	puts(with);
	int count = argz_count(*argz, *argz_len);
	char *pos, *chrptr, *vec;
	chrptr = malloc(*argz_len*sizeof(char));
	chrptr = *argz;
	vec = malloc((*argz_len - strlen(str) + 1) *sizeof(char));
	int j = 0, k = 0;
	for (int i = 0; i < count; i++)
	{
		while ((chrptr[j] != '\0') && (j < *argz_len))
		{
			vec[k] = chrptr[j];
			k++;
			j++;
		}
		pos = strstr(vec, str);
		if (pos != NULL)
		{
			break;
		}
		else
		{
			printf("str not fined\n");
			return;
		}
		j++;
		k = 0;
	}
	*argz_len = *argz_len - strlen(str) + strlen(with);
	char *help = malloc(*argz_len*sizeof(char));
	k = 0;
	int i = 0, p = 0;
	while (i < *argz_len)
	{
		if ((i < j - strlen(str)) || (i > j - strlen(str) + strlen(with)))
		{
			help[i] = chrptr[p];
			p++;
		}
		else
		{
			help[i] = with[k];
			if (k == strlen(with))
			{
				p += strlen(str) + 1;
			}
			k++;
		}
		i++;
	}
	*argz = help;
}

void argz_print(const char *argz, size_t argz_len)
{
	int i = 0;
	for (i = 0; i < argz_len; i++)
		printf("%c", argz[i]);
	printf("\n");
}
