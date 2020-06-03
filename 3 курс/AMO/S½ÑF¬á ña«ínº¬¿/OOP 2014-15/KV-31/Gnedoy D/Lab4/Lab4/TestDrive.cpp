/************************************************************************
*file: TestDrive.cpp
*author: Gnedoj D. O.
*group: KV-31, FPM
*written: 29/11/2014
*last modified: 29/11/2014
************************************************************************/

#include "Lab_4.h"

int main(){
	//testing creating by push_back
	DoublyLinkedList *test_1 = new DoublyLinkedList;
	ListNode cd("1");
	test_1->push_back(cd);
	ListNode cd1(".");
	test_1->push_back(cd1);
	ListNode cd2("Hello");
	test_1->push_back(cd2);
	ListNode cd3("world");
	test_1->push_back(cd3);	
	ListNode cd4("!");
	test_1->push_back(cd4);

	cout << "Create class \"test_1\" by push_back(): \n \t";
	test_1->print();
	cout << "\t";
	test_1->print_bkw();

	//testing creating by push_front
	DoublyLinkedList *test_2 = new DoublyLinkedList;
	ListNode cd_2("1");
	test_2->push_front(cd_2);
	ListNode cd1_2(".");
	test_2->push_front(cd1_2);
	ListNode cd2_2("Hello");
	test_2->push_front(cd2_2);
	ListNode cd3_2("world");
	test_2->push_front(cd3_2);	
	ListNode cd4_2("!");
	test_2->push_front(cd4_2);

	cout << "Create class \"test_2\" by push_front(): \n \t";
	test_2->print();
	cout << "\t";
	test_2->print_bkw();

	//testing sort of class
	test_1->sort();

	cout << "Sort class \"test_1\":\n \t";
	test_1->print();
	cout << "\t";
	test_1->print_bkw();

	//testing insert_after
	test_1->insert_after("Hello", cd2);

	cout << "Insert after \"Hello\" in class \"test_1\":\n \t";
	test_1->print();
	cout << "\t";
	test_1->print_bkw();

	//testing merge
	test_1->merge(*test_2);

	cout << "Merge class \"test_2\" in class \"test_1\":\n \t";
	test_1->print();
	cout << "\t";
	test_1->print_bkw();

	//testing erase
	test_1->erase("Hello");

	cout << "Delete all words \"Hello\" in class \"test_1\":\n \t";
	test_1->print();
	cout << "\t";
	test_1->print_bkw();

	//testing unique
	test_1->unique();

	cout << "Deletting all dublicates in class \"test_1\":\n \t";
	test_1->print();
	cout << "\t";
	test_1->print_bkw();

	//testing assign
	*test_2 = *test_1;
	test_2->assign(*test_1, 1, 2);

	cout << "Assign from 1 to 4 in class \"test_1\":\n \t";
	test_2->print();
	cout << "\t";
	test_2->print_bkw();
	cout << "test_1:\n \t";
	test_1->print();
	cout << "\t";
	test_1->print_bkw();

	//testing splice
	test_2->splice(2, *test_1);

	cout << "Splice to class \"test_2\":\n \t";
	test_2->print();
	cout << "\t";
	test_2->print_bkw();

	test_2->splice(0, *test_1, 0, 1);
	cout << "Splice to class \"test_2\" from 0 to 1 element:\n \t";
	test_2->print();
	cout << "\t";
	test_2->print_bkw();

	delete test_1;
	delete test_2;
	return 0;
}