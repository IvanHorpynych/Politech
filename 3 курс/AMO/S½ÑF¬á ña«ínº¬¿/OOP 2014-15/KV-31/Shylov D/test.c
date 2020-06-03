/************************************************************************************
*file: OOP_lab1_test.c																*
*Synopsis: this is a test file with main function. You can see all functions		*
*in the "OOP_lab1.c".																*
*related files: OOP_lab1.c															*
*author: Shilov D.A.															*
*written: 29/10/2014																*
*last modified: 01/11/2014																*
************************************************************************************/

#include "header.h"

int main(){
	double *mas;
	int s;

	printf("1)\t1 Word : %s \n \t2 Word : %s\n\t\t\t\tIndex = %d\n", "qwerty", "wer", substr("qwerty", "wer"));
	printf("\n2) \t1 Word : %s \n \t2 Word : %s\n\t\t\t\tCount = %d\n", "aaabbbcccddd", "zzzxxxaacccddd", subseq("aaabbbcccddd", "zzzxxxaacccddd"));
	printf("\n3) Word : %s \n\t\t\t\tIspal = %d\n", "123321x32", ispal("123321x32"));
	printf("\n4) Word : %s \n\t\t\t\tPalindrom: %s\n", "123456776", makepal("123456776"));

	mas = txt2double("12.5;0;7", &s);

	printf("\n5)\tLength of vector (S) = %d\n", s);
	printf("\tString: 12.5;0;7");
	printf("\n\tElements of vector:\n");
	if (s == 0)
		printf("\t\t\t\tError");
	else{
		for (int i = 0; i < s; i++)
			printf("\t\t\t\tmas[%d] = %-0.2f\n ", i, mas[i]);
	}

	_getch();
	return 0;
}