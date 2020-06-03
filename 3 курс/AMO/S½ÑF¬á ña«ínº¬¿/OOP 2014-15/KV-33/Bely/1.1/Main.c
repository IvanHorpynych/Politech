/************************************************************************
*file: Main.c
*synopsis: function definitions
*author: Turchaninov Gennady
*written: 04/10/2013
*last modified: 09/10/2013
************************************************************************/

#include <stdlib.h>
#include <string.h>
#include <malloc.h>

#pragma warning(disable:4996)

int substr(const char *string1, const char *string2)
{
	int index;
	char *b;
	b = strstr(string1, string2);
	if (b) index = b - string1;
	else index = -1;
	return index;
}

int subseq(const char *str1, const char *str2)
{
	int i;
	int max_i = 0, cur_i = 0;
	int str1_len = strlen(str1);
	int str2_len = strlen(str2);
	for (i = 0; i < str1_len; i++)
	{
		int j;
		for (j = 0; j < str2_len; j++)
		for (cur_i = 0; str1[i + cur_i] == str2[j] && str1[i + cur_i] && str2[j];)
		{
			cur_i++;
			j++;
		}
		if (max_i < cur_i)
			max_i = cur_i;
	}
	return max_i;
}

char ispal(const char *string)
{
	int index = 0;
	int str = strlen(string);
	for (index = 0; index<str / 2; index++)
	if (string[index] != string[strlen(string) - index - 1])
		return 0;
	return 1;
}

char* makepal(const char *string)
{
	int j;
	int i = 0;
	int str_len;
	char *temp_str;

	temp_str = (char *)malloc((strlen(string) + 1));
	strcpy(temp_str, string);
	str_len = strlen(temp_str);
	while (!ispal(temp_str) && i<str_len / 2)
	{
		temp_str = (char *)realloc(temp_str, str_len + 1);
		j = 0;
		while (j<i)
		{
			temp_str[str_len - j] = temp_str[str_len - j - 1];
			j++;
		}
		temp_str[str_len - i] = temp_str[i];
		temp_str[str_len + 1] = '\0';
		str_len = strlen(temp_str);
		i++;
	}
	return temp_str;

}

double* txt2double(const char *string, int *size)
{
	int k = 0;
	double * result;
	char * i;
	char *str;
	char ** vect;
	str = (char*)malloc(sizeof(string));
	strcpy(str, string);
	*size = 1;
	for (i = str; i[0] != '\0'; i++)
	if (*i == ';')
		*size = *size + 1;
	vect = (char**)malloc(*size * sizeof(char*));
	vect[k] = str;
	for (i = str + 1; i[0] != '\0'; i++)
	if (i[0] == ';')
	{
		*i = '\0';
		k++;
		vect[k] = i + 1;
	}
	result = (double*)malloc(*size * sizeof(double));
	for (k = 0; k<*size; k++)
		result[k] = atof(vect[k]);
	return result;
}
