/*!
* file: StringList.h
* StringList class declaration
* written: 01/06/2012
* Copyright (c) 2012 by Yu.Zorin
*/
#define  _CRT_SECURE_NO_WARNINGS
#pragma once
#include <string>
#include <iostream>
#include <conio.h>
using namespace std;

class cString{
public:
	//constructors/destructor
	cString();
	cString(const char *psz);
	cString(const cString& stringSrc);

	~cString();

	//methods
	int GetLength() const;
	bool IsEmpty()const;
	void Empty();
	void SetAt(int nIndex, char ch);
	int Compare(const cString &s) const;
	int Find(char ch) const;
	int Find(char *pszSub) const;

	cString Mid(int nFirst, int nCount) const;
	cString Left(int nCount) const;
	cString Right(int nCount) const;

	//operators
	const cString& operator =(const unsigned char* psz);
	char operator [](int indx);
	cString& operator =(const cString& stringSrc);

	cString operator +(const cString& str);
	cString& operator +=(const cString& str);
	void Print();
private:
	//put your own data members here
	int length;
	char *string;
};
