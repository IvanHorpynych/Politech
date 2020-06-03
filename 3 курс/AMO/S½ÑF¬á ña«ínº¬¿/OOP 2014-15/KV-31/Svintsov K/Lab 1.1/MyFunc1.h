/*********************************************************
*Синопсис: Объявление функций(их прототипы)
*Автор:Kyrylo Svintsov KV-31
*Написанно:5.10.2014
*Обновленно:5.10.2014
*********************************************************/

#include <string.h>
#include <stdlib.h>

#ifndef MYFUNC1_H
#define MYFUNC1_H

int substr(const char, const char);
int subseq(const char , const char );
char ispal(const char);
char* makepal(char);
double *txt2double(const char, int);

#endif