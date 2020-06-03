#include <stdlib.h>
#include <list>
#include <iterator>
#include <string.h>
#include "Student.h"
#include <iostream>
#include <fstream>

using namespace std;


CStudent::CStudent(const CStudent &st){
	SetInfo(st.fname, st.lname, st.id);
	SetGrades(st.grades, st.grades_nmb);
}

CStudent::CStudent(){
	fname = lname = NULL;
	grades = NULL;
	grades_nmb = 0;
	id = 0;
}

CStudent::~CStudent(){
		delete[] fname;
		delete[] lname;
		delete[] grades;
	printf("\nWORKS HERE");
}

void CStudent::SetGrades(int* grds, int num){
	grades = new int[num];
	for (int i = 0; i < num; i++){
		grades[i] = grds[i];
	}
	grades_nmb = num;
}

void CStudent::SetInfo(char *_fname, char *_lname, int _id){
	fname = new char[strlen(_fname)];
	lname = new char[strlen(_lname)];
	strcpy(fname, _fname);
	strcpy(lname, _lname);
	id = _id;
}

float CStudent::AverageGrade()const{
	int sum = 0;
	for (int i = 0; i < grades_nmb; i++){
		sum += grades[i];
	}
	return (float)sum / grades_nmb;
}

void CStudent::Setid(int _id){
	id = _id;
}

int CStudent::Getid()const{
	return id;
}

void CStudent::Printessentials()const{
	cout << "\nName:" << fname << " " << lname
		<< " id:" << id << endl;
	cout << "grades:";
	for (int i = 0; i < grades_nmb; i++)
		printf("%d ", grades[i]);
	cout << "\nAverage grade is:" << AverageGrade()<< endl;
}

CStudent& CStudent::operator=(const CStudent& copy_stud){
	SetInfo(copy_stud.fname, copy_stud.lname, copy_stud.id);
	SetGrades(copy_stud.grades, copy_stud.grades_nmb);
	return *this;
}

bool CStudent::operator<(const CStudent& stud)const{
	return (AverageGrade() < stud.AverageGrade()) ? 1 : 0;
}

void ListAll(_LStud _list){
	_LStud::iterator it = _list.begin();
	for (; it != _list.end(); ++it){
		it->Printessentials();
	}
}

void DeleteById(_LStud& _list, int id){
	_LStud::iterator it = _list.begin();
	for (; it != _list.end(); ++it){
		if (it->Getid() == id){
			_list.erase(it);
			return;
		}
	}
}

void SortByGrades(_LStud& _list){
	_list.sort();
}

CStudent BestAverage(_LStud _list){
	_LStud::iterator it = _list.begin();
	_LStud::iterator best;
	float comp = 0;
	for (; it != _list.end(); ++it){
		if (it->AverageGrade() > comp){
			best = it;
			comp = it->AverageGrade();
		}
	}
	return *best;

}

void DeleteAll(_LStud& _list){
	_list.clear();
	printf("\nList is cleared!\n");
}

void ReadFile(char* f_name,_LStud& _list){
	ifstream F;
	char str[50];
	int i;
	int j;//vec counter
	int k;
	char* fname, *lname;
	int* grades;
	int gradesnumb, id, counter;
	bool flag;
	
	F.open(f_name, ios::in);
	if (!F.is_open()){
		cout << "\nCan`t open the file\n";
	}
	else{
		while (!F.eof()){
			F >> str;
			CStudent* st = new CStudent();
			flag = false;
			k = 0;
			i = 0;
			j = 0;
			counter = 0;
			while (!flag){
				char* temp = new char[strlen(str)];
				while ((str[k] != ';')&&(str[k] != '\0')){
					
					temp[i] = str[k];
					i++;
					k++;
				};
				if (str[k] == '\0')
					flag = true;
				k++;
				temp[i] = '\0';
				counter++;
				switch (counter)
				{
				default:{
							grades[j] = atoi(temp);
							j++;
							break;
				}
				case 1:{
						   id=atoi(temp);
						   break;
				}
				case 2:{
						   fname = new char[20];
						   strcpy(fname, temp);
						   break;
				}
				case 3:{
						   lname = new char[20];
						   strcpy(lname, temp);
						   break;
				}
				case 4:{
						   gradesnumb = atoi(temp);
						   grades = new int[gradesnumb];
						   break;
				}

				}
				delete[] temp;
				i = 0;
			}
			st->SetInfo(fname, lname, id);
			st->SetGrades(grades, gradesnumb);
			_list.push_back(*st);
			delete st;
			delete[] grades;
			delete[] fname;
			delete[] lname;
		}
		F.close();
	}
}

