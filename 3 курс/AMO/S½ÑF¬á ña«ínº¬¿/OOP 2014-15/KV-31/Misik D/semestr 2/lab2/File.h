/******************************************************************************************
	name:					File.h
	description:			this files contains prototypes of methods of class File and
							some other prototypes
	author:					Dima
	date of creation:		12.01.2015
	written:				13.01.2015
	date of last change:	13.01.2015
******************************************************************************************/

#include <vector>

#pragma once

using namespace std;

class File{
public:
	//default constructor
	File();
	//copy constructor
	File(const File &);
	//destructor
	~File();
	void Empty();
	void Setdetails(const char *, int);
	void Setkeywords(const char*);
	void Deletekeywords();
	//overloaded assignment
	File & operator=(const File &);
	File & operator=(const char *);
	bool operator!=(const File &)const;
	char* operator+=(const char *);
	int Getsize()const{ return size; };
	void Setsize(int n) { size = n; };
	char *Getname() const{ return name; };
	int Getlengthkw() const{ return strlen(keywords); };
	int Getkeywordsnmb() const { return keywords_nmb; };
	char *Getkeywords() const{ return keywords; };
	void Changekeywordsnmb(int c) { keywords_nmb += c; };
	void Print()const;
	void ReadFromFile();
	void ReadFromFile(const char *, int);
	
private:
	char *name;//file name
	int size;//file length
	char *keywords;//set of keywords
	int keywords_nmb;//keywords number
};

void Listall(const vector<File> &f);
void Addkeyword(vector <File> &, char *);
void Delkeyword(vector<File> &, char *kwrd);
void Delkeywordbylen(vector<File> &, int len);
void Sortbysize(vector <File> &);
void Sortbykeywordsnumber(vector <File> &);