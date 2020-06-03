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
using namespace std;
class MultiString{
public:
	//constructors destructor
	MultiString();
	MultiString(int );
	MultiString(const MultiString & );
	~MultiString();

	//methods
	MultiString & operator=(const MultiString &ms){
		for(int i = 0; i < str_nmb; i++){
			delete buf[i];
		}
		delete [] buf;
		buf = new char*[str_nmb = ms.GetLength()];
		for(int i = 0; i<str_nmb; i++){
			buf[i] = new char[strlen(ms[i])];
			strcpy(buf[i],ms[i]);
		}
		return *this;
	};

	char * operator[] (int nindex) const;

	MultiString & operator+=(const MultiString &src){
		for(int i = 0; i<str_nmb; i++){
			strcat(buf[i], src[i]);
		}
		return *this;
	};
	MultiString* MergeMultiStringexclusive(const MultiString &);
	int Find(const char *pszsub ) const;
	int GetLength( ) const;
	bool IsEmpty()const{ return str_nmb == 0;};
	void Empty();
	void SetAt( int nindex, const char* str );
	void PrintStr(int nindex) const;
	void PrintAllStr()const;
	void PushBack(const char *str);
private:
	
	//attributes
	char **buf;//pointer to vector
	int str_nmb;//strings number
};
