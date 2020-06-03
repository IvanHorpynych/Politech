/*
	*File:testprog.cpp
	*autor:Lysenko Vitaliy
	*written:24.03.2015
	*last modified:25.03.2015
	*Program tests functions, which declared in StringList.h
*/

#include "StringList.h"
#include <iostream>

using namespace std;

int main(){
	cString test1, test2("abcde"), test3("abcdefg"), test4("abc"), test5("abc");
	int len1, len2;
	char elem = 'W';

	cout << "Testing Print" << "\n";
	test1.Print();
	test2.Print();
	cout << "\n";

	cout << "Testing GetLength" << "\n";
	len1 = test1.Getlength();
	len2 = test2.Getlength();
	cout << "Len 1 = " << len1 << "\n";
	cout << "Len 2 = " << len2 << "\n";
	cout << "\n";

	cout << "Testing IsEmpty" << "\n";
	cout << "Empty string: " << test1.Isempty() << "\n";
	cout << "Not empty string: " << test2.Isempty() << "\n";
	cout << "\n";

	cout << "Testing Empty" << "\n";
	test2.Empty();
	test2.Print();
	cout << "\n";

	cout << "Testing SetAt" << "\n";
	cout << "Before changing: ";
	test3.Print();
	test3.SetAt(2, elem);
	cout << "After changing: ";
	test3.Print();
	cout << "\n";

	cout << "Testing Compare: " << "\n";
	test4.Print();
	test5.Print();
	cout << "Result: " << test4.Compare(test5) << "\n";
	cout << "\n";

	cout << "Testing Find (by char): " << "\n";
	cout << "String: ";
	test3.Print();
	cout << "Find 'b': " << test3.Find('b') << "\n";
	cout << "Find 'R': " << test3.Find('R') << "\n";
	cout << "\n";

	cout << "Testing Find (by char*): " << "\n";
	cout << "String: ";
	test3.Print();
	cout << "Find 'de': " << test3.Find("de") << "\n";
	cout << "Find 'RG': " << test3.Find("RG") << "\n";
	cout << "\n";

	cout << "Testing Mid: " << "\n";
	cout << "String: ";
	test3.Print();
	int first = 1, count = 2;
	test1 = test3.Mid(first, count);
	cout << "Result (copy from " << first << ", " << count << " symbols): ";
	test1.Print();
	cout << "\n";


	cout << "Testing Left: " << "\n";
	cout << "String: ";
	test3.Print();
	count = 9;
	test1.Empty();
	test1 = test3.Left(count);
	cout << "Result (copy " << count << " symbols): ";
	test1.Print();
	cout << "\n"; 

	cout << "Testing Right: " << "\n";
	cout << "String: ";
	test3.Print();
	count = 3;
	test1.Empty();
	test1 = test3.Right(count);
	cout << "Result (copy " << count << " symbols): ";
	test1.Print();
	cout << "\n";

	cout << "Testing operator '=':" << "\n";
	cout << "Before: ";
	test1.Print();
	test1 = test3;
	cout << "After 'test1 = test3' : ";
	test1.Print();
	cout << "\n";

	cout << "Testing operator '=':" << "\n";
	cout << "Before: ";
	test1.Print();
	test1 = "abdfdsef";
	cout << "After 'test1 = 'some string' : ";
	test1.Print();
	cout << "\n";

	cout << "Testing operator '+':" << "\n";
	cout << "String 1: ";
	test4.Print();
	cout << "String 2: ";
	test5.Print();
	cout << "Result string after s1 + s2: ";
	test1 = test4 + test5;
	test1.Print();
	cout << "String 1 (after adding): ";
	test4.Print();
	cout << "String 2 (after adding): ";
	test5.Print();
	cout << "\n";

	cout << "Testing operator '+=':" << "\n";
	cout << "String 1: ";
	test4.Print();
	cout << "String 2: ";
	test5.Print();
	cout << "String 1 after s1 += s2: ";
	test4 += test5;
	test4.Print();
	cout << "String 2 (after adding): ";
	test5.Print();
	cout << "\n";

	cout << "Testing operator '[]':" << "\n";
	cout << "String 1: ";
	test4.Print();
	cout << "Output element with index 2 : ";
	cout << test4[2] << "\n";

}