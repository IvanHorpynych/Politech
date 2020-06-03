/*!
 * file: Student.h
 * CStudent  class declaration
 * written: 08/02/2015
*/
#pragma once
#include <iostream>
#include <vector>

using namespace std;

class CStudent {
public:
	//default constructor
	CStudent ();
		
	//copy constructor
	CStudent (const CStudent  &);
	//destructor
	~CStudent ();
	
	void SetInfo( char *_fname, char *_lname, int _id);
	void SetGrades(int *, int);
	float AverageGrade()const;

	//overloaded assignment
	CStudent  & operator=(const CStudent  &stud);
		

	bool operator<(const CStudent  &stud)const{
		return id < stud.id;
	};
	void SetId(int _id);

	int GetId()const;
	void PrintEssentials()const;
private:
	int id;//student’s id
	char *fname;//first name
	char *lname;//last name	
	int *grades;//grades
	unsigned int grades_nmb;//number of grades
};

void ListAll(vector<CStudent > );
void SortByGrades(vector<CStudent > &);
void DeleteById(vector<CStudent > &, int id);
CStudent  BestAverage(vector<CStudent > );
void DeleteAll(vector<CStudent > &);


