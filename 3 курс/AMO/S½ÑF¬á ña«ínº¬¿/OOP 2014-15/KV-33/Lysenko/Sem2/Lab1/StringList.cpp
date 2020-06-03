/*
	*File:StringList.cpp
	*autor:Lysenko Vitaliy
	*written:24.03.2015
	*last modified:25.03.2015
	*File consists functions? which declared in StringList.cpp
*/

#include "StringList.h"
#include <iostream>
#include <string.h>

using namespace std;

cString::cString(){
	str = NULL;
	length = 0;
}

cString::cString(const char *psz){
	//strcpy(str, psz);
	length = strlen(psz);
	str = (char*)malloc((length + 1) * sizeof(char));
	if (!str)
		return;
	strcpy(str, psz);

}
// copy constructor
cString::cString(const cString &stringsrc){
	length = stringsrc.length;
	str = (char*)malloc((length + 1) * sizeof(char));
	if (!str)
		return;
	str = stringsrc.str;

}

cString::~cString(){
	if (!str) free(str);
	length = 0;
}

int cString::Getlength() const {
	if (str) return strlen(str);
	return(NULL);
}

bool cString::Isempty() const {
	if (!str) return true;
	return false;
}

void cString::Print(){
	if (str) cout << str << "\n";
	else cout << "String is empty." << "\n";
}

void cString::Empty(){
	free(str);
//	str = (char*)malloc(sizeof(char));
	str = NULL;
	length = 0;
}

void cString::SetAt(int nindex, char ch){
	if (nindex > length) return;
	if (nindex < 0) return;
	str[nindex] = ch;
}

int cString::Compare(const cString &str2) const{
	return strcmp(str, str2.str);
}

int cString::Find(char ch) const{
	if (!str) return -1;
	int iterator = 0;
	while (iterator < length) {
		if (str[iterator] == ch) return iterator;
		++iterator;
	}
	return -1;
}

int cString::Find(char *pszsub) const{
	int lengthsub = strlen(pszsub);
	if (lengthsub > length || lengthsub < 0) return -1;
	int i, j;
	for (i = 0; i < length - lengthsub; i++){
		for (j = 0; j < lengthsub && pszsub[j] == str[j + i]; j++);
		if (j >= lengthsub) return i;
	}
	return -1;
}

cString cString::Mid(int nfirst, int ncount) const{
	int iterator, pos = 0;
	cString temp;
	if (!str) return temp;
	temp.str = (char*)malloc(ncount + 1);
	for (iterator = nfirst; iterator <= ncount + nfirst - 1 || iterator >= length; iterator++){
		temp.str[pos] = str[iterator];
		pos++;
	}
	temp.str[pos] = '\0';
	temp.length = ncount;
	return temp;
}

cString cString::Left(int ncount) const{
	cString temp;
	if (!str) return temp;
	int iterator, pos = 0;
	temp.str = (char*)malloc(ncount+1);
	for (iterator = 0; iterator <= ncount - 1 || iterator < length; iterator++){
		temp.str[pos] = str[iterator];
		pos++;
	}
	temp.str[pos] = '\0';
	temp.length = ncount;
	return temp;
}

cString cString::Right(int ncount) const{
	cString temp;
	if (!str) return temp;
	int iterator, pos = 0;
	temp.str = (char*)malloc(ncount + 1);
	for (iterator = length - ncount; iterator < length; iterator++){
		temp.str[pos] = str[iterator];
		pos++;
	}
	temp.str[pos] = '\0';
	temp.length = ncount;
	return temp;
}

cString& cString::operator=(const cString &stringsrc)
{
	if (this == &stringsrc)
		return *this;
	char *temp = (char*)malloc((stringsrc.length + 1) * sizeof(char));
	if (!temp)
		return *this;
	strcpy(temp, stringsrc.str);
	free(str);
	str = temp;
	length = stringsrc.length;
	return *this;
}

const cString& cString::operator=(const char* psz){
	if (!strlen(psz)) return *this;
	char *temp = (char*)malloc((strlen(psz) + 1) * sizeof(char));
	if (!temp)
		return *this;
	strcpy(temp, psz);
	free(str);
	str = temp;
	length = strlen(psz);
	return *this;
}

cString cString::operator +(const cString& string){
	char *temp = (char*)malloc((string.length + length + 1) * sizeof(char));
	if (!temp)
		return *this;
	strcpy(temp, str);
	strcat(temp, string.str);
	cString result(temp);
	return result;

}

cString& cString::operator +=(const cString& string){
	char *temp = (char*)malloc((string.length + length + 1) * sizeof(char));
	if (!temp)
		return *this;
	strcpy(temp, str);
	strcat(temp, string.str);
	free(str);
	str = temp;
	return *this;

}

char cString::operator [](int indx){
	char tmp = str[indx];
	return tmp;
}