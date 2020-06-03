/* file name: test.c
* programmer: Grushko Y. V.
* first written: 21/09/14
* last modified: 22/09/14
* related files: source.c, header.h
*/
#include "header.h"
int main()
{
	char *before = NULL, mas = "name=xvalue:sd=15:msdn=c:doc=admin";
	char with[] = "msdn=c", entr_0[] = "/*(argz_insert)*/", str[] = "sd=15", entr[] = "sd=15", *argz = NULL, *ptr = NULL, wit[] = "/*replace*/";
	int chg = 58, ch = NULL, len_s = strlen(mas);
	size_t len = 0;
	error_t check = OK;
	printf("Inp: ");
	argz_print(mas, len_s, OK);
	printf("\n.....argz_create_sep.....\n");
	argz_print(argz, len, argz_create_sep(mas, chg, &argz, &len));
	printf("\n.....argz_count.....Inp: ");
	argz_print(argz, len, OK);
	printf("Result: ");
	ch = argz_count(argz, len);
	if (ch == ERROR)
		printf("ERROR\n");
	else
		printf("%d\n", ch);
	printf("\n.....argz_add.....\nadd_string: %s\n", str);
	argz_print(argz, len, argz_add(&argz, &len, str));
	printf("\n.....argz_delete.....\n(entry: %s)\n", entr);
	argz_print(argz, len, argz_delete(&argz, &len, &entr));
	before = &argz[10];
	printf("\n.....argz_insert.....\n(insert: %s)\n", entr_0);
	argz_print(argz, len, argz_insert(&argz, &len, before, entr_0));
	printf("\n.....argz_replace.....\nrepl.: %s\n", entr_0);
	argz_print(argz, len, argz_replace(&argz, &len, entr_0, wit));
	ptr = argz_next(argz, len, with);
	printf("\n.....pointer.....\ninp: %s\n", with);
	if (ptr != 'E')
		printf("%s\n", ptr);
	free(argz);
	return 0;
}

