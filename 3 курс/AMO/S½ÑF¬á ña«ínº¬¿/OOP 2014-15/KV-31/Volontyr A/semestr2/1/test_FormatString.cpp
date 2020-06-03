/***********************************************************************
*file: test_FormatString.cpp
*synopsis: This file is used to test the functions located in file "FString_definition.cpp"
*and declared in file "FormattedString.h"
*author: Alex Volontyr
*written: 26/01/2015
************************************************************************/
#include <iostream>
#include "FormattedString.h"

using namespace std;

int main() {
	Formattedstring a, d;
	Formattedstring b("00 -17c5.0");
	Formattedstring c = b;
	a = c;
	cout << "'a' before:   ";
	a.Print();
	cout << "'b' before:   ";
	b.Print();
	b.Setat(3, '0');
	cout << "'b' after change:   ";
	b.Print();
	cout << "'c' before:   ";
	c.Print();
	cout << "Length 'a' = " << a.Getlength() << endl
		<< "Is 'a' empty (1 - true, 0 - false)?  Answer: " << a.Isempty() << endl
		<< "'b' to int = " << b.A2Int() << endl
		<< "'c' contains the number: " << c.Int2Str(51) << endl;
	a += c;
	cout << "'a' after concatination with 'c':	 ";
	a.Print();
	cout << "Length 'a' = " << a.Getlength() << endl;
	cout << "'c' after convertion of a number:   ";
	c.Print();
	d = b.Right(3);
	cout << "'d':   ";
	d.Print();
	cout << "'b':   ";
	b.Print();

	d = "class";
	cout << "result of comparing of 'b' and 'a': " << b.Compare(a) << endl
		<< "searching of a symbol in 'b': " << b.Find('7') << endl
		<< "searching of a string in 'b': " << b.Find("17") << endl
		<< "second symbol in 'b' is: " << b[1] << endl
		<< "'d' after modifying: ";
	d.Print();
	b = d + b;
	cout << endl << "'d' + 'b': ";
	b.Print();
}