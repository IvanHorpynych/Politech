/*
File: string_func.c
Author: Onoprienko M.I.
Group: KV-31, FPM
Written: 27.09.2014
*/

#include "header.h"
int substr(const char *string1, const char *string2) //search first input
{
	char *koko;
	koko = malloc(strlen(string2)*sizeof(char));
	koko = strstr(string1, string2);
	if (koko == NULL)
	{
		return 0;
	}
	else
		return koko - string1;
}

int subseq(const char *string1, const char *string2)// search overall the number of characters
{
	int max = 0, n = 0;
	if (strlen(string1) >= strlen(string2))
	{
		for (int i = 0; i < strlen(string1); i++)
		{
			for (int j = 0; j < strlen(string2); j++)
			{
				if (string1[i] == string2[j])
				{
					for (int k = i, p = j; k<strlen(string1) || p<strlen(string2); k++, p++)
					{
						if ((string1[k] == string2[p]))
						{
							n++;
						}
					}
				}
				else
				{
					max = fmax(n, max);
					n = 0;
				}
			}
		}
	}
	else
	{
		for (int i = 0; i <= strlen(string2); i++)
		{
			for (int j = 0; j <= strlen(string1); j++)
			{
				if (string2[i] == string1[j])
				{
					for (int k = i, p = j; k<strlen(string2) || p<strlen(string1); k++, p++)
					{
						if ((string2[k] == string1[p]))
						{
							n++;
						}
					}
				}
				else
				{
					max = fmax(n, max);
					n = 0;
				}
			}
		}
	}
	return  max;
}

char ispal(const char *string) // polindrom or not polindrom
{
	
	int n = strlen(string), f = 1;
	for (int i = 0; i<(n / 2); i++)
	{
		if (string[i] != string[n - i - 1])
		{
			f = 0;
		}

	}
	return f;
}

char* makepal(const char *string) //make any word polindrom
{
	char *lolo;
	int i, p = 0;
	if (strlen(string) == 0) return "Empty string";
	lolo = (char*)calloc(2 * strlen(string), sizeof(char));
	for (i = 0; i < strlen(string); i++)
	{
		lolo[i] = string[i];
	}
	while (ispal(lolo) != 1)
	{
		for (i = 0; i <= p; i++)
		{
			lolo[strlen(string) + i] = string[p - i];
		}
		p++;
	}
	return lolo;
}

double* txt2double(const char *str, int *size){ // make an array of strings 
	char *st;
	double *v = NULL;
	int len = strlen(str), i, num_len = 0, k = 0, ind_vect = 0;
	if (len == 0) {
		*size = 0;
		printf("Error: String is empty \n");
		return v;
	}
	for (i = 0; i<len; i++){
		if ((str[i]<'0' || str[i]>'9') && str[i] != ';' && str[i] != '.'){
			*size = -1;
			printf("Error: String have letter \n");
			return v;
		}
	}
	for (i = 0; i <= len; i++){
		if ((str[i] == ';') || (str[i] == '\0')) k++;
	}
	*size = k;
	v = (double*)malloc(*size * sizeof(double));

	for (i = 0; i <= len; i++){
		if (str[i] == ';' || str[i] == '\0'){
			st = (char*)calloc(num_len + 1, sizeof(char));
			k = 0;
			for (k = 0; k<num_len; k++) st[k] = str[i - num_len + k];
			v[ind_vect] = atof(st);
			ind_vect++;
			free(st);
			num_len = 0;
		}
		else num_len++;

	}
	return v;
}
