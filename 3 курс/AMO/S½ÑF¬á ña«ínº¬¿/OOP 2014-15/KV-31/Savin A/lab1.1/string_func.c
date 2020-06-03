/*
File: string_func.c
Purpose: declarations of functions used in main program.
Author: Savin A.D.
Group: KV-31, FPM
Written: 03.09.2014
*/

#include "string_func.h"
int substr(const char *string1, const char *string2)
{
	char *istr;
	/* istr = malloc(strlen(string2)*sizeof(char)); */
	istr = strstr(string1, string2); //Here we check if string2 is really substring of string1
	if (istr == NULL)
	{
		return -1;
	}
	else
		return istr - string1;
	/*free(istr);*/
	/*
	istr acquires value of the pointer which points to the first inclusion of string2 in string1.
	*/
}

int subseq(const char *string1, const char *string2)
{
	unsigned int i, j, c, k;
	/* Depending on which string is longer we start cycles differently */
	if (strlen(string1) >= strlen(string2)) {
		int length = 0, flag = 0; //length is for counting the length of the longest common substr, flag is for checking if there is such substrings
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


char* makepal(const char *string)
{
	/* Cheking if string is already a palindrome */
	if (ispal(string) == 1) return string; else
	{
		/*
		Here we check from which symbol string has equal symbols on right and left.
		For example: "Counu" has equal symbols from letter u, "unu" part is already a palindrome,
		so we only have to add "oC" to the end
		*/
		int i = 0, j = 0, k = 0, flag = 0, len = strlen(string);
		for (i = 1; i < len / 2; i++) {
			for (j = 0; j < len / 2 - i; j++)
			if (string[((len - 1) / 2) + i - (j + 1)] == string[((len - 1) / 2) + i + (j + 1)]) {
				flag = 0; continue;
			}
			else
			{
				flag = 1;
				break;
			}
			if (flag == 0) break;
		}
		char *helpstr = malloc((len + ((len - 1) / 2 + i - j))*sizeof(char));
		for (k = 0; k < len; k++)
			helpstr[k] = string[k];
		for (k = 0; k < ((len - 1) / 2 + i - j); k++)
			helpstr[len + k] = string[(len - 1) / 2 + i - j - k - 1];
		helpstr[(len + (len - 1) / 2 + i - j)] = '\0';
		return helpstr;
	}
}


double* txt2double(const char *string, int *size)
{
	int flag = 0, i = 0, count = 0, j = 0, k = 0, dotcount = 0;
	double *value;
	char *dstring;
	dstring = malloc(strlen(string)*sizeof(char));
	/* Cheking if string contains only numbers, '.' and ';' */
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
		/* Here we count the quantity of numbers and if there are more than one dot in one number */
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
				/* Here we rewrite each number one by one to the other string and than transform it into a double number */
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
