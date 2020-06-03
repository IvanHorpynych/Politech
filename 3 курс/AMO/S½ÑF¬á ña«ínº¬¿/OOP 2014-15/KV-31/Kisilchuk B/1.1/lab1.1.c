/************************************************************************
*file: function.c
*synopsis:
* function that may grow a string expects that string to have been allocated using malloc
* thosefunctions that only examine their arguments or modify them in place will work
*All argz functions that do memory allocation have a return type
* These functions are declared in the include file "lab1.1.h".
*related files: none
*written: 12/11/2014
*last modified: 18/11/2014
*************************************************************************/

#include <stdio.h>
#include <stdlib.h>
#include "lab1.1.h"

//The substr function finds the element in the string S1 at which begins entering in it substring S2
int substr(const char *string1, const char *string2){
	int i = 0, j = 0, ind = 0, k = 0;
	while (*string1){
		if (string1[i++] == string2[j++] ){
			if (k == 0)
				ind = i;
			k = 1;
		}
		else{
			k = 0;
			j = 0;
		}
		if (string2[j] == '\0')
			return ind;
		
	}
	return ind;
}

int subseq(const char *string1, const char *string2){
	int i = 0, j = 0, k = 0, max = 0, r = 0, l = 0;
	
	while (string1[i] != '\0'){
		l = i;
		while (string2[j] != '\0'){
			if (string1[i] == string2[j] != '\0'){
				k++;
				i++;
				j++;
				r = 1;
			}
			if ((string1[i] != string2[j]) || (string2[j]== '\0')){
				if (k > max){
					max = k;
				}
				if (r == 0){
					j++;
				}
				k = 0;
				r = 0;
				i = l;
			}
		}
		j = 0;
		i++;
	}
	return max;
}

char ispal(const char *string){
	int i, j;
	for (i = 0, j = (strlen(string) - 1); i < (strlen(string) / 2);) {
		if (string[i++] != string[j--])
			return 0;
	}
	return 1;
}

char* makepal(const char *string){
	if (string == NULL)
		return "NULL";
	if (ispal(string) == 1)
		return string;

	int i = 0,j = (strlen(string)-1),k = 0;
	while(i<j){
		if (string[i] == string[j]){
			k = i;
			
		}
		i++;
	}

	if (k==0)
		k = j;
	
	k--;

	char* plus = (char*)malloc(k+2);
	char* result = (char*)malloc(strlen(string)+k+1);

	for (i = 0; i <= k;i++){
		plus[i] = string[k-i];
	}
	
	plus[i] = '\0';
	

	strcpy(result, string);
	strcat(result, plus);

	return result;
}

/*The txt2double function transfers the number recorded in the line C1 
in numeric type and stores them in a dynamic array. 
If the transfer is impossible the function returns an error.*/

double* txt2double(const char *string, int *size){
	double *a = (double*)malloc((strlen(string) / 2)*sizeof(double));
	char *g = (char*)malloc(strlen(string) / 2);
	int i = 0, k = 0, j = 0;

	while (i<strlen(string)){
		while (string[i] == ';')
			i++;
		while ((string[i] != ';') && (i<strlen(string))){
			g[k] = string[i];
			k++;
			i++;
		}
		g[k] = '\0';

		a[j] = atof(g);

		if ((a[j] == 0.0) && (g[0] != '0')){
			*size = 0;
			free(a);
			return a;
		}
		j++;
		k = 0;
	}
	*size = j;

	return a;
}

	