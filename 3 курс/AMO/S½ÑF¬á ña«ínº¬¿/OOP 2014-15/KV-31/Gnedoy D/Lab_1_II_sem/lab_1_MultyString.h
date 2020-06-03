/************************************************************************
*file: lab_1_MultyString.h
*author: Gnedoj D.
*written: 11/02/2015
*last modified: 11/02/2015
*************************************************************************/
#pragma once
#include <string.h>
#include <stdio.h>
#include <iostream>

class MultiString{
public:
	MultiString();
	MultiString(int);
	MultiString(const MultiString &);
	~MultiString();

	MultiString & operator=(const MultiString &ms);
	char * operator[] (int nindex);
	MultiString & operator+=(const MultiString &);
	MultiString MergeMultiStringExclusive(const MultiString &);
	int Find(const char *pszsub) const;
	void Add(const char *str);
	int GetLength() const;
	bool IsEmpty()const;
	void Empty();
	void SetAt(int nindex, const char* str);
	void PrintStr(int nindex) const;
private:
	char **buf;
	int str_nmb;
};
