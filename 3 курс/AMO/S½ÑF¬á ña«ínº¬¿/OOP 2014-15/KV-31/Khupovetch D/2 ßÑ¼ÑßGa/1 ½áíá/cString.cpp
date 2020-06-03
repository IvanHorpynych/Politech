#include "cString.h"

cString::cString()
{
	string = NULL;
	length = 0;
}

cString::cString(const char *psz)
{
	int size = strlen(psz);
	string = new char[size + 1];
	strcpy(string, psz);
	length = size;
}

cString::cString(const cString &stringSrc)
{
	if (this!=&stringSrc)
	{
		int size = strlen(stringSrc.string);
		string = new char[size + 1];
		strcpy(string, stringSrc.string);
		length = size;
	}
}

cString::~cString()
{
	delete[] string;
	length = 0;
}

int cString::GetLength() const
{
	return length;
}

bool cString::IsEmpty()const
{
	if (length == 0)
		return true;
	else
		return false;
}

void cString::Empty()
{
	if (length != 0)
	{
		delete[] string;
		string = NULL;
		length = 0;
	}
}

void cString::SetAt(int nIndex, char ch)
{
	if (ch == '\0')
		return;
	if (nIndex <= length)
	{
		string[nIndex] = ch;
		return;
	}
}

int cString::Compare(const cString &s) const
{
	if (this->length == s.length)
		return 0;
	else
	{
		if (this->length < s.length)
			return 1;
		else
			return -1;
	}
}

int cString::Find(char ch) const
{
	int i;
	for (i = 0; i < length; i++)
	{
		if (string[i] == ch)
			return i;
		else
			continue;
	}
	return -1;
}

int cString::Find(char *pszSub) const
{
	if (pszSub == NULL)
		return -1;
	char *istr = strstr(string, pszSub);
	if (istr == NULL)
	{
		return -1;
	}
	else
		return istr - string;
}

cString cString::Mid(int nFirst, int nCount) const
{
	if (nCount < nFirst)
		return NULL;
	int i, j=0;
	char *help_string;
	help_string = new char[nCount - nFirst + 2];
	for (i = nFirst; i <= nCount; i++)
	{
		help_string[j] = this->string[i];
		j++;
	}
	help_string[j] = '\0';
	cString obj(help_string);
	return obj;
}

cString cString::Right(int nCount) const
{
	if (nCount > length)
		return NULL;
	int i, j = 0;
	char *help_string;
	help_string = new char[length - nCount + 2];
	for (i = length-1; i >= nCount ; i--)
	{
		help_string[j] = this->string[i];
		j++;
	}
	help_string[j] = '\0';
	cString obj(help_string);
	return obj;
}

cString cString::Left(int nCount) const
{
	if (nCount > length)
		return NULL;
	int i, j = 0;
	char *help_string;
	help_string = new char[nCount + 1];
	for (i = 0; i <= nCount; i++)
	{
		help_string[j] = this->string[i];
		j++;
	}
	help_string[j] = '\0';
	cString obj(help_string);
	return obj;
}

char cString::operator [](int nIndex) 
{
	return string[nIndex];
}

const cString& cString::operator =(const unsigned char* psz) 
{
	if (psz != NULL) 
	{
		length = strlen((const char*)psz);
		delete[] string;
		string = NULL;
		string = new char[length + 1];
		strcpy(string, (const char*)psz);
	}
	return *this;
}

cString& cString::operator =(const cString& stringSrc) 
{
	delete[] string;
	length = stringSrc.GetLength();
	string = new char[length + 1];
	strcpy(string, stringSrc.string);
	return *this;
}

cString cString::operator +(const cString& str)
{
	char *help_string = new char[length + str.GetLength() + 1];
	strcpy(help_string, string);
	strcat(help_string, str.string);
	cString obj(help_string);
	return obj;
}

cString& cString::operator +=(const cString& str)
{
	char *help_string = new char[length + 1];
	strcpy(help_string, string);
	delete[] string;
	string = NULL;
	string = new char[strlen(help_string) + str.GetLength() + 1];
	strcpy(string, help_string);
	strcat(string, str.string);
	return *this;
}

void cString::Print()
{
	if (string != NULL)
		cout << string << endl;
	else
		cout << "!!!!!!Empty string!!!!!!" << endl;
}