/************************************************************************
*file: mystring.h
*author: Ivanov Alexander
*written: 07/10/2014
*last modified: 22/10/2014
************************************************************************/
#ifndef MYSTR_H
#define MYSTR_H

#include<stdio.h>
#include<math.h>
#include<conio.h> 
#include<windows.h>



 int substr(const char *string1, const char *string2);
 int subseq(const char *string1, const char *string2);
 char ispal(const char *string);
 char *makepal(const char *s1);
 double* txt2double(const char *s1, int *size);

#endif