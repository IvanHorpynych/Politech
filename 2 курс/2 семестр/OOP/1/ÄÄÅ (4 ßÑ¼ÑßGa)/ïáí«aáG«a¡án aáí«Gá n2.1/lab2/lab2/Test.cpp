#define _CRT_SECURE_NO_WARNINGS 
#include <algorithm>
#include <iostream>
#include <fstream>
#include <vector>
#include "CStudent.h"

#define MAX_LENGTH 200
#define elem_shift 10

using namespace std;

//Вывести на консоль содержимое контейнера
void ListAll(vector<CStudent> students);

//Отсортировать элементы контейнера в порядке неубывания средней оценки.
void SortByGrades(vector<CStudent> &students);

//Удалить элемент контейнера с заданным id.
void DeleteById(vector<CStudent> &students, int id);

//Вернуть элемент контейнера с лучшей средней оценкой.
CStudent BestAverage(vector<CStudent> students);

//Удалить все элементы контенера.
void DeleteAll(vector<CStudent> & students);

CStudent *get_CStudent(char *csv_line);

void _csv_to_vector(char *csv_name, vector<CStudent> &students);

int main()
{
	vector<CStudent> students;
	char *csv = "Students.csv";
	_csv_to_vector(csv, students);

	ListAll(students);
	SortByGrades(students);
	ListAll(students);
	DeleteById(students, 1);
	ListAll(students);
	BestAverage(students).PrintEssentials();
	DeleteAll(students);
	system("PAUSE");
	return 0;
}

////////////////////////////////////////////////////////////

void ListAll(vector<CStudent> students)
{
	for (CStudent &x : students)
		x.PrintEssentials();

	cout << endl << endl;
}

////////////////////////////////////////////////////////////

void SortByGrades(vector<CStudent> &students)
{
	sort(students.begin(), students.end(), [](const CStudent & a, const CStudent & b) -> bool 
	{ 
		return a.AverageGrade() < b.AverageGrade(); 
	});
}

////////////////////////////////////////////////////////////

void DeleteById(vector<CStudent> &students, int id)
{
	for (vector<CStudent>::iterator it = students.begin(); it != students.end(); it++)
		if (it->GetId() == id)
		{
			students.erase(it);
			break;
		}
}

////////////////////////////////////////////////////////////

CStudent BestAverage(vector<CStudent> students)
{
	return *max_element(students.begin(), students.end(), [](const CStudent & a, const CStudent & b) -> bool 
	{ 
		return a.AverageGrade() < b.AverageGrade(); 
	});
}

////////////////////////////////////////////////////////////

void DeleteAll(vector<CStudent> & students)
{
	students.clear();
}

////////////////////////////////////////////////////////////

CStudent *get_CStudent(char *csv_line)
{
	CStudent *new_std = new CStudent;

	char buf[33], _fName[33], _lName[33];
	unsigned short counter = 0, buf_iter = 0, csv_line_iter = 0;
	int *_grades = new int[elem_shift], size = elem_shift, _grades_nmb = 0, _id;

	do{
		if (csv_line[csv_line_iter] == ';' || csv_line[csv_line_iter] == '\0')
		{
			buf[buf_iter] = '\0';

			switch (counter)
			{
				case 0:
					_id = atoi(buf);
					break;

				case 1:
					strcpy(_fName, buf);
					break;

				case 2:
					strcpy(_lName, buf);
					break;
	
				default:
					if (size < _grades_nmb)
					{
						size += elem_shift;
						int *new_arr = new int[size];
						copy(_grades, _grades + _grades_nmb, new_arr);

						delete[] _grades;

						_grades = new_arr;
					}

					_grades[_grades_nmb] = atoi(buf);
					_grades_nmb++;
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

	new_std->SetInfo(_fName, _lName, _id);
	new_std->SetGrades(_grades, _grades_nmb);

	return new_std;
}

////////////////////////////////////////////////////////////

void _csv_to_vector(char *csv_name, vector<CStudent> &students)
{
	ifstream csvout(csv_name);

	char csv_line[MAX_LENGTH];
	CStudent *buf_ptr;
	csvout.getline(csv_line, MAX_LENGTH);

	while (csvout)
	{
		buf_ptr = get_CStudent(csv_line);
		students.push_back(*buf_ptr);
		delete buf_ptr;
		csvout.getline(csv_line, MAX_LENGTH);
	}

	csvout.close();
}

////////////////////////////////////////////////////////////