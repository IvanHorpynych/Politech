#include <stdio.h>
#include "Student.h"
#include <list>

int main(){
	const int gr_num = 4;


	CStudent* st1 = new CStudent();
	st1->SetInfo("Kyrylo", "spok", 88);
	int vec2[gr_num] = { 5, 1, 3, 1 };
	st1->SetGrades(vec2, gr_num);

	CStudent* st3 = new CStudent();
	*st3 = *st1;

	//delete st1;


	_LStud MyList;
	ReadFile("journal.csv",MyList);
	MyList.push_back(*st3);
	
	
	SortByGrades(MyList);
	ListAll(MyList);
	printf("\nAfter erasing the element of the list:\n");
	DeleteById(MyList, 88);
	ListAll(MyList);
	DeleteAll(MyList);
	MyList.push_front(*st3);
	printf("\n View of the list after it was cleared:\n");
	ListAll(MyList);
	getchar();

	return 0;
}