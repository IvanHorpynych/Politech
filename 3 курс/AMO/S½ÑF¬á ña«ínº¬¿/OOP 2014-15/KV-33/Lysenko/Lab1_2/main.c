/* file name: main.c
* author: Lysenko Vitaliy
* first written: 08/11/14
* last modified: 11/11/14
* related files: lab1_2.c, Lab1_2.h
*/


#include <stdio.h>
#include "lab1_2.h"

int main()
{
	
	size_t i;
	const char *startStr = "SHELL=/bin/bash:usr=monty:PWD=/bin/monty:LANG=en_US.UTF-8";
	char *argz;
	size_t argz_len;
	char *elemToDelete;
	char *beforeElem;

	printf("Testing with string \"%s\"\n\n", startStr);

	printf("Argz create sep:\n");
	argz_create_sep(startStr, ':', &argz, &argz_len);
	argz_print(argz, argz_len);

	printf("Number of elements: %li\n", argz_count(argz, argz_len));
	printf("Argz add:\n");
	argz_add(&argz, &argz_len, "Added string");
	argz_print(argz, argz_len);

	printf("Argz delete:\n");
	elemToDelete = argz_next(argz, argz_len, argz);
	argz_delete(&argz, &argz_len, elemToDelete);
	argz_print(argz, argz_len);

	printf("Argz insert:\n");
	beforeElem = argz_next(argz, argz_len, elemToDelete);
	argz_insert(&argz, &argz_len, beforeElem, "Inserted string");
	argz_print(argz, argz_len);

	printf("Argz replace:\n");
	argz_replace(&argz, &argz_len, "PWD=/bin/monty", "PWD=/user/bin/OOP");
	argz_print(argz, argz_len);


	return 0;
}
