/********************************
 * file: FormattedString.h
 * FormattedString class declaration
 * written: 26/01/2015
 * Copyright (c) 2015 by Alex Volontyr
 ********************************/
#pragma once
class Formattedstring{
private:
	char *str; // buffer
	int len; //string length
public:
	Formattedstring();
	Formattedstring(const char *src);
	Formattedstring(const Formattedstring &);
	~Formattedstring();
	//methods
	int Getlength() const;
	bool Isempty() const;
	void Empty();
	void Setat(int nindex, char ch);
	int Compare(const Formattedstring& s ) const;
	int Find(char ch ) const;
  	int Find(char *pszsub ) const;
	char operator[](int nindex) const;
	Formattedstring & operator=(const Formattedstring &);
	const Formattedstring& operator=(const unsigned char* psz);
	Formattedstring & operator+=(const Formattedstring &);
	Formattedstring operator+(const Formattedstring& string);
	int A2Int() const; 
	char* Int2Str(int) const;
	void Print() const;
	Formattedstring Mid(int nfirst, int ncount = -1) const;
	Formattedstring Left(int ncount) const;
	Formattedstring Right(int ncount) const;
};

