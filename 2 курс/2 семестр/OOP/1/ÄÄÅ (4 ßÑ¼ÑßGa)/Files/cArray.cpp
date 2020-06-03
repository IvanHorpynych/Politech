#include "cArray.h"

////////////////////////////////////////////////////////////

template<typename ElemType>
cArray<ElemType>::cArray(int _size) : Size(_size), Count(0)
{
	Data = (char*)malloc(Size * sizeof(ElemType));
	UpperBound = -1;
	allocated.insert(-1);
}

template<typename ElemType>
cArray<ElemType>::cArray()
{
	this->cArray(GROWBY);
}

template<typename ElemType>
cArray<ElemType>::~cArray()
{
	free(Data);
}

////////////////////////////////////////////////////////////

template<typename ElemType>
ElemType& cArray<ElemType>::operator[] (int indx)
{
	if (!allocated.count(indx))
	{
		allocated.insert(indx);
		count++;
	}

	return *(Data + indx * sizeof(ElemType));
}

////////////////////////////////////////////////////////////

template<typename ElemType>
typename cArray<ElemType>::Iterator cArray<ElemType>::begin() const
{
	return *(new Iterator(Data));
}

template<typename ElemType>
typename cArray<ElemType>::Iterator cArray<ElemType>::UpperBoundIter() const
{
	return *(new Iterator(Data + UpperBound * sizeof(ElemType)));
}

template<typename ElemType>
typename cArray<ElemType>::Iterator cArray<ElemType>::end() const
{
	return *(new Iterator(Data + Size * sizeof(ElemType)));
}

////////////////////////////////////////////////////////////

template<typename ElemType>
int cArray<ElemType>::GetSize() const
{
	return Size;
}

template<typename ElemType>
int cArray<ElemType>::GetCount() const
{
	return Count;
}

template<typename ElemType>
int cArray<ElemType>::GetUpperBound() const
{
	return UpperBound;
}

////////////////////////////////////////////////////////////

template<typename ElemType>
bool cArray<ElemType>::IsEmpty() const
{
	return Count == 0;
}

////////////////////////////////////////////////////////////

template<typename ElemType>
void cArray<ElemType>::SetSize(int nNewSize)
{
	if (Size > nNewSize)
	{
		set<int>::const_iterator iter = allocated.end();
		while (*iter > nNewSize && iter > allocated.begin())
		{
			allocated.remove(*iter);
			Count--;
			iter = allocated.end();
		}

		UpperBound = *iter;
	}

	Size = nNewSize;
	realloc(Data, Size * sizeof(ElemType));
}

////////////////////////////////////////////////////////////

template<typename ElemType>
void cArray<ElemType>::FreeExtra()
{
	SetSize(UpperBound);
}

////////////////////////////////////////////////////////////

template<typename ElemType>
void cArray<ElemType>::RemoveAll()
{
	Size = 0;
	Count = 0;
	UpperBound = 0;
	free(Data);
}

////////////////////////////////////////////////////////////

template<typename ElemType>
int cArray<ElemType>::GetAt(int indx) const
{
	return operator[](indx);
}

template<typename ElemType>
void cArray<ElemType>::SetAt(ElemType n, int indx)
{
	operator[](indx) = n;
}

////////////////////////////////////////////////////////////

template<typename ElemType>
void cArray<ElemType>::Add(ElemType n)
{
	if (++UpperBound > Size)
		SetSize(Size + GROWBY);
	
	Count++;
	allocated.insert(UpperBound);
	operator[UpperBound] = n;
}

////////////////////////////////////////////////////////////

template<typename ElemType>
void cArray<ElemType>::Append(cArray *arr)
{
	if (arr->GetUpperBound() + UpperBound > Size)
		SetSize(Size + GROWBY * ceil(( Size - UpperBound + arr->GetUpperBound() ) / GROWBY));

	for (Iterator i = begin(); i != UpperBoundIter(); i++)
	{
		Add(*i);
	}
}

////////////////////////////////////////////////////////////

template<typename ElemType>
void cArray<ElemType>::Copy(cArray *arr)
{
	if (arr->Size() > Size)
		SetSize(GROWBY * ceil(arr->Size() / GROWBY));

	memcpy_s( Data, Size * sizeof(ElemType), arr->Data, arr->Size * sizeof(ElemType) );
	UpperBound = arr->UpperBound;
	allocated.clear();
	allocated.insert(arr->allocated.begin(), arr->allocated.end());
}

////////////////////////////////////////////////////////////

template<typename ElemType>
void cArray<ElemType>::InsertAt(ElemType n, int indx)
{
	operator[](indx) = n;
}

template<typename ElemType>
void cArray<ElemType>::RemoveAt(int i)
{
	allocated.remove(*allocated.end());
	if (i == UpperBound)
	{
		UpperBound = *allocated.end();;
	}
}

////////////////////////////////////////////////////////////