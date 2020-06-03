/************************************************************************
*file: argz.c
*purpose: declarations for argz functions, types, constants
*author: Khlevnoy Y.
*written: 03/11/2014
*last modified: 12/11/2014
*************************************************************************/
#include "argz.h"


error_t argz_create_sep (const char *string, int sep, char **argz, size_t *argz_len)
{
    int i;
    char *ch;
    int len = strlen(string);

    if(len==0)
        return ENOMEM;

    *argz = (char*) malloc (sizeof(char)*(len + 1));
    strcpy(*argz, string);
    *argz_len = strlen(*argz);
    ch = *argz;
    for(i = 0; i < len; i++) {
        if(*ch == sep) *ch='\0';
        ch++;
    }
    return OK;
}

size_t argz_count (const char *argz, size_t argz_len)
{
    int i, numLex = 0;
    const char *ch = argz;

    if(argz_len == 0)
        return -1;
    for(i = 0; i <= argz_len; i++) {
        if(*ch == '\0')
            numLex++;
        ch++;
    }
    return numLex;
}

error_t argz_add (char **argz, size_t *argz_len, const char *str)
{
    char *tmp;
    int i, len = strlen(str);

    tmp = (char*) malloc (sizeof(char)* (*argz_len+1));
    for(i = 0; i <= *argz_len; i++) {
        tmp[i] = (*argz)[i];
    }
    free(*argz);
    *argz = (char*) malloc (sizeof(char)*(*argz_len + len + 2));
    for(i = 0; i <= *argz_len; i++) {
        (*argz)[i] = tmp[i];
    }
    for(i = 0; i <= len; i++) {
        (*argz)[*argz_len + i + 1] = str[i];
    }
    *argz_len += len + 1;
    free(tmp);
    return OK;
}


int substr(const char *str1, size_t argz_len, const char *str2)
{
    int i, j, ind = -1;
    int len1 = argz_len, len2 = strlen(str2);

    if(len1 < len2)
        return -1;
    for (i = 0; i < len1 - len2 + 1; i++) {           // until there is sense for iterating
        if (str1[i] == str2[0]) {                     // comparison of the 1st symbols
            ind = i;
            for(j = 0; j < len2; j++) {               // comparison of the last symbols
                if(j == len1 || str1[i + j] != str2[j] ) {
                    ind = -1;
                    break;
                }
            }
            if (ind != -1)
                return ind;                           // substring found
        }
    }
    if(ind == -1)
        return ind;

    return ENOMEM;
}



void argz_delete (char **argz, size_t *argz_len, const char *entry)
{
    char *tmp, *ch;
    int len = strlen(entry);
    int ind = substr(*argz, *argz_len, entry);
    int i;

    if(ind == -1)
        return;
    tmp = (char*) malloc (sizeof(char) * (*argz_len-len));
    ch = tmp;
    for(i = 0; i < ind; i++) {
        *ch++ = (*argz)[i];
    }
    for(i = ind + len + 1; i <= *argz_len; i++) {
        *ch++ = (*argz)[i];
    }
    *argz_len -= len + 1;
    realloc (*argz, sizeof(char) * (*argz_len + 1));
    for(i = 0; i <= *argz_len; i++) {
        (*argz)[i] = tmp[i];
    }
    free(tmp);
}

error_t argz_insert (char **argz, size_t *argz_len, const char *before, const char *entry)
{
    char *tmp, *ch;
    int len = strlen(entry);
    int ind = substr(*argz, *argz_len, before);
    int i;

    if (ind == -1)
        return ENOMEM;
    tmp = (char*) malloc (sizeof(char) * (*argz_len + len));
    ch = tmp;

    for(i = 0; i <= ind -1; i++)
        *ch++ = (*argz)[i];
    for(i = 0; i <= len; i++)
        *ch++ = entry[i];
    for(i = ind; i <= *argz_len; i++)
        *ch++ = (*argz)[i];

    free(*argz);
    *argz = (char*) malloc (sizeof(char) * (*argz_len + len));
    *argz_len += len + 1;
    for(i = 0; i < *argz_len; i++) {
        (*argz)[i] = tmp[i];
    }

    free(tmp);
    return OK;
}

char * argz_next (char *argz, size_t argz_len, const char *entry)
{
    char *ch;
    int i;
    int len = strlen(entry);
    int ind = substr(argz, argz_len, entry);

    if (ind == -1)
        return argz;
    if(ind == argz_len - len)
        return 0;
    ch = argz;
    for(i = 0; i < ind + len + 1; i++)
        ch++;

    return ch;
}


error_t argz_replace(char **argz, size_t *argz_len, const char *str, const char *with)
{
    if(substr(*argz, *argz_len, str) == -1)
        return ENOMEM;
    argz_insert(argz, argz_len, str, with);
    argz_delete(argz, argz_len, str);

    return OK;
}


void argz_print(const char *argz, size_t argz_len)
{
    const char *ch;
    int i;

    ch = argz;
    for(i = 0; i < argz_len; i++) {
        if(*ch == '\0')
            printf("\n");
        else
            printf("%c",*ch);
        ch++;
    }
    printf("\n\n\n");
}
