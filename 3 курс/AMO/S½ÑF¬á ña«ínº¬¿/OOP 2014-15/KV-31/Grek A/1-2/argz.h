/************************************************************************
*file: argz.h
*synopsis: declarations for argz functions, types, constants
*author: Alex Grek
*written: 12/10/2014
*last modified: 30/10/2014
************************************************************************/


#ifndef ARGZ_H
#define ARGZ_H

#include <stdlib.h>
#include <stdio.h>
#include <stddef.h>
#include <malloc.h>
#include <string.h>

typedef enum { OK, ENOMEM } error_t;

/***********************************************************************
*Name argz_create_sep
*Usage argz_create_sep(string, 58, &argz_test, &argz_len)
*Prototype in argz.h
*Synopsis Creates the dynamic array from parametr "string" by splitting it
by symbol "sep" and changing the array`s length.
*Return value OK or ENOMEM
***********************************************************************/
error_t argz_create_sep(const char *, int, char **, size_t *);

/********************************************************
*Name argz_count
*Synopsis Counts the number of elements in the new dynamic array
Elements have a look :"name1=name2"
*Return value the number of elements. If can not count
then return 0
********************************************************/
size_t argz_count(const char *, size_t);

/****************************************************************
*Name argz_add
*Synopsis add a new element to dynamic array
*Return value OK or ENOMEM
****************************************************************/
error_t argz_add(char **, size_t *, const char);

/**************************************************************
*Name argz_delete
*Synopsis Deletes the element from dynamic array and changes
the length of it
*Return value void function
***************************************************************/
void argz_delete(char **, size_t *, char *);

/**************************************************************
*Name argz_print
*Synopsis Prints all elements of dynamic array
*Return value Void function
*************************************************************/
void argz_print(const char *, size_t);

/*******************************************************************
*Name argz_insert
*Synopsis Inserts  the elemnt "entry" before element "before" and
changes the length of array
*Return value OK or ENOMEM
*******************************************************************/
error_t argz_insert(char **, size_t *, char *, const char *);

/*****************************************************************
*Name argz_next
*Synopsis Finds the "entry" in dynamic array.
*Return value Pointer to the element after "entry"
or to the first element of array
******************************************************************/
char * argz_next(char *, size_t, const char);

/********************************************************************
*Name argz_replace
*Synopsis Replace the old element "str" by the new "with"
*Return value OK or ENOMEM
********************************************************************/
error_t argz_replace(char **, size_t *, const char *, const char *);

#endif
