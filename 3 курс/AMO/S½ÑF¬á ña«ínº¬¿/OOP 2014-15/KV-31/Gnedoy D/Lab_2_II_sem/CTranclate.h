/**************************************************
* file: CTranslate.h
* CTranslate class declaration
* written: 10/04/2015
* by Gnedoy Dmitry
****************************************************/
#include <cstdlib>
#include <iostream>
#include <algorithm>
#include <vector>
#include <string>
#include <math.h>
#include <stdio.h>
#include <iostream>
#include <fstream>
#include <iomanip>
#include <stdlib.h>

using namespace std;

#pragma once
enum SORTBY { ENG, ITA };
class CTranslate{
public:
	//default constructor
	CTranslate();
	//copy constructor
	CTranslate(const CTranslate &);
	//destructor
	~CTranslate();
	//overloaded assignment
	CTranslate & operator=(const CTranslate &);
	bool operator<(const CTranslate &);
	void MakePair(char *, char *);
	void Print()const;
	char *GetEng()const;
	char *GetIta()const;
	friend bool CompareIta(const CTranslate &tr, const CTranslate &tr2);
private:
	char *eng;//English word
	char *ita;//Italian word
};

void readFromCSV(vector<CTranslate> &, char *fileName);
void ListAll(const vector<CTranslate> &);
void DelByEng(vector <CTranslate> &, const char *en);
void DelByIta(vector <CTranslate> &, const char *it);
void SortByEng(vector <CTranslate> &);
void SortByIta(vector <CTranslate> &);
void TranslateEng(const vector <CTranslate> &, const char *en);
void TranslateIta(const vector <CTranslate> &, const char *ita);
bool CompareIta(const CTranslate &tr, const CTranslate &tr2);