/************************************************************************
*file: argz.h
*synopsis: declarations for argz functions, types, constants
*author: Kyrylo Svintsov
*written: 18/10/2014
*last modified: 18/10/2014
************************************************************************/


#ifndef ARGZ_H
#define ARGZ_H

#include <stdlib.h>
#include <stdio.h>
#include <stddef.h>
#include <malloc.h>

typedef enum {OK,ENOMEM} error_t;

error_t argz_create_sep(const char *, int, char **, size_t *);
size_t argz_count(const char *, size_t );
error_t argz_add(char **, size_t *, const char);
void argz_delete(char **, size_t *, char *);
void argz_print(const char *, size_t);
error_t argz_insert(char **, size_t *, char *, const char *);
char * argz_next(char *, size_t, const char);
error_t argz_replace(char **, size_t *, const char *, const char *);

#endif
