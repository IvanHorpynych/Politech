/*******************************************************************
*Lab 1
*File: Test.c
*Description: This file is for testing functions declared in lab1.h
*Author: Grek A.
*written 26/09/2014
*last modified: 28/09/2014
*******************************************************************/


#include "Lab1.h"

int main() {
	char str1[] = "This is string one",
		str2[] = "is string";
	printf("String1: %s\nString2: %s \nSubstr: %d\n\n", str1, str2, substr(str1, str2));

	char qq[] = "find the looongest sequence",
		qs[] = "I am so alooone, I let me find";
	printf("String1: %s\nString2: %s\n", qq, qs);
	printf("subseq(string1, string2) = %d\n", subseq(qq, qs));
	printf("subseq(string2, string1) = %d\n\n", subseq(qs, qq));

	char pal[] = "arexisixera",
		notpal[] = "golangna";
	printf("%s is %spalindrom\n", pal, ispal(pal) ? "" : "not ");
	printf("%s is %spalindrom\n\n", notpal, ispal(notpal) ? "" : "not ");

	printf("Created palindrom: %s\n", makepal(notpal));

	int n = 0;
	double *d_ptr;
	char *x = "3.14;2.48;69.12653";
	printf("\nInput: %s\n", x);
	d_ptr = txt2double(x, &n);
	printf("The second value of array is:%.4f\n", d_ptr[1]);
	printf("Number of values in array: %i\n", n);

	getchar();
	return 0;
}