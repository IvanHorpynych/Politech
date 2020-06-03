/***********************************************************
*file argz_test.c
*synopsis Main file of project. It includes data for calling function 
which are declared in argz.h and also shows how these functions works
*author Kyrylo Svintsov
*written: 18/10/2014
*last modified: 18/10/2014
***********************************************************/

#include "argz.h"

char *const string ="a=b:u=m";
char *argz_test;
int argz_len;


int main(){
	argz_create_sep(string, 58, &argz_test, &argz_len);
	printf("Length before addition=%i\n", argz_count(argz_test,argz_len));
	argz_print(argz_test, argz_len);

	argz_add(&argz_test, &argz_len,"c=d");
	printf("\nLength after addition=%i\n", argz_count(argz_test, argz_len));
	argz_print(argz_test, argz_len);
	printf("The length is:%i\n\n", argz_len);

	argz_delete(&argz_test, &argz_len,"u=m");
	argz_print(argz_test, argz_len);
	printf("The length is:%i\n\n", argz_len);

	argz_insert(&argz_test, &argz_len, "c=d", "u=m");
	printf("After insert:");
	argz_print(argz_test, argz_len);
	printf("The length is:%i\n\n", argz_len);

	printf("Next string:%s\n", argz_next(argz_test, argz_len, 0));
	printf("The length is:%i\n\n", argz_len);

	argz_replace(&argz_test, &argz_len, "c=d", "g=");
	argz_print(argz_test, argz_len);
	printf("The length is:%i\n\n", argz_len);
	getch();
	return 0;
}