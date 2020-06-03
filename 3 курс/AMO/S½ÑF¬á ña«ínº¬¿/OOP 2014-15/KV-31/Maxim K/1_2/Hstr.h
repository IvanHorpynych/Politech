/************************************************************************
*file: Hstr.h
*author:Maxim K.E.
*written: 30/09/2014
*last modified: 15/10/2014
************************************************************************/

#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <malloc.h>
#include <math.h>
#include <conio.h>

typedef enum  { OK, ENOMEM } error_t;

#ifndef HSTR_H
#define HSTR_H
/* function prototypes */

error_t argz_create_sep (const char *string, size_t sep, char **argz, size_t *argz_len);
size_t argz_count(const char *argz, size_t arg_len);
error_t argz_add(char **argz,size_t *argz_len,const char * str);
void argz_delete( char **argz, size_t *argz_len,char*entry);
error_t argz_insert(char **argz,size_t *argz_len,char *before, char *entry);
char *argz_next(char *argz,size_t argz_len,const char *entry);
error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with);
void argz_print(const char *argz, size_t argz_len);

#endif 