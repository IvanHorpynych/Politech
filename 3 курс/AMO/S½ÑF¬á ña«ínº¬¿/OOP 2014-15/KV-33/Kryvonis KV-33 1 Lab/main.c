/******************************************************************
*Laboratory work 1
*File: main.c
*Description:main program that runs the function and also tests their
*These file are declared in the include file "MyHeadFile.h".
*Author: Artem Kryvonis
*written: 08/09/2014
*last modified 13/09/2014
******************************************************************/
#include "MyHeadFile.h"

void main(){
	int *size;
	double *vect;

	char *str1 = "Hello";
	char *str2 = "looo";

	printf("substr(%s , %s)\t", str1, str2);
	printf(" result %d \n", substr(str1, str2));
	
	char *str3 = "mama";
	char *str4 = "m222ma333mam444mama";

	printf("subseq(%s , %s)\t ",str3,str4 );
	printf("result %d \n", subseq(str3,str4));
	
	char *str5 = "radar";

	printf("ispal(%s)\t\t ", str5);
	printf("result %d \n", ispal(str5));

	char *str6 = "abbba";

	printf("makepal(%s)\t\t ", str6);
	printf("result %s\n", makepal(str6));

	char *str7 = "123.5;3521;555";

	vect = txt2double(str7,&size);

	printf("txt2double(%s ,size = %d)\n",str7,size);

	for (int i = 0; i < size; i++){
		printf("vect[%d] = %f\n", i, vect[i]);
	}
	getch();

}