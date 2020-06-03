/******************************************************************
*Laboratory work 1
*File: MyHeadFile.h
*Description:Header file which includes different library
*Author: Artem Kryvonis
*written: 08/09/2014
*last modified 13/09/2014
******************************************************************/
#ifndef _OOP_LAB1_H
#define _OOP_LAB1_H

#include <stdio.h>
#include <stdlib.h>

int substr(char*, char*); //Функція що повертає індекс елемента в рядку string1, з якого починається підрядок, рівний string2. 
int subseq(char*, char*); //Функція що повертає найбільшу довжину  спільної підпослідовності символів рядків string1 й string2
char ispal(char*); //Функція що повертає 1, якщо string є паліндромом й 0 – у противному випадку
char* makepal(char*); //Функція що одержує як параметр вказівник на рядок символів, перетворює його на паліндром додаючи до нього найменше число символів у кінець рядка й повертає покажчик на рядок з паліндромом
double* txt2double(char*, int*); //Функція що розміщає числа в динамічному масиві й повертає покажчик на нього

#endif;
