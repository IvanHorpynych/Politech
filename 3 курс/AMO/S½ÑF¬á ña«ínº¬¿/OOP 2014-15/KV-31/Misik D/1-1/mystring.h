/******************************************************************************************
	name:					lab1_mpal.h
	description:			this file contains prototype of functions, that
							described in "mystring.c"
	author:					Dima
	date of creation:		02.09.2014
	written:				04.09.2014
	date of last change:	04.09.2014
******************************************************************************************/

typedef enum { false, true } bool;

double *txt2double(const char  *string, int *size);
int substr(const char *string1, const char *string2);
int subseq(const char *string1, const char *string2);
char ispal(const char *string1);
char *makepal(const char *string);