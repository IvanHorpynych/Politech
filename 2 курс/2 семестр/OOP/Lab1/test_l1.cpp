#include "cString.h"


int main()
{
	cString* s1 = new cString();
	const cString s2("qwerty");
	cString test;

	cout << "Now string \"s1\" is:";
	if (s1->IsEmpty() == true) cout << "empty" << endl << endl;
	else cout << "not empty" << endl << endl;

	cout << "New test string: ";
	*s1 = "test";
	s1->Print();
	cout << "\nNow string s1 is:";
	if (s1->IsEmpty() == true) cout << "empty" << endl;
	else cout << "not empty" << endl;

	cout << "\nTest Empty:" << endl;
	s1->Empty();
	cout << "Now string \"s1\" is: ";
	if (s1->IsEmpty() == true) cout << "empty" << endl << endl;
	else cout << "not empty" << endl << endl;

	cout << "Test operator \"=\"";
	*s1 = s2;
	cout << "\nNow string s1 is: ";
	s1->Print();

	cout << "\nTest \"Right\": ";
	test = s1->Right(3);
	test.Print();
	test.Empty();

	cout << "\nTest \"Left\": ";
	test = s1->Left(6);
	test.Print();
	test.Empty();

	cout << "\nTest \"Mid\":";
	test = s1->Mid(1, 2);
	test.Print();

	cout << "\nTest \"SetAt\": " << endl;
	cout << "Now string \"s1\" is: ";
	s1->Print();
	s1->SetAt(2, 'Q');
	cout << "After \"SetAt\" string \"s1\": ";
	s1->Print();

	cout << "\nTest \"Compare\": ";
	cout << s1->Compare(s2) << endl;

	cout << "\nTest operator \"+=\": " << endl;
	cout << "Test string: ";
	test.Print();
	test += *s1;
	cout << "Now test string: ";
	test.Print();

	cout << "\nTest \"Find\":" << endl;
	cout << "Posicion simbols 'e' in \"s1\": ";
	cout << s1->Find('e');

	cout << "\n\nTest \"Find(string)\"" << endl;
	cout << "Posicion first simbols string \"rt\" in \"s1\":";
	cout << s1->Find("rt");

	cout << "\n\nTest operator \"+\":" << endl;
	cString test2;
	test2 = test + s2;
	test2.Print();

	return 0;
}