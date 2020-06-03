/************************************************************************
*file: argz_test.c
*author: Kolesnyk V.S.
*written: 10/11/2014
*last modified: 18/11/2014
*************************************************************************/
#include "argz.h"

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

		printf("\nInserting:\n\t");
		argz_insert(&argz, &argz_len, "usr=monty", "London");
		argz_print(argz, argz_len);

		printf("\nLinks:\n\t");
		char *entry = 0;
		while ((entry = argz_next(argz, argz_len, entry)))
			printf("%s", entry);
		printf("\n");

		printf("\nReplacing:\n\t");
		argz_replace(&argz, &argz_len, "usr=monty", "LERA");
		argz_print(argz, argz_len);
		getchar();
		return 0;
	}
	else{
		printf("Our source string is empty! Try again.");
		getchar();
		return 0;
	}
}