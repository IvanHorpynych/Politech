/*
File: string_func.c
Korshun.A.S
*/

#include <stddef.h> // for  size_t
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <malloc.h>

#ifndef STRING_FUNC_H
#define STRING_FUNC_H

int substr(const char *string1, const char *string2); //¬озвращает индекс элемента, из которого string2 начинаетс€ в string1
int subseq(const char *string1, const char *string2); //¬озвращает длину самого длинного общего подстроке string1 and string2
char ispal(const char *string); //¬озвращает 0, если слово непалиндром и 1, если это полиндром
char* makepal(const char *); //создает палиндром
double* txt2double(const char *, int *size);

#endif 