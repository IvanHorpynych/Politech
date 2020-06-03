#define _CRT_SECURE_NO_WARNINGS
#define D_SCL_SECURE_NO_WARNINGS
#include "CStudent.h"
#include <cstring>
#include <algorithm>
#include <iostream>

////////////////////////////////////////////////////////////

CStudent::CStudent() : id(0), fName(nullptr), lName(nullptr), grades(nullptr), grades_nmb(0)
{}



CStudent::CStudent(const CStudent &another_std)
{
	id = another_std.id;
	grades_nmb = another_std.grades_nmb;


	fName = new char[strlen(another_std.fName) + 1];
	lName = new char[strlen(another_std.lName) + 1];

	strcpy(fName, another_std.fName);
	strcpy(lName, another_std.lName);

	grades = new int[grades_nmb];

	std::copy(another_std.grades, another_std.grades + another_std.grades_nmb, grades);
}



CStudent::~CStudent()
{
	delete[] fName;
	delete[] lName;
	delete[] grades;
}

////////////////////////////////////////////////////////////

void CStudent::SetInfo(char *_fName, char *_lName, int _id)
{
	id = _id;

	delete[] fName;
	delete[] lName;
	
	fName = new char[strlen(_fName) + 1];
	lName = new char[strlen(_lName) + 1];

	strcpy(fName, _fName);
	strcpy(lName, _lName);
}



void CStudent::SetId(int _id)
{
	id = _id;
}



void CStudent::SetGrades(int *_grades, int _grades_nmb)
{
	grades_nmb = _grades_nmb;

	delete[] grades;

	grades = new int[grades_nmb];

	std::copy(_grades, _grades + _grades_nmb, grades);
}



float CStudent::AverageGrade() const
{
	int sigma = 0;
	for (int i = 0; i < grades_nmb; i++)
	{
		sigma += grades[i];
	}

	return (float)sigma / grades_nmb;
}

////////////////////////////////////////////////////////////

CStudent &CStudent::operator=(const CStudent &another_std)
{
	if (this != &another_std) 
	{
		id = another_std.id;
		grades_nmb = another_std.grades_nmb;

		delete[] fName;
		delete[] lName;
		delete[] grades;

		fName = new char[strlen(another_std.fName) + 1];
		lName = new char[strlen(another_std.lName) + 1];

		strcpy(fName, another_std.fName);
		strcpy(lName, another_std.lName);

		grades = new int[grades_nmb];

		std::copy(another_std.grades, another_std.grades + another_std.grades_nmb, grades);
	}

	return *this;
}



bool CStudent::operator<(const CStudent &another_std) const
{
	return id < another_std.id;
}

bool CStudent::operator>(const CStudent &another_std) const
{
	return id > another_std.id;
}

bool CStudent::operator==(const CStudent &another_std) const
{
	return id == another_std.id;
}

bool CStudent::operator!=(const CStudent &another_std) const
{
	return id != another_std.id;
}

////////////////////////////////////////////////////////////

int CStudent::GetId() const
{
	return id;
}

////////////////////////////////////////////////////////////

void CStudent::PrintEssentials() const
{
	std::cout << id << ": " << fName << " " << lName << " - " << AverageGrade() << std::endl;
}

////////////////////////////////////////////////////////////