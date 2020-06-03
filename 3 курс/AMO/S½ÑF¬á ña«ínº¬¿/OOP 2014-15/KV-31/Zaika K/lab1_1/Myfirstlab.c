/*********************************************************
*file:MyfirstLab.c
*synopsis:Файл проекта предназначен для тестирования функций объявленных в файле MyFunc.h
*author:Zaika Kirill KV-31
*written:5.10.2014
*last modified:5.10.2014
*********************************************************/
#include "MyFunc.h"

char str1[] = "kirkirill";
char str2[] = "kirill";
char pal[] = "51212";
char str3[] = "3.14;5.3;6.47";
int i, n = 0;
double *d_ptr;

int main(){

	if (substr(str1, str2) != 0)
		printf("The index is : %d\n", substr(str1, str2));
	else printf("The string is empty or can not be found\n");
	printf("The common length between str1 and str2 is: %d\n\n", subseq(str1, str2));
	printf("The palindrome from pal : %s\n\n", makepal(pal));
	n = strlen(str3);
	d_ptr = txt2double(str3, &n);
	printf("Str3 :%s\n", str3);
	if (n != 0){
		printf("Vector:");
		for (i = 0; i < n; i++){
			printf(" %.4f", d_ptr[i]);
		}
	}
	else
		printf("Something went wrong or uncorrect format of input string");
	getch();
	return 0;
}
