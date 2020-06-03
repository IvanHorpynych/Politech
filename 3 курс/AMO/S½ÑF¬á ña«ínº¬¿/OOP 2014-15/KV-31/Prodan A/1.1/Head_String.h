
/************************************************************************
*file: Head_String.h
*author:Prodan A.O.
*written: 28/09/2014
*last modified: 30/09/2014
************************************************************************/
#include <stddef.h> 
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <malloc.h>

#ifndef HEAD_STRING_H
#define HEAD_STRING_H
int substr(const char *string1, const char *string2);
int subseq(const char *string1, const char *string2);
char ispal(const char *string);
char* makepal(const char* string1);
double* txt2double(const char *string, int *size);
#endif