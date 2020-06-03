/*
File: MyString.c
Synopsis: declaration of functions described in MyString.h
Author: Chukhlib Y.V.
Group: KV-31, the faculty of applied math (FPM)
Created: 13.09.2014
*/

#include "MyString.h"
#define _CRT_SECURE_NO_WARNINGS


int substr(const char * string1, const char * string2)
{

	char * checkResult = strstr(string1, string2);
	if (checkResult == NULL)
	{
		return -1;
	}
	else
		return checkResult - string1 + 1;

}

int subseq(const char * string1, const char * string2)
{


	int m;
	int n;
	m = strlen(string1);
	n = strlen(string2);
	int result = 0;

	if (strlen(string1) < strlen(string2)){
		m = strlen(string2);
		n = strlen(string1);
	}

	int solutionArray[100][100];

	for (int i = 0; i <= m; i++){
		for (int j = 0; j <= n; j++){
			solutionArray[i][j] = 0;
		}
	}

	for (int i = 0; i <= m; i++){
		for (int j = 0; j <= n; j++){
			if (i == 0 || j == 0)
				solutionArray[i][j] = 0;
			else if (string1[i - 1] == string2[j - 1]){
				solutionArray[i][j] = solutionArray[i - 1][j - 1] + 1;
				result = max(result, solutionArray[i][j]);
			}
			else
				solutionArray[i][j] = 0;
		}
	}
	return result;
}


char ispal(const char * string){

	int currentLength = strlen(string)-1;
	for (int i = 0; i <= (int)(currentLength / 2) +1; i++){
		if (string[i] != string[currentLength])
			return 0;
		currentLength--;
	}
	return 1;
}

int counter = 0;


char* makepal(const char *str){
	char *res_str;
	int len = strlen(str);
	int i, k = 0;

	if (len == 0) return "Empty string";

	res_str = (char*)calloc(2 * len, sizeof(char)); 
	for (i = 0; i<len; i++){
		res_str[i] = str[i];
	}
	while (ispal(res_str) != 1){
		for (i = 0; i <= k; i++){
			res_str[len + i] = str[k - i];
		}
		k++;
	}

	return res_str;
}


double * txt2double(char * string, int * size)
{
	int flag = 0, i = 0, count = 0, j = 0, k = 0, dotcount = 0;
	double * value;
	char * dstring;
	dstring = malloc(strlen(string) * sizeof(char));
	for (i = 0; i < strlen(string); i++)
		if ((string[i] >= '0') && (string[i] <= '9') ||
			((string[i] == '.') && (string[i + 1] != '.') && (string[i] != '\0')) ||
			((string[i] == ';') && (string[i + 1] != ';') && (string[i] != '\0'))) flag = 0;
		else{
			flag = 1;
			break;
		}
	if (flag == 1){
		size = 0;  printf("Wrong string\n"); 
		return 0;
	}
	else {
		for (i = 0; i < strlen(string); i++){
			if (string[i] == ';'){
				count++;
				if (dotcount > 1){
					printf("Wrong string\n");
					return 0;
				}
				dotcount = 0;
			}
			if (string[i] == '.') 
				dotcount++;
		}
		if (dotcount > 1){
			printf("Wrong string\n"); 
			return 0;
		}
		else{
			value = malloc((count + 1) * sizeof(double));
			for (i = 0; i <= strlen(string); i++)
				if (string[i] != ';' && string[i] != '\0'){
				    dstring[j] = string[i];
				    dstring[j + 1] = '\0';
				    j++;
				} else{
					value[k] = atof(dstring);
					j = 0;
					if ((int)((int)(value[k] * 10) % 10) == 0)
					    printf("%.0f\n", value[k]);
					else
						printf("%.1f\n", value[k]);
					k++;
				}
				size = k;
				return value;
		}
	}
}


/*
double * txt2double(char *str1, int *size)
{
	char ch, st[100] = "";
	//gets(str1);
	int  count = 0, k = 0, flag = 1, p = 0;
	double *mas;
	for (int i = 0; i<strlen(str1); i++)
	{
		if (str1[i] == ';')
		{
			count++;
		}
	}
	count++;
	mas = (double*)malloc(count*sizeof(double));
	for (int i = 0; i < strlen(str1); i++){
		if (str1[i] != ';')
			st[strlen(st)] = str1[i];
		else{
			puts(st);
			for (int j = 0; j<strlen(st); j++){
				if (((st[j] < 47) || (st[j] > 58)) && (st[j] != '.'))
					flag = 0;
			}
			if (flag == 1){
				mas[k] = atof(st);
				
				k++;
			}

			for (int j = 0; j<10; j++){
				st[j] = 0;
			}
			flag = 1;
			//printf("%f", mas[k]);
		}
	}
	k++;
	mas[k] = atof(st);
	//printf("%f.1", mas[k]);

	//count++;
	size = count;
	return size;
	for (int i = 0; i <= k+1; i++)
		printf(" %f ", mas[i]);
	printf("%f.1", atof(st));
}*/














