/************************************************************************
*file: string.h
*synopsis: declarations for string functions 
*author: volontyr alexandr
*written: 03/09/2014
*last modified: 06/09/2014
************************************************************************/

#ifndef _STRING_H
#define _STRING_H

/* function prototypes */
char* put_str();
int substr(const char *, const char *);
int subseq(const char *, const char *);
char ispal(const char *);
char* makepal(char **);
double* txt2double(const char *, int **);

#endif