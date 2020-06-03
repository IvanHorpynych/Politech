#include "Manager.h"

#pragma region Manager

Manager & Manager::operator=(const Manager & m)
{
	if (this != &m)
	{
		Employee::operator=(m);
		subordinates = m.subordinates;
	}
	return *this;
}

void Manager::Display(int i)
{
	cout << setw(i * 10) << "Employment type: Employee" << endl
		 << setw(i * 10) << "id: " << id << endl
		 << setw(i * 10) << f_name << " " << l_name << " age: " << age << " salary: " << salary << endl
		 << setw(i * 10) << "Department: " << department << endl
		 << setw(i * 10) << "Subordinates" << endl;
	
	if (subordinates.empty()) cout << "none" << endl;

	for (Person *p : subordinates)
		p->Display(i + 1);
	
}

Person* Manager::AddSubordinate(Person * p)
{
	 subordinates.push_back(p);
	 return p;
}

void Manager::DisplaySubordinates()
{
	for (Person *p : subordinates)
		p->Display();
}

#pragma endregion 


#pragma region ManagerCreator

Person* ManagerCreator::CreatePerson()
{
	return new Manager();
}

Person* ManagerCreator::CreatePerson(string _f_name, string _l_name, int _age)
{
	return new Manager(_f_name, _l_name, _age);
}

Person* ManagerCreator::CreatePerson(const Manager & e)
{
	return new Manager(e);
}

#pragma endregion 
