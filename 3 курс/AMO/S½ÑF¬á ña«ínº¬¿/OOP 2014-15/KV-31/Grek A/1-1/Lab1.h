/***********************************************************************
*file: "Lab1.h"
*purpose: declarations for Lab1.c functions, types, constants
*Author: Grek A.
*written 20/09/2014
*last modified: 20/09/2014
***********************************************************************/



#ifndef _HEADER_L1
#define _HEADER_L1


#include <stdio.h>
#include <string.h>
#include <stdlib.h>

/*Returns index of substring in string, returns -1 if substring not found*/
int substr(const char *, const char *);

/*Returns length of the longest sequence in two strings*/
int subseq(const char *, const char *);

/*Check string for palindrom, returns true if string is palindrom*/
char ispal(const char *);

/*Creates palindrom from the string and returns pointer to it*/
char* makepal(const char *);

/*Returns pointer to the array of numbers*/
double* txt2double(const char *, int *);


#endif
