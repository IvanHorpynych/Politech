#define _CRT_SECURE_NO_WARNINGS

#include "References.h"
#include <fstream>
#define MAX_LENGTH 1536
#define elem_shift 10



#pragma region _references

List<_references> _references::ref_list;
List<_references> _references::deleted;

_references::_references(char *_word, int *_pages, int __size) : word(new char[strlen(_word) + 1]), pages(_pages), _size(__size)
{
	strcpy(word, _word);
}

_references::_references(const _references &ref) : word(new char[strlen(ref.word) + 1]), pages(new int[ref._size]), _size(ref._size)
{
	strcpy(word, ref.word);
	std::copy(ref.pages, ref.pages + ref._size, pages);
}

_references::~_references()
{
	delete[] word;
	delete[] pages;
}

//----------------------------------------------------------

void _references::delete_ref()
{
	ref_list.splice(ref_list.find(*this), deleted);
}

//----------------------------------------------------------

void _references::edit_word(char *_word)
{
	delete[] word;
	word = new char[strlen(_word) + 1];
	strcpy(word, _word);
}

//----------------------------------------------------------

bool _references::operator==(const _references &another_std) const
{
	return !_stricmp(word, another_std.word);
}

//----------------------------------------------------------

bool _references::operator!=(const _references &another_std) const
{
	return _stricmp(word, another_std.word);
}

//----------------------------------------------------------

void _references::load(const char *filename)
{
	std::ifstream csvout(filename);

	char csv_line[MAX_LENGTH];
	csvout.getline(csv_line, MAX_LENGTH);

	while (csvout)
	{
		char buf[33], *_word = new char[33];
		int counter = 0, buf_iter = 0, csv_line_iter = 0,
			*_pages = new int[elem_shift], size = elem_shift, _pages_nmb = 0;

		do{
			if (csv_line[csv_line_iter] == ';' || csv_line[csv_line_iter] == '\0')
			{
				buf[buf_iter] = '\0';

				switch (counter)
				{
				case 0:
					strcpy(_word, buf);
					break;

				default:
					if (size < _pages_nmb)
					{
						size += elem_shift;
						int *new_arr = new int[size];
						std::copy(_pages, _pages + _pages_nmb, new_arr);

						delete[] _pages;

						_pages = new_arr;
					}

					_pages[_pages_nmb] = atoi(buf);
					_pages_nmb++;
					break;
				}

				csv_line_iter++;
				counter++;

				buf_iter = 0;
			}
			else
			{
				buf[buf_iter] = csv_line[csv_line_iter];
				csv_line_iter++;
				buf_iter++;
			}
		} while (csv_line[csv_line_iter - 1] != '\0');

		create(_word, _pages, _pages_nmb);

		csvout.getline(csv_line, MAX_LENGTH);
	}

	csvout.close();
}

//----------------------------------------------------------

List<_references>::Iterator _references::begin()
{
	return ref_list.begin();
}

List<_references>::Iterator _references::end()
{
	return ref_list.end();
}

//----------------------------------------------------------

void _references::delete_ref(List<_references>::Iterator &itr)
{
	ref_list.splice(itr, deleted);
}

void _references::delete_less(int count)
{
	List<_references>::Iterator itr = ref_list.begin();
	while (itr)
		if ((*itr)._size < count) delete_ref(itr);
			else itr++;
}

void _references::delete_all()
{
	deleted.conjunction(ref_list);
}

//----------------------------------------------------------

void _references::reestablish(char *_word)
{
	ref_list.splice(deleted.find(_references(_word, nullptr, 0)), ref_list);
}

void _references::reestablish_all()
{
	ref_list.conjunction(deleted);
}

//----------------------------------------------------------

int _references::size()
{
	return ref_list.size();
}

//----------------------------------------------------------

bool _references::empty()
{
	return ref_list.empty();
}

//----------------------------------------------------------

List<_references>::Iterator _references::find(char *_word)
{
	return ref_list.find(_references(_word, nullptr, 0));
}

//----------------------------------------------------------

List<_references>::Iterator create(char *_word, int *_pages, int __size)
{
	_references::ref_list.push_front(_references(_word, _pages, __size));
	return  _references::ref_list.begin();
}

//----------------------------------------------------------

std::ostream& operator<<(std::ostream& os, const _references &dt)
{
	os << "Word \"" << dt.word << "\" appears " << dt._size << " times on pages: ";
	int i = 0;
	for (i; i < dt._size - 1; i++)
		os << dt.pages[i] << ", ";

	os << dt.pages[i] << ". ";

	return os;
}

#pragma endregion