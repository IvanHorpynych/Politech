/************************************************************************
>file: TestFile.c
>author: Hlibov A.R.
>group: KV-31, FPM
>written: 10/09/2014
>last modified: 15/09/2014
************************************************************************/

#include "Lab1.h"

int main(){
	char s[50] = "Existentialismsil";
	char s1[5] = "tial";

	
	printf("Second string starts at %d position", substr(s, s1));
	getchar();

	system("cls");
	printf("Strings have %d common chars", subseq(&s, &s1));
	getchar();

	system("cls");
	printf("%c", ispal(s));
	getchar();

	system("cls");
	printf("%s", makepal(&s));
	getchar();

	system("cls");
	char numbers[20] = "3.14;6;15.5;1.22;4;";
	int size = 0;
	double* vector = txt2double(&numbers, &size);

	for (int i = 0; i < size; i++)
		printf(" %8.2f ", *(vector + i));
	free(txt2double(&numbers, &size));
	getchar();

	return 0;
}