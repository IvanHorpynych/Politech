#ifndef CARRAY_H
#define CARRAY_H

#include <string.h>
#include <stdio.h>
#include <stdexcept>

#define GROWBY 10
class cArray{
public:
	cArray();
	~cArray();

	int size() const;
	int capacity() const;

	bool isEmpty()const;
	void resize( int nnewsize );

	void FreeExtra();
	void RemoveAll();

	int GetAt(int indx)const;
	void SetAt(int n, int indx);

	void Add(int n);
	void Append(cArray *arr);
	void Copy(cArray *arr);

	void InsertAt(int n, int indx);
	void RemoveAt(int indx);

	int& operator [](int indx);

private:
	void setCapacity(size_t newcapacity);

	int* _array;
	int _size;
	int _capacity;
};

#endif //CARRAY_H