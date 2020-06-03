#include "References.h"

int main()
{
	_references::load("References.csv");
	
	for (List<_references>::Iterator itr = _references::begin(); itr; itr++)
		std::cout << *itr << std::endl;
	
	std::cout << std::endl;

	_references::delete_less(4);

	for (List<_references>::Iterator itr = _references::begin(); itr; itr++)
		std::cout << *itr << std::endl;

	_references::reestablish_all();

	std::cout << std::endl;

	for (List<_references>::Iterator itr = _references::begin(); itr; itr++)
		std::cout << *itr << std::endl;

	List<_references>::Iterator itr = _references::find("Darnytsia");
	(*itr).edit_word("Vinnytsia");

	std::cout << std::endl;

	for (List<_references>::Iterator itr = _references::begin(); itr; itr++)
		std::cout << *itr << std::endl;

	system("PAUSE");
	return 0;
}