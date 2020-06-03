/******************************************************************************************
	name:					teststrings.c
	description:			main file, which call all functions and contains functions
							"Menu", where you choose number of task
	author:					Dima
	date of creation:		02.09.2014
	written:				04.09.2014
	date of last modified:	10.09.2014
******************************************************************************************/

#include <windows.h>
#include "mystring.h"
#include <stdio.h>
#include <stdlib.h>
#include <conio.h>

char *EnterString() {																//enter dynamic string
	int SLen = 0;																	//SLen - length of entered string
	char ch;
	char *str = (char *)malloc(sizeof(char)* 256);									//string
	while ((ch = getchar()) != '\n') if (SLen != 255) str[SLen++] = ch;				//enter string by chars
	str[SLen] = '\0';																//add in end null symbol
	str = (char *)realloc(str, (1 + SLen) * sizeof(char));							//reallocation
	return str;
}
	
void gotoxy(const int x, const int y) {												//set cursor in console on {X,Y}
	COORD c = { x, y };
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), c);
}

int Menu() {																		//Choosing menu
	int KEY;
	do {
		gotoxy(35, 7);
		printf("Part 1");
		gotoxy(35, 9);
		printf("1. task 1.1");
		gotoxy(35, 10);
		printf("2. task 1.2");
		gotoxy(35, 11);
		printf("3. task 1.3");
		gotoxy(35, 12);
		printf("4. task 1.4");
		gotoxy(35, 13);
		printf("5. task 1.5");
		KEY = _getch();
		if (KEY <= '5' && KEY >= '1') break;
	} while (true);
	system("cls");
	return KEY - 49;
}

int main() {																		//Main program
	char *string1, *string2, *pal;
	int *size, i;
	double *arr;
	switch (Menu()) {
	case 0:

		printf("Print first string\n");
		string1 = EnterString();
		system("cls");

		printf("Print second string\n");
		string2 = EnterString();
		system("cls");

		printf("String 2 starts in String 1 from position %d", substr(string1, string2));
		free(string1);
		free(string2);
		break;

	case 1:

		printf("Print first string\n");
		string1 = EnterString();
		system("cls");

		printf("Print second string\n");
		string2 = EnterString();
		system("cls");

		printf("Max subsequence - %d", subseq(string1, string2));
		free(string1);
		free(string2);
		break;

	case 2:

		printf("Print string\n");
		string1 = EnterString();
		system("cls");

		if (ispal(string1) == 1) printf("It is palindrome");
		else printf("It isn't palindrome");
		free(string1);
		break;

	case 3:
		printf("Print string\n");
		string1 = EnterString();
		system("cls");
		
		printf(pal = makepal(string1));
		free(string1);
		free(pal);
		break;

	case 4:

		printf("Print string\n");
		string1 = EnterString();
		size = malloc(sizeof(int));
		system("cls");

		arr = txt2double(string1, size);
		for (i = 0; i < *size; i++) printf("%d. - %f\n", i, arr[i]);
		if (*size == 0) printf("Error");
		free(string1);
		free(size);
		free(arr);
		break;
	}
	_getch();
	return 0;
}
