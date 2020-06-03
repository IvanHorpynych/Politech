/************************************************************************
*file: mystring.h
*synopsis: declarations for string UDF-functions, types, constants
*author: Khlevnoy Y.A.
*written: 20/09/2014
*last modified: 12/10/2014
************************************************************************/


#ifndef MYSTRING_H
#define MYSTRING_H
    #include <stdlib.h>

    /// Function finds the position of occurrence of the substring in the string.
	int substr (const char *pStr, const char *pSub);

	/// Function finds the length of the longest common subsequence of two strings.
    int subseq (const char *pStr, const char *pSub);

    /// Function verifies whether input string a palindrome or not.
    int ispal  (const char *pStr);

    /// Function makes as small as possible palindrome out of an input string.
    char* makepal(const char *pStr);

    /// Function parses input string into allocated array of type double.
    double* txt2double(const char *pStr, int *size);

#endif /* MYSTRING_H */
