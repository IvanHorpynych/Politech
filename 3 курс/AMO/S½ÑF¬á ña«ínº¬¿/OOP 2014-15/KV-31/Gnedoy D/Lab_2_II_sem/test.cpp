// *** test.cpp ***

#include "ctranslate.h"
#include <fstream>
#include <vector>

void Test() {
	list<CTranslate> translateObjectList;
	ifstream input("input.csv");
	while(! input.eof()) {
		char englishWord[256] = {0}, italianWord[256] = {0};
		input >> englishWord >> italianWord;
		CTranslate translateObject;
		translateObject.MakePair(englishWord, italianWord);
		translateObjectList.push_back(translateObject);
	}
	cout << "   *** loaded data ***\n";
	CTranslate :: ListAll(translateObjectList);
	
	CTranslate :: DeleteByEnglishWord(translateObjectList, "lazy");
	cout << "\n   *** 'lazy' english word was deleted *** \n";
	CTranslate :: ListAll(translateObjectList);
	
	cout << "\n   *** 'pronto' and 'runo' italian words were deleted *** \n";
	CTranslate :: DeleteByItalianWord(translateObjectList, "pronto");
	CTranslate :: DeleteByItalianWord(translateObjectList, "runo");
	CTranslate :: ListAll(translateObjectList);

	cout << "\n   *** list was sorted by english words ***\n";
	CTranslate :: SortByEnglishWords(translateObjectList);
	CTranslate :: ListAll(translateObjectList);

	cout << "\n   *** list was sorted by italian words ***\n";
	CTranslate :: SortByItalianWords(translateObjectList);
	CTranslate :: ListAll(translateObjectList);

	cout << "\n   *** 'jump' english word translation ***\n";
	CTranslate :: TranslateEnglishWord(translateObjectList, "jump");

	cout << "\n   *** 'trasalire' italian word translation ***\n";
	CTranslate :: TranslateItalianWord(translateObjectList, "trasalire");
}