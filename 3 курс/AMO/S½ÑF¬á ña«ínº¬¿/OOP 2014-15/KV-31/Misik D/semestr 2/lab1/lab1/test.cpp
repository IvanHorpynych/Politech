/******************************************************************************************
	name:					test.cpp
	description:			test file
	author:					Dima
	date of creation:		12.01.2015
	written:				13.01.2015
	date of last change:	13.01.2015
******************************************************************************************/

#include "Multistring.h"
#include <stdio.h>

int main() {
	Multistring *M1 = new Multistring(5);
	Multistring *M2 = new Multistring(5);
	Multistring *M3 = new Multistring(6);
	
	M2->Setat(0, "start");
	M2->Setat(1, "begin");
	M2->Setat(2, "middle");
	M2->Setat(3, "finish");
	M2->Setat(4, "end");

	M3->Setat(0, "1");
	M3->Setat(1, "2");
	M3->Setat(2, "3");
	M3->Setat(3, "4");
	M3->Setat(4, "5");
	M3->Setat(5, "6");

	*M1 = *M2;
	M1->Print();
	*M2 += *M3;
	M2->Print();

	Multistring *M4 = new Multistring(*M2);
	M4->Print();

	Multistring *M5 = new Multistring(M4->Mergemultistringexclusive(*M3));
	M5->Print();
	delete M5;
    M5 = new Multistring(M4->Mergemultistringexclusive(*M2));
	M5->Print();

	M2->Setat(0, "start1");
	M2->Setat(1, "begin");
	M2->Setat(2, "middle3");
	M2->Setat(3, "finish");
	M2->Setat(4, "end5");

	delete M5;
	M5 = new Multistring(M4->Mergemultistringexclusive(*M2));

	printf("M1\n");
	M1->Print();

	printf("\nM2\n");
	M2->Print();

	printf("\nM3\n");
	M3->Print();
	
	printf("\nM4\n");
	M4->Print();

	printf("\nM5\n");
	M5->Print();

	printf("4 in M3 has index %d\n", M3->Find("4"));
	printf("length of M3 is %d\n", M3->Getlength());
	printf("length of M5 is %d\n", M5->Getlength());

	delete M1;
	delete M2;
	delete M3;
	delete M4;

	return 0;
}