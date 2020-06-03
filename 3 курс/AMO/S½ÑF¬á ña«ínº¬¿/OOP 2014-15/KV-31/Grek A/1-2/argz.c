/***********************************************************************
*file: argz.c
*synopsis: The argz functions use malloc/realloc to allocate/grow argz vectors,
and so any argz vector creating using these functions may be freed by using free;
conversely,any argz function that may grow a string expects that string
to have been allocated using malloc those argz functions that only examine
their arguments or modify them in place will work on any sort of memory).
All argz functions that do memory allocation have a return type of error_t,and
return 0 for success, and ENOMEM if an error occurs.
These functions are declared in the file "argz.h"
*author: Alex Grek
*written: 12/10/2014
*last modified: 26/11/2014
************************************************************************/

#include "argz.h"

error_t argz_create_sep(const char *string, int sep, char **argz, size_t *argz_len){
	char *p = NULL;
	int i = 0, counter;
	counter = 1;
	//copy symbols from input string to new array till the end of string
	for (; string[i] != '\0'; i++, counter++){
		p = (char *)realloc(p, counter*sizeof(char));
		if (p != NULL){
			if (string[i] != sep)
				p[i] = string[i];
			else{
				p[i] = '\0';
			}
		}
		else{
			//free memory and return if error happened
			free(p);
			return ENOMEM;
		}
	}
	//save new length 
	p = (char *)realloc(p, counter*sizeof(char));
	if (p != NULL){
		p[i] = '\0';
		*argz = p;
		*argz_len = i + 1;
	}
	else
		return ENOMEM;
	return OK;
}

size_t argz_count(const char *argz, size_t arg_len){
	int c = 0, /*counter*/ i = 0 /*iterator*/;
	for (; i < arg_len; i++){
		if (argz[i] == '\0')
			c++;
	}
	return c;
}

error_t argz_add(char **argz, size_t *argz_len, const char *str){
	int i, j = 0;
	//reallocate memory and put new element in
	i = (*argz_len) + 1;
	for (; str[j] != '\0'; j++, i++){
		(*argz) = (char *)realloc(*argz, i*sizeof(char));
		if (*argz != NULL){
			(*argz)[i - 1] = str[j];
			(*argz_len)++;
		}
		else
			return ENOMEM;
	}
	(*argz) = (char *)realloc(*argz, i*sizeof(char));
	if (*argz != NULL){
		//end string with 0
		(*argz)[i - 1] = '\0';
		//change length
		(*argz_len)++;
	}
	else
		return ENOMEM;
	return OK;
}


void argz_delete(char **argz, size_t *argz_len, char *entry){
	int i, j, ind, flag = 0 /*flag: element found - 1, not found - 0*/;
	ind = 0;
	//find element
	for (i = 0; ((i < (*argz_len)) && (flag == 0)); i++){
		j = 0;
		while ((*argz)[i] == entry[j]){
			if (j == 0)
				ind = i;
			if ((entry[j] == '\0') && ((*argz)[i] == '\0')){
				flag = 1;
				break;
			}
			i++;
			j++;
		}
	}
	//delete element if it was found
	if (flag == 1){
		for (j = 0; j < strlen(entry) + 1; j++){
			for (i = ind; i < (*argz_len - 1); i++){
				(*argz)[i] = (*argz)[i + 1];
			}
		}
		(*argz) = (char *)realloc(*argz, ((*argz_len) - strlen(entry))*sizeof(char));
		if (*argz != NULL) {
			//update length
			*argz_len = (*argz_len) - strlen(entry) - 1;
		}
	}
}

void argz_print(const char *argz, size_t argz_len){
	int i;
	for (i = 0; i < argz_len - 1; i++){
		if (argz[i] == '\0')
			printf(" "); //separate elements with space
		else
			printf("%c", argz[i]);
	}
	printf("\n");
}


error_t argz_insert(char **argz, size_t *argz_len, char *before, const char *entry){
	int i, j, ind, flag = 0/*found - 1, not found - 0*/;
	ind = 0; //index
	//search for element in argz
	for (i = 0; ((i < (*argz_len)) && (flag == 0)); i++){
		j = 0;
		while ((*argz)[i] == before[j]){
			if (j == 0)
				ind = i;
			if ((before[j] == '\0') && ((*argz)[i] == '\0')){
				flag = 1;
				break;
			}
			i++;
			j++;
		}
	}
	//if *before element is found
	//shift all the elements right and put in the new one
	if (flag == 1){
		for (j = 1; j <= strlen(before) + 1; j++){
			*argz = (char *)realloc(*argz, ((*argz_len) + j)*sizeof(char));
			if (*argz != NULL){
				for (i = (*argz_len) + j; i > ind; i--){
					(*argz)[i] = (*argz)[i - 1];
				}
			}
			else
				return ENOMEM;
		}
		for (i = ind, j = 0; j < strlen(entry) + 1; i++, j++){
			(*argz)[i] = entry[j];
		}
		//update length
		*argz_len += strlen(entry) + 1;
		return OK;
	}
	else{
		return ENOMEM;
	}
}


char * argz_next(char *argz, size_t argz_len, const char *entry){
	int i, j, index, flag = 0/*found - 1, not found - 0*/;
	index = 0;
	//find element
	if (entry == 0)
		return &(argz[0]);
	for (i = 0; ((i < (argz_len)) && (flag == 0)); i++){
		j = 0;
		while (argz[i] == entry[j]){
			if (j == 0)
				index = i;
			if ((entry[j] == '\0') && (argz[i] == '\0')){
				flag = 1;
				break;
			}
			i++;
			j++;
		}
	}
	//return next element
	if (flag == 1){
		if (argz_len > index + strlen(entry) + 1)
			return &(argz[index + strlen(entry) + 1]);
		else{
			//if entry element is the last
			return NULL;
		}
	}
	else
		//if element was not found
		return NULL;
}

error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with){
	int i, j, ind, f = 0/*flag: found - 1, not found - 0*/;
	ind = 0; //index
	//find element
	for (i = 0; ((i < (*argz_len)) && (f == 0)); i++){
		j = 0;
		while ((*argz)[i] == str[j]){
			if (j == 0)
				ind = i;
			if ((str[j] == '\0') && ((*argz)[i] == '\0')){
				f = 1;
				break;
			}
			i++;
			j++;
		}
	}
	if (f == 1){
		if (strlen(str) >= strlen(with)){
			//shift argz
			for (i = ind; i < *argz_len; i++){
				(*argz)[i] = (*argz)[i + 1];
			}
			//input new string
			for (i = ind, j = 0; j <= strlen(with); j++, i++){
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
			for (i = *argz_len; i > ind; i--){
				(*argz)[i] = (*argz)[i - 1];
			}
			//input new string
			for (i = ind, j = 0; j <= strlen(with); j++, i++){
				(*argz)[i] = with[j];
			}
		}
		return OK;
	}
	else
		return ENOMEM;
}
