/********************************
 * file: CStudent.h
 * CStudent  class declaration
 * written: 11/03/2015
 * Copyright (c) 2015 by Alex Volontyr
 ********************************/
#include <list>

#pragma once
class CStudent {
public:
	//default constructor
	CStudent ();	
	//copy constructor
	CStudent (const CStudent&);
	//destructor
	~CStudent ();
	
	void SetInfo(char *_fname, char *_lname, int _id);
	void SetGrades(int *, int);
	float AverageGrade() const;

	//overloaded assignment
	CStudent& operator=(const CStudent&);
	bool operator<(const CStudent&) const;
	void SetId(int _id);

	int Getid() const;
	void Printessentials() const;
	
private:
	int id;//student’s id
	char *fname;//first name
	char *lname;//last name	
	int *grades;//grades
	unsigned int grades_nmb;//number of grades
};