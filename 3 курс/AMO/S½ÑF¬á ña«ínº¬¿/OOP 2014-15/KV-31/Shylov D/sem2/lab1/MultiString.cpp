#include "MultiString.h"

MultiString::MultiString()
{
	buf = NULL;
	str_nmb = 0;
}

MultiString::MultiString(int amount)
{
	str_nmb = amount;
	buf = new char*[str_nmb];
	for (int i = 0; i < str_nmb; i++)
	{
		buf[i] = NULL;
	}
}

MultiString::MultiString(const MultiString &ms)
{
	buf = new char*[str_nmb = ms.str_nmb];

	for (int i = 0; i < ms.str_nmb; i++)
	{
		if (ms.buf[i] != NULL)
		{
			buf[i] = new char[strlen(ms.buf[i]) + 1];
			strcpy(buf[i], ms.buf[i]);
		}
		else
		{
			buf[i] = NULL;
		}

	}
}
MultiString::~MultiString()
{
	for (int i = 0; i < str_nmb; i++)
	{
		delete[] buf[i];
	}
	delete[] buf;
}

MultiString& MultiString::operator=(const MultiString &ms)
{
	if (this == &ms)
		return *this;
	Empty();

	buf = new char*[str_nmb = ms.str_nmb];

	for (int i = 0; i < str_nmb; i++)
	{
		if (ms.buf[i] != NULL)
		{
			buf[i] = new char[strlen(ms.buf[i]) + 1];
			strcpy(buf[i], ms.buf[i]);
		}
		else
		{
			buf[i] = NULL;
		}
	}
	return *this;
}

char* MultiString::operator[] (int nIndex) const
{
	if ((nIndex < str_nmb) && (buf[nIndex] != NULL))
	{
		char *copy = new char[strlen(buf[nIndex]) + 1];
		strcpy(copy, buf[nIndex]);
		return copy;
	}
	else
		return NULL;
}

MultiString& MultiString::operator+=(const MultiString &ms)
{
	if (ms.str_nmb == 0) return *this;
	else
	{
		char **copy = new char*[str_nmb + ms.str_nmb + 1];

		for (int i = 0; i < str_nmb; i++)
			copy[i] = buf[i];
		for (int i = 0; i < ms.str_nmb; i++)
			copy[str_nmb + i] = ms.buf[i];

		buf = copy;
		str_nmb += ms.str_nmb;
		return *this;
	}
}

void MultiString::Adding_str(const char *str)
{
	if (str != NULL)
	{
		str_nmb++;
		char **add_str;
		add_str = new char*[str_nmb + 1];

		for (int i = 0; i < str_nmb - 1; i++)
			add_str[i] = buf[i];

		add_str[str_nmb - 1] = new char[strlen(str) + 1];
		strcpy(add_str[str_nmb - 1], str);
		delete[] buf;
		buf = add_str;
	}
	else return;
}

int MultiString::Find(const char *pszSub) const
{
	if ((!IsEmpty()) && (pszSub != NULL)){
		int index = 0;
		do {
			if (buf[index] != NULL) strcmp(buf[index], pszSub);
			index++;
		} while (index < str_nmb);

		if (index >= str_nmb) return -1;
		else return index;
	}
	else
		return -1;
}

int MultiString::GetLength() const{
	return str_nmb;
}

bool MultiString::IsEmpty() const{
	return  (str_nmb == 0);
}

void MultiString::Empty()
{

	for (int i = 0; i < str_nmb; i++)
	{
		delete[] buf[i];
	}
	delete[] buf;
	buf = NULL;
	str_nmb = 0;
}

void MultiString::SetAt(int nIndex, const char* str)
{
	if (nIndex < str_nmb)
	{
		delete[] buf[nIndex];
		buf[nIndex] = new char[strlen(str) + 1];
		strcpy(buf[nIndex], str);
	}
}

void MultiString::PrintStr(int nIndex) const
{
	if ((nIndex < str_nmb) && (buf[nIndex] != NULL))
		printf("%s", buf[nIndex]);
	else
		printf("There is no string with this index or it is empty.");
}

MultiString MultiString::MergeMultiStringExclusive(const MultiString &cl)
{
	MultiString Add_Obj;

	Add_Obj.operator=(cl);

	for (int i = 0; i < str_nmb; i++)
	{
		if (Add_Obj.Find(buf[i]) == -1)
			Add_Obj.Adding_str(buf[i]);
	}

	return Add_Obj;
}