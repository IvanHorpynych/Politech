/************************************************************************
*file: lab1.2.c
*synopsis: The argz functions use malloc/realloc to allocate/grow argz vectors, and so any argz
* vector creating using these functions may be freed by using free; conversely, any argz
* function that may grow a string expects that string to have been allocated using malloc
* (those argz functions that only examine their arguments or modify them in place will work
* on any sort of memory). All argz functions that do memory allocation have a return type
* of error_t, and return 0 for success, and ENOMEM if an allocation error occurs.
* These functions are declared in the include file "lab1.2.h".
*related files: none
*author: Kisilchuk Bogdan
*written: 10/11/2014
*last modified: 18/11/2014
************************************************************************/
#include "lab1.2.h"
/*
The argz_create_sep function converts the null-terminated string string into an
argz vector (returned in argz and argz len) by splitting it into elements at every
occurrence of the character sep.
*/
error_t argz_create_sep(const char *string, int sep, char **argz, size_t *argz_len){
	int i = 0;
	if (string == NULL)
		return ENOMEM;
	*argz_len = strlen(string);
	*argz = (char*)malloc(*argz_len);
	for (i = 0; i <= strlen(string); i++){
		(*argz)[i] = string[i];
	}
	i = 0;
	while (i < *argz_len){
		if ((*argz)[i] == sep)
			(*argz)[i] = NULL;
		i++;
	}
	
	return OK;

}
//Returns the number of elements in the argz vector
size_t argz_count(const char *argz, size_t argz_len){
	int i = 0,j = 0;
	while (i <= argz_len){
		if ((argz[i] == 58) || (argz[i] == NULL))
			j++;
		i++;
	}
	return j;
}
//The argz_add function adds the string str to the end of the argz vector // *argz, and updates *argz and *argz_len accordingly.
error_t argz_add(char **argz, size_t *argz_len, const char *str){
	int i = 0, len = *argz_len;
	
	if (*argz == NULL)
		return ENOMEM;
	
	*argz_len += strlen(str);
	
	
	*argz = (char*)realloc(*argz, *argz_len);
	

	for (i = 0; i < strlen(str); i++){
		(*argz)[len + i ] = str[i];
	}

	i = 0;
	while (i < *argz_len){
		if ((*argz)[i] == 58)
			(*argz)[i] = NULL;
		i++;
	}
		
	return OK;
}
/*If entry points to the beginning of one of the elements in the argz vector *argz,
the argz_delete function will remove this entry and reallocate *argz, modifying *argz and *argz_len accordingly.
Note that as destructive argz functions usually reallocate their argz argument,
pointers into argz vectors such as entry will then become invalid.
*/
void argz_delete(char **argz, size_t *argz_len, char *entry){
	int i = 0, j = 0, k = 0;
	while (i < *argz_len){
		if (((*argz)[i] == entry[j]) && ((*argz)[i] != NULL) && (entry[j] != NULL)){
			i++;
			j++;
			continue;
		}
		if (j == strlen(entry)){
			for (k = 0; (i + k) < *argz_len; k++){
				(*argz)[i - j + k] = (*argz)[i + k ];
			}
			*argz_len -= strlen(entry);
			*argz = (char*)realloc(*argz, (*argz_len));
			return;
		}
		else j = 0;
		i++;
	}

	i = 0;
	while (i < *argz_len){
		if ((*argz)[i] == 58)
			(*argz)[i] = NULL;
		i++;
	}
	
	
}
/*
The argz_insert function inserts the string entry into the argz vector *argz at a point just before the existing
element pointed to by before, reallocating *argz and updating *argz and *argz_len. If before is 0, entry is added
to the end instead (as if by argz_add). Since the first element is in fact the same as *argz, passing in *argz as
the value of before will result in entry being inserted at the beginning.
*/
error_t argz_insert(char **argz, size_t *argz_len, char *before, const char *entry){
	int i = 0, k = 0;

	if (*argz == NULL)
		return ENOMEM;

	while (i < *argz_len){
		if ((*argz)[i] == before[0])
			k = i - 1;
		i++;
	}
	*argz_len += strlen(entry);
	*argz = (char*)realloc(*argz, (*argz_len));
	for (int j = *argz_len-1; j >= k + strlen(entry); j--){
		(*argz)[j] = (*argz)[j - strlen(entry)];
	}

	for (i = 0; i < strlen(entry); i++){
		(*argz)[++k] = entry[i];
	}

	i = 0;
	while (i < *argz_len){
		if ((*argz)[i] == 58)
			(*argz)[i] = NULL;
		i++;
	}

	return OK;
}
/*
The argz_next function provides a convenient way of iterating over the elements in the argz vector argz.
It returns a pointer to the next element in argz after the element entry, or 0 if there are no elements following entry.
If entry is 0, the first element of argz is returned.
This behavior suggests two styles of iteration:
char *entry = 0;
while ((entry = argz_next (argz, argz_len, entry)))
action;
(the double parentheses are necessary to make some C compilers shut up about what they consider a questionable while-test) and:
char *entry;
for (entry = argz; entry; entry = argz_next (argz, argz_len, entry))
action;
Note that the latter depends on argz having a value of 0 if it is empty (rather than a pointer to an empty block of memory);
this invariant is maintained for argz vectors created by the functions here.
*/
char * argz_next(char *argz, size_t argz_len, const char *entry){
	int j = 0, i = 0, k = 0;
	char* tmpargz;
	if (entry == NULL)
		return argz;
	while (i < argz_len){
		while (((argz)[i] == entry[j]) && (entry[j] != NULL)){
			i++;
			j++;
		}
		if (entry[j] == NULL){
			k = i - j + 1;
			break;
		}
		else
			j = 0;
		i++;
	}

	while (argz[k] != '\0')
		k++;
	k++;
	j = 0;
	tmpargz = (char*)malloc(argz_len);
	while (argz[k] != '\0'){
		tmpargz[j] = argz[k];
		j++;
		k++;
	}
	tmpargz[j] = '\0';
	return tmpargz;
}
/*
Replace the string str in argz with string with, reallocating argz as
necessary.
*/
error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with){
	int i = 0, j = 0, f = 0, k = 0, l = 0;
	if (*argz == NULL)
		return ENOMEM;
	char* temp = (char*)malloc(*argz_len + strlen(with) - strlen(str));
	while (i < *argz_len){
		while (((*argz)[i] == str[j]) && (str[j] != NULL)){
			i++;
			j++;
		}
		if (str[j] == NULL){
			k = i - j + 1;
			f = 1;
			break;
		}
		else
			j = 0;
		i++;
	}

	

	if (f == 1){
		while (l < k-1) {
			temp[l] = (*argz)[l];
			l++;
		}	
		for (j = 0; j < strlen(with); j++, l++){
			temp[l] = with[j];
		}
		while (l < *argz_len){
			temp[l] = (*argz)[l + strlen(str)- strlen(with)];
			l++;
		}
	}	
	else 
		return ENOMEM;

	*argz_len = *argz_len + strlen(with) - strlen(str);
	*argz = (char*)realloc(*argz, *argz_len);

	for (i = 0; i < *argz_len; i++)
		(*argz)[i] = temp[i];
	(*argz)[i] = NULL;

	return OK;
}
//prints argz vector
void argz_print(const char *argz, size_t argz_len){
	int i = 0;
	printf("\t\t");
	while (i < argz_len){
		if (argz[i] != '\0')
			printf("%c", argz[i]);
		else printf("\n\t\t");
		i++;
	}
}	



