/***********************************************************************
*file: oop_lab1.2.h
*purpose: declarations for oop_lab1.2.c functions, types, constants
*author: Grushko Y.V.
*written: 15/09/2014
*last modified 15/09/2014
***********************************************************************/

#ifndef _HEADER_H
#define _HEADER_H

#include <stddef.h> // for  size_t
#include <stdio.h>
#include <string.h>
#include <stdlib.h>

typedef enum  { OK, ERROR } error_t;
typedef char* pChar;

/* function prototypes */
error_t argz_create_sep(const char *string, int sep, char **argz, size_t *argz_len);
size_t argz_count(const char *argz, size_t arg_len);
error_t argz_add(char **argz, size_t *argz_len, const char *str);
error_t argz_delete(char **argz, size_t *argz_len, char *entry);
error_t argz_insert(char **argz, size_t *argz_len, char *before, const char *entry);
char* argz_next(char *argz, size_t argz_len, const char *entry);
error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with);
void argz_print(const char *argz, const size_t argz_len, error_t check);
int enother_work(char *argz, const size_t *argz_len, const char *entry, int key);
int check_null(const char *argz, const size_t *argz_len);

#endif /* HEADER_H */
