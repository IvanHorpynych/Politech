#include "ConcreteReference.h"
#include "References.h"

int main()
{
	References &refs = References::Instance();

	refs.load("References.csv");
	
	for (List<ConcreteReference>::Iterator itr = refs.begin(); itr; ++itr)
		std::cout << *itr << std::endl;
	
	std::cout << std::endl;

	refs.delete_less(4);

	for (List<ConcreteReference>::Iterator itr = refs.begin(); itr; ++itr)
		std::cout << *itr << std::endl;

	refs.reestablish_all();

	std::cout << std::endl;

	for (List<ConcreteReference>::Iterator itr = refs.begin(); itr; ++itr)
		std::cout << *itr << std::endl;

	List<ConcreteReference>::Iterator itr = refs.find("Darnytsia");
	(*itr).edit_word("Vinnytsia");

	std::cout << std::endl;

	for (List<ConcreteReference>::Iterator itr = refs.begin(); itr; ++itr)
		std::cout << *itr << std::endl;

	system("PAUSE");
	return 0;
}