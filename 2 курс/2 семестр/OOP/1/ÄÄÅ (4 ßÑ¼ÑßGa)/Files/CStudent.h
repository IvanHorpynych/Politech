#pragma once

class CStudent
{
	private:
		int id;						//student’s id
		char *fName;				//first name
		char *lName;				//last name	
		int *grades;				//grades
		unsigned int grades_nmb;	//number of grades
	public:
		//default constructor
		CStudent();

		//copy constructor
		CStudent(const CStudent &);

		//destructor
		~CStudent();

		void SetInfo(char *_fName, char *_lName, int _id);
		void SetId(int _id);
		void SetGrades(int *, int);

		float AverageGrade()const;

		//overloaded assignment
		CStudent &operator=(const CStudent &);
		bool operator<(const CStudent &) const;
		bool operator>(const CStudent &) const;
		bool operator==(const CStudent &) const;
		bool operator!=(const CStudent &) const;


		int GetId() const;

		void PrintEssentials() const;
};