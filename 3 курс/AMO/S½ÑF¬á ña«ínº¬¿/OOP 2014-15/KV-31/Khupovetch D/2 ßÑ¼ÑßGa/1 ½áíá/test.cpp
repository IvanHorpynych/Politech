#include "cString.h"

int main()
{
	cout << "создание объекта:" << endl << endl;
	cString obj1, obj2("Dima_kpi"), obj3(obj2);
	obj1.Print();
	obj2.Print();
	obj3.Print();
	cout << "\a----------------------\n";
	
	
	cout << "GetLength:" << endl << endl;
	cout << obj1.GetLength() << endl;
	cout << obj2.GetLength() << endl;
	cout << obj3.GetLength() << endl;
	cout << "\a----------------------\n";
	
	
	cout << "IsEmpty:" << endl << endl;
	cout << obj1.IsEmpty() << endl;
	cout << obj2.IsEmpty() << endl;
	cout << obj3.IsEmpty() << endl;
	cout << "\a----------------------\n";
	
	
	cout << "Empty:" << endl << endl;
	obj3.Empty();
	obj3.Print();
	cout << "\a----------------------\n";
	
	
	cout << "setAt:" << endl << endl;
	obj2.SetAt(2, 'p');
	obj2.Print();
	cout << "\a----------------------\n";
	
	
	cout << "Compare:" << endl << endl;
	cout << obj1.Compare(obj2) << endl;
	cout << "\a----------------------\n";
	
	
	cout << "Find char:" << endl << endl;
	cout << obj2.Find('y') << endl;
	cout << "Find string:" << endl << endl;
	cout << obj2.Find("_kpi") << endl;
	cout << "\a----------------------\n";
	
	
	cout << "Mid:" << endl << endl;
	cString obj4 = obj2.Mid(2,5);
	obj4.Print();
	cout << "\a----------------------\n";
	
	
	cout << "Left:" << endl << endl;
	cString obj5 = obj2.Left(5);
	obj5.Print();
	cout << "\a----------------------\n";
	
	
	cout << "Right:" << endl << endl;
	cString obj6 = obj2.Right(2);
	obj6.Print();
	cout << "\a----------------------\n";
	
	
	cout << "оператор[]:" << endl << endl;
	cout << obj2[1] << endl;
	cout << "\a----------------------\n";
	
	
	cout << "const char *" << endl << endl;
	obj1.Print();
	obj1 = "cat";
	obj1.Print();
	cout << "\a----------------------\n";
	
	cout << "const char *" << endl << endl;
	obj1.Print();
	obj2 = obj1;
	obj2.Print();
	cout << "\a----------------------\n";

	cout << "+" << endl << endl;
	cString obj7 = obj2 + obj1;
	obj7.Print();
	cout << "\a----------------------\n";
	
	
	cout << "+=" << endl << endl;
	obj1 += obj2;
	obj1.Print();
	obj1.~cString();
	obj2.~cString();
	obj3.~cString();
	obj4.~cString();
	obj5.~cString();
	obj6.~cString();
	//obj7.~cString();
	_getch();

}