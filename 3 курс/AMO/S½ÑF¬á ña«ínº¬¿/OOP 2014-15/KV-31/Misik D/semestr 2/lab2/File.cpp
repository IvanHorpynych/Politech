/******************************************************************************************
	name:					File.cpp
	description:			this file contains methods of class File
							File() - default constructor
							File(const File) - copy constructor
							~File() - destructor
							Empty() - delete name and keywords of File
							Setdetails(char *, int) - set name of File and size
							Deletekeywords() - delete keywords from class File
							operator=(const File &) - assigning class File to another
							operator!=(const File &) - return true, if Files are 
							the same
							operator+=(const char *) - add string to keyword
							Print() - print information about File
							ReadFromFile() - read keywords from file name
							Listall - prints all Files of vector
							Addkeyword - add keyword in all Files of vector
							Delkeywrd - delete string from all Files of vector
							Delkeywordbylen - delete all strings of keyword that
							are bigger than n
	author:					Dima
	date of creation:		12.01.2015
	written:				13.01.2015
	date of last change:	13.01.2015
******************************************************************************************/

#include "File.h"
#include <string>
#include <stdio.h>

File::File() {
	name = new char[1];
	name[0] = '/0';
	size = 0;
	keywords = new char[1];
	keywords_nmb = 0;
}

File::File(const File &f) {
	name = new char[strlen(f.name) + 1];
	strcpy(name, f.name);
	size = f.size;
	keywords = new char[strlen(f.keywords) + 1];
	strcpy(keywords, f.keywords);
	keywords_nmb = f.keywords_nmb;
}

File::~File() {
	Empty();
}

void File::Empty() {
	delete[] name;
	size = 0;
	delete[] keywords;
	keywords_nmb = 0;
}

void File::Setdetails(const char *new_name, int n) {
	Empty();
	name = new char[strlen(new_name) + 1];
	strcpy(name, new_name);
	size = n;
}

void File::Setkeywords(const char *kw) {
	if (strlen(kw) <= size) {
		keywords = new char[size];
		strcpy(keywords, kw);
		int sz = strlen(keywords);
		keywords_nmb = 1;
		for (int i = 0; i < sz; i++)
			if (keywords[i] == ';') keywords_nmb++;
	} else printf("Error in method Setkeywords: kw to large");
}

void File::Deletekeywords() {
	delete[] keywords;
	keywords_nmb = 0;
}

File & File::operator=(const File &f) {
	if (this != &f) {
		Setdetails(f.name, f.size);
		Setkeywords(f.keywords);
	}
	return *this;
}

File & File::operator=(const char *kw) {
	Setkeywords(kw);
	return *this;
}

bool File::operator!=(const File &f)const {
	if (strcmp(name, f.name) == 0 && size == f.size) return false;
	return true;
}

char *File::operator+=(const char *kw) {
	if (size > Getlengthkw() + strlen(kw)) {
		strcat(keywords, ";");
		strcat(keywords, kw);
		keywords_nmb++;
	}
	return keywords;
}

void File::Print() const{
	printf("Name - %s\n", name);
	printf("Size - %d\n", size);
	printf("Keywords - %s\n", keywords);
	printf("Keywords number - %d\n\n", keywords_nmb);
}

void File::ReadFromFile(){
	FILE *f;
	f = fopen(name, "r");
	char *buf = new char[strlen(name) + 6];
	fread(buf, sizeof(char), strlen(name) + 6, f);
	buf[strlen(name) + 5] = '\0';
	size = atoi(buf + strlen(name) + 1);
	delete[] buf;
	buf = new char[size];
	fscanf(f, "%s", buf);
	Setkeywords(buf);
	delete[] buf;
	fclose(f);
}

void File::ReadFromFile(const char *new_name, int sz) {
	Setdetails(new_name, sz);
	ReadFromFile();
}

void Listall(const vector<File> &f) {
	for (int i = 0; i < f.size(); i++) f[i].Print();
}

void Addkeyword(vector <File> &f, char *kw) {
	for (int i = 0; i < f.size(); i++)
		f[i] += kw;
}

void Delkeyword(vector<File> &f, char *kwrd) {
	for (int i = 0; i < f.size(); i++) {
		bool flag;
		do {
			char* j = strstr(f[i].Getkeywords(), kwrd);
			flag = false;
			if (j != NULL) {
				char* k = j + strlen(kwrd);
				if (f[i].Getkeywords() == j) strcpy(j, k + 1);
				else strcpy(j - 1, k);
				flag = true;
				f[i].Changekeywordsnmb(-1);
			}
		} while (flag);
	}
}

void Delkeywordbylen(vector<File> &f, int len) {
	for (int i = 0; i < f.size(); i++) {
		char *str = f[i].Getkeywords();
		bool flag = true;
		do {
			char *temp = strstr(str, ";");
			if (temp == NULL) {
				temp = str + strlen(str);
				flag = false;
			}
			char tempor = temp[0];
			temp[0] = '\0';
			if (strlen(str) > len) {
				char *del = new char[strlen(str) + 1];
				strcpy(del, str);
				temp[0] = tempor;
				Delkeyword(f, del);
			} else {
				temp[0] = tempor;
				str = temp + 1;
			}
			
		} while (flag);
	}
}

void Sortbysize(vector <File> &f) {
	for (int i = 0; i < f.size() - 1; i++) {
		File min = f[i];
		int indmin = i;
		for (int j = i + 1; j < f.size(); j++) {
			char *Min = min.Getname();
			char *Str = f[j].Getname();
			if (strcmp(Min, Str) > 0) {
				min = f[j];
				indmin = j;
			}
		}
		File temp = min;
		f[indmin] = f[i];
		f[i] = temp;
	}
}

void Sortbykeywordsnumber(vector <File> &f) {
	for (int i = 0; i < f.size() - 1; i++) {
		File min = f[i];
		int indmin = i;
		for (int j = i + 1; j < f.size(); j++) {
			int Min = min.Getkeywordsnmb();
			int Str = f[j].Getkeywordsnmb();
			if (Min > Str) {
				min = f[j];
				indmin = j;
			}
		}
		File temp = min;
		f[indmin] = f[i];
		f[i] = temp;
	}
}