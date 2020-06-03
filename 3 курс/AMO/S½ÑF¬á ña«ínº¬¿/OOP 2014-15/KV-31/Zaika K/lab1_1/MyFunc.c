/*********************************************************
*file:MyFunc.c
*synopsis:these file contains the realization of functions,which are declarated in the file "MyFunc.h"
*author: Zaika Kirill KV-31
*written:5.10.2014
*last modified:5.10.2014
*********************************************************/

#include "MyFunc.h"

/*******************************************************
*��������: substr
*�������������: int value = substr(str1,str2)
*�������� � MyFunc.h
*�������:�-��� ���������� ������ �� ������ ��������� str2 � ������ str1
*******************************************************/
int substr(const char *string1, const char *string2){
	/*��������� �� 1��� ���������*/
	char *istr = strstr(string1, string2);
	return (istr != 0) ? istr - string1 : 0;
}

/******************************************************
*��������: subseq
*������������� : int len = subseq(str1,str2)
*�������� � MyFunc.h
*C�������: �-��� ���� �������� ����� ������� ���������������������
� ������� str2 � str1
*�������: ���������� ����� ����� ������� ���������������������
******************************************************/


int subseq(const char *string1, const char *string2){
	int i, j, k, l, tmpCount, count;
	i = tmpCount = count = 0; //������������� ���������-���������
	//����� ������� ��������
	for (; i < (strlen(string2) - 1); i++){
		for (j = 0; j<strlen(string1) - 1; j++){
			tmpCount = 0;
			k = j;
			l = i;
			//���� ������� ������ �������,
			//���������� �������� �����������
			while ((string2[l] == string1[k]) && (string2[l] != '\0')){
				k++;
				l++;
				tmpCount++;
			}
			//���� ����� ������������������ ������
			//���������� �
			if (tmpCount>count)
				count = tmpCount;
		}

	}
	return count;
}

/*************************************************************
*��������: ispal
*������������� int flag = ispal(palindrome)
*�������� � MyFunc.h
*��������:�-��� ��������� ������ string, �������� �� �����������
*�������: ���������� 1,���� ���������. ����� 0.
*************************************************************/
char ispal(const char *string){
	/*����������-�������,������ ������, ��������� ����������*/
	int i, n, flag;
	flag = 1;
	for (i = 0, n = strlen(string); i < (strlen(string)) / 2, n >(strlen(string)) / 2; i++, n--){
		/*� ����� ����������� �� ��������� ������ � ������ � ����� ������,
		���� �� ����������� � ��������*/
		if (string[i] != string[n - 1]){
			/*� ������ ��������, ��������� ���������� ����������� 0
			� ������������ ��������*/
			flag = 0;
			break;
		}
	}
	return (flag == 1) ? 1 : 0;
}

/*************************************************************
*��������: makepal
*������������� char *ptr = makepal(str)
*�������� � MyFunc.h
*��������: �-��� ��������� ������ string �� ���������
���� ���, �� ����������� ������ � ������� ���.�������
*�������: ��������� �� ���������
*************************************************************/
char* makepal(char *string){
	int len, count, i = 0;				/*������ ������,��������*/
	char *p;							/*������� ������-���������*/
	len = strlen(string);
	p = calloc(len, sizeof(char));		/*�������� ��������� ������*/
	if (!p){							/*���� �� ������� ��������,
										�� ���������� ��������� ������*/
		return string;
	}
	else {
		/*����,���� �� ����� ���.������*/
		for (i = 0; i < len; i++){
			/*� ������-��������� ������� ��� �������� ���.������*/
			p[i] = string[i];
		}
		p[len] = '\0';
		count = strlen(p) + 1;		/*����������� ������� ��� ��������� ������*/
		i = 0;
		/*����, ���� ������ �� �����  �����������*/
		while (ispal(p) == 0){
			p = (char *)realloc(p, count * sizeof(char));	/*�������� ������*/
			if (p != NULL){
				/*���� ������ ��������*/
				/*� ������ ��� ��������� ������ ��������� � �����*/
				if (i == 0){
					p[count - 1] = string[i];
					p[count] = '\0';
					i++;
					count++;
				}
				/*� ��������� ���� �������� ����� ������ ������
				� �� ����� ������ ��������� �������*/
				else{
					p[count - 1] = p[count - 2];
					p[count - 2] = string[i];
					p[count] = '\0';
					i++;
					count++;
				}
			}
		}
		return p;
	}
}

/**************************************************************
*��������: txt2double
*������������� double *d = txt2double(str1,&n)
*�������� � MyFunc.h
*��������: ������� ������� ���-�� ���� � ������ string,
������������ ����� �� ������� ������ � ������������ �
������� �� � ������ �������.�����
*�������: ���������� ��������� �� ������ �������. �����
*************************************************************/
double *txt2double(const char *string, int *size){
	int i, j, count;					/*����������-��������*/
	i = j = count = 0;

	int flag = 0;						/*���������� ��� �������� ������� ����*/
	double *ptr = NULL;					/*������ ����� ���� double*/
	char *word;							/*������ ��� "������" ����*/
	word = calloc(1, sizeof(char));		/*��������� ��������� ������*/
	for (; i <= (size); i++){		/*����,������������� ��� ����� ������*/
		/*���� ������� ������ ;
		�� ���������� ������ ��� ������� ����� � ��������� ���������������� �����*/
		if ((string[i] == ';') || (string[i] == '\0')) {
			word[j] = '\0';
			count++;
			ptr = (double *)realloc(ptr, count * sizeof(double));
			ptr[count - 1] = atof(word);
			/*� ������ ���������� ��������������� ��������� �������� 0*/
			if (ptr[count - 1] == 0){
				*size = 0;
				return NULL;
			}
			j = 0;
		}
		/*��������� ������ ������ ������ ������ � ������ ��� "������"*/
		else {
			word = (char *)realloc(word, (j + 1)*sizeof(char));
			if (word != NULL){
				word[j] = string[i];
				j++;
			}
			else{
				*size = 0;
				return NULL;
			}
		}
	}

	/*��� ������� ��������������� ����� �������. ���-�� ���� � ������*/
	/*size = count;*/

	return ptr;
}