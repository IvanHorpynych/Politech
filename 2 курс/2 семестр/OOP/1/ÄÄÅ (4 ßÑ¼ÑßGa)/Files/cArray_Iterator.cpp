#include "cArray.h"

////////////////////////////////////////////////////////

template<typename ElemType>
cArray<ElemType>::Iterator::Iterator(ElemType *p) : ptr(p)
{}

////////////////////////////////////////////////////////

template<typename ElemType>
typename cArray<ElemType>::Iterator &cArray<ElemType>::Iterator::operator++ ()
{
	ptr = ptr + sizeof(ElemType);
	return *this;
}

template<typename ElemType>
typename cArray<ElemType>::Iterator &cArray<ElemType>::Iterator::operator-- ()
{
	ptr = ptr - sizeof(ElemType);
	return *this;
}

////////////////////////////////////////////////////////

template<typename ElemType>
ElemType& cArray<ElemType>::Iterator::operator* ()
{
	return *ptr;
}

template<typename ElemType>
ElemType& cArray<ElemType>::Iterator::operator-> ()
{
	return *ptr;
}

////////////////////////////////////////////////////////

template<typename ElemType>
bool cArray<ElemType>::Iterator::operator== (Iterator &right)
{
	return ptr == right.ptr;
}

template<typename ElemType>
bool cArray<ElemType>::Iterator::operator!= (Iterator &right)
{
	return ptr != right.ptr;
}

////////////////////////////////////////////////////////
