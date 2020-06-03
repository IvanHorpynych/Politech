/****************************************************
*file: OOP_lab1.h									*
*synopsis: declarations for OOP_lab1 functions		*
*author: Chernysh Andrey							*
*written: 15/09/2014								*
*last modified: 01/10/2014							*
****************************************************/

#ifndef _OOP_LAB1_H_
#define _OOP_LAB1_H_

int substr(const char *string1, const char *string2);
int subseq(const char *string1, const char *string2);
char ispal(const char *string);
char *makepal(const char *string);
double *txt2double(char *string, int *size);

#endif