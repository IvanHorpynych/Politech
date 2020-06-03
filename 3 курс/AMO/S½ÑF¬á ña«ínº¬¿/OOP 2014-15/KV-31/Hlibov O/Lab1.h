/************************************************************************
>file: Lab1.h
>author: Hlibov A.R.
>group: KV-31, FPM
>written: 10/09/2014
>last modified: 15/09/2014
************************************************************************/

#include <stdio.h>
#include <string.h>
#include <stdlib.h>

#ifndef LAB1_H
#define LAB1_H

int substr(const char *string1, const char *string2);
int subseq(const char *string1, const char *string2);
char ispal(const char *string);
char* makepal(const char* string);
double* txt2double(const char *num, int *size);

#endif LAB1_H