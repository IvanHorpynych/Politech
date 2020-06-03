/************************************************************************
*file: lab1_2string.h
*author: Dnedoj D.
*written: 31/09/2014
*last modified: 31/09/2014
*************************************************************************/
#ifndef _ARGZ_H
#define _ARGZ_H
#include <stddef.h> // for  size_t
#include <stdio.h>
#include <string.h>
#include <stdlib.h>


typedef enum  { OK, ENOMEM } error_t;


/* function prototypes */

error_t argz_create_sep(const char *string, size_t sep, char **argz, int *argz_len);
size_t argz_count(const char *argz, size_t arg_len);
error_t argz_add(char **argz, size_t *argz_len, const char *str);
void argz_delete(char **argz, size_t *argz_len, char *entry);
error_t argz_insert(char **argz, size_t *argz_len, char *before, const char *entry);
char * argz_next(char *argz, size_t argz_len, const char *entry);
error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with);
void argz_print(const char *argz, size_t argz_len);
#endif
