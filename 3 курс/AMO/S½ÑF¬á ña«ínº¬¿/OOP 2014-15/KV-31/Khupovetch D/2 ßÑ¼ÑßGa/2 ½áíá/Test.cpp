#include "CTranslate.h"
#include <iterator>
#include <list>

int main()
{ 
	list<CTranslate> lst;
	FillList(&lst, "123.csv");
	ListAll(&lst);
	cout << "__________________________" << endl << endl;
	SortByIta(&lst);
	ListAll(&lst);
	cout << "\n________________________" << endl << endl;
	_getch();
}