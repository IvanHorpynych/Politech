#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <stddef.h> 

typedef enum  {OK, ENOMEM} error_t;
int help_argz_cr_sep (const char *str, int sep, char **argz);
error_t argz_create_sep (const char *str, int sep, char **argz, size_t *argz_len);
size_t argz_count (const char *argz, size_t argz_len);
error_t argz_add (char **argz, size_t *argz_len, const char *str);
int argz_find(char *argz, size_t argz_len, const char *str, int flag);
void argz_delete (char **argz, size_t *argz_len,const char *entry);
error_t argz_insert (char **argz, size_t *argz_len, char *before, const char *entry);
int argz__count(char *argz, const char *entry, int begin);
char * argz_next (char *argz, size_t argz_len, const char *entry);
error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with);
void argz_print(const char *argz, size_t argz_len);
