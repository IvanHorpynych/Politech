/**********************************************
* file: CTranslate.h
* CTranslate class testing
* written: 10/04/2015
* by Gnedoy Dmitry
***********************************************/
#include "CTranclate.h"

int main(){
	vector<CTranslate> ourArray;
	readFromCSV(ourArray, "input.csv");
	//SortByEng(ourArray);
	//SortByIta(ourArray);
	ListAll(ourArray);
	//DelByEng(ourArray, "lazy");
	cout << endl;
	//ListAll(ourArray);
	TranslateIta(ourArray, "pronto");
	//TranslateEng(ourArray, "lazy");
	return 0;
}