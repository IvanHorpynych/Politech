/*
File: func.c
Author: Onoprienko M.I.
Group: KV-31, FPM
Written: 13.11.2014
*/
#include "Header.h"

error_t argz_create_sep(const char *string, int sep, char **argz, size_t *argz_len)//make new strings with one string
{
	if (*argz_len != 0)
	{
		return ENOMEM;
	}
	puts(string);
	char *chrptr;
	*argz_len = strlen(string) + 1;
	chrptr = malloc(*argz_len*sizeof(char));
	for (int i = 0; i < *argz_len; i++)
	{
		if (string[i] != sep)
		{
			chrptr[i] = string[i];
		}
		else
		{
			chrptr[i] = '\0';
		}
	}
	*argz = chrptr;
	return OK;
}

size_t argz_count(const char *argz, size_t arg_len)// search number of elements
{
	int count = 0;
	for (int i = 0; i < arg_len; i++)
	{
		if (argz[i] == '\0')
		{
			count++;
		}
	}
	return count;
}

error_t argz_add(char **argz, size_t *argz_len, const char *str) // add string to the end
{
	puts(str);
	char *chrptr;
	chrptr = malloc((*argz_len + strlen(str) + 1)*sizeof(char));
	chrptr = *argz;
	for (int i = 0; i < strlen(str) + 1; i++)
	{
		chrptr[*argz_len + i] = str[i];
	}
	*argz_len += strlen(str) + 1;
	*argz = chrptr;
	return 0;
}

void argz_delete(char **argz, size_t *argz_len, char *entry) // delete element
{
	puts(entry);
	int count = argz_count(*argz, *argz_len), k = 0, j = 0, i = 0;
	char *chrptr, *vec, *pos;
	chrptr = malloc(*argz_len*sizeof(char));
	chrptr = *argz;
	vec = malloc((*argz_len - strlen(entry) + 1) *sizeof(char));
	for (i = 1; i <= count; i++)
	{
		while ((chrptr[j] != '\0') && (j < *argz_len))
		{
			vec[k] = chrptr[j];
			k++;
			j++;
		}
		pos = strstr(vec, entry);
		if (pos != NULL)
		{

			if (k != j - strlen(entry))
			{
				printf("ERROR\n");
				return;
			}
			break;
		}
		j++;
		k = 0;
	}
	if (i == count)
	{
		printf("Entry not fined\n");
		return;
	}

	k = 0;
	for (i = 0; i < *argz_len; i++)
	{
		if ((i < j - strlen(entry)) || (i > j))
		{
			vec[k] = chrptr[i];
			k++;
		}
	}
	*argz = vec;
	*argz_len -= strlen(entry) + 1;
}

error_t argz_insert(char **argz, size_t *argz_len, char *before, const char *entry) // input this element  before that element
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
		j++;
		k = 0;
	}
	if (i == count)
	{
		printf("Entry not fined\n");
		return;
	}
	*argz_len += strlen(entry) + 1;
	char *help = malloc(*argz_len*sizeof(char));
	k = 0;
	i = 0;
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

char* argz_next(char *argz, size_t argz_len, const char *entry) //input pointer to next element
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

error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with) // replace this element to other element
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
	for (int i = 0; i < argz_len; i++)
	{
		printf("%c", argz[i]);
	}
	printf("\n");
}
