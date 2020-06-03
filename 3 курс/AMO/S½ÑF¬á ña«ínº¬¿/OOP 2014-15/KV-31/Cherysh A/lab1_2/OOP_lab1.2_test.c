/************************************************************************************
*file: OOP_lab2_test.c																*
*Synopsis: this is a test file with main function.									*
*in the "OOP_lab2.c".																*
*related files: OOP_lab2.c															*
*author: Chernysh Andrey															*
*written: 1/10/2014																	*
*last modified: 08/10/2014															*
************************************************************************************/

#include "OOP_lab1.2.h"

int main(){
	char *argz;
	int argz_len;
	int i = 0;

	printf("Task 1 (argz_create_sep): \n");
	argz_create_sep("SHELL=/bin/bash:usr=monty:PWD=/bin/monty:LANG=en_US.UTF-8", 58, &argz, &argz_len);
	printf("\targz_len = %d\n", argz_len + 1);
	printf("\tVector : \n");
	argz_print(argz, argz_len);

	printf("\nTask 2 (argz_count) : \n");
	printf("\tArgz_count = %d", argz_count(argz, argz_len));

	printf("\nTask 3 (argz_add): \n");
	printf("Add : C = BIN/CPP/:D = BIN/C+\ : \n\n");
	argz_add(&argz, &argz_len, "C = BIN/CPP/:D = BIN/C++");
	argz_print(argz, argz_len);

	printf("\nTask 4 (argz_delete): \n");
	printf("Delete PWD : \n\n");
	argz_delete(&argz, &argz_len, "PWD");
	argz_print(argz, argz_len);

	printf("\nTask 5 (argz_insert) : \n");
	printf("Insert lolo before usr\n\n");
	argz_insert(&argz, &argz_len, "usr", "lolo ");
	argz_print(argz, argz_len);

	printf("\nTask 6 (argz_next): \n");
	char *entry = 0;
	int count = 0;
	while ((entry = argz_next(argz, argz_len, entry)) && (count < argz_count(argz,argz_len))){
		printf("\t\t");
		printf(entry, argz_len);
		count++;
		printf("\n");
	}
	
	printf("\nTask 7 (argz_replace): \n");
	printf("Replace string =monty to STRING\n\n");
	argz_replace(&argz, &argz_len, "=monty", "STRING");
	argz_print(argz, argz_len);

	printf("\nTask 8 (argz_print): \n");
	argz_print(argz, argz_len);

	_getch();
	return 0;
}