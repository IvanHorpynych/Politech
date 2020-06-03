#pragma once
#include "Person.h"

class Employee : public Person
{
	public:
		Employee() {};

		Employee(string _f_name, string _l_name, int _age) :
			Person(_f_name, _l_name, _age) {};

		Employee(string _f_name, string _l_name, int _age, int _id) :
			Person(_f_name, _l_name, _age), id(_id) {};

		Employee(const Employee &e) :
			Person(e), department(e.department), salary(e.salary), id(e.id) {};

		Employee& operator=(const Employee &e);

		void SetSalary(int s);
		void SetDepartment(string dept);
		void SetId(int n);

		int GetId();
		string GetDepartment();

		virtual void Display(int);

		//Add here whatever you need

	protected:
		string department;
		int salary;
		int id;
};


class EmployeeCreator : public PersonCreator
{
	public:
		virtual Person* CreatePerson();
		virtual Person* CreatePerson(string _f_name, string _l_name, int _age);
		virtual Person* CreatePerson(const Employee &e);
};
