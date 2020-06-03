/******************************************************************************************
	name:					Multistring.cpp
	description:			file contains declarations of methods of class Multistring
							it contains 3 constructors:
							Multistring() - creates empty vector
							Multistring(int n) - creates vector with n empty strings
							Multistring(const Multistring &) - copy constructor
							Empty() - cleans vector
							operator[](int index) - returns  copy of index string
							operator+=(const Multistring &) - concatenation of all strings
								of vector
							operator=(const Multistring &) - copy vector
							Mergemultistringexclusive(const Multistring &) - returns vector
								that include only ellements that meets once
							Setat(int nindex, const char* str) - set ta nindex copy of str
							Find(const char *pszsub) - returns index of pszsub in vector
								or -1, if ellement is absent
							Getlength() - returns length of vector
							Printstr(int nindex) - prints nindex string of vector
							Print() - print all ellements of vector
	author:					Dima
	date of creation:		12.01.2015
	written:				13.01.2015
	date of last change:	13.01.2015
******************************************************************************************/

#include "Multistring.h"
#include <string>

Multistring::Multistring() {
	buf = new char *[1];
	str_nmb = 1;
	buf[0] = new char[1];
	buf[0][0] = '\0';
}

Multistring::Multistring(int n) {
	buf = new char *[n];
	str_nmb = n;
	for (int i = 0; i < n; i++) {
		buf[i] = new char[1];
		buf[i][0] = '\0';
	}
}

Multistring::Multistring(const Multistring &old) {
	buf = new char *[old.str_nmb];
	str_nmb = old.str_nmb;
	for (int i = 0; i < str_nmb; i++) {
		buf[i] = new char[strlen(old.buf[i]) + 1];
		strcpy(buf[i], old.buf[i]);
	}
}

void Multistring::Empty() {
	if (!Isempty()) {
		for (int i = 0; i < str_nmb; i++) {
			delete[] buf[i];
		}
		delete[] buf;
		str_nmb = 0;
	}
}

char * Multistring::operator[] (int index) const {
	if (index < str_nmb) {
		char *s = new char[strlen(buf[index]) + 1];
		strcpy(s, buf[index]);
		return s;
	}
	return NULL;
}

Multistring & Multistring::operator+=(const Multistring &src) {
	int m;
	if (src.str_nmb < str_nmb) m = src.str_nmb;
	else m = str_nmb;
	for (int i = 0; i < m; i++) {
		char *s = new char[strlen(buf[i]) + strlen(buf[i]) + 1];
		s[0] = '\0';
		strcat(s, buf[i]);
		strcat(s, src.buf[i]);
		delete[] buf[i];
		buf[i] = s;
	}
	return *this;
}

Multistring & Multistring::operator=(const Multistring &ms) {
	int m = ms.str_nmb < str_nmb ? ms.str_nmb : str_nmb;
	Multistring *res = new Multistring(m);
	for (int i = 0; i < m; i++) {
		char *s = new char[strlen(ms.buf[i]) + 1];
		strcpy(s, ms.buf[i]);
		delete[] buf[i];
		buf[i] = s;
	}
	return *res;
}

Multistring Multistring::Mergemultistringexclusive(const Multistring &src) {
	int num = str_nmb + src.str_nmb;
	if (num != 0) {
		Multistring *temp = new Multistring(num);
		temp->str_nmb = num;
		*temp += *this;
		for (int i = 0; i < src.str_nmb; i++) temp->Setat(i + str_nmb, src.buf[i]);

		for (int i = 0; i < temp->str_nmb - 1; i++) {
			if (temp->buf[i][0] != '\0') {
				bool flag = false;
				for (int j = i + 1; j < temp->str_nmb; j++) if (strcmp(temp->buf[i], temp->buf[j]) == 0) {
					temp->buf[j][0] = '\0';
					num--;
					flag = true;
				}
				if (flag) {
					temp->buf[i][0] = '\0';
					num--;
				}
			}
		}
		if (num != 0) {
			Multistring *res = new Multistring(num);
			res->str_nmb = num;
			int in_res = 0, in_temp = 0;
			while (in_temp < temp->str_nmb) {
				if (temp->buf[in_temp][0] != '\0') {
					res->Setat(in_res, temp->buf[in_temp]);
					in_res++;
				}
				in_temp++;
			}
			return *res;
		}
	} 
	return Multistring();
}

void Multistring::Setat(int nindex, const char* str) {
	if (nindex < str_nmb) {
		char *s = new char[strlen(str) + 1];
		strcpy(s, str);
		buf[nindex] = s;
	}
}

Multistring::~Multistring() {
	Empty();
}

int Multistring::Find(const char *pszsub) const {
	for (int i = 0; i < str_nmb; i++) {
		if (strcmp(buf[i], pszsub) == 0) return i;
	}
	return -1;
}

int Multistring::Getlength() const {
	int n = 0;
	for (int i = 0; i < str_nmb; i++) {
		n += strlen(buf[i]);
	}
	return n;
}

void Multistring::Printstr(int nindex) const {
	printf("%s\n", buf[nindex]);
}

void Multistring::Print() const {
	for (int i = 0; i < str_nmb; i++) Printstr(i);
}