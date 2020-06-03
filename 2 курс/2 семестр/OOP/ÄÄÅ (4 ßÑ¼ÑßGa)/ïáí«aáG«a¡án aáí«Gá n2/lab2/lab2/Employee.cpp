#include "Employee.h"

#pragma region Employee

Employee & Employee::operator=(const Employee &e)
{ 
	if (this != &e) 
	{ 
		f_name = e.f_name;  
		l_name = e.l_name; 
		age = e.age; 
		department = e.department;
		salary = e.salary;
		id = e.id;
	}
	return *this;
}

void Employee::SetSalary(int s)
{
	salary = s;
}

void Employee::SetDepartment(string dept)
{
	department = dept;
}

void Employee::SetId(int n)
{
	id = n;
}

int Employee::GetId()
{
	return id;
}

string Employee::GetDepartment()
{
	return department;
}

void Employee::Display(int i)
{
	cout << setw(i * 10) << "Employment type: Employee" << endl
		 << setw(i * 10) << "id: " << id << endl
		 << setw(i * 10) << f_name << " " << l_name << " age: " << age << " salary: " << salary << endl
		 << setw(i * 10) << "Department: " << department << endl << endl;
}

#pragma endregion 


#pragma region EmployeeCreator

Person* EmployeeCreator::CreatePerson()
{
	return new Employee();
}

Person* EmployeeCreator::CreatePerson(string _f_name, string _l_name, int _age)
{
	return new Employee(_f_name, _l_name, _age);
}

Person* EmployeeCreator::CreatePerson(const Employee &e)
{
	return new Employee(e);
}

#pragma endregion