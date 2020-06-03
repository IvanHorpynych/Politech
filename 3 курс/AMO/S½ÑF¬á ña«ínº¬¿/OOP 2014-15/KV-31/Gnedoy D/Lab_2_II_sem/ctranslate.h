// *** ctranslate.h ***

#pragma once

#include <list>
#include <iostream>
#include <string.h>
using namespace std;

#define nullptr 0

#pragma warning (disable: 4996)

class CTranslate {
public:
	CTranslate();
	CTranslate(const CTranslate &);
	~CTranslate();
	
	CTranslate & operator = (const CTranslate &);
	bool operator < (const CTranslate &);
	bool operator == (const CTranslate &);
	void MakePair(const char *, const char *);
	void Print(bool) const;
	char * GetEnglishWord() const;
	char * GetItalianWord() const;

	static void ListAll(list<CTranslate> &);
	static void DeleteByEnglishWord(list<CTranslate> &, const char *);
	static void DeleteByItalianWord(list<CTranslate> &, const char *);
	static void SortByEnglishWords(list<CTranslate> &);
	static void SortByItalianWords(list<CTranslate> &);
	static void TranslateEnglishWord(list<CTranslate> &, const char *);
	static void TranslateItalianWord(list<CTranslate> &, const char *);

private:
	void FreeMemory();
	void ReallocateMemory(const char *, const char *);

private:
	char * englishWord;
	char * italianWord;
};
