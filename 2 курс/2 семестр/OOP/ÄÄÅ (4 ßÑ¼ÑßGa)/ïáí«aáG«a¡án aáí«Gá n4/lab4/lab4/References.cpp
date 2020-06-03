#define _CRT_SECURE_NO_WARNINGS

#include "References.h"
#include <fstream>
#define MAX_LENGTH 1536
#define elem_shift 10

#pragma region _references

References& References::Instance()
{
	static References _self;
	return _self;
}

//----------------------------------------------------------

References::References()
{}

//----------------------------------------------------------

void References::load(const char *filename)
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

		ref_list.push_front(ConcreteReference(_word, _pages, _pages_nmb));

		csvout.getline(csv_line, MAX_LENGTH);
	}

	csvout.close();
}

//----------------------------------------------------------

List<ConcreteReference>::Iterator References::begin()
{
	return ref_list.begin();
}

List<ConcreteReference>::Iterator References::end()
{
	return ref_list.end();
}

List<ConcreteReference> References::GetRefList()
{
	return ref_list;
}

List<ConcreteReference> References::GetDeleted()
{
	return deleted;
}

//----------------------------------------------------------

void References::delete_ref(List<ConcreteReference>::Iterator &itr)
{
	ref_list.splice(itr, deleted);
}

void References::delete_less(int count)
{
	List<ConcreteReference>::Iterator itr = ref_list.begin();
	while (itr)
		if ((*itr).GetSize() < count) delete_ref(itr);
			else ++itr;
}

void References::delete_all()
{
	deleted.conjunction(ref_list);
}

//----------------------------------------------------------

void References::reestablish(char *_word)
{
	ref_list.splice(deleted.find(ConcreteReference(_word, nullptr, 0)), ref_list);
}

void References::reestablish_all()
{
	ref_list.conjunction(deleted);
}

//----------------------------------------------------------

int References::size()
{
	return ref_list.size();
}

//----------------------------------------------------------

bool References::empty()
{
	return ref_list.empty();
}

//----------------------------------------------------------

List<ConcreteReference>::Iterator References::find(char *_word)
{
	return ref_list.find(ConcreteReference(_word, nullptr, 0));
}

#pragma endregion