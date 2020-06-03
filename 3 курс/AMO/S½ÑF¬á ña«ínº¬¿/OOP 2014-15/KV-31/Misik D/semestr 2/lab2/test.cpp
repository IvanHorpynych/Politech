/******************************************************************************************
	name:					test.cpp
	description:			test file
	author:					Dima
	date of creation:		12.01.2015
	written:				13.01.2015
	date of last change:	13.01.2015
******************************************************************************************/

#include "File.h"

int main() {
	vector<File> f(3);
	f[0].ReadFromFile("test3.csv", 128);
	f[1].ReadFromFile("test2.csv", 128);
	f[2].ReadFromFile("test1.csv", 128);
	printf("Start\n");
	Listall(f);
	Addkeyword(f, "deleteme");
	printf("\nAfter adding\n");
	Listall(f);
	Delkeyword(f, "dima");
	printf("\nAfter delete\n");
	Listall(f);
	printf("\nAfter delete (by lentgth)\n");
	Delkeywordbylen(f, 4);
	Listall(f);
	printf("\nAfter first sort\n");
	Sortbysize(f);
	Listall(f);
	printf("\nAfter second sort\n");
	Sortbykeywordsnumber(f);
	Listall(f);
	return 0;
}