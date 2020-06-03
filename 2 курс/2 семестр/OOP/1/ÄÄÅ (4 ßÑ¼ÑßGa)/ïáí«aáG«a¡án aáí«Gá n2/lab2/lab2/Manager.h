#pragma once
#include <list>
#include "Employee.h"

class Manager : public Employee
{
	public:
		Manager() {};
		
		Manager(string _f_name, string _l_name, int _age) :
			Employee(_f_name, _l_name, _age) {};

		Manager(string _f_name, string _l_name, int _age, int _id) :
			Employee(_f_name, _l_name, _age, _id) {};
	
		Manager(const Manager &m) :
			Employee(m), subordinates(m.subordinates) {}
		
		Manager& operator=(const Manager &m);

		virtual void Display(int);

		//add an employee to the subordinates list
		Person* AddSubordinate(Person *p);
		void DisplaySubordinates();
		//Add here whatever you need

	private:
		list<Person*> subordinates;	//список подчиненных
};


class ManagerCreator : public PersonCreator
{
public:
	virtual Person* CreatePerson();
	virtual Person* CreatePerson(string _f_name, string _l_name, int _age);
	virtual Person* CreatePerson(const Manager &e);
};