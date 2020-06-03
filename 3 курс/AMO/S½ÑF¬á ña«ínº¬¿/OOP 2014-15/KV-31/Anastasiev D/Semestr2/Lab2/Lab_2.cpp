// Lab_2.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "Student.h"
using namespace std;

#define SZ  5

int _tmain(int argc, _TCHAR* argv[])
{
	int data1[] = {5,5,4,4,5,5};
	int data2[] = {5,4,5,4,2,3};
	int data3[] = {5,5,5,4,3,3};
	int data4[] = {5,5,4,3,1,3};
	int data5[] = {3,2,2,4,3,5};

	int* data[] = {data1, data2, data3, data4, data5};

	char* name[] = {"Dmytro", "Marian", "Artem", "Evgen", "Vadim"};
	char* surname[] = {"Anastasiev", "Kindzer", "Ktyvonis", "Stepuk", "Zhydenko"};
	vector<CStudent> students;
	for(int i = 0; i<SZ; i++){
		CStudent stud;
		stud.SetInfo(name[i], surname[i], i+1);
		stud.SetGrades(data[i], 6);
		students.push_back(stud);
	}

	ListAll(students);

	cout<<"\n\nSort by Grades:\n\n";
	SortByGrades(students);
	ListAll(students);

	cout<<"\n\nBest Average:\n\n";
	CStudent bestStudent = BestAverage(students);
	bestStudent.PrintEssentials();

	cout<<"\n\nDelete by id:\n\n";
	DeleteById(students, 3);
	ListAll(students);

	cout<<"\n\nDelete All:\n\n";
	DeleteAll(students);
	ListAll(students);

	return 0;
}

