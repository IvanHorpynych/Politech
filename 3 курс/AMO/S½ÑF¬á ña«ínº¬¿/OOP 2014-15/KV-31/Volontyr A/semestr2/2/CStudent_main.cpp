/***********************************************************************
*file: CStudent_main.cpp
*synopsis: This file is used to test the functions located in file "CStudent_definition.cpp"
*and declared in file "CStudent.h"
*author: Alex Volontyr
*written: 11/03/2015
************************************************************************/
#include <iostream>
#include <fstream>
#include <string>
#include <list>
#include <iterator>
#include <algorithm>
#include "CStudent.h"

using namespace std;


void ListAll(list<CStudent>& stud_lst) {
	for (list<CStudent>::iterator it = stud_lst.begin(); it != stud_lst.end(); it++) {
		(*it).Printessentials();
	}	
}

bool Greater(const CStudent first, const CStudent second) {
	return (first.AverageGrade() < second.AverageGrade());	
}

void SortByGrades(list<CStudent>& stud_lst) {
	stud_lst.sort(Greater);
}

void DeleteById(list<CStudent >& stud_lst, int id) {
	for (list<CStudent>::iterator it = stud_lst.begin(); it != stud_lst.end(); it++) {
		if (id == (*it).Getid()) { 
			stud_lst.erase(it); 
			break; 
		}
	}	
}

CStudent BestAverage(list<CStudent>& stud_lst) {
	 float avrg = 0;
	 CStudent tmp;

	for (list<CStudent>::iterator it = stud_lst.begin(); it != stud_lst.end(); it++) {
		if (avrg < (*it).AverageGrade()) {  
			avrg = (*it).AverageGrade();
			tmp = (*it);
		}
	}
	
	return tmp;
}

void DeleteAll(list<CStudent >& stud_lst) {
	stud_lst.clear();
}

int main() {
	int gr[3] = {5, 5, 5};
	char *file_name = new char[10];
	int ind_found;
	int i, k;
	string str;
	string tmp;
	CStudent a;
	ifstream file;
	char *fname = new char[15];
	char *lname = new char[15];
	int id;
	unsigned int num_grades;
	int *grades;
	CStudent tmp_student;
	list<CStudent> students_list; 

	a.SetInfo("Alex", "Volontyr", 0);
	a.SetGrades(gr, 3);
	cout << "a: " << endl;
	a.Printessentials();
	CStudent b = a;
	cout << "b: " << endl;
	b.Printessentials();

	while (!file.is_open()) {
		cout << "Please, enter file name: " << endl;
		cin >> file_name;
		file.open(file_name);
	}

	while(!file.eof()) {
		getline(file, str);
		if (str == "") break;
		i = 0;
		ind_found = str.find_first_of(";");
		str[ind_found] = 'X';
		tmp = str.substr(i, ind_found - i);
		id = stoi(tmp);
		i += tmp.length() + 1;

		ind_found = str.find_first_of(";");
		str[ind_found] = 'X';
		tmp = str.substr(i, ind_found - i);
		fname = new char[tmp.size() + 1];
		strcpy(fname, tmp.c_str());
		i += tmp.length() + 1;

		ind_found = str.find_first_of(";");
		str[ind_found] = 'X';
		tmp = str.substr(i, ind_found - i);
		lname = new char[tmp.size() + 1];
		strcpy(lname, tmp.c_str());
		i += tmp.length() + 1;

		ind_found = str.find_first_of(";");
		str[ind_found] = 'X';
		tmp = str.substr(i, ind_found - i);
		num_grades = stoi(tmp);
		i += tmp.length() + 1;

		grades = new int[num_grades];
		k = 0;
		while (i < str.length()) {
			ind_found = str.find_first_of(";");
			if (ind_found < str.length()) str[ind_found] = 'X';
			tmp = str.substr(i, ind_found - i);
			grades[k++] = stoi(tmp);
			i += tmp.length() + 1;
		}
		tmp_student.SetInfo(fname, lname, id);
		tmp_student.SetGrades(grades, num_grades);
		students_list.push_back(tmp_student);
		//tmp_student.Printessentials();
	}
	
	cout << "List contains: " << endl;
	ListAll(students_list);
	
	cout << "List after deletion of 1 string: " << endl;
	DeleteById(students_list, 1);
	ListAll(students_list);

	cout << "Student with best average grade: " << endl;
	a = BestAverage(students_list);
	a.Printessentials();

	cout << "Sorted List: " << endl;
	SortByGrades(students_list);
	ListAll(students_list);

	DeleteAll(students_list);
	ListAll(students_list);

	cin >> gr[0];
}