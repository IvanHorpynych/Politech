#include "cString.h"

int main()
{
	cout << "Create 3 objects:" << endl;
	cString obj1, obj2("something"), obj3(obj2);
	obj1.Print();
	obj2.Print();
	obj3.Print();
	cout << endl;
	
	
	cout << "Show length of objects:" << endl;
	cout << obj1.GetLength() << endl;
	cout << obj2.GetLength() << endl;
	cout << obj3.GetLength() << endl;
	cout << endl;
	
	cout << "IsEmpty():" << endl;
	cout << (obj1.IsEmpty() ? "true" : "false") << endl;
	cout << (obj2.IsEmpty() ? "true" : "false") << endl;
	cout << (obj3.IsEmpty() ? "true" : "false") << endl;
	cout << endl;
	
	
	cout << "Empty object 3:" << endl;
	obj3.Empty();
	obj3.Print();
	cout << endl;
	
	cout << "SetAt(3, '$'):" << endl;
	obj2.SetAt(3, '$');
	obj2.Print();
	cout << endl;
	
	cout << "Compare length:" << endl;
	cout << obj1.Compare(obj2) << endl;
	cout << obj2.Compare(obj2) << endl;
	cout << endl;

	cout << "Find char $:" <<  endl;
	cout << obj2.Find('$') << endl;
	cout << "Find string 'thi':" << endl;
	cout << obj2.Find("thi") << endl;
	cout << endl;
	
	cout << "Mid(1, 5):" <<  endl;
	cString obj4 = obj2.Mid(1,5);
	obj4.Print();
	cout << endl;
	
	cout << "Left (5):" << endl;
	cString obj5 = obj2.Left(5);
	obj5.Print();
	cout << endl;
	
	cout << "Right (3):" << endl;
	cString obj6 = obj2.Right(3);
	obj6.Print();
	cout << obj6.GetLength() << endl;
	cout << endl;
	
	cout << "[1]:" << endl;
	cout << obj2[1] << endl;
	cout << endl;
	
	cout << "const char * 'love' " << endl;
	obj1 = "love";
	obj1.Print();
	cout << endl;

	cout << "obj2 = obj1" << endl;
	obj1.Print();
	obj4 = obj1;
	obj4.Print();
	cout << endl;


	cout << "obj2 + obj1" <<  endl;
	cString obj7 = obj2 + obj1;
	obj7.Print();
	cout << endl;
	
	cout << "obj1 += obj2" << endl;
	obj1 += obj2;
	obj1.Print();

	//destroy everything:
	obj1.~cString();
	obj2.~cString();
	obj3.~cString();
	obj4.~cString();
	obj5.~cString();
	obj6.~cString();
	//obj7.~cString();
	int nothing;
	cin >> nothing;

}