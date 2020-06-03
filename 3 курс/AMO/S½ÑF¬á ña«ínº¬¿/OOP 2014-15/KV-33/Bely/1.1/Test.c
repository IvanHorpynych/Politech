/************************************************************************
*file: Test.c
*synopsis: test functions
*author: Turchaninov Gennady
*written: 04/10/2013
*last modified: 09/10/2013
************************************************************************/

#include "mystring.h"
#include <stdio.h>

void main()
{
	double* test_arr;
	int length, i;
	printf("Test program \n \n");

	printf("Test substr \nInput: Hello, world & world \n");
	printf("Result(index) -> %d \n", substr("Hello, world", "world"));
	printf("Input: kykyratchi & kyra \n");
	printf("Result(index) -> %d \n \n", substr("kykyratchi", "kyra"));

	printf("Test subseq \nInput: student & docent \n");
	printf("Result (length of the longest subsequence) -> %d \n", subseq("student", "docent"));
	printf("Input: kykyratchi & atchi \n");
	printf("Result (length of the longest subsequence) -> %d \n \n", subseq("kykyratchi", "atchi"));

	printf("Test ispal. Return vault : 1 -> string is a palindrome \n");
	printf("                         : 0 -> string is not a palindrome \n");
	printf("Input: genaneg \nResult -> %d \n \n", ispal("genaneg"));

	printf("Test makepal \nInput: gena \n");
	printf("Result -> %s \n", makepal("gena"));
	printf("Input: 123454 \n");
	printf("Result -> %s \n \n", makepal("123454"));

	printf("Test  txt2double \nInput : 25;3.14;-12;0.5 \n");
	test_arr = txt2double("25;3.14;-12;0.5", &length);
	printf("Return length = %d \n", length);
	printf("Value of array \n");
	for (i = 0; i<length; i++)
		printf("test_arr[%d] = %f \n", i, test_arr[i]);

	printf("Testing was completed successfully");
	getchar();
}
