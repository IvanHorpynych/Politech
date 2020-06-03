/* file name: source.c
* made: Grushko Y.V.
* first written: 21/09/14
* last modified: 22/09/14
* related files: header.h
*/

#include "header.h"

error_t argz_create_sep(const char *string, int sep, char **argz, size_t *argz_len){
	int i;
	char *temp;

	if ((string == 0) || (*argz != 0) || (*argz_len != 0)) return ERROR;
	temp = (char*)malloc(strlen(string)*sizeof(char)+1);

	for (i = 0; i < strlen(string); i++){
		if (string[i] == sep){
			temp[i] = 0;
		}
		else{
			temp[i] = string[i];
		}
	}
	*argz = temp;
	*argz_len = (i--);
	return OK;
}

size_t argz_count(const char *argz, size_t arg_len){
	int i, result = 0;
	if ((*argz == 0) || (arg_len == 0))
		return ERROR;
	for (i = 0; i < arg_len; i++){
		if (argz[i] == 0) result++;
	}
	return result+1;
}

error_t argz_add(char **argz, size_t *argz_len, const char *str){
	int s_len = 0;
	if (0 > enother_work(*argz, argz_len, str, 0))
		return ERROR;
	s_len = strlen(str);
	*argz = (char*)realloc(*argz, (*argz_len + s_len + 4) * sizeof(char));
	memmove(*argz + *argz_len, str, s_len + 1);
	s_len++;
	*argz_len += s_len;
	error_t argz_create_sep(const char *string, int sep, char **argz, size_t *argz_len);
	return OK;
}

error_t argz_delete(char **argz, size_t *argz_len, char *entry)
{
	int len = strlen(entry), res = 0;
	res = enother_work(*argz, argz_len, entry, 0);
	if (0 > res)
		return ERROR;
	memmove(*argz + res - 1, *argz + res + len, *argz_len - len - res);
	len++;
	*argz_len -= len;
	return OK;
}

void argz_print(const char *argz, const size_t argz_len, error_t check)
{
	int i;
	if ((check != OK) || (check < 0) || argz == 0)
	{
		printf("ERROR\n");
		return;
	}
	for (i = 0; i < argz_len; i++)
	if (argz[i] == 0)
		printf("\\0");
	else
		printf("%c", argz[i]);
	printf("\n");
}

error_t argz_insert(char **argz, size_t *argz_len, char *before, const char *entry)
{
	int s_entry = 0;
	if (!((*argz <= before) && (before < *argz + *argz_len * sizeof(char))))
		return ERROR;
	if (2 != enother_work(*argz, argz_len, entry, 0))
		return ERROR;
	s_entry = strlen(entry) + 1;
	*argz = (char*)realloc(*argz, (*argz_len + s_entry) * sizeof(char));
	memmove(before + s_entry, before, *argz_len - (before - *argz) * sizeof(char));
	memmove(before, entry, s_entry * sizeof(char));
	*argz_len += s_entry;
	return OK;
}

char* argz_next(char *argz, size_t argz_len, const char *entry)
{
	char *pointer = NULL;
	int i, j = 0;
	i = enother_work(argz, &argz_len, entry, 1);
	if (0 > i)
		return "ERROR";
	j = strlen(entry);
	if ((argz[i + j] == 0) && (argz[i + j + 1] != 0))
	{
		pointer = &argz[i + j + 1];
		return pointer;
	}
	else
		return NULL;
}

error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with)
{
	int i, s_len = 0, s_len_re = 0;
	i = enother_work(*argz, argz_len, str, 1);
	if (0 > i)
		return ERROR;
	s_len = strlen(str);
	s_len_re = strlen(with);
	*argz = (char*)realloc(*argz, *argz_len + s_len_re);
	memmove(*argz + i + s_len_re, *argz + i + s_len, *argz_len - i - s_len);
	memmove(*argz + i, with, s_len_re);
	*argz_len += (s_len_re - s_len);
	*argz = (char*)realloc(*argz, *argz_len);
	return OK;
}


int enother_work(char *argz, size_t *argz_len, const char *entry, int key){
	int i = 0, j = 0, counter = 0, len = 0;
	if (1 != check_null(argz, argz_len))
		return -1;
	if (!*entry == 0)
	{
		len = strlen(entry);
		for (i = 0; i < *argz_len; i++)
		if (argz[i] == entry[0])
		{
			counter = 0;
			for (j = 0; j < len; j++)
			if (argz[i + j] == entry[j])
				counter++;
			if (key != 1)
			{
				if ((counter == len) && (argz[i + j] == 0))
					return i;
			}
			else
			if (counter == len)
				return i;
		}
	}
	else
		return -1;
	return 2;
}

int check_null(const char *argz, const size_t *argz_len){
	if ((*argz_len == 0) || (*argz == 0))
		return 0;
	return 1;
}
