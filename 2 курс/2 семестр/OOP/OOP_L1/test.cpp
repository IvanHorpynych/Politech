#include "cArray.h"
#include <iostream>

using namespace std;

int main(){
	cArray ar_1;
	cArray ar_2;
	cout << "Added 10 elements to ar_1:" << endl;
	for (int i = 0; i < 10; i++)
		ar_1.Add(i);
	for (int i = 0; i < ar_1.size(); i++){
		cout << ar_1[i] << " ";
	}
	cout << endl;

	cout << "\n\nAdded 15 elements to ar_2:" << endl;
	for (int i = 0; i < 15; i++)
		ar_2.Add(i);
	for (int i = 0; i < ar_2.size(); i++) {
		ar_2[i] = ar_2.size() - i;
		cout << ar_2[i] << " ";
	}
	cout << endl;

	cout << endl << "\n\nInserted element \"23\" on index 5 in ar_1:" << endl;
	ar_1.InsertAt(23, 5);
	for (int i = 0; i < ar_1.size(); i++)
		cout << ar_1[i] << " ";

	cout << endl;

	cout << endl << "\n\nRemoved element on index 9 in ar_1:" << endl;
	ar_1.RemoveAt(9);
	for (int i = 0; i < ar_1.size(); i++)
		cout << ar_1[i] << " ";
	

	cout << endl << "\n\nOld size ar_1: " << ar_1.size() << endl;
	for (int i = 0; i < ar_1.size(); i++)
		cout << ar_1[i] << " ";
		ar_1.resize(15);
		cout << endl << "\n\nNew size ar_1:" << ar_1.size() << endl;
		for (int i = 0; i < ar_1.size(); i++)
			cout << ar_1[i] << " ";
	
	cout << endl << "\n\nIs empty ar_1: " << ar_1.isEmpty();
	cout << "\n\nSize ar_1: " << ar_1.size() << endl<< endl;
	cout << "Capacity ar_1: " << ar_1.capacity() << endl;
	for (int i = 0; i < ar_1.size(); i++){
		cout << ar_1[i] << " ";
	}
	cout << endl;

	cout << endl << "ar_1 copy in ar_2:" << endl;
	ar_2.Copy(&ar_1);
	for (int i = 0; i < ar_1.size(); i++)
		cout << ar_1[i] << " ";
	
	cout << endl;


	cout << endl << "\n\nInserted element \"999\" on index 1 in ar_1:" << endl;
	ar_1.InsertAt(999, 1);
	for (int i = 0; i < ar_1.size(); i++)
		cout << ar_1[i] << " ";

	cout << endl;

	cout << endl << "\n\nRemoved element on index 4 in ar_1:" << endl;
	ar_1.RemoveAt(4);
	for (int i = 0; i < ar_1.size(); i++)
		cout << ar_1[i] << " ";

	cout << endl << "\nAppend ar_1 to a_r2" << endl;
	ar_2.Append(&ar_1);
	for (int i = 0; i < ar_2.size(); i++)
		cout << ar_2[i] << " ";

	cout << "\n\nSize ar_2:" << ar_2.size() << endl;
	cout << "\nCapacity ar_2: " << ar_2.capacity() << endl;
	
	cout << endl << endl;
	return 0;
}