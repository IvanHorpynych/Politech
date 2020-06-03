#include "Header.h"

void main()
{
	const char string[100] = "123:qq:q";
	int sep = 58;
	char *argz;
	int argz_len = 0;

	printf("1) Create argz\n");
	argz_create_sep(string, sep, &argz, &argz_len);
	printf("length = %d\n", argz_len);
	argz_print(argz, argz_len);
	printf("count = %d\n", argz_count(argz, argz_len));

	const char str[100] = "48";
	printf("2) Add to the end\n");
	argz_add(&argz, &argz_len, str);
	printf("length = %d\n", argz_len);
	argz_print(argz, argz_len);
	printf("count = %d\n", argz_count(argz, argz_len));

	printf("3) Delete\n");
	char *entry = "qqq";
	argz_delete(&argz, &argz_len, entry);
	printf("length = %d\n", argz_len);
	argz_print(argz, argz_len);
	printf("count = %d\n", argz_count(argz, argz_len));

	printf("4) Add entry1 before before\n");
	char *before = "qq";
	const char entry1[100] = "qweq";
	argz_insert(&argz, &argz_len, before, entry1);
	printf("length = %d\n", argz_len);
	argz_print(argz, argz_len);
	printf("count = %d\n", argz_count(argz, argz_len));

	printf("5) Pointer to the next\n");
	char *entry2 = "123";
	printf("%s\n", argz_next(argz, argz_len, entry2));

	printf("6) Replace str1 on the with\n");
	const char str1[100] = "123";
	const char with[100] = "91207";
	argz_replace(&argz, &argz_len, str1, with);
	printf("length = %d\n", argz_len);
	argz_print(argz, argz_len);
	printf("count = %d\n", argz_count(argz, argz_len));
	getch();
}