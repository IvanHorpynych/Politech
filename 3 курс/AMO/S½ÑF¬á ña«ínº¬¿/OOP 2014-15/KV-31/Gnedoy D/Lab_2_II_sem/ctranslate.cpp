// *** ctranslate.cpp ***

#include "ctranslate.h"

void CTranslate :: FreeMemory() {
	if(englishWord != nullptr)
		delete [] englishWord;
	if(italianWord != nullptr)
		delete [] italianWord;
	englishWord = italianWord = nullptr;
}

void CTranslate :: ReallocateMemory(const char * englishWord, const char * italianWord) {
	FreeMemory();
	this->englishWord = new char [strlen(englishWord) + 1];
	this->italianWord = new char [strlen(italianWord) + 1];
	strcpy(this->englishWord, englishWord);
	strcpy(this->italianWord, italianWord);
}

CTranslate :: CTranslate() {
	englishWord = nullptr;
	italianWord = nullptr;
}

CTranslate :: CTranslate(const CTranslate & translateObject) {
	englishWord = italianWord = nullptr;
	ReallocateMemory(translateObject.englishWord, translateObject.italianWord);
}

CTranslate :: ~CTranslate() {
	FreeMemory();
}

CTranslate & CTranslate :: operator = (const CTranslate & translateObject) {
	if(* this == translateObject)
		return (* this);
	ReallocateMemory(translateObject.englishWord, translateObject.italianWord);
	return (* this);
}

bool CTranslate :: operator < (const CTranslate & translateObject) {
	return strcmp(englishWord, translateObject.englishWord) < 0;
}

bool CTranslate :: operator == (const CTranslate & translateObject) {
	return (strcmp(englishWord, translateObject.englishWord) == 0) &&
		(strcmp(italianWord, translateObject.italianWord) == 0);
}

void CTranslate :: MakePair(const char * englishWord, const char * italianWord) {
	ReallocateMemory(englishWord, italianWord);
}

void CTranslate :: Print(bool putEndLineSymbol = true) const {
	cout << englishWord << "\t" << italianWord ;
	if(putEndLineSymbol)
		cout << endl;
}

char * CTranslate :: GetEnglishWord() const {
	return englishWord;
}

char * CTranslate :: GetItalianWord() const {
	return italianWord;
}

void CTranslate :: ListAll(list<CTranslate> & translateObjectsList) {
	list<CTranslate>::iterator listIterator = translateObjectsList.begin();
	while(listIterator != translateObjectsList.end()) {
		listIterator->Print();
		listIterator++;
	}
}

void CTranslate :: DeleteByEnglishWord(list<CTranslate> & translateObjectsList, const char * englishWord) {
	list<CTranslate>::iterator listIterator = translateObjectsList.begin();
	while(listIterator != translateObjectsList.end()) {
		list<CTranslate>::iterator nextElementIterator = listIterator;
		nextElementIterator++;
		if(! strcmp(listIterator->englishWord, englishWord))
			translateObjectsList.remove(* listIterator);
		listIterator = nextElementIterator;
	}
}

void CTranslate :: DeleteByItalianWord(list<CTranslate> & translateObjectsList, const char * italianWord) {
	list<CTranslate>::iterator listIterator = translateObjectsList.begin();
	while(listIterator != translateObjectsList.end()) {
		list<CTranslate>::iterator nextElementIterator = listIterator;
		nextElementIterator++;
		if(! strcmp(listIterator->italianWord, italianWord))
			translateObjectsList.remove(* listIterator);
		listIterator = nextElementIterator;
	}
}

void CTranslate :: SortByEnglishWords(list<CTranslate> & translateObjectsList) {
	translateObjectsList.sort();
}

bool CompareTranslateObjectsByItalianWord(CTranslate & firstObject, CTranslate & secondObject) {
	return strcmp(firstObject.GetItalianWord(), secondObject.GetItalianWord()) < 0;
}

void CTranslate :: SortByItalianWords(list<CTranslate> & translateObjectsList) {
	translateObjectsList.sort(CompareTranslateObjectsByItalianWord);
}

void CTranslate :: TranslateEnglishWord(list<CTranslate> & translateObjectsList, const char * englishWord) {
	list<CTranslate>::iterator listIterator = translateObjectsList.begin();
	int wordsFound = 0;
	while(listIterator != translateObjectsList.end()) {
		if(! strcmp(listIterator->englishWord, englishWord)) {
			if(! wordsFound)
				cout << englishWord << "\t\t" << listIterator->italianWord << endl;
			else
				cout << "\t\t" << listIterator->italianWord << endl;
			wordsFound++;
		}
		listIterator++;
	}
}

void CTranslate :: TranslateItalianWord(list<CTranslate> & translateObjectsList, const char * italianWord) {
	list<CTranslate>::iterator listIterator = translateObjectsList.begin();
	int wordsFound = 0;
	while(listIterator != translateObjectsList.end()) {
		if(! strcmp(listIterator->italianWord, italianWord)) {
			if(! wordsFound)
				cout << italianWord << "\t" << listIterator->englishWord << endl;
			else
				cout << "\t\t" << listIterator->englishWord << endl;
			wordsFound++;
		}
		listIterator++;
	}
}