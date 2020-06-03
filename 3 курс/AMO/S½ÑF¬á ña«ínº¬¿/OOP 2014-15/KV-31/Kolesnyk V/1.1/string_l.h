/*
File: string_l.h
Synopsis: described functions to work with strings
Author: Kolesnyk V.S.
Group: KV-31, the faculty of applied math (FPM)
Created: 30.09.2014
*/

#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <malloc.h>
#include <math.h>
#include <conio.h>
#include <math.h>

int  substr(const char * string1, const char * string2);
int  subseq(const char * string1, const char * string2);
char ispal(const char * string);
char * makepal(const char * string);
double* txt2double(const char *str, int *size);

