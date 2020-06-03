#include "argz.h"

int main() {

	char *string = "SHELL=/bin/bash:usr=monty:PWD=/bin/monty:LANG=en_US.UTF-8";
	char *argz; size_t argz_len;
	char *entry; /* for argz_next */

	 /*  argz_create_sep & argz_print */
	argz_create_sep(string, ':', &argz, &argz_len);
	argz_print(argz, argz_len);
	printf("Argz_len == %d\n\n", argz_len);

	/*argz_count */
	printf("Argz_count == %d\n\n", argz_count(argz, argz_len));

	/*argz_add */
	argz_add(&argz, &argz_len, "added=element");
	argz_print(argz, argz_len);
	printf("Argz_len == %d\n\n", argz_len);

	/*argz_delete */
	argz_delete(&argz, &argz_len, "usr=monty");
	argz_print(argz, argz_len);
	printf("Argz_len == %d\n\n", argz_len);

	/*argz_insert */
	argz_insert(&argz, &argz_len, "LANG=en_US.UTF-8", "one.more=added_element");
	argz_print(argz, argz_len);
	printf("Argz_len == %d\n\n", argz_len);

	/*argz_next */
	printf("First letters of ARGZ elements: ");
	for (entry = argz; entry; entry = argz_next(argz, argz_len, entry)) printf("%c ", *entry);
	printf("\n\n");

	/*argz_replace */
	argz_replace(&argz, &argz_len, "one.more=added_element", "replaced=element");
	argz_print(argz, argz_len);
	printf("Argz_len == %d\n\n", argz_len);
}