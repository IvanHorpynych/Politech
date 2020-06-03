/********************************
 * file: FString_definition.cpp
 * FormattedString class definition
 * written: 26/01/2015
 * Copyright (c) 2015 by Alex Volontyr
 ********************************/
#include <iostream>
#include "FormattedString.h"

Formattedstring::Formattedstring() { // creates an empty string
	str = new char[1];
	str[0] = '\0';
	len = strlen(str);
}

Formattedstring::Formattedstring(const char *src) { // creates string using standard C-string
	if (src != 0 && strlen(src) > 0) {
		str = new char[strlen(src) + 1];
		strcpy(str, src);
	}
	else str = 0;
	len = strlen(str);
}

Formattedstring::Formattedstring(const Formattedstring &source) { // copy constructor
	str = new char[source.len + 1];
	if (str)
		for (int i = 0; i <= source.len; i++) {
			str[i] = source.str[i];
		}
	len = strlen(str);
}

Formattedstring::~Formattedstring() { delete[] str; } // destructor

int Formattedstring::Getlength() const { return len; } // returns length of the string

bool Formattedstring::Isempty() const { // checks if the string is empty; returns true if it's empty and false otherwise
	if (len == 0) return true;
	else return false;
}

void Formattedstring::Empty() { // makes the string an empty one 
	delete[] str;
	str = new char[1];
	str[0] = '\0';
	len = 0;
}

void Formattedstring::Setat(int nindex, char ch) { // set a symbol 'ch' at position with number 'nindex'
	if (len > nindex && nindex >= 0) str[nindex] = ch;
}

int Formattedstring::Compare(const Formattedstring& s) const { // compares the string with string 's'
	int i = 0;
	if (len > s.len || len < s.len) return len - s.len;
	for (i; i < len && str[i] == s.str[i]; i++) {}
	return (int)str[i] - (int)s.str[i];
}

int Formattedstring::Find(char ch) const { // returns position of symbol 'ch' in the string or -1 if it's not found
	int i = 0;
	for (i; i < len && str[i] != ch; i++) {}
	if (i == len) i = -1;
	return i;
}

int Formattedstring::Find(char *pszsub) const { // returns position of 'pszsub' in the string or -1 if 'pszsub' isn't found in it
	int i = 0, j = 0, k = 0;
	while (j < len) {
		for (i; i < len && str[i] == pszsub[k]; i++) { k++; }
		if (!k) i++;
		if (k != strlen(pszsub)) k = 0;
		else return i - k;
		j = i;
	}
	return -1;
}

char Formattedstring::operator[](int nindex) const { // returns symbol that is at 'nindex' position int the string
	if (nindex >= 0 && nindex < len) return str[nindex];
}

// overloaded assignment operators
Formattedstring& Formattedstring::operator=(const Formattedstring& src) { 
	if (str != src.str) {
		delete[] str;
		str = new char[src.len + 1];
		for (int i = 0; i <= src.len; i++) {
			str[i] = src.str[i];
		}
		len = src.len;
	}
	return *this;
}

const Formattedstring& Formattedstring::operator=( const unsigned char* psz ) { 
	delete[] str;
	str = new char[strlen((char*)psz) + 1];
	for (int i = 0; i <= strlen((char*)psz); i++) {
		str[i] = psz[i];
	}
	len = strlen((char*)psz);
	return *this;
}

// adding operator 
Formattedstring Formattedstring::operator+(const Formattedstring& string) {
	Formattedstring tmp;
	tmp.str = new char[len + string.len + 1];
	for (int i = 0; i < len; i++) {
		tmp.str[i] = str[i];
	}
	for (int i = 0; i <= string.len; i++) {
		tmp.str[len + i] = string.str[i];
	}
	tmp.len = len + string.len + 1;
	return tmp;
}

// performs concatenation of two strings: this one and 'src'
Formattedstring& Formattedstring::operator+=(const Formattedstring& src) { 
	char *tmp = new char[len + 1];
	strcpy(tmp, str);
	delete[] str;
	str = new char[len + src.len + 1];
	strcpy(str, tmp);
	for (int i = 0; i <= src.len; i++) {
		str[len + i] = src.str[i];
	}
	len = strlen(str);
	delete[] tmp;
	return *this;
}

int Formattedstring::A2Int() const { // converts the string into integer
	int res = 0, ord = 1, i = 0;
	while (str[i] == ' ' || str[i] == '0') { i++; }
	if (str[i] == '-') { i++; }
	for (int j = i + 1; j < len && str[j] >= 48 && str[j] <= 57; j++) { ord *= 10; }
	for (int j = i; j < len && str[j] >= 48 && str[j] <= 57; j++) {
		res += ((int)str[j] - 48) * ord; 
		ord /= 10;
	}
	if (str[i - 1] == '-') { res = -res; }
	return res;
}

char* Formattedstring::Int2Str(int val) const { // fill the string with number 'val'
	int length = 0, tmp = val, i = 0;
	while ((tmp = (int)(tmp / 10)) != 0) { length++; }
	if (val < 0) { 
		length++;
		tmp = -val;
	} else tmp = val; 
	for (i; i <= length; i++) {
		str[length - i] = char(48 + tmp % 10);
		tmp /= 10;
	}
	if (val < 0) { str[0] = '-'; }
	str[length + 1] = '\0';
	return str;
}

void Formattedstring::Print() const { // prints the string
	std::cout << str << std::endl;
}

/*returns the FormattedString object that contains 'ncount' symbols copy 
starting with 'nfirst' one of this string*/ 
Formattedstring Formattedstring::Mid(int nfirst, int ncount) const { 
	Formattedstring tmp = *this;
	if (ncount == -1 || len < ncount - nfirst) { ncount = len - nfirst; }
	if (nfirst < 0 || nfirst > len - 1) ncount = 0;
	tmp.str = new char[ncount + 1];
	for (int i = 0; i < ncount; i++) {
		tmp.str[i] = str[i + nfirst];
	}
	tmp.str[ncount] = '\0';
	tmp.len = ncount;
	return tmp;
}

/*returns the FormattedString object that contains 'ncount' symbols copy 
starting with the first one of this string*/ 
Formattedstring Formattedstring::Left(int ncount) const {
	Formattedstring tmp = *this;
	if (len < ncount) { ncount = len; }
	if (ncount < 0) ncount = 0;
	tmp.str = new char[ncount + 1];
	for (int i = 0; i < ncount; i++) {
		tmp.str[i] = str[i];
	}
	tmp.str[ncount] = '\0';
	tmp.len = ncount;
	return tmp;
}

/*returns the FormattedString object that contains 'ncount' symbols copy 
starting with the last one of this string*/ 
Formattedstring Formattedstring::Right(int ncount) const {
	Formattedstring tmp = *this;
	if (len < ncount) { ncount = len; }
	if (ncount < 0) ncount = 0;
	tmp.str = new char[ncount + 1];
	for (int i = 0; i < ncount; i++) {
		tmp.str[i] = str[len - ncount + i];
	}
	tmp.str[ncount] = '\0';
	tmp.len = ncount;
	return tmp;
}

