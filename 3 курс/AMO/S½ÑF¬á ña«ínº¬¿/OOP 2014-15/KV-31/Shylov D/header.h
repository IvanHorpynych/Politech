/****************************************************
*file: OOP_lab1.h									*
*synopsis: declarations for OOP_lab1 functions		*
*author:Shilov D.V.															*
*written: 29/10/2014																*
*last modified: 01/11/2014															*

****************************************************/

#ifndef header
#define header

int substr(const char *string1, const char *string2);
int subseq(const char *string1, const char *string2);
char ispal(const char *string);
char *makepal(const char *string);
double *txt2double(char *string, int *size);

#endif