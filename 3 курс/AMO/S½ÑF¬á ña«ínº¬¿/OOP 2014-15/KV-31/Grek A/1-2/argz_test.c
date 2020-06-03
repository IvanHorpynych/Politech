/***********************************************************
*file argz_test.c
*synopsis Main file of project. It includes data for calling function
which are declared in argz.h and also shows how these functions works
*author Alex Grek
*written: 12/10/2014
*last modified: 26/11/2014
***********************************************************/

#include "argz.h"

char *const string = "foo=bar:csharp=perfect"; //start string
char *argz_test;
int argz_len;


int main(){
	argz_create_sep(string, 58, &argz_test, &argz_len);
	printf("Length after creation = %i\n", argz_count(argz_test, argz_len));
	argz_print(argz_test, argz_len);
	printf("String length is:%i\n\n", argz_len);

	argz_add(&argz_test, &argz_len, "a=b");
	printf("\nLength after addition = %i\n", argz_count(argz_test, argz_len));
	argz_print(argz_test, argz_len);
	printf("String length is:%i\n\n", argz_len);

	argz_delete(&argz_test, &argz_len, "foo=bar");
	printf("\nLength after deletion = %i\n", argz_count(argz_test, argz_len));
	argz_print(argz_test, argz_len);
	printf("String length is:%i\n\n", argz_len);

	argz_insert(&argz_test, &argz_len, "a=b", "m=x");
	printf("\nLength after insertion = %i\n", argz_count(argz_test, argz_len));
	argz_print(argz_test, argz_len);
	printf("String length is:%i\n\n", argz_len);

	printf("Next string is: %s\n", argz_next(argz_test, argz_len, 0));

	argz_replace(&argz_test, &argz_len, "a=b", "PTN=PNH");
	printf("\nLength after replacement = %i\n", argz_count(argz_test, argz_len));
	argz_print(argz_test, argz_len);
	printf("String length is:%i", argz_len);
	getch();
	return 0;
}
