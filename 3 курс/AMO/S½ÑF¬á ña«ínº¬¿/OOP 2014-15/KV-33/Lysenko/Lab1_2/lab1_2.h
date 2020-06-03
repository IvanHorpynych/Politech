/* file name: lab1_2.c
* author: Lysenko Vitaliy
* first written: 08/11/14
* last modified: 11/11/14
* describes: Access to some string-handling routines
*/


#ifndef LAB1_2_H
#define LAB1_2_H

#include <string.h>

typedef enum
{
	OK,
	ENOMEM
} error_t;

typedef unsigned long ulong;
typedef unsigned int uint;
typedef unsigned short ushort;
typedef unsigned char uchar;

error_t argz_create_sep(const char *str, char sep, char **argz, size_t *argz_len);
size_t argz_count(const char *argz, size_t argz_len);
error_t argz_add(char **argz, size_t *argz_len, const char *str);
void argz_delete(char **argz, size_t *argz_len, char *entry);
error_t argz_insert(char **argz, size_t *argz_len, char *before, const char *entry);
char *argz_next(char *argz, size_t argz_len, const char *entry);
error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with);
void argz_print(const char *argz, size_t argz_len);

#endif