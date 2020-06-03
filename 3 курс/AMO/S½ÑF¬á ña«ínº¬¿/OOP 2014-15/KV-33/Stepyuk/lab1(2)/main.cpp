/************************************************************************
*file: main.c
*purpose: declarations for argz functions, types, constants
*author: Stepyuk E.
*written: 19/10/2014
*last modified: 19/10/2014
*************************************************************************/
#include "oop_lab1p2.h"

void main(){
	char *argz;
	char *const string = "Hello:my:name:is:Zheka";
	size_t argz_len;

	argz_create_sep(string,58,&argz,&argz_len);
	printf(" %d\n %d\n", argz_count(argz, argz_len), argz_len);
	argz_add(&argz, &argz_len, "Stepyuk");
	argz_print(argz, argz_len);
	argz_delete(&argz, &argz_len, "Hello");
	argz_print(argz, argz_len);
	argz_insert(&argz, &argz_len, "my", "teacher");
	argz_print(argz, argz_len);
	printf("%s\n", argz_next(argz, argz_len, "name"));
	argz_replace(&argz, &argz_len, "teacher", "proffesor");
	argz_print(argz, argz_len);
}