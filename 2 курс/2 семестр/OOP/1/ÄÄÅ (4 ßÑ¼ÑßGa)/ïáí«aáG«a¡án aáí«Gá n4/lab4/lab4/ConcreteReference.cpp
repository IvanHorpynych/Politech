#define _CRT_SECURE_NO_WARNINGS

#include "ConcreteReference.h"

#pragma region concrete_reference

ConcreteReference::ConcreteReference(char *_word, int *_pages, int __size) 
	: word(new char[strlen(_word) + 1]), pages(_pages), _size(__size), lst(References::Instance())
{
	strcpy(word, _word);
}

ConcreteReference::ConcreteReference(const ConcreteReference &ref)
	: word(new char[strlen(ref.word) + 1]), pages(new int[ref._size]), _size(ref._size), lst(References::Instance())
{
	strcpy(word, ref.word);
	std::copy(ref.pages, ref.pages + ref._size, pages);
}

ConcreteReference::~ConcreteReference()
{
	delete[] word;
	delete[] pages;
}

char* ConcreteReference::GetWord()
{
	return word;
}

int* ConcreteReference::GetPages()
{
	return pages;
}

int ConcreteReference::GetSize()
{
	return _size;
}

//----------------------------------------------------------

void ConcreteReference::delete_ref()
{
	lst.GetRefList().splice(lst.GetRefList().find(*this), lst.GetDeleted());
}

//----------------------------------------------------------

void ConcreteReference::edit_word(char *_word)
{
	delete[] word;
	word = new char[strlen(_word) + 1];
	strcpy(word, _word);
}

//----------------------------------------------------------

bool ConcreteReference::operator==(const ConcreteReference &another_std) const
{
	return !_stricmp(word, another_std.word);
}

//----------------------------------------------------------

bool ConcreteReference::operator!=(const ConcreteReference &another_std) const
{
	return _stricmp(word, another_std.word);
}

//----------------------------------------------------------

std::ostream& operator<<(std::ostream& os, const ConcreteReference &dt)
{
	os << "Word \"" << dt.word << "\" appears " << dt._size << " times on pages: ";
	int i = 0;
	for (i; i < dt._size - 1; i++)
		os << dt.pages[i] << ", ";

	os << dt.pages[i] << ". ";

	return os;
}

#pragma endregion