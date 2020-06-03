/*******************************************************************
*Laboratory work #1
*File: lab11.c
*Description: This file describes functions which works with strings
*These functions are declared in the file "lab11_Header.h"
*Author: Anastasiev D. V.
*written 18/09/2014
*last modified: 18/09/2014
*******************************************************************/



#include "Lab11_Header.h"

int main(){
	double *vect;
	int s ,i;
	printf("1. Substr %d \n\n",substr("HelloWorld","Worldg"));
	printf("2. Subseq %d \n\n",subseq("wert","werkufgwert"));
	printf("3. Ispal %d \n\n",ispal("ded"));
	printf("4. Makepal %s \n\n",makepal("aat"));
	printf("5. Txt2double \n\n");
	vect = txt2double("323;123.43",&s);
	if(s!=0){
		for(i=0;i<s;i++){
			printf("vect[%d]=%.2f\n",i,vect[i]);
		}
	}
	free(vect);
	return 0;
}
	