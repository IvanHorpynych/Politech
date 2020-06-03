/*
File: string_l.c
Synopsis: declaration of functions described in MyString.h
Author: Kolesnyk V.S.
Group: KV-31, the faculty of applied math (FPM)
Created: 30.09.2014
*/

#include "string_l.h"
#include <math.h>

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

char ispal(const char *string){

	int currentLength = strlen(string) - 1;
	for (int i = 0; i <= (int)(currentLength / 2); i++){
		if (string[i] != string[currentLength])
			return 0;
		currentLength--;
	}
	return 1; 

}

int counter = 0;

char * makepal(const char * string)
{
	int i; int counter = 0;
	int max = strlen(string);
	char *string1 = "";

	if (ispal(string) == 1)
	{
		for (i = 0; (i < max); i++){
			string1[i] = string[i]; 
		}
		return string1;
	}	
	else{
		char *temp;
		temp = (char*)calloc(2 * max, sizeof(char));

		for (i = 0; (i < max); i++){
			temp[i] = string[i]; 
			temp[max] = '\0';  
		}
		i = 0;
		while (ispal(temp) == 0) 
		{
			for (i = 0; i <= counter; i++){ 
				temp[max + i] = string[counter - i];
			}
			counter++;
		}
		return temp;
	}

}



double* txt2double(const char *str, int *size)
{
	char sep[2] = ";";
	double *p, temp;
	int i = 0, size1 = 0, size2 = 0;

	char *istr;                    //переменная в кот будут заносится начальные адреса частей строки
	char *context = "";
	char istr1[200];
	double mas[3];
	int len;

	strcpy_s(istr1, strlen(str) + 1, str);
	len = strlen(istr1);

	for (i = 0; i < len; i++)
	{
		if (istr1[i] == ';')
		{
			size1++;
		}

	}

	size1++;


	p = (double *)malloc(size1*sizeof(double));
	istr = strtok_s(istr1, sep, &context);   //разбиваем


	temp = atof(istr);        //преобразовываем из чар в число дабл
	p[0] = temp;             //записываем в массив
	mas[0] = temp;

	i = 0;
	do
	{

		istr = strtok_s(NULL, sep, &context);
		if (istr != NULL)
		{
			temp = atof(istr);
			if (temp == 0.0)
			{
				size2 = -1;
			}

			i++;
			p[i] = temp;
		}

	} while (istr != NULL);


	for (i = 0; i < size1; i++)
	{
		printf("\t A[%i]=%6.2f \n", i, p[i]);
	}

	if (size2 != -1)
	{

		*size = size1;  //без * это адрес, а с * это значение
	}
	else
	{
		*size = 0;
	}
	return p;
	free(p);

}
