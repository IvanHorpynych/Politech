/*!
* file: OOP_lab1.cpp
* Test file
* written: 19/02/2015
* Copyright (c) 2015 Chernysh A.A.
*/
#include <iostream>
#include <conio.h>
#include"OOP_lab1.h"

using namespace std;

int main(){
	cArray arr;
	
	int count = 0;

	//SetSize:
	cout << "Enter size of your array : ";
	cin >> count;
	arr.SetSize(count);

	//Getsize:
	cout << "Size of your array : " << arr.Getsize() << endl;

	//Getcount:
	cout << "Count of numbers in your array : " << arr.Getcount() << endl;

	//Getupperbound:
	cout << "Upperbound : " << arr.Getupperbound() << endl;

	//isEmpty:
	if (arr.IsEmpty()) cout << "Your array is empty!" << endl;
	else cout << "Your array is NOT empty!" << endl;

	//Add:
	cout << "Your array is : ";
	for (int i = 0; i < arr.Getsize(); i++){
		arr.Add(i);
		cout << i << "  ";
	}
	//SetAt:
	cout << "\nSetAt end of the array number 100: " << endl;
	arr.SetAt(100, count);
	
	//Demonstration parameters:
	cout << "Your new parameters :" << "\n\tSize : " << arr.Getsize();
	cout << "\n\tcount : " << arr.Getcount();
	cout << "\n\tUpperbount : " << arr.Getupperbound() << endl;
	if (arr.IsEmpty()) cout << "Your array is empty!" << endl;
	else cout << "Your array is NOT empty!" << endl;

	//GetAt:
	cout << "GetAt element with index 3 : " << arr.GetAt(3) << endl;
	
	//FreeExtra:
	cout << "FreeExtra : ";
	arr.FreeExtra();
	cout << "Your array is : ";
	for (int i = 0; i < arr.Getcount(); i++)
		cout << (arr)[i] << "  ";
	cout << endl;

	//operator[]:
	cout << "Enter index of the element, which you want to show : ";
	cin >> count;
	cout << "Your element is : " << (arr)[count] << endl;
	
	
	cArray arr1;
	arr.SetSize(count);
	//Copy:
	cout << "Copy : ";
	arr1.Copy(&arr);
	cout << "New vector arr : ";
	for (int i = 0; i < arr.Getcount(); i++)
	cout << (arr)[i] << "  ";
	cout << endl;
	//Append:
	cout << "Append str1 to str : ";
	arr.Append(&arr1);
	for (int i = 0; i < arr.Getcount(); i++)
		cout << (arr)[i] << "  ";

	cout << endl;
	
	//InsertAt:
	cout << "Enter index, where you want to insert number 200 :";
	cin >> count;
	arr.InsertAt(200, count);
	cout << "Your new array : ";
	for (int i = 0; i < arr.Getcount(); i++)
		cout << (arr)[i] << "  ";
	cout << endl;
	
	//RemoveAt:
	cout << "Enter index, where you want to remove element : ";
	cin >> count;
	arr.RemoveAt(count);
	cout << "Your new array : ";
	for (int i = 0; i < arr.Getcount(); i++)
		cout << (arr)[i] << "  ";
	cout << endl;
	
	//RemoveAll:
	arr.RemoveAll();
	if (arr.IsEmpty()) cout << "Your array is empty!" << endl;
	else cout << "Your array is NOT empty!" << endl;
	

	_getch();
	return 0;
}