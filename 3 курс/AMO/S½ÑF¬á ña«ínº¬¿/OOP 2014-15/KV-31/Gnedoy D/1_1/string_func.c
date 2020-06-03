/***********************************************************************
*file: string_func.c
*author: Gnedoj D. O.
*group: KV-31, FPM
*written: 10/09/2014
*last modified: 10/09/2014
************************************************************************/

#include "string_func.h"

int substr(const char *string1, const char *string2)
{
	int i, j;
	int count = -1;
	
	for (i = 0; i <= strlen(string1) - strlen(string2); i++)
	{
		for (j = 0; j < strlen(string2); j++)
		{
			if (string1[i+j] == string2[j]) continue;
			else break;
		}
		if (j == strlen(string2))
		{
			count = i+1;
			break;
		}
	}
	if (count > -1) return count;
	else
		return -1;
}

int subseq(const char *string1, const char *string2)
{
	int i, j, c, k;
	int count = 0, ControleCount = 0;
	
	for ( i = 0; i < strlen(string1); i++)
	{
		for ( j = 0; j < strlen(string2); j++)
		{
			if (string1[i] == string2[j])
				{
					count = 0;
					for (c = i, k = j; (string1[c] == string2[k]) && ((string1[c] != '\0') || (string1[k] != '\0')); c++, k++)
						count++;
				}	
			if (count > ControleCount) ControleCount = count;
		}
	}
	if (ControleCount != 0) return ControleCount;
	else 
		return 0;
}

char ispal(const char *string)
{
	int n = strlen(string), i;
	for (i = 0; i < (n / 2); i++)
	{
		if (string[i] == string[n - i - 1]) continue;
		else break;
	}
	if (i >= (n / 2))  return 1;
	else 
		return 0;
}

char* makepal(const char *str)
{
	int n = strlen(str), index = 0;
	if (str[0] == '\0') {
		char *Fail = "String is empty!";
		return Fail;
	}
	else{
		int strcentre = n/2, longinp = 0;
	
		if (ispal(str)) return str;
		else{
			for (int i = strcentre; i < n; i++) 
				{
				if (str[i - 1] == str[i + 1])
					{
					index = 0; // index from start add
					for (int j = 1; str[i - j] == str[i + j]; j++)
						{
							++index;
						}
						longinp = n - 1 - 2*index;
					} 
				else if ((str[i + 1] == '\0') && ((longinp + i + 1) <= n)) longinp = n-1;
		
				}

			char *ress;

			if (NULL == (ress = (char*) malloc(n + longinp + 1 ))) 
			{
				printf("Error\n");
				return 0;
			}
			else
			{
				char *add;
				strcpy(ress, str);
				if (NULL == (add = (char*) malloc(longinp + 1 ))) 
				{
					printf("Error\n");
					return 0;
				}
				else
				{
					for (int i = 1; i <= longinp; i++)
					{
						add[i-1] = str[longinp - i];
					}
					add[longinp] = '\0';
					strcat(ress, add);
					free(add);
					return ress;
				}
			}
		}
	}
	
}

double* txt2double( const char *str , int *size){
	int sizestr = strlen(str);
	int i = 0;

	for (i = 0; i < sizestr; i++)
	{
		if ((str[i] == '.') || (str[i] == ';') || ((str[i] >= '0') && (str[i] <= '9'))) {
			if (str[i] == ';') *size += 1;
			continue;
		}
		else break;
	}
	if (i < sizestr) 
		{
			*size = 0;
			return 0;
		}
	else 
		{
			*size += 1;
			double *mass;
			if (NULL == (mass = (double*) malloc(sizeof(double) * *size + 1))) 
			{
				printf("Error\n");
				return 0;
			}
			else
			{
				int sizeadd = 0;
				int j = 0;
				for (int i = 0; i < *size; i++)
				{
					char *add;
					add = (char*)malloc(sizestr + 1);
					for (j = 0; (str[sizeadd + j] != '\0') && (str[sizeadd + j] != ';'); j++)
					{
						add[j] = str[sizeadd + j];
					}
					add[j + 1] = '\0';
					mass[i] = atof(add);
					//free(add);
					sizeadd += j + 1;
				}
				return mass;
			}
		}
}