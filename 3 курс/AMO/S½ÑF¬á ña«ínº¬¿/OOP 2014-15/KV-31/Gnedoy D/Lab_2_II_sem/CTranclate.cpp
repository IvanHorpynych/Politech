/************************************************************
* file: CTranslate.cpp
* CTranslate class realization
* written: 10/04/2015
* by Gnedoy Dmitry
*************************************************************/
#define _CRT_SECURE_NO_WARNINGS
#include "CTranclate.h"

CTranslate::CTranslate(){
	eng = NULL;
	ita = NULL;
}

CTranslate::CTranslate(const CTranslate &tr){
	*this = tr;
}

CTranslate::~CTranslate(){
	free(eng);
	free(ita);
}

CTranslate& CTranslate::operator=(const CTranslate &tr){
	//delete
	this->MakePair(tr.GetEng(), tr.GetIta());
	return *this;
}

bool CTranslate::operator<(const CTranslate &tr){
	if (strcmp(this->GetEng(), tr.GetEng()) < 0)
		return true;
	return false;
}

void CTranslate::MakePair(char *eng, char *ita){
	char* engTmp = (char*)malloc(sizeof(char*)*strlen(eng) + 1);
	char* itaTmp = (char*)malloc(sizeof(char*)*strlen(ita) + 1);
	strcpy(engTmp, eng);
	strcpy(itaTmp, ita);
	this->eng = engTmp;
	this->ita = itaTmp;
}

void CTranslate::Print()const{
	if (eng == NULL || ita == NULL){
		printf("Error!\n");
		return;
	}
	printf("English: %s, Italic: %s;\n", eng, ita);
}

char* CTranslate::GetEng()const{
	if (eng != NULL) return eng;
	return "-";
}

char* CTranslate::GetIta()const{
	if (ita != NULL) return ita;
	return "-";
}

//other functions
void readFromCSV(vector <CTranslate> &tr, char* fileName){
	fstream F;
	char* a = (char*)malloc(255);
	F.open(fileName);
	if (F){
		while (!F.eof()){
			F >> a;
			int i = 0;
			for (; a[i] != ';'; ++i);
			int j = i+1;
			for (; a[j] != ';'; ++j);
			char* engTmp = (char*)malloc(i+1);
			char* itaTmp = (char*)malloc(j-i);
			strncpy(engTmp, a, i);
			engTmp[i] = '\0';
			strncpy(itaTmp, a+i+1, j-i-1);
			itaTmp[j-i-1] = '\0';
			CTranslate *tmp = new CTranslate();
			tmp->MakePair(engTmp, itaTmp);
			tr.push_back(*tmp);
		}
		F.close();
	}
}

void ListAll(const vector<CTranslate> &tr){
	if (tr.empty()) cout << "Conteiner is empty" << endl;
	for (int i = 0; i < tr.size(); ++i)
		tr[i].Print();
}

void DelByEng(vector <CTranslate> &tr, const char *eng){
	for (int i = 0; i < tr.size();){
		if (strcmp(tr[i].GetEng(), eng) == 0){
			tr.erase(tr.begin() + i);
			continue;
		}
		++i;
	}
}

void DelByIta(vector <CTranslate> &tr, const char *ita){
	for (int i = 0; i < tr.size();){
		if (strcmp(tr[i].GetIta(), ita) == 0){
			tr.erase(tr.begin() + i);
			continue;
		}
		++i;
	}
}

void SortByEng(vector <CTranslate> &tr){
	sort(tr.begin(), tr.end());
}

void SortByIta(vector <CTranslate> &tr){
	sort(tr.begin(), tr.end(), CompareIta);
}

void TranslateEng(const vector <CTranslate> &tr, const char *eng){
	cout << eng << ": ";
	for (int i = 0; i < tr.size(); ++i)
	if (strcmp(tr[i].GetEng(), eng) == 0)
		cout << tr[i].GetIta() << "; ";
	cout << endl;
}

void TranslateIta(const vector <CTranslate> &tr, const char *ita){
	cout << ita << ": ";
	for (int i = 0; i < tr.size(); ++i)
		if (strcmp(tr[i].GetIta(), ita) == 0)
			cout << tr[i].GetEng() << "; ";
	cout << endl;
}

bool CompareIta(const CTranslate &tr, const CTranslate &tr2){
	if (strcmp(tr.GetIta(), tr2.GetIta()) < 0)
		return true;
	else return false;
}