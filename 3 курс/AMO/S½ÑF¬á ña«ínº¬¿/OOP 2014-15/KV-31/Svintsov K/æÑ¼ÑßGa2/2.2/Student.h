
#pragma once
#include <list>

using namespace std;

class CStudent {
public:
	//default constructor
	CStudent();
	//copy constructor
	CStudent(const CStudent &);
	//destructor
	~CStudent();
	void SetInfo(char *, char* , int );
	void SetGrades(int *, int);
	float AverageGrade()const;
	//overloaded assignment
	CStudent & operator=(const CStudent &);
	bool operator<(const CStudent &)const;
	void Setid(int );
	int Getid()const;
	void Printessentials()const;
private:
	int id;//student’s id
	char *fname;//first name
	char *lname;//last name
	int *grades;//grades
	unsigned int grades_nmb;//number of grades
};

typedef list<CStudent> _LStud;

void ListAll(_LStud);
void DeleteById(_LStud &, int );
CStudent BestAverage(_LStud);
void SortByGrades(_LStud &);
void DeleteAll(_LStud &);
void ReadFile(char*,_LStud&);