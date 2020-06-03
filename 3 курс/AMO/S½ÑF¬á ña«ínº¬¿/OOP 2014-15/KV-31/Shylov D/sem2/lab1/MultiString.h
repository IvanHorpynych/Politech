/*!
* file: MultiString.h
* MultiString class definition
* written: 01/05/2012
* Copyright (c) 2012 by Yu.Zorin
*/
#pragma once
#include <string.h>
#include <stdio.h>
#include <iostream>

class MultiString{
public:
	//constructors destructor
	MultiString();
	MultiString(int);
	MultiString(const MultiString &);
	~MultiString();

	//methods
	MultiString & operator=(const MultiString &ms);
	char * operator[] (int nIndex) const;
	MultiString & operator+=(const MultiString &);
	MultiString MergeMultiStringExclusive(const MultiString &cl);
	void Adding_str(const char*);
	int Find(const char *pszSub) const;
	int GetLength() const;
	bool IsEmpty()const;
	void Empty();
	void SetAt(int nIndex, const char* str);
	void PrintStr(int nIndex) const;

private:
	//forbidden copy constructor
	//MultString (const MultString &ms){};

	//attributes
	char **buf;//pointer to vector
	int str_nmb;//strings number
};