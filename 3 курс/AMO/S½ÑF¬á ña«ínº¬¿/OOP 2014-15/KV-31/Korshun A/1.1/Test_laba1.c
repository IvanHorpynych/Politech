
#include "string_func.h"
int main()
{
	char *tsubstr1 = "qwtrtwe";//1
	char *tsubstr2 = "we";
	printf("1)  String %s begins in string %s from %d position\n", tsubstr1, tsubstr2, substr(tsubstr1, tsubstr2));
	char *tsubseq1 = "ert";//2
	char *tsubseq2 = "wertyr";
	printf("2)  String 1 - %s\n    String 2 - %s\n    The longest common substring is %d\n", tsubseq1, tsubseq2, subseq(tsubseq1, tsubseq2));
	char *tispal = "abbba";//3
	printf("3)  String %s is a palindrom if ispal returns 1 or not a palindrom if ispal returns 0\nispal - %d\n", tispal, ispal(tispal));
	char *tmakepal = "abbcb";//4
	printf("4)  Palindrome made from %s is %s\n", tmakepal, makepal(tmakepal));
	char *ttxt2double = "13;3.14;5.375";//5
	int *tsize = 0;
	printf("5)  String %s is turned to double\n", ttxt2double);
	txt2double(ttxt2double, tsize);
	char c = getch();
	return 0;
}