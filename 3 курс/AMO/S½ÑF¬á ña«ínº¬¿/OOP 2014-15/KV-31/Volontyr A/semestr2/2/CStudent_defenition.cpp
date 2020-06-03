/********************************
 * file: CStudent_definition.cpp
 * CStudent class definition
 * written: 11/03/2015
 * Copyright (c) 2015 by Alex Volontyr
 ********************************/
#include <iostream>
#include <list>
#include "CStudent.h"

CStudent::CStudent() {
	id = 0;
	fname = new char[1];
	lname = new char[1];
	fname[0] = '\0';
	lname[0] = '\0';
	grades = new int[1];
	grades[0] = 0;
	grades_nmb = 0;
}

CStudent::CStudent(const CStudent& src) {
	id = src.id;
	fname = new char[strlen(src.fname) + 1];
	lname = new char[strlen(src.lname) + 1];
	grades = new int[src.grades_nmb];
	strcpy(fname, src.fname);
	strcpy(lname, src.lname);
	for (int i = 0; i < src.grades_nmb; i++) {
		grades[i] = src.grades[i];	
	}
	grades_nmb = src.grades_nmb;
}

CStudent::~CStudent() {
	delete[] fname;
	delete[] lname;
	delete[] grades;
}

void CStudent::SetInfo(char *_fname, char *_lname, int _id) {
	if (fname) delete[] fname;
	if (lname) delete[] lname;
	fname = new char[strlen(_fname) + 1];
	lname = new char[strlen(_lname) + 1];
	strcpy(fname, _fname);
	strcpy(lname, _lname);
	id = _id;
}

void CStudent::SetGrades(int *grds, int num) {
	if (grades) delete[] grades;
	grades = new int[num];
	for (int i = 0; i < num; i++) {
		grades[i] = grds[i];	
	}
	grades_nmb = num;
}

float CStudent::AverageGrade() const {
	float avrg = 0;

	for (int i = 0; i < grades_nmb; i++) {
		avrg += grades[i];
	}
	
	return avrg/grades_nmb;
}

CStudent& CStudent::operator=(const CStudent& src) {
	if (fname) delete[] fname;
	if (lname) delete[] lname;
	if (grades) delete[] grades;
	
	fname = new char[strlen(src.fname) + 1];
	lname = new char[strlen(src.lname) + 1];
	grades = new int[src.grades_nmb];

	strcpy(fname, src.fname);
	strcpy(lname, src.lname);
	
	for (int i = 0; i < src.grades_nmb; i++) {
		grades[i] = src.grades[i];	
	}

	grades_nmb = src.grades_nmb;
	id = src.id;

	return *this;
}

bool CStudent::operator<(const CStudent& src) const {
	return id < src.id;
}

void CStudent::SetId(int _id) {
	id = _id;	
}

int CStudent::Getid() const { 
	return id;
}

void CStudent::Printessentials() const {
	printf("%d %s %s %f\n", id, fname, lname, AverageGrade());	
}
