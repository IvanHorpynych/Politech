/******************************************************************************************
	name:					argz.h
	description:			this file contains prototype of functions, that
							described in "argz.c"
	author:					Dima
	date of creation:		04.09.2014
	written:				06.09.2014
	date of last modified:	06.09.2014
******************************************************************************************/

typedef int size_t;
typedef enum  { OK, ENOMEM } error_t;

error_t argz_create_sep(const char *string, int sep, char **argz, size_t *argz_len);
size_t argz_count(const char *argz, size_t arg_len);
error_t argz_add(char **argz, size_t *argz_len, const char *str);
void argz_delete(char **argz, size_t *argz_len, char *entry);
error_t argz_insert(char **argz, size_t *argz_len, char *before, const char *entry);
char * argz_next(char *argz, size_t argz_len, const char *entry);
void argz_print(const char *argz, size_t argz_len);
error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with);
