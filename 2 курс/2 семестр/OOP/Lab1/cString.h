#pragma once
#include <string.h>
#include <stdio.h>
#include <iostream>
using namespace std;


class cString {
public:

	cString();
	cString(const char *psz);
	cString(const cString& stringsrc);

	~cString();

	int GetLength() const;
	bool IsEmpty()const;
	void Empty();
	void SetAt(int nindex, char ch);
	int Compare(const cString s) const;
	int Find(char ch) const;
	int Find(char *pszsub) const;

	cString Mid(int nfirst, int ncount) const;
	cString Left(int ncount) const;
	cString Right(int ncount) const;

	const cString& operator =(const char* psz);
	char operator [](int indx);
	cString& operator =(const cString& stringsrc);
	friend cString operator +(const cString& string1, const cString& string2);
	cString& operator +=(const cString& string);

	void Print();
private:
	char* bufstr;
};
