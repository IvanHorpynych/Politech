/******************************************************************
*Laboratory work 1
*File: oop_func.c
*Description: This file describes functions, which do some actions with strings
*These functions are declared in the include file "oop_first_lab.h".
*Author: Artem Kryvonis
*written: 08/09/2014
*last modified 13/09/2014 256 day =) 
******************************************************************/

#include "MyHeadFile.h"
#include <stdbool.h>

/*������� �� ������� ������ �������� � ����� string1, � ����� ���������� �������, ����� string2. */

int substr(const char *s1, const char *s2){
	bool flag = false;							//��������� ��� ������������ �������� �������
	int tmp = -1;								//��� ��������������� ������� �������
	int len1 = strlen(s1);
	int len2 = strlen(s2);
	if (len1 < len2 || len1 == 0 || len2 == 0){			//�������� �� ������� � �� ������� ������� �� ��� �����
		return -1;
	}
	/*������ ����� ��������� flag = true , � ��� �������� �� ������� ���������� �� ���������� �����,
	���� �������� ������ �� flag = false ����� ����� �� ������� ����� ������� ��������� � ��������, �� ���� ������� �� �������
	*/
	for (int i = 0; i < len1; i++){
		if (s1[i] == s2[0]){
			for (int j = 0; j < len2; j++){
				flag = true;
				tmp = i;
				if (s1[i + j] != s2[j]){
					flag = false;
					break;
				}

			}

		}
	}
	if (flag)
		return tmp;
	else{
		return -1;
	}
}
/*������� �� ������� �������� �������  ������ ������������� ������� ����� string1 � string2*/
int subseq(const char *s1, const char *s2){
	int i, j, k;
	int	result;									//��� ��������� ������� �������� 
	int minlen;									//�������� ������� ����� 
	int sumlen;									//���� ������
	int max = 0;
	int len1 = strlen(s1), len2 = strlen(s2);
	if (len1 == 0 || len2 == 0) return 0;			//�������� �� �������

	sumlen = len1 + len2;
	/*���������� �������� ������� ��� ���������� ���������*/
	
	/*�������� � ������� ���� �� ����� � ����� �������� �������,
	  ������ ����� ����� � ���������� ����� � ������ ��������� � ������� �����
	  ���� ������ �������� �������� ��� �������������� ����� � �� ������� � ������� �����
	  ��������� ����� � ��� ���������� ��������� ��������.
	  ��������� �� �� ����������� � ��������� "result>ma�".
	  ϳ��� ����� ��������� ������ ������� ����������� �����������.
	  */
	if (len1 >= len2){
		minlen = len2;
		for (i = 0; i < len1; i++){
			k = 0;
			result = 0;

			for (j = 0; j<len2; j++){
				if (s1[i + k] == s2[j]){
					result++;
					k++;
				}
				else k = 0;
			}
			if (result>max) max = result;

		}
	}
	else{
		for (i = 0; i < len2; i++){
			k = 0;
			result = 0;

			for (j = 0; j<len1; j++){
				if (s2[i + k] == s1[j]){
					result++;
					k++;
				}
				else k = 0;
			}
			if (result>max) max = result;

		}
	}
	return max;
}
/*������� �� ������� 1, ���� string � ���������� � 0 � � ���������� �������*/
char ispal(const char *s){
	// ���� �-��� ������ ��������� � len-i-1 �� ��� ����� �� ��������� ����������
	int len = strlen(s);
	for (int i = 0; i < len / 2; i++){
		if (s[i] != s[len - i - 1]) return 0;
	}
	return 1;
}
/*������� �� ������ �� �������� �������� �� ����� �������,
���������� ���� �� �������� ������� �� ����� �������� ����� ������� � ����� �����
� ������� �������� �� ����� � ����������*/
char* makepal(const char *s){
	char *res;										//������������ ����� ���� ������� ��������
	int len = strlen(s);
	bool flag=true;
	if (len == 0) {
		res = "Exeption:INPUT STRING IS EMPTY\n";
		return res;
	}
	if (ispal(s)) return s;

	res = (char*)calloc(2 * len, sizeof(char));		//�������� ������ ��� ����� � 2 ���� ����� �� ������� ����� ������� ������� �������� �� ������ "\0"
	int tmp,j=0;

	strcpy(res, s);
	for (int i = 0; i < len - 1; i++){
		while (j <= i){
			res[len + j] = s[i - j];
			j++;
		}
		if (ispal(res)) return res;
		j = 0;

	}
	return res;
}
/*������� �� ������ ����� � ���������� ����� � ������� �������� �� �����*/
double* txt2double(const char *s, int *size){
	int len = strlen(s);
	double *resvect = NULL;												//������������ ����� �����
	int k = 0, num_len = 0 , vect_ind = 0;								//k-������� ";"  num_len - ������� �����
	char *str;
	/* 
	�������� �� �� �� � � �������� ����� �������� �� ������ �������
	*/

	if (len == 0) return 0;

	for (int i = 0; i < len; i++){
		if ((s[i] < '0' || s[i] > '9') && s[i] != '.' && s[i] != ';'){
			*size = 0;
			return resvect;
		}
	}
	/*
	ϳ�������� ������� ���� �� ������� ��������
	*/
	for (int i = 0; i <= len; i++){
		if (s[i] == ';' || s[i] == '\0') k++;
	}
	*size = k;
	// �������� ������ ��� ����� ������������� ������
	resvect = calloc(*size, sizeof(double));

	/*
	�������� �����������, 
	���� ��������� ";" �������� ������ ��� ����� � ���� �������� ����� ����� ��� ����� �������
	���������� ����� � ���� � ������ �. �������� � ������ ������������� ������ ����� �� �������� �����
	��������� ������� ����� � ��������� �������� ������ �� �����
	������ �������� ������� �����.
	*/
	for (int i = 0; i <= len; i++){
		if (s[i] == ';' || s[i] == '\0'){
			str = (char*)malloc((num_len + 1)*sizeof(char));
			for (k = 0; k < num_len; k++) str[k] = s[i - num_len + k];
			resvect[vect_ind++] = atof(str);
			num_len = 0;
			free(str);
			str = NULL;
		}
		else num_len++;
	}
	return resvect;
}