/************************************************************************
*file: OOP_lab1.2.c
*synopsis: The argz functions use malloc/realloc to allocate/grow argz vectors, and so any argz
* vector creating using these functions may be freed by using free; conversely, any argz
* function that may grow a string expects that string to have been allocated using malloc
* (those argz functions that only examine their arguments or modify them in place will work
* on any sort of memory). All argz functions that do memory allocation have a return type
* of error_t, and return 0 for success, and ENOMEM if an allocation error occurs.
* These functions are declared in the include file "oop_LAB1.2.h".
*related files: none
*author: Chernysh Andrey
*written: 01/10/2014
*last modified: 08/10/2014
************************************************************************/
#include <stddef.h> // for  size_t
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include "OOP_lab1.2.h"

//Help functions;
//Sep_check change ":" to "\0" in the string;
void sep_check(char **argz, size_t *len){
	int i = 0;
	while (i < len){
		while (((*argz)[i] != 58) && (i < len)){
			i++;
		}
		(*argz)[i] = '\0';
		i++;
	}
}
//Substr find index 1 element string2 in the string1;
int substr(const char *string1, const char *string2){
	int index = 0;
	int i, j;
	i = 0; j = 0;
	while (1){
		if ((string1[i] == string2[j]) && (string2[j] != '\0')){
			i++;
			j++;
			continue;
		}
		else{
			if (string2[j] == '\0') { index = i - j + 1; return index; }
			else { i++; j = 0; }
		}
		if (string1[i] == strlen(string1)){
			return 0;
		}
	}
}
/*
The argz_create_sep function converts the null-terminated string string into an
argz vector (returned in argz and argz len) by splitting it into elements at every
occurrence of the character sep.
*/
error_t argz_create_sep(const char *string, int sep, char **argz, size_t *argz_len){
	int i = 0;
	*argz_len = strlen(string);
	
	if (*argz == NULL)
		return ENOMEM;

	*argz = (char*)malloc(strlen(string)*sizeof(char));
	for (int j = 0; j < strlen(string); j++){
		(*argz)[j] = string[j];
	}
	sep_check(argz, *argz_len);
	
		/*argz[count] = (char*)malloc(j);
		for (k = 0; k < j; k++){
			argz[count][k] = string[i - j + k];
		}
		argz[count][k] = '\0';
		j = 0;
		i++;
		count++;*/
	
	return OK;
}
/*prints argz vector */
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
//Returns the number of elements in the argz vector.
size_t argz_count(const char *argz, size_t argz_len){
	int i = 0, m = 0;
	while (i < argz_len){
		if (argz[i] == '\0')
			m++;
		i++;
	}
	return m+1;
}
//The argz_add function adds the string str to the end of the argz vector // *argz, and updates *argz and *argz_len accordingly.
error_t argz_add(char **argz, size_t *argz_len, const char *str){
	int old_len = *argz_len;
	*argz_len += strlen(str);
	*argz = (char*)realloc(*argz, *argz_len*(sizeof(char)));
	int i = 0;
	(*argz)[old_len] = '\0';
	while (i < strlen(str)){
		(*argz)[old_len + i + 1] = str[i];
		i++;
	}
	
	if (*argz == NULL)
		return ENOMEM;
	
	i = 0;
	
	sep_check(argz, *argz_len);
	
	return OK;
}
/*If entry points to the beginning of one of the elements in the argz vector *argz,
the argz_delete function will remove this entry and reallocate *argz, modifying *argz and *argz_len accordingly.
Note that as destructive argz functions usually reallocate their argz argument, 
pointers into argz vectors such as entry will then become invalid.
*/
void argz_delete(char **argz, size_t *argz_len, char *entry){
	int i = 0,j = 0;
	while (1){
		if (((*argz)[i] == entry[j]) && (j < strlen(entry) && (i < *argz_len) && ((*argz)[i+1] == entry[j+1]))){
			i++;
			j++;
			continue;
		}
		if (j == strlen(entry)-1){
			for (int k = 0; i + k < *argz_len; k++)
				(*argz)[i - j + k] = (*argz)[i+k+1];
			*argz_len = *argz_len - strlen(entry);
			*argz = (char*)realloc(*argz, (*argz_len)); 
			return;
		}
		else j = 0;
		if (i == *argz_len)
			return;
		i++;
	}
	sep_check(argz, argz);
}
/*
The argz_insert function inserts the string entry into the argz vector *argz at a point just before the existing 
element pointed to by before, reallocating *argz and updating *argz and *argz_len. If before is 0, entry is added
to the end instead (as if by argz_add). Since the first element is in fact the same as *argz, passing in *argz as
the value of before will result in entry being inserted at the beginning.
*/
error_t argz_insert(char **argz, size_t *argz_len, char *before, const char *entry){
	if (*argz == NULL)
		return ENOMEM;
	int i = substr(*argz, before) -1, j = 0, m = 0, old_len = strlen(*argz), entry_len = strlen(entry);
	*argz_len += entry_len;
    *argz = (char*)realloc(*argz, *argz_len*sizeof(char));
	for (int k = *argz_len; k >= i + entry_len; k--){
		(*argz)[k] = (*argz)[k - entry_len];
	}
	for (j; j < entry_len; j++){
		(*argz)[i] = entry[j];
		i++;
	}
	sep_check(argz, *argz_len);
	return OK;
	
}
/*
Replace the string str in argz with string with, reallocating argz as
necessary.
*/
error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with){
	if (*argz == NULL)
		return ENOMEM;
	if (strlen(str) < strlen(with)){
		int ost = (strlen(with) - strlen(str)), k = 0;
		*argz_len = *argz_len + ost;
		*argz = (char*)realloc(*argz, *argz_len*sizeof(char));
		int i = substr(*argz, str) - 1;
		for (k = *argz_len; k >= i + strlen(str) + ost; k--)
			(*argz)[k] = (*argz)[k - ost];
		for (k = 0; k < strlen(with); k++){
			(*argz)[i] = with[k];
			i++;	
		}
		sep_check(argz, *argz_len);
		return OK;
	}
	else if (strlen(str) > strlen(with)){
		int ost = (strlen(str) - strlen(with)), k = 0;
		int i = substr(*argz, str) - 1;
		for (k = i + strlen(str); k < *argz_len; k++)
			(*argz)[k - ost] = (*argz)[k];
		for (k = 0; k < strlen(with); k++){
			(*argz)[i] = with[k];
			i++;
		}
		*argz_len = *argz_len - ost;
		*argz = (char*)realloc(*argz, *argz_len*sizeof(char));
		sep_check(argz, *argz_len);
		return OK;
		}
	else {
		int i = substr(*argz, str) - 1;
		for (int k = 0; k < strlen(with); k++){
			(*argz)[i] = with[k];
			i++;
		}
	}
	sep_check(argz, *argz_len);
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
	int j = 0;
	char* tmpargz;
	if (entry == NULL)
		return argz;
	int i = substr(argz, entry);
	while (argz[i] != '\0')
		i++;
	i++;
	
	tmpargz = (char*)malloc(argz_len);
	while (argz[i] != '\0'){
		tmpargz[j] = argz[i];
		j++;
		i++;
	}
	tmpargz[j] = '\0';
	return tmpargz;
}

	
	






