/*
File: header.h
Synopsis: contains functions for working with strings
Author: Onoprienko M.I.
Group: KV-31, FPM
Written: 27.09.2014
*/

#include <stddef.h> // for  size_t
#include <stdio.h>
#include <conio.h>
#include <string.h>
#include <math.h>
#include <stdlib.h>
#include <malloc.h>


int substr(const char *string1, const char *string2); //Returns index of element from which string2 starts in string1
int subseq(const char *string1, const char *string2); //Returns length of the longest common substring of string1 and string2
char ispal(const char *string); //Returns 0 if word is not a palindrome and 1 if it is
char* makepal(const char *); //Turns word in to palindrom with lowest quantity of added symbols
double* txt2double(const char *, int *size);
/*
First parameter is a pointer to a string which contains numbers divided by ;
For example: 13.125;3.14;12
Function places these numbers in dynamic array and returns pointer to it.
Parameter size acquires value of array length in case of successful ending and 0 if fail
(Fail means if one of the numbers cannot be correctly converted).
For example: "1123.45;Sb3;3.14"
Obviously you cannot convert "Sb3"
*/
