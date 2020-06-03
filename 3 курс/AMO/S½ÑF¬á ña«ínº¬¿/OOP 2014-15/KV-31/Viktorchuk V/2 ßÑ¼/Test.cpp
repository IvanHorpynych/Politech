#include "cArray.h"

int main()
{

	cArray obj1, obj2, obj3;
	for (int i = 0; i < 5; i++)
		obj1.Add(i + 1);
	obj1.print();
	cout << "size = " << obj1.GetSize() << endl;
	cout << "number of elements = " << obj1.GetCount() << endl;
	cout << "max index = " << obj1.GetUpperBound() << endl;
	cout << endl << "_______________" << endl;
	cout << "obj1 is empty?  " << obj1.IsEmpty() << endl;
	cout << "obj2 is empty?  " << obj2.IsEmpty() << endl;
	cout << endl << "_______________" << endl;
	cout << "size = " << obj2.GetSize() << endl;
	for (int i = 0; i < 5; i++)
		obj2.Add(i*i + 1);
	obj2.print();
	cout << "size = " << obj2.GetSize() << endl;
	obj2.SetSize(3);
	obj2.print();
	cout << "size = " << obj2.GetSize() << endl;
	cout << endl << "_______________" << endl;
	for (int i = 0; i < 5; i++)
		obj3.Add(i*i + 5);
	obj3.print();
	cout << "size = " << obj3.GetSize() << endl;
	obj3.FreeExtra();
	obj3.print();
	cout << "size = " << obj3.GetSize() << endl;
	cout << endl << "_______________" << endl;
	obj3.print();
	cout << "size = " << obj3.GetSize() << endl;
	obj3.RemoveAll();
	obj3.print();
	cout << "size = " << obj3.GetSize() << endl;
	cout << endl << "_______________" << endl;
	obj1.print();
	cout << obj1.GetAt(1) << endl;
	obj1.SetAt(60, 1);
	obj1.print();
	cout << endl << "_______________" << endl;
	obj1.print();
	cout << "size = " << obj1.GetSize() << endl;
	obj2.print();
	cout << "size = " << obj2.GetSize() << endl;
	obj1.Append(&obj2);
	obj1.print();
	cout << "size = " << obj1.GetSize() << endl;
	cout << endl << "_______________" << endl;
	obj3.print();
	cout << "size = " << obj3.GetSize() << endl;
	obj3.Add(100);
	obj3.print();
	obj2.print();
	obj3.Copy(&obj2);
	obj3.print();
	cout << endl << "_______________" << endl;
	obj1.Add(75);
	obj1.Add(90);
	obj1.print();
	cout << endl << "Insert" << endl;
	obj1.InsertAt(666, 2);
	obj1.print();
	cout << endl << "_______________" << endl;
	obj1.print();
	obj1.RemoveAt(1);
	obj1.print();
	cout << endl << "_______________" << endl;
	obj1.print();
	cout << obj1[2];
	_getch();
}