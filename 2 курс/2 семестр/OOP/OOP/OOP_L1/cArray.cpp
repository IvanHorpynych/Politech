#include "cArray.h"

using namespace std;

void cArray::setCapacity(size_t newcapacity) {
	_capacity = newcapacity;
	int* new_array = new int[_capacity];
	for (int i = 0; i < _size; i++)
		new_array[i] = _array[i];

	for (int i = _size; i <newcapacity; i++)
		new_array[i] = 0;

	delete[] _array;
	_array = new_array;
}

cArray::cArray() {
	_array = NULL;
	_size = 0;
	_capacity = 0;
}

cArray::~cArray() {
	delete[] _array;
}

int cArray::size() const {
	return _size;
}

int cArray::capacity() const {
	return _capacity;
}

bool cArray::isEmpty() const {
	return (_size != 0) ? false : true;
}

void cArray::resize(int nnewsize) {
	if (nnewsize > _capacity)
		setCapacity(nnewsize + GROWBY - nnewsize % GROWBY);
	_size = nnewsize;
}

void cArray::FreeExtra() {
	if (_size < _capacity) {
		int* new_array = new int[_size];
		memcpy(new_array, _array, sizeof(int)*(_size));
		delete[] _array;
		_array = new_array;
		_capacity = _size;
	}
}

void cArray::RemoveAll() {
	delete[] _array;
	_array = NULL;
	_size = 0;
	_capacity = 0;
}

int cArray::GetAt(int indx)const {
	if (indx >= _size)
		throw range_error("GetAt");
	return _array[indx];
}

void cArray::SetAt(int n, int indx) {
	if (indx >= _size)
		throw range_error("SetAt");
	_array[indx] = n;
}

void cArray::Add(int n) {
	if (_size == _capacity)
		setCapacity(_capacity + GROWBY);
	_array[_size] = n;
	_size++;
}

void cArray::Append(cArray *arr) {
	if ((_size + arr->size()) > _capacity)
		setCapacity(_size + arr->size() + GROWBY - (_size + arr->size()) % GROWBY);
	for (int i = 0; i < arr->_size; i++)
		_array[i + _size] = arr->_array[i];
	_size += arr->size();
}

void cArray::Copy(cArray *arr) {
	int* new_array = new int[arr->capacity()];
	for (int i = 0; i < arr->size(); i++)
		new_array[i] = arr->_array[i];
	delete[] _array;
	_array = new_array;
	_size = arr->size();
	_capacity = arr->capacity();
}

void cArray::InsertAt(int n, int indx) {
	if (indx >= _size)
		throw new range_error("InsertAt");

	if (_size == _capacity)
		setCapacity(_capacity + GROWBY);
	for (int i = _size - 1; i >= indx; i--)
		_array[i + 1] = _array[i];
	_array[indx] = n;
	_size++;
}

void cArray::RemoveAt(int indx) {
	if (indx >= _size)
		throw new range_error("RemoveAt");

	for (int i = indx; i < _size; i++)
		_array[i] = _array[i + 1];
	_size--;
}

int& cArray::operator[] (int indx) {
	if (indx >= _size)
		throw range_error("operator[]");

	return _array[indx];
}