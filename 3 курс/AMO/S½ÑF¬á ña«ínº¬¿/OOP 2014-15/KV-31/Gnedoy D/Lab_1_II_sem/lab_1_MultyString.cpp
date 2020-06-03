/************************************************************************
*file: lab_1_MultyString.cpp
*author: Gnedoj D.
*written: 11/02/2015
*last modified: 11/02/2015
*************************************************************************/
#define _CRT_SECURE_NO_WARNINGS
#include "lab_1_MultyString.h"

MultiString::MultiString(){
	buf = NULL;
	str_nmb = 0;
}

MultiString::MultiString(int n){
	buf = new char*[n];
	str_nmb = 0;
	for (int i = 0; i < n; i++){
		buf[i] = NULL;
	}
}

MultiString::MultiString(const MultiString & someString){
	*this = someString;
}

MultiString::~MultiString(){
	delete []buf;
	str_nmb = 0;
}

MultiString& MultiString::operator=(const MultiString &ms){
	this->Empty();
	this->str_nmb = ms.str_nmb;
	this->buf = new char*[this->str_nmb];
	for (int i = 0; i < ms.str_nmb; i++){
		this->buf[i] = NULL;
		this->buf[i] = (char*)calloc(sizeof(char), strlen(ms.buf[i]) + 1);
		strcpy(this->buf[i], ms.buf[i]);
	}
	return *this;
}

char* MultiString::operator[](int nIndex){
	if (nIndex < str_nmb){
		char * newBuf;
		newBuf = (char*)calloc(sizeof(char), strlen(buf[nIndex]) + 1);
		strcpy(newBuf, buf[nIndex]);
		return newBuf;
	}
	return NULL;
}

MultiString & MultiString::operator+=(const MultiString &src){
	char** temp = NULL;
	if (this->str_nmb == src.str_nmb){
		temp = new char*[src.str_nmb];
		char* str_help = NULL;
		for (int i = 0; i < src.str_nmb; i++){
			str_help = (char*)calloc(sizeof(char), strlen(this->buf[i]) + strlen(src.buf[i]) + 1);
			strcpy(str_help,this->buf[i]);
			strcat(str_help, src.buf[i]);
			temp[i] = str_help;
		}
		this->Empty();
		this->buf = temp;
		this->str_nmb = src.str_nmb;
		return *this;
	}
	if(this->str_nmb < src.str_nmb){
		temp = new char*[src.str_nmb];
		char* str_help = NULL;
		for (int i = 0; i < this->str_nmb; i++){
			str_help = (char*)calloc(sizeof(char), strlen(this->buf[i]) + strlen(src.buf[i]) + 1);
			strcpy(str_help,this->buf[i]);
			strcat(str_help, src.buf[i]);
			temp[i] = str_help;
		}
		for (int i = this->str_nmb; i < src.str_nmb; i++){
			str_help = (char*)calloc(sizeof(char), strlen(src.buf[i]) + 1);
			strcpy(str_help,src.buf[i]);
			temp[i] = str_help;
		}
		this->Empty();
		this->buf = temp;
		this->str_nmb = src.str_nmb;
		return *this;
	}
	temp = new char*[this->str_nmb];
	char* str_help = NULL;
	for (int i = 0; i < src.str_nmb; i++){
		str_help = (char*)calloc(sizeof(char), strlen(this->buf[i]) + strlen(src.buf[i]) + 1);
		strcpy(str_help,this->buf[i]);
		strcat(str_help, src.buf[i]);
		temp[i] = str_help;
	}
	for (int i = src.str_nmb; i < this->str_nmb; i++){
		str_help = (char*)calloc(sizeof(char), strlen(this->buf[i]) + 1);
		strcpy(str_help,this->buf[i]);
		temp[i] = str_help;
	}
	int tmp = this->str_nmb;
	this->Empty();
	this->buf = temp;
	this->str_nmb = tmp;
	return *this;
}

void MultiString::Add(const char *str){
	if (str_nmb > 0){
		char **temp;
		temp = new char*[str_nmb+1];
		for (int i = 0; i < str_nmb + 1; i++){
			temp[i] = NULL;
		}
		for (int i = 0; i < str_nmb; i++){
			if (buf[i] == NULL) continue;
			temp[i] = (char*)calloc(sizeof(char), strlen(buf[i]) + 1);
			strcpy(temp[i], buf[i]);
		}
		delete[] buf;
		buf = temp;
	}
	buf[str_nmb] = (char*)calloc(sizeof(char), strlen(str)+1);
	strcpy(buf[str_nmb], str);
	str_nmb += 1;
}

MultiString MultiString::MergeMultiStringExclusive(const MultiString &src){
	MultiString *result = new MultiString();
	bool isThere = false;

	result->Add(this->buf[0]);
	for (int i = 1; i < this->str_nmb; i++){
		for (int j = 0; j < result->str_nmb; j++){
			if (!strcmp(this->buf[i], result->buf[j])){
				isThere = true;
				break;
			}
		}
		if (!isThere){
			result->Add(this->buf[i]);
		}
		isThere = false;
	}
	for (int i = 0; i < src.str_nmb; i++){
		for (int j = 0; j < result->str_nmb; j++){
			if (!strcmp(result->buf[j], src.buf[i])){
				isThere = true;
			}
		}
		if (!isThere){
			result->Add(src.buf[i]);
		}
		isThere = false;
	}
	return *result;
}

int MultiString::Find(const char *pszSub) const{
	if (!IsEmpty()){
		for (int i = 0; i < str_nmb; i++){
			if (!strcmp(buf[i], pszSub)){
				return i;
			}
		}
	}
	return -1;
}

int MultiString::GetLength() const{
	return str_nmb;
}

bool MultiString::IsEmpty() const{
	if (str_nmb == 0){
		return true;
	}
	return false;
}

void MultiString::Empty(){
	if (!IsEmpty()){
		for (int i = 0; i < str_nmb; i++){
			free(buf[i]);
		}
		str_nmb = 0;
	}
}

void MultiString::SetAt(int nIndex, const char* str){
	if (nIndex < str_nmb){
		free(buf[nIndex]);
		buf[nIndex] = (char*)calloc(sizeof(char), strlen(str) + 1);
		strcpy(buf[nIndex], str);
	}else{
		//printf("Error! \n");
	}
}

void MultiString::PrintStr(int nIndex) const{
	if (nIndex < str_nmb){
		printf("Srting with index %d : %s \n", nIndex, buf[nIndex]);
	}else{
		printf("Error! \n");
	}
}
