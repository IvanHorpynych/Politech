
#ifndef _MARIAN
#define _MARIAN

#include <stdio.h>
#include <string.h>
#include <stdlib.h>



int substr(const char *str1, const char *str2);
int subseq(const char *str1, const char *str2);
char ispal( const char *str);
char* makepal( const char *str);
double* txt2double(const char * , int *size);

#endif