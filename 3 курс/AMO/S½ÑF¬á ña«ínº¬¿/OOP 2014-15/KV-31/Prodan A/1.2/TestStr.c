
/*
File: TestStr.c
Synopsis: declaration of functions described in Hstr.h
Author: Prodan A.O.
Group: KV-31, the faculty of applied math (FPM)
Created: 25.10.2014
*/
#include "Hstr.h"
int main()
{
	char *taskstr_1 = "SHELL=/bin/bash:usr=monty:PWD=/bin/monty:LANG=en_US.UTF-8";
	char *task3 = "Slash";
	char *task4 = "/bin/bash";
	char *task5_1 = "monty";

	char *task5_2 = "fifth";
	char *task6 = "monty";
	char *task_str = "monty";
	char *task_with = "health";

	int CountOfElements = 0, CountNewStr = 0;
	if (strlen(taskstr_1) != 0){
		char *argz;

		int argz_len = 0;

		printf("Our string:\n\t%s\n", taskstr_1);

		printf("\nCreating:\n\t");
		argz_create_sep(taskstr_1, 58, &argz, &argz_len);

		CountOfElements = argz_count(argz, argz_len);
		argz_print(argz, argz_len);
		printf("Elements = %d\n", CountOfElements);
		printf("Our string:\n\t%s\n", task3);
		printf("\nAdding:\n\t");
		argz_add(&argz, &argz_len, task3);
		argz_print(argz, argz_len);

		printf("\nDeletting:\n\t");
		argz_delete(&argz, &argz_len, task4);
		argz_print(argz, argz_len);
		printf("\nInserting:\n\t");
		argz_insert(&argz, &argz_len, task5_1, task5_2);
		argz_print(argz, argz_len);
		printf("\nLinks:\n\t");

		task6 = argz_next(argz, argz_len, task6);
		printf("%s", task6);
		printf("\n");

		printf("\nRepleasing:\n\t");
		argz_replace(&argz, &argz_len, task_str, task_with);
		argz_print(argz, argz_len);
		getchar();

	}
	return 0;
}