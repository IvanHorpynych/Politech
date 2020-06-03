/*
File: string_func.c
Korshun.A.S
*/

#include "string_func.h"
int substr(const char *string1, const char *string2)
{
	char *istr;
	istr = strstr(string1, string2); //ищет первое вхождение Подстроки в строку
	if (istr == NULL)
	{
		return -1;
	}
	else
		return istr - string1;

}

int subseq(const char *string1, const char *string2)
{
	unsigned int i, j, c, k;
	if (strlen(string1) >= strlen(string2)) {
		int length = 0, flag = 0; //length подщет длины общей подстроки, flag проверка есть ли такиие подстроки
		for (i = 0; i < strlen(string1); i++)
		{
			for (j = 0; j < strlen(string2); j++)
			{
				if (string1[i] == string2[j])
				{
					length = 0;
					for (c = i, k = j; (string1[c] == string2[k]) && ((string1[c] != '\0') || (string2[k] != '\0')); c++, k++)
						length++;
				}
				if (length > flag) flag = length;

			}
		}
		if (flag != 0) return flag;
		else
			return 0;
	}
	else {
		int length = 0, flag = 0;
		for (i = 0; i < strlen(string2); i++)
		{
			for (j = 0; j < strlen(string1); j++)
			{
				if (string2[i] == string1[j])
				{
					length = 0;
					for (c = i, k = j; (string2[c] == string1[k]) && ((string2[c] != '\0') || (string1[k] != '\0')); c++, k++)
						length++;
				}
				if (length > flag) flag = length;

			}
		}
		if (flag != 0) return flag;
		else
			return 0;

	}
}


char ispal(const char *string)
{
	int i, flag = 0, len;
	len = strlen(string);
	for (i = 0; i < len / 2; i++)
	if (string[i] != string[len - i - 1]) {
		flag = 1;
		break;
	}
	if (flag == 0) return 1; else return 0;
}

char* makepal(const char *string){
	char *res_str;
	int len = strlen(string);
	int i, k = 0;

	if (len == 0) return "Empty string";

	res_str = (char*)calloc(2 * len, sizeof(char));
	for (i = 0; i<len; i++){
		res_str[i] = string[i];
	}
	while (ispal(res_str) != 1)
	{
		for (i = 0; i <= k; i++){
			res_str[len + i] = string[k - i];
		}
		k++;
	}

	return res_str;
}



double* txt2double(const char *string, int *size)
{
	int flag = 0, i = 0, count = 0, j = 0, k = 0, dotcount = 0;
	double *value;
	char *dstring;
	dstring = malloc(strlen(string)*sizeof(char));
	/* проверка на наличие етих символов, '.' and ';' */
	for (i = 0; i < strlen(string); i++)
	if ((string[i] >= '0') && (string[i] <= '9') ||
		((string[i] == '.') && (string[i + 1] != '.') && (string[i] != '\0')) ||
		((string[i] == ';') && (string[i + 1] != ';') && (string[i] != '\0'))) flag = 0;
	else
	{
		flag = 1;
		break;
	}
	if (flag == 1) {
		size = 0;  printf("Wrong string\n"); return 0;
	}
	else {
		/* здесь подщитуем кол. чисел и проверяем на кол. точок в одной строке*/
		for (i = 0; i < strlen(string); i++)
		{
			if (string[i] == ';') {
				count++;
				dotcount = 0;
			}
			if (string[i] == '.') dotcount++;
		}
		if (dotcount > 1) {
			printf("Wrong string\n"); return 0;
		}
		else {
			value = malloc((count + 1)*sizeof(double));
			/* Здесь пререпишем в string по одному перед тем как переписать числа в double  */
			for (i = 0; i <= strlen(string); i++)
			if (string[i] != ';' && string[i] != '\0') {
				dstring[j] = string[i];
				dstring[j + 1] = '\0';
				j++;
			}
			else {
				value[k] = atof(dstring);
				j = 0;
				printf("%f\n", value[k]);
				k++;
			}
			size = k;
			return value;
		}
		free(value); free(dstring);
	}
}
