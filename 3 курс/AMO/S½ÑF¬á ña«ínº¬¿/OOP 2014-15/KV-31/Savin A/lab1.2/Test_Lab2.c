#include "string_func_2.h"

int main()
{
	char *const sepstr = "SHELL=/bin/bash:usr=monty:PWD=/bin/monty:LANG=en_US.UTF-8";
	char *argz;
	int argz_len;
	printf("Source string is %s\n", sepstr);
	argz_create_sep(sepstr, 58, &argz, &argz_len);
	argz_print(argz, argz_len);
	printf("Number of elements in\n"); 
	argz_print(argz, argz_len);
	printf("is %d\n", argz_count(argz, argz_len));
	char *addstr = "Savin_A.D.";
	argz_add(&argz, &argz_len, addstr);
	argz_print(argz, argz_len);
	char *delstr = "PWD=/bin/monty";
	argz_delete(&argz, &argz_len, delstr);
	argz_print(argz, argz_len);
	char *instr = "OOP_Lab2";
	char *befstr = "SHELL=/bin/bash";
	argz_insert(&argz, &argz_len, befstr, instr);
	argz_print(argz, argz_len);
	char *nextstr = "OOP_Lab2";
	printf("%s\n", argz_next(argz, argz_len, nextstr));
	printf("Number of elements in\n");
	argz_print(argz, argz_len);
	argz_replace(&argz, &argz_len, "usr=monty", "Hello");
	argz_print(argz, argz_len);
	printf("is %d\n", argz_count(argz, argz_len));
	return 0;
}