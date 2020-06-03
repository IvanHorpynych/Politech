/*******************************************************************
*Lab 1
*File: lab1.c
*Description: This file describes functions which works with strings
*These functions are declared in the file "lab1.h"
*Author: Grek A.
*written 20/09/2014
*last modified: 26/09/2014
*******************************************************************/


#include "Lab1.h"

#define MAX(a, b) (a > b ? a : b)

int substr(const char *string1, const char *string2) {
	char *s = strstr(string1, string2); /*get pointer to the first occurrence of string2 in string1*/
	return s != NULL ? s - string1 : -1; /*return -1 if string1 does not contain string2, or index of string2 in string1*/
}

int subseq(const char *string1, const char *string2) {
	int i=0, j, counter, total = 0;		/*counters initialization*/
	
	while (string1[i] != '\0') { /*loop till the end of string1*/
		j = 0;
		while (string2[j] != '\0') { /*loop till the end of string2*/
			counter = 0;
			int tempi = i, tempj = j;
			/*loop until the end of string or different chars*/
			while (string1[tempi] != '\0' && string2[tempj] != '\0' && string1[tempi] == string2[tempj]) { 
				/*increase counters if similar character was found*/
				counter++;
				tempi++;
				tempj++;
			}
			j++;
			total = MAX(total, counter); /*update total counter if longer sequence was found*/
		}
		i++;
	}
	return total;
}

char ispal(const char *string) {
	int i = 0, j = strlen(string) - 1;	/*counters initialization*/
	while (i < j) {
		if (string[i++] != string[j--]) { /*check a character at i and j positions and move counters to center of the word*/
			return 0; /*if characters are different - it is not palindrom*/
		}
	}
	return 1; /*if different characters were not found - it is palindrom*/
}

char* makepal(char *string) {
	int inputlen = strlen(string); /*length of input string*/
	int slen = inputlen + 1;	   /*length of new string, including final zero character*/
	int i, j;

	char *word = (char *)calloc(1, slen * sizeof(char)); /*create new string*/
	if (!word){							/*if memory cannot be allocated*/
		return string;					/*return input string*/
	}

	for (i = 0; i < slen; i++) /*copy input string to new string*/
		word[i] = string[i];

	for (i = 0; !ispal(word); i++) {
		word = (char *)realloc(word, (++slen)*sizeof(char)); /*expand string and allocate memory for it allocate memory*/
		if (!word) return string;

		if (i == 0) {
			word[slen - 2] = string[0]; /*copy first character to the end of new string*/
			word[slen - 1] = 0;			/*and add final zero*/
		}
		else {
			for (j = slen - 3; j >= inputlen; j--) /*shift right all added characters*/
				word[j + 1] = word[j];
			word[slen - 1] = 0;			/*add zero character to the end of string*/
			word[inputlen] = string[i]; /*copy new character from original string*/
		}
	}
	return word;
}

double *txt2double(const char *string, int *size) {
	*size = 0;
	int i, k, count = 0;
	double *arrptr = NULL;	/*output array*/
	char *num;				/*string to collect digits*/

	num = (char *)calloc(1, sizeof(char)); 
	if (!num) return NULL; /*return NULL if memory cannot be allocated, and *size == 0 */

	k = 0;
	for (i = 0; string[i] != 0; i++) { /*loop till the end of string*/
		if (string[i] == ';') { /*add value to array*/
			num[k] = 0;
			arrptr = (double *)realloc(arrptr, (count + 1)*sizeof(double));
			if (!arrptr) {
				free(num);
				return NULL; /*return NULL if memory cannot be allocated, and *size == 0 */
			}
			arrptr[count] = atof(num);
			if (arrptr[count] == 0) {
				free(num);
				free(arrptr);
				return NULL; /*return NULL if string cannot be converted, and *size == 0 */
			}
			count++;
			k = 0;
		}
		else { /*collect digits in num string*/
			num = (char *)realloc(num, (k + 2)*sizeof(char));
			if (!num) {
				free(arrptr);
				return NULL; /*return NULL if memory cannot be allocated, and *size == 0 */
			}
			num[k] = string[i];
			k++;
		}
	}

	/*add last value to array*/
	num[k] = 0;
	arrptr = (double *)realloc(arrptr, (count + 1)*sizeof(double));
	if (!arrptr) return NULL;
	arrptr[count] = atof(num);
	if (arrptr[count] == 0) {
		free(num);
		free(arrptr);
		return NULL; /*return NULL if string cannot be converted, and *size == 0 */
	}

	*size = ++count;
	
	return arrptr;
}