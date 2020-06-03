/******************************************************************************************
	name:					argztest.c
	description:			this file contains function "EnterString" that return pointer
							to string you entered and "main", that calls functions from
							file "argz.h" (functions in "argz.h" described in this file)
	author:					Dima
	date of creation:		04.09.2014
	written:				06.09.2014
	date of last modified:	10.09.2014
******************************************************************************************/

#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include "argz.h"

char *EnterString(char* string) {													//enter dynamic string
	int SLen = 0;																	//SLen - length of entered string
	char ch;
	char *str = (char *)malloc(sizeof(char)* 256);									//string
	printf("Enter %s string\n", string);
	while ((ch = getchar()) != '\n' && SLen != 255) str[SLen++] = ch;				//enter string by chars
	str[SLen] = '\0';																//add in end null symbol
	str = (char *)realloc(str, (1 + SLen) * sizeof(char));							//reallocation
	system("cls");
	return str;
}

/******************************************************************************************
	ARGZ - string which is formed argz
	ADD - string which would be added after call function "argz_add"
	DELETE - string which would be deleted after call function "argz_delete"
	BFR - string which would be sended in function "argz_insert"
	INSERT - string which would be inserted after call function "argz_insert"
	argz - array of strings
	START - pointer to ellements of array
******************************************************************************************/

int main() {
	char *ARGZ = EnterString("first"), *ADD = EnterString("added"), *DELETE = EnterString("delete"),
		*BFR = EnterString("before"), *INSERT = EnterString("insert"), *REPL = EnterString("replace"),
		*WITH = EnterString("with"),  *argz = 0, *START = 0;
	size_t *size = malloc(sizeof(int));
	int sep, i;
	char *c = malloc(sizeof(char));
	printf("Enter SEP\n");
	scanf("%c", c);
	system("cls");
	sep = *c;
	free(c);
	*size = 0;

	if (argz_create_sep(ARGZ, sep, &argz, size) == OK) {
		argz_print(argz, *size);
		if (argz_add(&argz, size, ADD) == OK) argz_print(argz, *size);
		else printf("Error of adding\n\n");

		argz_delete(&argz, size, DELETE);
		argz_print(argz, *size);

		if (argz_insert(&argz, size, BFR, INSERT) == OK) argz_print(argz, *size);

		else printf("Error of insert\n\n");
		argz_replace(&argz, size, REPL, WITH);
		argz_print(argz, *size);
		do {
			START = argz_next(argz, *size, START);
			if (START != 0) for (i = 0; START[i] != '\0'; i++) printf("%c", START[i]);
			printf("\n");
		} while (START != 0);
		
	} else printf("Error of input\n\n");
			
	free(ARGZ);
	free(ADD);
	free(DELETE);
	free(BFR);
	free(REPL);
	free(WITH);
	free(INSERT);
	free(START);
	free(argz);
	free(size);
	_getch();
	return 0;
}