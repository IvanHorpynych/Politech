/*!
* file: cArray.h
* cArray class declaration
* written: 15/04/2015
* Copyright (c) 2015 by E.Iasnicky
*/
#ifndef CARRAY_H
#define CARRAY_H
#pragma once
#include <iostream>
#include <vector>
#include <iterator>

#define GROWBY  10

typedef std::vector<int> CArr;
typedef std::vector<int>::iterator IT;

class cArray
{
public:
	//Constructs an empty array.
	cArray();
	~cArray();

	//Attributes
	//Gets the number of elements in this array.
	int GetSize();
	//Gets the number of elements in this array.
	int GetCount();

	//Returns the largest valid index.
	int GetUpperBound() const;
	//Determines whether the array is empty.
	bool IsEmpty()const;
	//Establishes the size of an empty or existing array; allocates memory if necessary.
	void SetSize(int nNewSize);

	//Operations
	//Frees all unused memory above the current upper bound.
	//This function has no effect on the upper bound of the array.
	void FreeExtra();

	//Removes all the elements from this array.
	void RemoveAll();

	//Element Access
	//Returns the value at a given index.
	int GetAt(int indx)const;
	//Sets the value for a given index; array not allowed to grow.
	void SetAt(int n, int indx);

	//Growing the Array
	//Adds an element to the end of the array; grows the array if necessary.
	void Add(int n);
	//Appends another array to the array; grows the array if necessary
	void Append(cArray *ar);
	//Copies another array to the array; grows the array if necessary.
	void Copy(cArray *ar);


	//Insertion/Removal
	//Inserts an element at a specified index.
	void InsertAt(int n, int indx);

	//Removes an element at a specific index.
	void RemoveAt(int indx);

	//Operators
	//Sets or gets the element at the specified index
	int& operator [](int indx);
private:
	int size; //length
	CArr data; //vector
	IT it; //iterator
};


#endif CARRAY_H