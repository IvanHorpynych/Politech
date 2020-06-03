#include "cArray.h"

cArray::cArray()
{
	massive = NULL;
	size = 0;
	max_index = 0;
	number = 0;
}

cArray::~cArray()
{
	free (massive);
	size = 0;
	max_index = 0;
}

int cArray::GetSize() const
{
	return size;
}

int cArray::GetCount() const
{
	return max_index;
}

int cArray::GetUpperBound() const
{
	return max_index - 1;
}

bool cArray::IsEmpty() const
{
	if (size)
		return 0;
	else
		return 1;
}

void cArray::Add(int n)
{
	if (max_index < size)
	{
		massive[max_index++] = n;
	}
	else
	{
		massive = (int*)realloc(massive, ++number*GROWBY*sizeof(int));
		massive[max_index++] = n;
		size = number*GROWBY;
		for (int i = max_index; i < size; i++)
			massive[i] = NULL;
	}
}

void cArray::Append(cArray *obj)
{
	for (int i = 0; i < obj->max_index; i++)
		this->Add(obj->massive[i]);
}

void cArray::SetSize(int nNewSize)
{
	if (size == nNewSize)
		return;

	if (nNewSize < size)
	{
		for (int i = nNewSize; i < size; i++)
			massive[i] = NULL;
		massive = (int*)realloc(massive, nNewSize*sizeof(int));
		size = nNewSize;
		max_index = nNewSize;
	}
	else
	{
		massive = (int*)realloc(massive, nNewSize*sizeof(int));
		for (int i = size; i < nNewSize; i++)
			massive[i] = NULL;
		size = nNewSize;
	}
}

void cArray::FreeExtra()
{
	for (int i = max_index; i < size; i++)
		massive[i] = NULL;
	massive = (int*)realloc(massive, max_index*sizeof(int));
	size = max_index;
}

void cArray::RemoveAll()
{
	for (int i = 0; i < size; i++)
	{
		massive[i] = NULL;
	}
	size = 0;
	max_index = 0;
	number = 0;
}

int cArray::GetAt(int indx) const
{
	if (indx>size)
		return -1;

	return massive[indx];
}

void cArray::SetAt(int n, int indx)
{
	massive[indx] = n;
}

void cArray::Copy(cArray * ar)
{
	this->SetSize(ar->size);
	for (int i = 0; i < size; i++)
	{
		massive[i] = ar->massive[i];
	}
}

void cArray::InsertAt(int n, int indx)
{

	if (indx < 0)
	{
		cout << endl << "error: indx < 0" << endl;
		return;
	}

	if (indx > max_index)
	{
		cout << endl << "error: indx > max_index" << endl;
		return;
	}

	if (max_index == size)
	{
		this->Add(NULL);
	}

	if (indx == max_index)
	{
		massive[max_index + 1] = n;
		swap(massive[max_index], massive[max_index + 1]);
	}
	else
	{
		
		int a = massive[indx];
		
		for (int i = max_index; i >indx; i--)
		{
			massive[i] = massive[i - 1];
		}
		massive[indx] = n;
	}
	

	max_index++;
	if (max_index >= size)
	{
		massive = (int*)realloc(massive, ++number*GROWBY*sizeof(int));
		size = number*GROWBY;
	}
}

void cArray::RemoveAt(int indx)
{
	if (indx < 0)
	{
		cout << endl << "error: indx < 0" << endl;
		return;
	}

	if (indx > max_index)
	{
		cout << endl << "error: indx > max_index" << endl;
		return;
	}

	for (int i = indx; i < max_index - 1; i++)
	{
		massive[i] = massive[i + 1];
	}
	massive[max_index] = NULL;
	max_index--;
	size--;
}

int& cArray::operator [](int indx)
{
	return this->massive[indx];
}

void cArray::print()
{
	cout << endl;
	if (size == 0)
		cout << "empty massive" << endl;
	for (int i = 0; i < size; i++)
	{
		if (massive[i] == NULL)
			cout << "NULL" << ", ";
		else
			cout << massive[i] << ", ";
	}
	cout << endl;
}