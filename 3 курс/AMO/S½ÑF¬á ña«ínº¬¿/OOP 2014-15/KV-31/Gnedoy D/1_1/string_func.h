/************************************************************************
*file: string_func.h
*author: Gnedoj D. O.
*group: KV-31, FPM
*written: 10/09/2014
*last modified: 10/09/2014
************************************************************************/

#include <stddef.h> // for  size_t
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <malloc.h>

#ifndef STRING_FUNC_H
#define STRING_FUNC_H

int substr(const char *string1, const char *string2); //Returns index of element from which string2 starts in string1
int subseq(const char *string1, const char *string2); //Returns length of the longest common substring of string1 and string2
char ispal(const char *string); //Returns 0 if word is not a palindrome and 1 if it is
char* makepal(const char *str); //Turns word in to palindrom with lowest quantity of added symbols
double* txt2double(const char *str, int *size);

#endif /* STRING_FUNC_H */
