/*!
* file: Translate.h
* CTranslate class declaration
* written: 15/02/2015
* Copyright (c) 2015 by Hupovetc
*/
#define  _CRT_SECURE_NO_WARNINGS
#pragma once
#include <string>
#include <iostream>
#include <conio.h>
#include <iterator>
#include <list>
using namespace std;

class CTranslate{
public:
	//default constructor
	CTranslate();
	//copy constructor
	CTranslate(const CTranslate& elem);
	//destructor
	~CTranslate();
	//overloaded assignment
	CTranslate& operator=(const CTranslate&);/*const CTranslate&*/
	bool operator<(const CTranslate &);
	void MakePair(char *, char *);
	void Print()const;
	char *GetEng()const;
	char *GetIta()const;

private:
	char *eng;//English word
	char *ita;//Italian word
};

void FillList(list<CTranslate> *listTr, char * path);

void ListAll(list<CTranslate> *);

void DelByEng(list<CTranslate> *listTr, const char *en);

void DelByIta(list <CTranslate> *listTr, const char *ital);

void SortByEng(list<CTranslate> *listTr);

void SortByIta(list<CTranslate> *listTr);

void TranslateEng(list<CTranslate> *listTr, const char *en);

void TranslateIta(list<CTranslate> *listTr, const char *ita);


