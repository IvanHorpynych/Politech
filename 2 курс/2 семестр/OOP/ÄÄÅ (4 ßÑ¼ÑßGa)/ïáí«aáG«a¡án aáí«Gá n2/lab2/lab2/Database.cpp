#include "Database.h"
#define MAX_LENGTH 80

bool Database::LoadFromFile(const char *file)
{
	std::ifstream csvout(file);

	char csv_line[MAX_LENGTH];
	csvout.getline(csv_line, MAX_LENGTH);

	while (csvout)
	{
		char buf[33], *_word = new char[33];
		int counter = 0, buf_iter = 0, csv_line_iter = 0,
			*_pages = new int[elem_shift], size = elem_shift, _pages_nmb = 0;

		do {
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
	return false;
}

void Database::ArrangeSubordinates()
{
}

Person * Database::HireEmployee(Person * p)
{
	return nullptr;
}

void Database::DisplayDepartmentEmployees(string _department)
{
}

bool Database::FireEmployee(int id)
{
	return false;
}

void Database::DisplayAll()
{
}
