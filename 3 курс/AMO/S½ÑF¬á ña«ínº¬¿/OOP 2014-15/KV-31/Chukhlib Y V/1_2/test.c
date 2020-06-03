/*
File: test.c
Synopsis: declaration of functions described in MyString.h
Author: Chukhlib Y.V.
Group: KV-31, the faculty of applied math (FPM)
Created: 13.10.2014
*/

#include "source.h"
//#include "header.h"
int main()
{
	char *before = NULL, mas[] = "SHELL=/bin/bash:usr=:ytr:PWD=/bi:ytr:usd=:LANG=en_US.UTF-8";
	char with[] = "usr=", entr_0[] = "/*(argz_insert)*/", str[] = "sd=15", entr[] = "sd=15", *argz = NULL, *ptr = NULL, wit[] = "/*replace*/";
	int chg = 58, ch = NULL, len_s = strlen(mas);
	size_t len = 0;
	error_t check = OK;

	printf("Input - ");
	argz_print(mas, len_s, OK);

	printf("\n_____argz_create_sep_____\n");
	argz_print(argz, len, argz_create_sep(mas, chg, &argz, &len));

	printf("\n_____argz_count_____Inp: ");
	argz_print(argz, len, OK);
	printf("Result - ");
	ch = argz_count(argz, len);
	if (ch == ERROR)
		printf("ERROR\n");
	else
		printf("%d\n", ch);

	printf("\n_____argz_add_____\nAdd_string: %s\n", str);
	argz_print(argz, len, argz_add(&argz, &len, str));

	printf("\n_____argz_delete_____\nEntry: %s\n", entr);
	argz_delete(&argz, &len, entr);
    argz_print(argz, len, OK);

	before = "usr=";
	printf("\n_____argz_insert_____\nInsert: %s\n", entr_0);
	argz_print(argz, len, argz_insert(&argz, &len, before, entr_0));

	ptr = argz_next(argz, len, with);
	printf("\n_____argz_next_____\nInp: %s\n", with);
	printf("%s\n", ptr);

	printf("\n_____argz_replace_____\nrepl.: %s\n", entr_0);
	argz_print(argz, len, argz_replace(&argz, &len, entr_0, wit));
	
	free(argz);
	return 0;
}


