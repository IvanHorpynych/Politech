/************************************************************************
*file: lab12.c
*purpose: declarations for argz functions, types, constants
*author: Boyko D.
*written: 19/10/2014
*last modified: 19/10/2014
*************************************************************************/
#include "oop_lab1p2.h"

error_t argz_create_sep(const char *s, int sep, char **argz, size_t *argz_len){
	int i = 0;

	if (strlen(s) == 0) return ENOMEM;

	*argz = (char*)malloc(sizeof(char)* 100);
	*argz_len = strlen(s) + 1;

	while (s[i]){
		if (s[i] == sep) (*argz)[i] = '\0';
		else (*argz)[i] = s[i];
		i++;
	}
	(*argz)[i] = '\0';

	return OK;
}

size_t argz_count(const char *argz, size_t argz_len){
	int i, count = 0;

	if (strlen(argz) == 0) return 0;

	for (i = 0; i < argz_len; i++) if (argz[i] == '\0') count++;
	return count;
}

error_t argz_add(char **argz, size_t *argz_len, const char *str){
	int i = 0;

	while (str[i]){
		(*argz)[*argz_len + i] = str[i];
		i++;
	}
	(*argz)[*argz_len + i] = '\0';
	*argz_len += strlen(str) + 1;

	return OK;
}

void argz_delete(char **argz, size_t *argz_len, char *entry){
	int s_pos, end_pos, i, j = 0;
	char *argz_c;

	argz_c = (char*)malloc(sizeof(char)* 100);
	
	for (i = 0; i < *argz_len; i++){
		if ((*argz)[i] == entry[j]){
			if (j == 0) s_pos = i;
			if (entry[j] == '\0'){
				end_pos = i;
				break;
			}
			j++;
		}
		else{
			j = 0;
			s_pos = -1;
		}
	}

	if (s_pos == -1) return;
	
	j = 0;
	for (i = 0; i < *argz_len; i++){
		if (i >= s_pos && i <= end_pos) continue;
		argz_c[j] = (*argz)[i];
		j++;
	}
	argz_c[j] = '\0';
	*argz = argz_c;
	*argz_len = j + 1;
}

error_t argz_insert(char **argz, size_t *argz_len, char *before, const char *entry){
	int i, j = 0, k = 0, s_pos;
	char *argz_c;

	argz_c = (char*)malloc(sizeof(char)* 100);

	for (i = 0; i < *argz_len; i++){
		if ((*argz)[i] == before[j]){
			if (j == 0) s_pos = i;
			if (before[j] == '\0') break;
			j++;
		}
		else{
			j = 0;
			s_pos = -1;
		}
	}

	if (s_pos == -1) return ENOMEM;

	j = 0;
	i = 0;
	while (j < *argz_len){
		if (j == s_pos){
			while (argz_c[i] = entry[k]) { i++; k++; }
			i++;
		}
		argz_c[i] = (*argz)[j];
		i++;
		j++;
	}
	argz_c[i] = '\0';
	*argz = argz_c;
	*argz_len += strlen(entry) + 1;
	return OK;
}

char * argz_next(char *argz, size_t argz_len, const char *entry){
	int i, j = 0, s_pos;
	char *next;

	next = (char*)malloc(sizeof(char)* 15);

	if (entry == 0) s_pos = 0; 
	else{
		for (i = 0; i < argz_len; i++){
			if (argz[i] == entry[j]){
				if (entry[j] == '\0'){
					s_pos = i + 1;
					break;
				}
				j++;
			}
			else{
				j = 0;
				s_pos = -1;
			}
		}
	}

	if (s_pos == -1) return 0;

	i = s_pos;
	j = 0;
	while (next[j] = argz[i]) { i++; j++; }
	return next;
}

error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with){
	char *before;

	before = (char*)malloc(sizeof(char)* 15);

	before = argz_next(*argz, *argz_len, str);
	argz_delete(argz, argz_len, (char*)str);
	argz_insert(argz, argz_len, before, (char*)with);

	return OK;
}

void argz_print(const char *argz, size_t argz_len){
	int i = 0;

	for (; i < argz_len; i++) printf("%c", argz[i]);
	printf("\n");
}

