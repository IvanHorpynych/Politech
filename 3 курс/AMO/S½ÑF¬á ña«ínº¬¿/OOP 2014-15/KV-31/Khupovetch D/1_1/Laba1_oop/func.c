/*
File: string_func.c
Author: Khupovetch D.Y.
Group: KV-31, FPM
Written: 01.10.2014
*/

#include "header.h"
int substr(const char *string1, const char *string2)
{
	char *istr;
	istr = malloc(strlen(string2)*sizeof(char));
	istr = strstr(string1, string2);
	if (istr == NULL)
	{
		return 0;
	}
	else
		return istr - string1;
}

int subseq(const char *string1, const char *string2)
{
	int max = 0, n = 0;
	if (strlen(string1) >= strlen(string2))
	{
		for (int i = 0; i <= strlen(string1); i++)
		{
			for (int j = 0; j <= strlen(string2); j++)
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

char ispal(const char *string)
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

char* makepal(const char *string)
{
	char *res;
	int i, p = 0;
	if (strlen(string) == 0) return "Empty string";
	res = (char*)calloc(2 * strlen(string), sizeof(char));
	for (i = 0; i < strlen(string); i++)
	{
		res[i] = string[i];
	}
	while (ispal(res) != 1)
	{
		for (i = 0; i <= p; i++)
		{
			res[strlen(string) + i] = string[p - i];
		}
		p++;
	}
	return res;
}


double* txt2double(const char *string, int *size)
{
	char st[100] = "";
	double *mas;
	int boo = 1, j = 0, count = 0;
	while (j < strlen(string) && boo)
	{
		if ((string[j] != 48) && (string[j] != 49) && (string[j] != 50) && (string[j] != 51) && (string[j] != 52) && (string[j] != 53) && (string[j] != 54) && (string[j] != 55) && (string[j] != 56) && (string[j] != 57) && (string[j] != 46) && (string[j] != 59))
		{
			boo = 0;
		}
		j++;
	}
	if (boo == 0)
	{
		printf("String have a character\n");
		size = 0;
		return 0;
	}
	else
	{
		int k = 0, flag = 1;
		for (int i = 0; i < strlen(string); i++)
		{
			if (string[i] == ';') 
			{
				count++;
			}
		}
		count++;
		mas = (double*)malloc(count * sizeof(double));
		for (int i = 0; i < strlen(string); i++)
		{
			if (string[i] != ';')
			{
				st[strlen(st)] = string[i];
			}
			else
			{
				for (int j = 0; j < strlen(st); j++)
				{
					/*if ((st[j] < 48) && (st[j] > 57) && (st[j] != 46))
					{
						flag = 0;
					}*/
				}
				if (flag)
				{
					mas[k] = atof(st);
					k++;
				}
				for (int j = 0; j < 10; j++)
				{
					st[j] = 0;
				}
				flag = 1;
			}
		}
		mas[k] = atof(st);
	}
	*size = count;
	return mas;
}

