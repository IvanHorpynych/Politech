/*
File: Func_String.c
Synopsis: declaration of functions described in Head_String.h
Author: Prodan A.O.
Group: KV-31, the faculty of applied math (FPM)
Created: 28.09.2014
*/


#include "Head_String.h"
int substr(const char*string1, const char*string2){
	char *string = strstr(string1, string2);
	if (string == NULL)
	{
		return 0;
	}
	else
		return string - string1;
}
int subseq(const char*string1, const char*string2){
	unsigned	int i, j, k, m;
	int maxLength = 0, length = 0;
	if (strlen(string1) >= strlen(string2))
	{
		for (i = 0; i <= strlen(string1); i++)
		{
			for (j = 0; j <= strlen(string2); j++)
			{
				if (string1[i] == string2[j])
				{
					length = 0;
					for (k = i, m = j; ((string1[k] == string2[m]) && (string1[k] != '\0')); k++, m++)

						length++;
					if (length>maxLength)
						maxLength = length;
				}
			}
		}
	}
	else
	{
		for (i = 0; i <= strlen(string2); i++)
		{
			for (j = 0; j <= strlen(string1); j++)
			{
				if (string1[i] == string2[j])
				{
					length = 0;
					for (k = i, m = j; ((string1[k] == string2[m]) && (string2[m] != '\0')); k++, m++)

						length++;

					if (length>maxLength)
						maxLength = length;
				}
			}
		}
	}
	return maxLength;
}
char ispal(const char *string){
	int i;
	int flag = 0;

	if ((strlen(string)) % 2 == 0)
	{
		for (i = 0; (i <= ((strlen(string) / 2) - 1)); i++)
		{
			if (string[i] == string[strlen(string) - i - 1])
				flag = 1;
			else
			{
				flag = 0;
				break;
			}
		}
	}
	else
	{
		for (i = 0; (i <= ((strlen(string) / 2))); i++)
		{
			if (string[i] == string[strlen(string) - i - 1])
				flag = 1;
			else
			{
				flag = 0;
				break;
			}
		}

	}
	if (flag == 1)
		return 1;
	else return 0;
}





char * makepal(const char * string){
	int i; int counter = 0;

	if (ispal(string) == 1)
		return string;
	else{
		char *temp;
		int max = strlen(string);
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
			}
			else{
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

