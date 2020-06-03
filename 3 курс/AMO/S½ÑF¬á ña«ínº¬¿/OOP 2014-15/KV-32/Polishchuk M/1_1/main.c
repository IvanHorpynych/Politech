#include "mystring.h"

#include <stdio.h>

main()
{
	char *str2 = "2134  1 2 3 412 4321", *str1 = "1343.65;3f2.5;7";

	printf("Laba #1 (part_1) \nmade by Polishchuk. M.O. (KV - 32) \n\n");
	printf("---------------------------------------------------------------\n");
	
	printf("string #1: %s",str1 );
	printf("\nstring #2: %s", str2);
	
	printf("\n---------------------------------------------------------------\n");
	printf("\n\nFunction 1 (subtr): \nresult: %d\n______________________________\n", substr(str1, str2), "\n\n");
	printf("\nFunction 2 (subseq): \nresult: %d\n______________________________\n", subseq(str1, str2), "\n");

	printf("\nFunction 3 (ispal): \nresult: %d\n______________________________\n", ispal(str1), "\n\n");
	printf("\nFunction 4 (makepal): \nresult: %s\n______________________________\n", makepal(str1));

	int massize;
	double *p;

	printf("\nFunction 5 (txt2double): \nresult: ");
	p = txt2double(str1, &massize);
    printf("\n  - size = %d\n", massize);
	for (int k = 0; k < massize; k++)
		printf("\n Element %d = %.5f\n", k, p[k]);
	
	getchar();
	return 0;
}