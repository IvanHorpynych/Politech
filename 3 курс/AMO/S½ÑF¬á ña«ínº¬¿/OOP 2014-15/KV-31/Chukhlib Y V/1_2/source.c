/*
File: source.c
Synopsis: declaration of functions described in MyString.h
Author: Chukhlib Y.V.
Group: KV-31, the faculty of applied math (FPM)
Created: 13.11.2014
*/

#include "source.h"

error_t argz_create_sep(const char * string, int sep, char ** argz, size_t * argz_len){
	int i;
	char * temp;
	if (string == 0)
		return ERROR; 
	temp = (char *)malloc(strlen(string) * sizeof(char) + 1);
	for (i = 0; i < strlen(string); i++)
		if (string[i] == sep)
			temp[i] = '\0';
		else 
			temp[i] = string[i];
	*argz = temp;
	*argz_len = i--;
	return OK;
}
size_t argz_count(const char * argz, size_t arg_len){
	int i = 0, 
		count = 1;
	if (strlen(argz) != 0){
		for (i = 0; i < arg_len; i++){
			if (argz[i] == '\0') 
				count++;
		}
		return count;
	}
	else{
		printf("String is empty!\n");
		return 0;
	}
}

void argz_delete(char **argz, size_t *argz_len, char *entry){
	int count = argz_count(*argz, *argz_len), 
		k = 0, 
		j = 0;
	char *chrptr, *vec, *pos;
	chrptr = malloc(*argz_len * sizeof(char));
	chrptr = *argz;
	vec = malloc((*argz_len - strlen(entry) + 1) *sizeof(char));
	for (int i = 1; i <= count; i++){
		while ((chrptr[j] != '\0') && (j < *argz_len)){
			vec[k] = chrptr[j];
			k++;
			j++;
		} 
		pos = strstr(vec, entry);
		if (pos != NULL){
			break; 
		}
		j++;
		k = 0;
	}
	printf("Let's delete from %d to %d symbol\n", j - strlen(entry), j);
	k = 0;
	for (int i = 0; i < *argz_len; i++){
		if ((i < j - strlen(entry)) || (i > j)){
			vec[k] = chrptr[i];
			k++;
		}
	}
	*argz = vec;
	*argz_len -= strlen(entry) + 1;
}

error_t argz_add(char **argz, size_t *argz_len, const char * str){
	int count = 0, 
		i = 0, 
		j, k = 0;
	char * dopstr;
	int size_add;
	if (strlen(argz) != 0 && strlen(str) != 0){
		size_add = strlen(str) + *argz_len + 1;
		dopstr = (char *)malloc(size_add * sizeof(char));
		for (i = 0; i < *argz_len; i++)
			dopstr[i] = argz[0][i];
		dopstr[*argz_len] = '\0';
		for (k = 0; k < strlen(str) + 1; k++){
			i++;
			dopstr[i] = str[k];
		}
		dopstr[size_add] = '\0';
		*argz_len = size_add;
		*argz = dopstr;
		return OK;
	}
	else return ERROR;
}

void argz_print(const char *argz, const size_t argz_len, error_t check){
	int i;
	if ((check != OK) || (check < 0) || argz == 0){
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

error_t argz_insert(char **argz, size_t *argz_len, char *before, char *entry){
	int i = 0, k = 0, dcon = 0, j = 0;
	char * count;
	int flag = 0;
	char * dstr;
	int size_add;
	if (strlen(*argz) != 0){
		dstr = (char *)malloc(*argz_len + strlen(entry));
		for (i = 0; i <= *argz_len; i++){
			dstr[i] = argz[0][i];
		}
		i = 0;
		while (i < *argz_len){
			if (dstr[i] == before[0]){
				for (j = 0; j <= strlen(before); j++){
					if (dstr[i + j] == before[j]) flag = 1;
					else { flag = 0; break;}
				}
				if (flag == 1) {
					for (dcon = 0; dcon <= strlen(entry); dcon++){
						for (k = (*argz_len + strlen(entry)); k >= (i + 1); k--){
							dstr[k] = dstr[k - 1];

						}
					}
					for (dcon = 0; dcon <= strlen(entry); dcon++) dstr[i + dcon] = entry[dcon];
					i = i + strlen(before) + strlen(entry);
					*argz_len = *argz_len + strlen(entry) + 1;
				}
				else	 i++;
			}
			else  i++;
		}
		dstr[*argz_len] = '\0';
		*argz = dstr;
		return OK;
	}
	else{
		printf("String is empty! \n");
		return ERROR;
	}
}

char *argz_next(char *argz, size_t argz_len, const char *entry)
{
	int i = 0, j = 0, flag = 0;
	char*pointer = 0;
	pointer = (char *)malloc(argz_len);
	if (entry == 0){
		pointer = argz;
		return pointer;
	}
	else{
		for (i = 0; i < argz_len; i++){
			if (argz[i] == entry[0]){
				for (j = 0; j <= strlen(entry); j++){
					if (argz[i + j] == entry[j]) {
						flag = 1;
						if (j >= strlen(entry)) break;
					}
					else { flag = 0; break; }
				}
				if (flag == 1)
					pointer = &argz[i + strlen(entry) + 1];

				else pointer = NULL;
				return pointer;
			}
		}
	}
}

error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with){
	int i, j, index, flag = 0;
	index = 0;
	//search for element
	for (i = 0; ((i < (*argz_len)) && (flag == 0)); i++){
		j = 0;
		while ((*argz)[i] == str[j]){
			if (j == 0)
				index = i;
			if ((str[j] == '\0') && ((*argz)[i] == '\0')){
				flag = 1;
				break;
			}
			i++;
			j++;
		}
	}
	if (flag == 1){
		if (strlen(str) >= strlen(with)){
			//shift argz
			for (i = index; i < *argz_len; i++){
				(*argz)[i] = (*argz)[i + 1];
			}
			//input new string
			for (i = index, j = 0; j <= strlen(with); j++, i++){
				(*argz)[i] = with[j];
			}
			//delete elements
			*argz = (char *)realloc(*argz, *argz_len - (strlen(str) - strlen(with)));
			*argz_len -= (strlen(str) - strlen(with));
		}
		else{
			//add new memory
			*argz = (char *)realloc(*argz, *argz_len + (strlen(with) - strlen(str)));
			*argz_len += (strlen(with) - strlen(str));
			//shift argz
			for (i = *argz_len; i > index; i--){
				(*argz)[i] = (*argz)[i - 1];
			}
			//input new string
			for (i = index, j = 0; j <= strlen(with); j++, i++){
				(*argz)[i] = with[j];
			}
		}
		return OK;
	}
	else
		return ERROR;
}

