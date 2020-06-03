/************************************************************************
*file: testing.c
*author: Dnedoj D.
*written: 31/09/2014
*last modified: 31/09/2014
*************************************************************************/
#include "lab1_2string.h"

int main(){
	char *taskstr_1 = "SHELL=/bin/bash:usr=monty:PWD=/bin/monty:LANG=en_US.UTF-8";
	if (strlen(taskstr_1) != 0){
		char *argz;
		int argz_len = 0;
		printf("Our string:\n\t%s\n", taskstr_1);

		printf("\nCreating:\n\t");
		argz_create_sep(taskstr_1, 58, &argz, &argz_len);
		int CountOfElements = argz_count(argz, argz_len);
		argz_print(argz, argz_len);
		printf("Elements = %d\n", CountOfElements);

		printf("\nAdding:\n\t");
		argz_add(&argz, &argz_len, "Hello World!");
		argz_print(argz, argz_len);

		printf("\nDeletting:\n\t");
		argz_delete(&argz, &argz_len, "SHELL=/bin/bash");
		argz_print(argz, argz_len);
		/*
		printf("\nInserting:\n\t");
		argz_insert(&argz, &argz_len, "SHELL=/bin/bash", "London");
		argz_print(argz, argz_len);

		printf("\nLinks:\n\t");
		char *entry = 0;
		while ((entry = argz_next(argz, argz_len, entry)))
			printf("%s", entry);
		printf("\n");

		printf("\nRepleasing:\n\t");
		argz_replace(&argz, &argz_len, "usr=monty", "Hello");
		argz_print(argz, argz_len);
		*/
		return 0;
	}
	else{
		printf("Our source string is empty! Try again.");
		return 0;
	}
}	