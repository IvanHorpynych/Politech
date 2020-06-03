#include <stdio.h>
#include <math.h>
#include <string.h>
#include <stdlib.h>
#pragma warning(disable : 4996)


int substr(const char *string1, const char *string2)  // ����� ���������
{
	int num = 0, i, j = 0;
	char res = -1;

	for (i = 0; string2[i] != '\0'; i++){
		if (string1[j] == string2[i]){
			++j;
			if (string1[j] == '\0'){      // �������� �� ��������� ���������
				res = num + 1;            // ����������� ����������
				break;
			}
			if (string1[0] == string2[i])      // ����������� ���������� ��������� ���������
				num = i;
		}
		else{
			j = 0;
		}
	}

	return res;
}

int subseq(const char *str1, const char *str2)      // ����� ������ ���������� ����� ���������
{
	int len = 0, nlen = 0, N1 = strlen(str1), N2 = strlen(str2), m, k, i, j;

	// ����������� �� ���� ��������� �����
	for (i = 0; i < N1; i++)
	{
		for (j = 0; j < N2; j++)
		{
			nlen = 0;
			m = i;
			k = j;
			while ((str1[m] == str2[k]) && (m < N1) && (k < N2))      // ����� ����� ���������
			{
				++nlen;     // ������ ����� ������������������
				++m;
				++k;
			}
			// ������ ����� ������� �� �������������������
			if (nlen > len)
				len = nlen;
		}
	}

	return len;
}

char ispal(const char *string)  // �������� �� ���������� ������
{
	int st = -1, i;
	char res;
	// ����� ������ ���������
	for (i = 0; string[i] != '\0'; i++)
		++st;
	// �������� �� ���������
	for (i = 0; i <= (st / 2); i++){
		if (string[i] == string[st - i])
			res = 1;
		else{
			res = 0;
			break;
		}
	}

	return res;
}

char *makepal(const char *str)  // �������� �����������
{
	int i, j, N = strlen(str), min = N - 1, pos = N - 1;

	for (i = N - 2; i >= (N - 1) / 2; i--)    
	{
		j = 0;
		while (i + j < N && i - j > -1 && str[i - j] == str[i + j])          // �������� �� ��������� ����������� �������
			j++;
		if (2 * i - (N - 1) < min && i + j == N)
		{
			min = 2 * i - (N - 1);       // �������� ��� ����������
			pos = i;                     // ����������� �������
		}

		j = 0;
		while (i + 1 + j < N && i - j > -1 && str[i - j] == str[i + 1 + j])  // �������� �� ������ ����������� �������
			j++;
		if (2 * i - (N - 2) < min && i + 1 + j == N)
		{
			min = 2 * i - (N - 2);       // �������� ��� ����������
			pos = i;                     // ������� ������ �������� ����� ������ ����������� 
		}

	}

	// ��������� ��� ���������� ��� �������� �����������
	char *add = (char*)malloc(min + 1);       // �������� ����������� ������ ����������� ���������
	for (i = 0; i < min; i++)
		add[i] = str[min - 1 - i];
	add[i] = '\0';

	// ������ ����������
	char *res = (char*)malloc(N + min + 1);
	strcpy(res, str);                       // �������� �������� ������ � ���������
	strcat(res, add);                       // ��������� ����������� �������� � ������ ����������

	free(add);

	return res;
}

double *txt2double(char *string, int *size) // �������� ������������� �������
{
	int Len = strlen(string), num = 0, ind = 0, i, sz = 0;
	double *mas = NULL;
	char *strn;
	// �������� �� ������ ������
	if (Len == 0) {
		*size = 0;
		printf("Error.(empty string)\n");
		return mas;
	}
	else{            // �������� �� ������� ��������� � ������ ��� ������������� �������
		for (i = 0; i < Len; i++){
			if ((string[i] < '0' || string[i] > '9') && string[i] != ';' && string[i] != '.'){
				*size = 0;
				printf("Error.(letter in the string) \n");
				return mas;
			}
		}
		// ����� ������ ��� �������� �������
		for (i = 0; i <= Len; i++){
			if ((string[i] == ';') || (string[i] == '\0'))
				sz++;
		}
		*size = sz;
		// ��������� ������ ��� �������
		mas = (double*)malloc(*size * sizeof(double));

		for (i = 0; i <= Len; i++){
			if (string[i] == '\0' || string[i] == ';'){
				strn = (char*)calloc(num + 1, sizeof(char));
				int j = 0;
				for (j = 0; j < num; j++)
					strn[j] = string[i - num + j];      // ������ ����� � ���. ������
				mas[ind] = atof(strn);                  // ������� �� char � double � ������ ����� 
				ind++;
				num = 0;
				free(strn);        // ������������ ������
			}
			else
				num++;
		}
	}
	return (mas);
}
