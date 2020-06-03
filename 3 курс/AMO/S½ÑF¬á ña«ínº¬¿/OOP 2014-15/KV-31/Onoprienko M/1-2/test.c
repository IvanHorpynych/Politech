#include "Header.h"

void main()
{
	const char string[100] = "123:4567:89";
	int sep = 58;
	char *argz;
	int argz_len = 0;
	printf("1) Create argz\n\n");
	argz_create_sep(string, sep, &argz, &argz_len);
	printf("len = %d\n", argz_len);
	argz_print(argz, argz_len);
	printf("count = %d\n", argz_count(argz, argz_len));
	printf("-----\n");
	const char str[100] = "48";
	printf("2) Add to the end\n\n");
	argz_add(&argz, &argz_len, str);
	printf("len = %d\n", argz_len);
	argz_print(argz, argz_len);
	printf("count = %d\n", argz_count(argz, argz_len));
	printf("-----\n");
	printf("3) Delete\n\n");
	char *entry = "4567";
	argz_delete(&argz, &argz_len, entry);
	printf("len = %d\n", argz_len);
	argz_print(argz, argz_len);
	printf("count = %d\n", argz_count(argz, argz_len));
	printf("-----\n");
	printf("4) Add entry1 before before\n\n");
	char *before = "89";
	const char entry1[100] = "107";
	argz_insert(&argz, &argz_len, before, entry1);
	printf("len = %d\n", argz_len);
	argz_print(argz, argz_len);
	printf("count = %d\n", argz_count(argz, argz_len));
	printf("-----\n");
	printf("5) Pointer to the next\n\n");
	char *entry2 = "107";
	printf("%s\n", argz_next(argz, argz_len, entry2));
	printf("-----\n");
	printf("6) Replace str1 on the with\n\n");
	const char str1[100] = "89";
	const char with[100] = "91207";
	argz_replace(&argz, &argz_len, str1, with);
	printf("len = %d\n", argz_len);
	argz_print(argz, argz_len);
	printf("count = %d\n", argz_count(argz, argz_len));
	getch();
}