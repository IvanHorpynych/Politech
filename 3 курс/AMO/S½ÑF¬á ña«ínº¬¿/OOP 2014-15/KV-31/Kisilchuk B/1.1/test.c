/************************************************************************************
*file: test.c																
*Synopsis: this is a test file with main function. You can see all functions		
*in the "lab1.c".																
*related files: lab1.c															
*author: Kisilchuk Bogdan														
*written: 12/11/2014
*last modified: 18/11/2014
*************************************************************************/

#include "lab1.1.h"

int main(){
	double *mas;
	int s;

	printf("1)\t1 Word : %s \n \t2 Word : %s\n\t\t\t\tIndex = %d\n", "qwerty", "wer", substr("qwerty", "wer"));
	printf("\n2) \t1 Word : %s \n \t2 Word : %s\n\t\t\t\tCount = %d\n", "docent", "student", subseq("docent", "student"));
	printf("\n3) Word : %s \n\t\t\t\tIspal = %d\n", "qwerre", ispal("qwerre"));
	printf("\n4) Word : %s \n\t\t\t\tPalindrom: %s\n", "qwertytre", makepal("qwertytre"));

	mas = txt2double("127;25.4;0.3;12", &s);

	printf("\n5)\tLength of vector (S) = %d\n", s);
	printf("\tString: 127;25.4;0.3;12");
	printf("\n\tElements of vector:\n");

	if (s == 0)
		printf("\t\t\t\tError");
	else{
		for (int i = 0; i < s; i++)
			printf("\t\t\t\tmas[%d] = %f\n ", i, mas[i]);
	}

	_getch();
	return 0;
}