#pragma once
#include <string>
#include <iostream>
#include <iomanip>

using namespace std;

//An abstract class
class Person
{
	public:
		Person() {};
	
		Person(string _f_name, string _l_name, int _age) : age(_age),
			f_name(_f_name), l_name(_l_name) {}

		Person(const Employee &e) : age(e.age),
			f_name(e.f_name), l_name(e.l_name) {}

		void Display() { Display(0); }
		virtual void Display(int) = 0;

		protected:
			string f_name;	//first name
			string l_name;	//last name
			int age;
};


class PersonCreator
{
	public:
		virtual Person* CreatePerson() = 0;
		virtual Person* CreatePerson(string _f_name, string _l_name, int _age) = 0;
		virtual Person* CreatePerson(const Employee &e) = 0;
};