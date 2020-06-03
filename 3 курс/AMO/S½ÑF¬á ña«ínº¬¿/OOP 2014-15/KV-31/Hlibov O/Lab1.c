/************************************************************************
>file: Lab1.c
>author: Hlibov A.R.
>group: KV-31, FPM
>written: 10/09/2014
>last modified: 15/09/2014
************************************************************************/

#include "Lab1.h"

int substr(const char *string1, const char *string2){
	char *i;
	int a;
	i = strstr(string1, string2);
	if (i != NULL){
		a = i - string1;
		
	}
	else
		printf("String1 doesn't contain string2");

	return a+1;
}

int subseq(const char *string1, const char *string2){
	int i, j, n, m, max = 0, k, i1, j1;
	n = strlen(string1);
	m = strlen(string2);
	for (i = 0; i < n; i++){
		for (j = 0; j < m; j++){
			k = 0;
			if (string1[i] == string2[j]){
				i1 = i;
				j1 = j;
				while ((string1[i1] == string2[j1]) && (i1 < n) && (j1 < m)){
					++i1;
					++j1;
					++k;
				}
			}
			if (max < k)
				max = k;
		}
	}
	return max;
}

char ispal(const char *string){            
	int i, k = -1, n = strlen(string) - 1;
	i = n / 2 ;
	for (int j = 0; (j <= i) && (string[j] == string[n - j]); j++) k++;

	if (k == i)
		return('1');
	else
		return('0');
}

char* makepal(const char* string){
	size_t n = strlen(string);
	char *s = (char*)malloc(n * 2 );
	if (s == NULL) exit(1);
	s = string;
	int i = 1, count = n, k = 2;

	while (ispal(s) == '0'){
		for (i = k ; i > 0; i--){
			s[n + i] = s[n + i - 1];
			s[n + i - 1] = s[n - count];
		}
		count--;
		k++;
	}
	
	return s;
}

double* txt2double(const char *num, int *size){
	int k = 0, d, count = 0;
	char ar[10];
	memset(ar, 0, 10);
	double *mas = malloc(sizeof(double)* (strlen(num) / 2));
	int size_of_str = strlen(num);

	for (int j = 0; j < size_of_str; j++){
		if (num[k] != ';'){
			d = 0;
			
			while ((num[k] != ';') && (k < size_of_str)){
				ar[d] = num[k];
				k++;
				d++;
			}
			*size += 1;
			mas[count] = atof(ar);
			count++;
			memset(ar, 0, 10);
		}   
		else k++;
		if (k >= size_of_str) break;
	}  
	return mas;
}