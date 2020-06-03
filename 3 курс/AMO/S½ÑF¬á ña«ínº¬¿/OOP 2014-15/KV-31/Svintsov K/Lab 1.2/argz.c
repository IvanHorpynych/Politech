/***********************************************************************
*file: argz.c
*synopsis: The argz functions use malloc/realloc to allocate/grow argz vectors,
and so any argz vector creating using these functions may be freed by using free; 
conversely,any argz function that may grow a string expects that string
to have been allocated using malloc those argz functions that only examine 
their arguments or modify them in place will work on any sort of memory). 
All argz functions that do memory allocation have a return type of error_t,and
return 0 for success, and ENOMEM if an allocation error occurs.
 These functions are declared in the include file "argz.h".
*related files: none
*author: Kyrylo Svintsov
*written: 18/10/2014
*last modified: 18/10/2014
************************************************************************/

#include "argz.h"
#include <string.h>

/***********************************************************************
*Name argz_create_sep
*Usage argz_create_sep(string, 58, &argz_test, &argz_len)
*Prototype in argz.h
*Synopsis Creates the dynamic array from parametr "string" by splitting it 
by symbol "sep" and changing the array`s length.
*Return value OK or ENOMEM
***********************************************************************/

error_t argz_create_sep(const char *string, int sep, char **argz, size_t *argz_len){
	char *p = NULL;
	int i = 0, counter;
	counter = 1;
	//while not the end of string it copies the symbols from "string" to 
	//new allocated memory
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
			free(p);
			return ENOMEM;
		}
	}
	//in the end it saves the new length 
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


/********************************************************
*Name argz_count
*Usage int i =argz_count(argz_test,argz_len)
*Prototype in argz.h
*Synopsis Counts the number of elements in the new dynamic array
Elements have a look :"name1=name2"
*Return value the number of elements. If can not count
then return 0
********************************************************/
size_t argz_count(const char *argz, size_t arg_len){
	int counter,i = 0;
	counter = 0;
	for (; i < arg_len; i++){
		if (argz[i] == '\0')
			counter++;
	}
	return counter;
}

/****************************************************************
*Name argz_add
*Usage argz_add(&argz_test, &argz_len,"c=d")
*Prototype in argz.h
*Synopsis add a new element to dynamic array
*Return value OK or ENOMEM
****************************************************************/
error_t argz_add(char **argz, size_t *argz_len, const char *str){
	int i, j = 0;
	//change the size of dynamic array by adding new memory
	//and put in new element
	i = (*argz_len)+1;
	for (; str[j] != '\0'; j++, i++){
		(*argz) = (char *)realloc(*argz,i*sizeof(char));
		if (*argz != NULL){
			(*argz)[i-1] = str[j];
			(*argz_len)++;
		}
		else
			return ENOMEM;
	}
	(*argz) = (char *)realloc(*argz, i*sizeof(char));
	if (*argz != NULL){
		(*argz)[i - 1] = '\0';
		(*argz_len)++;
	}
	else
		return ENOMEM;
	return OK;
	
}


/**************************************************************
*Name argz_delete
*Usage argz_delete(&argz_test, &argz_len,"u=m")
*Prototype in argz.h
*Synopsis Deletes the element "entry" from dynamic array and changes
the length of it
*Return value void function
***************************************************************/

void argz_delete(char **argz, size_t *argz_len, char *entry){
	int i,j,index,flag = 0;
	index = 0;
	//search for element in argz
	for (i = 0; ((i < (*argz_len))&&(flag==0)); i++){
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
		for (j = 0; j < strlen(entry)+1; j++){
			for (i = index; i < (*argz_len - 1); i++){
				(*argz)[i] = (*argz)[i + 1];
			}
		}
		(*argz) = (char *)realloc(*argz, ((*argz_len) - strlen(entry))*sizeof(char));
		if (*argz == NULL)
			printf("Unsuccessful deleting\n");
		else{
			printf("Successful delete\n");
			*argz_len = (*argz_len) - strlen(entry)-1;
		}
	}
	else
		printf("Cannot find the chosen string for delete");
}

/**************************************************************
*Name argz_print
*Usage argz_print(argz_test, argz_len)
*Prototype in argz.h
*Synopsis Prints all elements of dynamic array
*Return value Void function
*************************************************************/
void argz_print(const char *argz, size_t argz_len){
	int i, count;
	for (i = 0; i < argz_len-1; i++){
		if (argz[i] == '\0')
			printf(" ");
		else
			printf("%c", argz[i]);
	}
	printf("\n");
}

/*******************************************************************
*Name argz_insert
*Usage argz_insert(&argz_test, &argz_len, "c=d", "u=m")
*Prototype in argz.h
*Synopsis Inserts  the elemnt "entry" before element "before" and
changes the length of array
*Return value OK or ENOMEM
*******************************************************************/
error_t argz_insert(char **argz, size_t *argz_len, char *before, const char *entry){
	int i, j, index, flag = 0;
	index = 0;
	//search for element in argz
	for (i = 0; ((i < (*argz_len)) && (flag == 0)); i++){
		j = 0;
		while ((*argz)[i] == before[j]){
			if (j == 0)
				index = i;
			if ((before[j] == '\0') && ((*argz)[i] == '\0')){
				flag = 1;
				break;
			}
			i++;
			j++;
		}
	}
	//when "before"is found it changes the size of array and then
	//shifts the elements right to put in element "entry"
	if (flag == 1){
		for (j = 1; j <= strlen(before)+1; j++){
			*argz = (char *)realloc(*argz, ((*argz_len) + j)*sizeof(char));
			if (*argz != NULL){
				for (i = (*argz_len)+j; i > index ; i--){
					(*argz)[i] = (*argz)[i-1];
				}
			}
			else
				return ENOMEM;
		}
		for (i = index, j = 0; j < strlen(entry)+1; i++, j++){
			(*argz)[i] = entry[j];
		}
		//in the end it changes the new length
		*argz_len += strlen(entry)+1;
		return OK;
	}
	else{
		printf("Can not find the chosen string for input\n");
		return ENOMEM;
	}
}

/*****************************************************************
*Name argz_next
*Usage argz_next(argz_test, argz_len, 0)
*Prototype in argz.h
*Synopsis Finds the "entry" in dynamic array.
*Return value Pointer to the element after "entry"
or to the first element of array
******************************************************************/
char * argz_next(char *argz, size_t argz_len, const char *entry){
	int i, j, index, flag = 0;
	index = 0;
	//search for element in argz
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
	//after "entry"
	if (flag == 1){
		if (argz_len > index + strlen(entry) + 1)
			return &(argz[index + strlen(entry) + 1]);
		else{
			printf("Next element was not found\n");
			return NULL;
		}
	}
	else
		return NULL;
}
/********************************************************************
*Name argz_replace
*Usage argz_replace(&argz_test, &argz_len, "c=d", "c=")
*Prototype in argz.h
*Synopsis Replace the old element "str" by the new "with"
*Return value OK or ENOMEM
********************************************************************/
error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with){
	int i, j, index, flag = 0;
	index = 0;
	//search for element in argz
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
		return ENOMEM;
}

