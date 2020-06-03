/***********************************************************************
*file: "lab11_Header.h"
*purpose: declarations for laba11.c functions, types, constants
*Author: Anastasiev D. V.
*written 18/09/2014
*last modified: 18/09/2014
***********************************************************************/



#ifndef _HEADER_H
#define _HEADER_H


#include <stdio.h>
#include <string.h>
#include <stdlib.h>

//Функция возвращает индекс подрядка в рядке, если индекс вернуть нельзя - возврает -1
int substr(const char *, const char *);

//Функция возвращает максимальную подпоследовательность двух строк 
int subseq(const char *, const char *);

//Функция проверки на палиндром
char ispal( const char *);

/*Функция возвращает палиндром созданый из началного рядка последством 
добавления минимального количества символов в конец начального рядка */
char* makepal(const char *);
//Функция возвращает указатель на массив представленый из чисел записаных в строке
double* txt2double(const char * , int *);


#endif