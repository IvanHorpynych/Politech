/*
File: func.c
Author: Onoprienko M.I.
Group: KV-31, FPM
Written: 13.11.2014
*/

#include <string.h>
#include <math.h>
#include <stdlib.h>
#include <malloc.h>
#include <stdio.h>

typedef enum  { OK, ENOMEM } error_t;
error_t argz_create_sep(const char *string, int sep, char **argz, size_t *argz_len); //separates string when meets sep
size_t argz_count(const char *argz, size_t arg_len); //counts number of elements in argz
error_t argz_add(char **argz, size_t *argz_len, const char *str); //adds string str to argz
void argz_delete(char **argz, size_t *argz_len, char *entry); //deletes entry from argz
error_t argz_insert(char **argz, size_t *argz_len, char *before, const char *entry); //inserts entry before "before"
char * argz_next(char *argz, size_t argz_len, const char *entry); //returns pointer to argz after entry, or zero if there is no such element
error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with); //replaces str with "with"
void argz_print(const char *argz, size_t argz_len); //prints argz
