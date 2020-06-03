#include<stdio.h>
#include<string.h>
#include<stdlib.h>
#include<malloc.h>
#include<ctype.h>
int substr(const char *str1, const char *str2);
int subseq(const char *str1, const char *str2);
char ispal(const char *s);
char *makepal(const char *);
double* txt2double(const char*s, int *size);