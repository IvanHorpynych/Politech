/******************************************************************************************
	name:					mystring.c
	description:			function "substr", that search in string1
							subsequence, that equals to string2 and return -1 if
							subsequence didn't founded and position of first entry
							of string2 in string1
							function "subseq" search maximal subsequence that contains
							string1 and string2. function returns 0, if string1 and
							string2 hasn't mutual subsequence and
							return length of subsequence, if they have
							function "ispal" that returns 0 if
							string isn't palindrome and returns 1 if string
							is palindrome
							function "makepal" makes with minimal adding characters
							in end from string palindrome. returns this palindrome
							function "IsCorrect" returns true if string can be
							translated into array of double and false in another case.
							function "FindSemicolon" search first entering of semicolon
							and returns position of it. if semicolon doesn't exist,
							function returns -1.
							function "FindDot" search entering of dot and returns position
							of it. if dot doesn't exist, function returns length of string.
							function "Converter" converts string into float
							function "Converting" calls function "Converter" twice 
							and returns full float number.
							function "txt2double" translate string into array of float
							numbers if it is possible. If it is impossible, function
							returns pointer into NULL, if it is possible - pointer into
							array of float numbers. 
							Format of numeric string - [integer].[mantissa]
	author:					Dima
	date of creation:		02.09.2014
	written:				04.09.2014
	date of last change:	10.09.2014
******************************************************************************************/

#include "mystring.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int substr(const char *string1, const char *string2) {
	int start = 0;																		//position of subsequence
	bool flag = false;																	//flag == true - subsequence founded, false - didn't founded
	while (flag == false && string1[start + strlen(string2) - 1] != '\0') {
		int pos = 0;
		flag = true;
		while (string1[start + pos] == string2[pos] && string2[pos] != '\0') pos++;		//check symbols of strings
		if (string2[pos] != '\0') {														//subsequence didin't founded from position #start
			flag = false;
			start++;
		}
	}
	if (flag == true) return start;														//return position of first subsequence
	else return -1;																		//subsequence didn't founded
}

int subseq(const char *string1, const char *string2) {
	unsigned CUR_LEN = 1, MAX_LEN = 0, CUR_POS = 0, i;									//CUR_LEN - current length, MAX_LEN - maximal length, CUR_POS - current position
	char *STR = malloc(sizeof(char)* 2);											//STR - subsequence
	STR[1] = '\0';
	while (CUR_LEN < strlen(string2) && CUR_LEN < strlen(string1)) {
		if (string2[CUR_POS + CUR_LEN - 1] == '\0') {
			CUR_POS = 0;
			STR = (char *)realloc(STR, sizeof(char)* ((++CUR_LEN) + 1));
			STR[CUR_LEN] = '\0';
		}
		for (i = 0; i < CUR_LEN; i++) STR[i] = string2[CUR_POS + i];

		if (substr(string1, STR) != -1) MAX_LEN = CUR_LEN;
		CUR_POS++;
	}
	free(STR);
	return MAX_LEN;
}

char ispal(const char *string) {
	char PAL = 1;
	int LEN = strlen(string), POS = 0;
	while (POS < LEN / 2) {
		if (string[POS] != string[LEN - POS - 1]) {
			PAL = 0;
			break;
		}
		else POS++;
	}
	return PAL;
}

char *makepal(const char *string) {
	unsigned ADD = 0, i, STR_LEN = strlen(string);
	char *STR = malloc(strlen(string) + 1);
	for (i = 0; i <= strlen(string); i++) STR[i] = string[i];
	while (ispal(STR) == 0) {
		STR = (char *)realloc(STR, ((++ADD) + STR_LEN + 1) * sizeof(char));
		for (i = 0; i < ADD; i++) STR[ADD + STR_LEN - i - 1] = STR[i];
		STR[ADD + STR_LEN] = '\0';
	}
	return STR;
}

bool IsCorrect(const char *string) {																		//check string for errors in numbers
	bool flag = true;
	unsigned i;
	for (i = 0; i < strlen(string) - 1; i++)
	if ((string[i] < '0' || string[i] > '9') && string[i] != ';' && string[i] != '.') {
		flag = false;
		break;
	}
	else if ((string[i] == ';' || string[i] == '.') && i != 0 && i != string[strlen(string) - 1]) {
		if (string[i - 1] < '0' || string[i - 1] > '9') flag = false;
		else if (string[i + 1] < '0' || string[i + 1] > '9') flag = false;
	}
	else if ((string[i] == ';' || string[i] == '.') && (i == 0 || i == string[strlen(string) - 1])) flag = false;

	return flag;
}

int FindSemicolon(char *string) {																		//function returns position of semicolon 	
	unsigned i;																								//and change semicolon into comma
	for (i = 0; i < strlen(string); i++) if (string[i] == ';') {
		string[i] = ',';
		return i;
	}
	return -1;
}

int FindDot(char* string) {																				//find position of point in number
	unsigned i;
	for (i = 0; i < strlen(string); i++) if (string[i] == '.') return i;
	return strlen(string);
}

double Converter(char* string, int pos, double m, double *res, bool flag) {										//convert string mantissa into float 
	switch (string[pos]) {
	case '1': *res += m; break;
	case '2': *res += 2 * m; break;
	case '3': *res += 3 * m; break;
	case '4': *res += 4 * m; break;
	case '5': *res += 5 * m; break;
	case '6': *res += 6 * m; break;
	case '7': *res += 7 * m; break;
	case '8': *res += 8 * m; break;
	case '9': *res += 9 * m; break;
	}
	if (flag == false && string[pos] != '\0') Converter(string, ++pos, m * 0.1, res, false);
	else if (flag == true && pos - 1 != -1) Converter(string, --pos, m * 10, res, true);
	return *res;
}

double Converting(char* string) {
	double *res = malloc(sizeof(double));
	int pos = FindDot(string);
	*res = 0;
	Converter(string, pos - 1, 1, res, true);
	Converter(string, pos + 1, 0.1, res, false);
	return *res;
}

double *txt2double(const char *string, int *size) {
	char *str = malloc(sizeof(char)*strlen(string));
	unsigned i;
	for (i = 0; i <= strlen(string); i++) str[i] = string[i];
	int LEN = strlen(str);
	str = (char *)realloc(str, LEN + 2);
	str[LEN] = ';';
	str[LEN + 1] = '\0';
	double *arr = malloc(sizeof(double));
	int SP = 0;
	*size = 0;
	while (IsCorrect(string) && str[SP] != '\0') {
		int SMCLN = FindSemicolon(str);
		if (SMCLN != -1) {
			char *num = malloc(sizeof(char)* (SMCLN - SP + 1));
			for (i = 0; i < SMCLN - SP; i++) num[i] = str[SP + i];
			num[SMCLN - SP] = '\0';
			arr[*size] = (double)Converting(num);
			arr = (double *)realloc(arr, sizeof(double)* ((++(*size)) + 1));
			SP = SMCLN + 1;
			free(num);
		}
	}
	if (*size == 0) {
		free(arr);
		arr = NULL;
	}
	return arr;
}

