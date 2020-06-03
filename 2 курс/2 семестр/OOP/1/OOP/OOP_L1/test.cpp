#include "cArray.h"
#include <iostream>

using namespace std;

int main(){
	cArray a1;
	cArray a2;
	cout << "Added 6 elements" << endl;
	for (int i = 0; i < 6; i++)
		a1.Add(i);
	for (int i = 0; i < a1.size(); i++){
		cout << a1[i] << " ";
	}
	cout << endl;

	cout << endl << "Inserted element 10 on index 0" << endl;
	a1.InsertAt(10, 0);
	for (int i = 0; i < a1.size(); i++){
		cout << a1[i] << " ";
	}
	cout << endl;

	cout << endl << "Removed element on index 1" << endl;
	a1.RemoveAt(1);
	for (int i = 0; i < a1.size(); i++){
		cout << a1[i] << " ";
	}
	cout << endl;

	cout << endl << "Old size: " << a1.size() << endl;
	a1.resize(11);
	cout << endl << "New size: " << a1.size() << endl;
	for (int i = 0; i < a1.size(); i++){
		cout << a1[i] << " ";
	}
	cout << endl;

	cout << endl << "Is empty: " << a1.isEmpty() << endl;
	cout << "Size: " << a1.size() << endl;
	cout << "Capacity: " << a1.capacity() << endl;
	for (int i = 0; i < a1.size(); i++){
		cout << a1[i] << " ";
	}
	cout << endl;

	cout << endl << "Now a2 is a copy of a1" << endl;
	a2.Copy(&a1);
	for (int i = 0; i < a1.size(); i++){
		cout << a1[i] << " ";
	}
	cout << endl;


	cout << endl << "Append a1 to a2" << endl;
	a2.Append(&a1);
	cout << "a2 size:" << a2.size() << endl;
	cout << "a2 capacity: " << a2.capacity() << endl;
	for (int i = 0; i < a2.size(); i++){
		cout << a2[i] << " ";
	}
	cout << endl;
	return 0;
}