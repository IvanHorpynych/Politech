/***********************************************************************
*file: project1.c
*synopsis: This file is used to test the functions located in file "str_work.c"
*and declared in file "string.h"
*author: volontyr alexandr
*written: 03/09/2014
*last modified: 06/09/2014
************************************************************************/

#include <stdio.h>
#include <stdlib.h>
#include "string.h"

int main(){
	char *str1 = NULL;
	char *str2 = NULL;
	char *str3 = NULL;
	char *str4 = NULL;
	char *str5 = NULL;
	int *arr_size;
	double *array = NULL;
	arr_size = NULL;

	printf("Enter the first line\n");
	str1 = put_str();
	printf("Enter the second line\n");
	str2 = put_str();
	printf("result = %d\n", substr(str1, str2));

	printf("max general length = %d\n", subseq(str1, str2));

	printf("Enter the word you want to check for palindromicity\n");
	str3 = put_str();
	printf("polyndrom or not ('1' or '0'): %d\n", ispal (str3));

	printf("Enter any word\n");
	str4 = put_str();
	printf("polyndrom maden of the word = %s\n", makepal(&str4));

	printf("Enter the line of some numbers separated with a semicolon\n");
	str5 = put_str();
	printf("Array of these numbers: ");
	array = txt2double(str5, &arr_size);
	if (arr_size != 0) {
		for (int i = 0, len = *arr_size; i < len; i++) {
			printf("%f\t", array[i]);
		}
		printf("\n");
	}
	else {
		printf("convertation error\n");
	}
	return 0;
}

