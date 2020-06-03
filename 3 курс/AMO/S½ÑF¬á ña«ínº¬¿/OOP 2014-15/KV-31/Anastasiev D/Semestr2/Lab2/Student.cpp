#include "stdafx.h"
#include "Student.h"
#include <string.h>
#include <algorithm>



CStudent::CStudent(void)
{
	id = 0; 
	fname = NULL;//new char[1];
	//fname[0] = '\0';
	lname = NULL;//new char[1];
	//lname[0] = '\0';
	grades =NULL;// new int;
	grades_nmb = 0;

}


CStudent::~CStudent(void)
{
	delete [] fname;
	delete [] lname;
	delete [] grades;
}

CStudent::CStudent (const CStudent  &stud){
	id = stud.id;
	fname = new char[strlen(stud.fname) + 1];
	strcpy(fname, stud.fname);
	lname = new char[strlen(stud.lname) + 1];
	strcpy(lname, stud.lname);
	grades_nmb = stud.grades_nmb;
	if (grades_nmb == 0){
		grades = NULL;
	}else{
		grades = new int[grades_nmb];
		for(int i = 0; i < grades_nmb; i++){
			grades[i] = stud.grades[i];
		}
	}

}

void CStudent::SetInfo( char *_fname, char *_lname, int _id){
	id = _id;
	delete [] fname;
	fname = new char[strlen(_fname) + 1];
	strcpy(fname, _fname);
	delete [] lname;
	lname = new char[strlen(_lname) + 1];
	strcpy(lname, _lname);
}

void CStudent::PrintEssentials()const{
	cout<<"First name: "<<fname<<endl;
	cout<<"Last name: "<<lname<<endl;
	cout<<"Id: "<<id<<endl;
	cout<<"Average Grade: "<< AverageGrade()<< endl;
}

void CStudent::SetGrades(int *_grades, int _grades_nmb){
	grades_nmb = _grades_nmb;
	delete [] grades;
	grades = new int[_grades_nmb];
	for (int i=0; i<_grades_nmb; i++) {
		grades[i] = _grades[i];
	}
}

float CStudent::AverageGrade()const{
	if(grades_nmb == 0) return 0;
	float averageGrade = 0;
	for(int i = 0; i < grades_nmb; i++ ){
		averageGrade+=grades[i];
	}
	return averageGrade/grades_nmb;
}

void CStudent::SetId(int _id){
	id = _id;
}

int CStudent::GetId()const{
	return id;
}

void ListAll(vector<CStudent > vect){
	vector<CStudent>::iterator it;
	if(vect.empty()){
		cout<<"Vector is empty"<<endl;
		return;
	}
	for(it = vect.begin(); it != vect.end(); it++){
		it->PrintEssentials();
		cout<<endl;
	}
}
struct LesserStudent{
	bool operator ()(const CStudent st1, const CStudent st2){
		return st1.AverageGrade() < st2.AverageGrade();
	}

};

void SortByGrades(vector<CStudent > &vect){
	sort(vect.begin(), vect.end(), LesserStudent());
}

CStudent  & CStudent:: operator=(const CStudent  &stud){
	delete [] fname;
		delete [] lname;
		delete [] grades;
		id = stud.id;
		fname = new char[strlen(stud.fname) + 1];
		strcpy(fname, stud.fname);
		lname = new char[strlen(stud.lname) + 1];
		strcpy(lname, stud.lname);
		grades_nmb = stud.grades_nmb;
		if (grades_nmb == 0){
			grades = new int;
		}else{
			grades = new int[grades_nmb];
			for(int i = 0; i<grades_nmb; i++){
				grades[i] = stud.grades[i];
			}
		}
		return *this;
	
}

void DeleteById(vector<CStudent> &vect, int id){
	if(id<0 || id>=vect.size()) exit(1);
	vector<CStudent>::iterator it;
	for(it = vect.begin(); it != vect.end(); it++){
		if(it->GetId() == id){
			vect.erase(it);
			break;
		}
	}
}

CStudent  BestAverage(vector<CStudent > vect){
	vector<CStudent>::iterator it;
	CStudent best = vect[0];
	double MaxAverage = best.AverageGrade();
	for(it = vect.begin() + 1; it != vect.end(); it++){
		double Average = it->AverageGrade();
		if(Average>MaxAverage){
			MaxAverage = Average;
			best = *it;
		}
	}
	return best;
}

void DeleteAll(vector<CStudent > &vect){
	if(vect.empty()) return;
	vect.erase(vect.begin(), vect.end());

}


