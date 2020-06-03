#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <malloc.h>
/* SUBSTR возвращает индекс элемента в строке string1, с которого начинается подстрока, равная string2 */
int substr (const char *, const char *);
/* SUBSEQ возвращает наибольшую длину  общей подпоследовательности символов строк */
int subseq (const char *, const char *);
/* ISPAL возвращает 1, если string является палиндромом и 0 – в противном случае */
char ispal (const char *);
/* MAKEPAL преобразует строку в палиндром добавляя к ней наименьшее число символов  */
char* makepal (const char *);
/* TXT2DOUBLE размещает эти числа в динамическом массиве и возвращает указатель на него */
double* txt2double (const char *, int *);