/************************************************************************
*file: lab1_2string.h
*author: Dnedoj D.
*written: 31/09/2014
*last modified: 31/09/2014
*************************************************************************/
#include "lab1_2string.h"

/*
The argz_create_sep function converts the null-terminated string string into an
argz vector (returned in argz and argz len) by splitting it into elements at every
occurrence of the character sep.
*/
error_t argz_create_sep(const char *string, int sep, char **argz, int *argz_len){
	if (strlen(string) != 0){
		*argz_len = strlen(string) + 1;
		char *dob;
		dob = (char *)malloc(*argz_len);
		for (int i = 0; i < *argz_len; i++)
		if (string[i] != sep) dob[i] = string[i];
		else dob[i] = '\0';
		dob[*argz_len] = '\0';
		*argz = dob;
		return OK;
	}
	else{
		printf("String is empty! Try again.\n");
		return ENOMEM;
	}
}
	
//Returns the number of elements in the argz vector.
size_t argz_count(const char *argz, int arg_len){
	if (strlen(argz) != 0){
		int count = 0;
		for (int i = 0; i < arg_len; i++)
		if (argz[i] == '\0') ++count;
		return count;
	}
	else{
		printf("String is empty! Try again.\n");
		return 0;
	}
}

//The argz_add function adds the string str to the end of the argz vector // *argz, and updates *argz and *argz_len accordingly.
error_t argz_add(char **argz, int *argz_len, const char *str){
	if (strlen(argz) != 0 && strlen(str) != 0){
		int size_add = strlen(str) + *argz_len + 1;
		char *dob = (char *)malloc(size_add);
		for (int i = 0; i < size_add; ++i)
			if (i < *argz_len) dob[i] = argz[0][i];
			else dob[i] = str[i - *argz_len];
		dob[size_add] = '\0';
		*argz_len = size_add;
		
		*argz = dob;
		return OK;
	}
	else{
		printf("String is empty! Try again.\n");
		return ENOMEM;
	}
}


/*If entry points to the beginning of one of the elements in the argz vector *argz, the argz_delete function will remove this entry and reallocate *argz, modifying *argz and *argz_len accordingly. Note that as destructive argz functions usually reallocate their argz argument, pointers into argz vectors such as entry will then become invalid.
*/
void argz_delete(char **argz, int *argz_len, char *entry){
	if (strlen(argz) != 0 && strlen(entry) != 0){
		int i, j = 0;
		for (i = 0; i < *argz_len; ++i){
			if (argz[0][i] == entry[0])
				for (j = 0; argz[0][i + j] == entry[j] && (j <= strlen(entry) + 1); ++j);
			if (j > strlen(entry)) break;
		}
		if (i < *argz_len){
			*argz_len = *argz_len - strlen(entry) - 1;
			char *dob = (char *)malloc(*argz_len);
			for (j = 0; j < *argz_len; ++j)
				if (j < i) dob[j] = argz[0][j];
				else dob[j] = argz[0][j + strlen(entry) + 1];
			dob[*argz_len] = '\0';
			*argz = dob;
			return OK;
		}
		else{
			printf("There are no matches! Try again.\n");
			return ENOMEM;
		}
	}
	else{
		printf("String is empty! Try again.\n");
		return ENOMEM;
	}
}

/*
The argz_insert function inserts the string entry into the argz vector *argz at a point just before the existing element pointed to by before, reallocating *argz and updating *argz and *argz_len. If before is 0, entry is added to the end instead (as if by argz_add). Since the first element is in fact the same as *argz, passing in *argz as the value of before will result in entry being inserted at the beginning.
*/
error_t argz_insert(char **argz, int *argz_len, char *before, const char *entry){
	if (strlen(argz) != 0 && strlen(entry) != 0){
		int i, j = 0;
		for (i = 0; i < *argz_len; ++i){
			if (argz[0][i] == before[0])
				for (j = 0; argz[0][i + j] == before[j] && (j <= strlen(before) + 1); ++j);
			if (j > strlen(before)) break;
		}
		if (i < *argz_len){
			*argz_len = *argz_len + strlen(entry) + 1;
			char *dob = (char *)malloc(*argz_len);
			for (j = 0; j < *argz_len; ++j){
				if (j < i) dob[j] = argz[0][j];
				else
					if ((j >= i) && (j < (i + strlen(entry) + 1))) dob[j] = entry[j - i];
					else dob[j] = argz[0][j - strlen(entry) - 1];
			}
			dob[*argz_len] = '\0';
			*argz = dob;
			return OK;
		}
		else{
			printf("There are no matches! Try again.\n");
			return ENOMEM;
		}
	}
	else{
		printf("String is empty! Try again.\n");
		return ENOMEM;
	}
}

/*
The argz_next function provides a convenient way of iterating over the elements in the argz vector argz. It returns a pointer to the next element in argz after the element entry, or 0 if there are no elements following entry. If entry is 0, the first element of argz is returned.
This behavior suggests two styles of iteration:
char *entry = 0;
while ((entry = argz_next (argz, argz_len, entry)))
action;
(the double parentheses are necessary to make some C compilers shut up about what they consider a questionable while-test) and:
char *entry;
for (entry = argz; entry; entry = argz_next (argz, argz_len, entry))
action;
Note that the latter depends on argz having a value of 0 if it is empty (rather than a pointer to an empty block of memory); this invariant is maintained for argz vectors created by the functions here.
*/
char * argz_next(char *argz, int argz_len, const char *entry){
	char *pointer = 0;
	if (entry == 0){
		pointer = argz;
		return pointer;
	}
	else{
		int i, j = 0;
		for (i = 0; i < argz_len; ++i){
			if (argz[i] == entry[0])
				for (j = 0; (argz[i + j] == entry[j]) && (j < strlen(entry)); ++j);	
			if (j >= strlen(entry)) break;
		}
		if (i + j < argz_len - 1) pointer = argz + i + strlen(entry) + 1;	
		else pointer = NULL;
		return pointer;
	}
}

/*
Replace the string str in argz with string with, reallocating argz as
necessary.
*/
error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with){
	if (strlen(argz) != 0){
		char *helpstr = (char *)malloc(*argz_len);
		for (int i = 0; i < *argz_len; i++)
			helpstr[i] = argz[0][i];
		int len = *argz_len;
		char *dob = argz_next(helpstr, len, str);
		if (dob != NULL) {
			argz_insert(&helpstr, &len, dob, with);
			argz_delete(&helpstr, &len, str);
			*argz = helpstr;
			*argz_len = len;
			return OK;
		}
		else{
			printf("String is empty! Try again.\n");
			return ENOMEM;
		}
	}
	else{
		printf("String is empty! Try again.\n");
		return ENOMEM;
	}
}
/*prints argz vector */
void argz_print(const char *argz, size_t argz_len){
	if (strlen(argz) != 0){
		for (int i = 0; i < argz_len; i++)
			printf("%c", argz[i]);
		printf("\n");
	}
	else
		printf("String is empty! Try again.\n");
}
