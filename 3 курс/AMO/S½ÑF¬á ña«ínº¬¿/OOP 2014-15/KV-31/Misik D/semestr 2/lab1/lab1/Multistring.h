/******************************************************************************************
	name:					Multistring.h
	description:			header file that contains prototypes of functions
							that descriped in file "Multistring.cpp"
	author:					Dima
	date of creation:		12.01.2015
	written:				13.01.2015
	date of last change:	13.01.2015
******************************************************************************************/

#pragma once

class Multistring{
public:
	//constructors destructor
	Multistring();
	Multistring(int);
	Multistring(const Multistring &);
	~Multistring();

	//methods
	Multistring & operator=(const Multistring &ms);
	char * operator[] (int nindex) const;
	Multistring & operator+=(const Multistring &);
	Multistring Mergemultistringexclusive(const Multistring &);
	int Find(const char *pszsub) const;
	int Getlength() const;
	bool Isempty()const{ return str_nmb == 0; };
	void Empty();
	void Setat(int nindex, const char* str);
	void Printstr(int nindex) const;
	void Print() const;
private:
	//attributes
	char **buf;//pointer to vector
	int str_nmb;//strings number
};
