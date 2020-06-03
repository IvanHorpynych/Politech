
#include "string_func.h"
#include "string_func_2.h"
int main()
{
	char *tsubstr1 = "aldfgjldk";
	char *tsubstr2 = "awdlfkg";
	printf("String %s begins in string %s from %d position\n", tsubstr1, tsubstr2, substr(tsubstr1, tsubstr2));
	char *tsubseq1 = "The longest common substring here is";
	char *tsubseq2 = "Theq longestq commonq substringq q is";
	printf("String 1 - %s\nString 2 - %s\nThe longest common substring is %d\n", tsubseq1, tsubseq2, subseq(tsubseq1, tsubseq2));
	char *tispal = "ababa";
	printf("String %s is a palindrom if ispal returns 1 or not a palindrom if ispal returns 0\nispal - %d\n", tispal, ispal(tispal));
	char *tmakepal = "ababa";
	printf("Palindrome made from %s is %s\n", tmakepal, makepal(tmakepal));
	char *ttxt2double = "13;3.14;5.375";
	int *tsize = 0;
	printf("String %s is turned to double\n", ttxt2double);
	txt2double(ttxt2double, tsize);
	char c = getch();
	return 0;
}