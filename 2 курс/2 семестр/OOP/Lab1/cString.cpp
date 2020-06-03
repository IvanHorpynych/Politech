#define _CRT_SECURE_NO_WARNINGS
#include "cString.h"


cString::cString()
{
	bufstr = NULL;
}

cString::cString(const  char* psz)
{
	bufstr = new char[strlen(psz) + 1];
	strcpy(bufstr, psz);
	bufstr[strlen(psz)] = 0;
}

cString::cString(const cString& stringSrc)
{
	bufstr = new char[strlen(stringSrc.bufstr) + 1];
	strcpy(bufstr, stringSrc.bufstr);
	bufstr[strlen(stringSrc.bufstr)] = 0;
}

cString::~cString()
{
	delete[]bufstr;
}


int cString::GetLength() const
{
	if (bufstr == NULL)
		return 0;
	return strlen(bufstr);
}

bool cString::IsEmpty()const
{
	if (bufstr == NULL || strlen(bufstr) == 0) return true;
	return false;
}

void cString::Empty()
{
	delete[]bufstr;
	bufstr = NULL;
}


void cString::SetAt(int nIndex, char ch)
{
	if (bufstr == NULL)
		return;
	if (ch == 0) return;
	if (nIndex >= 0 && nIndex < this->GetLength() - 1) bufstr[nIndex] = ch;
	return;
}

int cString::Compare(const cString s) const
{
	if (this->IsEmpty() && s.IsEmpty() || strcmp(this->bufstr, s.bufstr) == 0)
		return 0;
	return (this->GetLength() - s.GetLength());
}

cString cString::Left(int nCount) const
{
	cString temp = *this;
	if (this->GetLength() < nCount) nCount = this->GetLength();
	else
		if (nCount < 0) nCount = 0;
	char* buf = new char[nCount + 1];
	strncpy(buf, bufstr, nCount);
	buf[nCount] = '\0';
	strcpy(temp.bufstr, buf);
	delete[]buf;
	return temp;
}

cString cString::Right(int nCount) const
{
	cString temp = *this;
	if (this->GetLength() < nCount) nCount = this->GetLength();
	else if (nCount < 0) nCount = 0;
	for (int i = 0;i < nCount;i++)
		temp.bufstr[i] = bufstr[this->GetLength() - i - 1];
	temp.bufstr[nCount] = 0;
	return temp;
}


cString cString::Mid(int nFirst, int nCount) const
{
	cString temp = *this;
	temp.bufstr = new char[nCount + 1];
	if (this->GetLength() < nCount) nCount = this->GetLength();
	else if (nFirst < 0 || nFirst > this->GetLength() - 1) nCount = 0;
	for (int i = 0;i < nCount;i++)
	{
		temp.bufstr[i] = bufstr[nFirst + i];
	}
	temp.bufstr[nCount] = 0;
	return temp;
}

int cString::Find(char ch) const
{
	int count = 0, len = this->GetLength();
	while (bufstr[count] != ch && count != len) count++;
	if (count != len) return count;
	return -1;
}

int cString::Find(char *pszSub) const
{
	int head_ind, ind, pos = -1, len_1 = strlen(bufstr), len_2 = strlen(pszSub);
	for (head_ind = 0; head_ind < len_1 - len_2 + 1; head_ind++)
	{
		ind = 0;
		while (bufstr[head_ind + ind] == pszSub[ind])
		{
			ind++;
		}
		if (ind >= len_2 - 1) pos = head_ind;
	}
	return pos;
}


char cString::operator [](int indx)
{
	return bufstr[indx];
}

const cString& cString::operator =(const char* psz)
{
	delete[]bufstr;
	bufstr = new char[strlen(psz) + 1];
	strcpy(bufstr, psz);
	bufstr[strlen(psz)] = 0;
	return *this;
}


cString& cString::operator =(const cString& stringSrc)
{
	if (this != &stringSrc && !stringSrc.IsEmpty())
	{
		if (this->IsEmpty())
		{
			delete[] bufstr;
			bufstr = new char[stringSrc.GetLength() + 1];
		}
		strcpy(bufstr, stringSrc.bufstr);
		bufstr[strlen(stringSrc.bufstr)] = 0;
	}
	return *this;
}

cString operator +(const cString& string1, const cString& string2)
{

	char* buf = new char[string1.GetLength() + string2.GetLength() + 1];
	strcpy(buf, string1.bufstr);
	strcat(buf, string2.bufstr);
	buf[string1.GetLength() + string2.GetLength()] = 0;
	cString bufobj(buf);
	return bufobj;
}

cString& cString::operator +=(const cString& string)
{
	char* buf = new char[this->GetLength() + string.GetLength()];
	strcpy(buf, bufstr);
	strcat(buf, string.bufstr);
	delete[]bufstr;
	bufstr = new char[strlen(buf) + 1];
	strcpy(bufstr, buf);
	bufstr[strlen(buf)] = 0;
	return *this;
}

void cString::Print()
{
	if (!this->IsEmpty())cout << bufstr << endl;
}