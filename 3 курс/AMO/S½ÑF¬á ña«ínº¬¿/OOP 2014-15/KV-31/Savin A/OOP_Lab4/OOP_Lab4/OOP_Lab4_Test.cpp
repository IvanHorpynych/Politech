#include "OOP_Lab4.h"
int main(){
	//push_back creation
	DoublyLinkedList *list1 = new DoublyLinkedList;
	ListNode l1("1");
	list1->push_back(l1);
	ListNode l2("6");
	list1->push_back(l2);
	ListNode l3("4");
	list1->push_back(l3);
	ListNode l4("2");
	list1->push_back(l4);
	ListNode l5("3");
	list1->push_back(l5);
	cout << "Create class \"list1\" by push_back(): \n \t";
	list1->print();
	cout << "\t";
	list1->print_bkw();
	cout << "Number of elements in the list1 is: " << list1->size() << "\n";

	//push_front creation
	DoublyLinkedList *list2 = new DoublyLinkedList;
	ListNode l_1("7");
	list2->push_front(l_1);
	ListNode l_2("4");
	list2->push_front(l_2);
	ListNode l_3("8");
	list2->push_front(l_3);
	ListNode l_4("3");
	list2->push_front(l_4);
	ListNode l_5("2");
	list2->push_front(l_5);
	cout << "Create class \"list2\" by push_front(): \n \t";
	list2->print();
	cout << "\t";
	list2->print_bkw();
	cout << "Number of elements in the list2 is: " << list2->size() << "\n";
	//testing pop_back and pop_front
	list1->pop_back();
	list2->pop_front();
	cout << "List1 with deleted last element by pop_back(): \n \t";
	list1->print();
	cout << "\t";
	list1->print_bkw();
	cout << "List2 with deleted first element by pop_front(): \n \t";
	list2->print();
	cout << "\t";
	list2->print_bkw();
	//returning elements to the list
	list1->push_back(l5);
	cout << "List1 with returned element \n \t";
	list1->print();
	cout << "\t";
	list1->print_bkw();
	list2->push_front(l_5);
	cout << "List2 with returned element \n \t";
	list2->print();
	cout << "\t";
	list2->print_bkw();
	//testing sort of class
	list1->sort();
	cout << "Sort class \"list1\":\n \t";
	list1->print();
	cout << "\t";
	list1->print_bkw();
	//testing insert ord
	ListNode l6("5");
	list1->insert_ord(l6);
	cout << "Insert_ord class \"list1\":\n \t";
	list1->print();
	cout << "\t";
	list1->print_bkw();
	//testing insert_after
	ListNode l7("!");
	list1->insert_after("3", l7);

	cout << "Insert after \"3\" in class \"list1\":\n \t";
	list1->print();
	cout << "\t";
	list1->print_bkw();

	//testing merge
	list1->merge(*list2);
	cout << "Merge class \"list2\" in class \"list1\":\n \t";
	list1->print();
	cout << "\t";
	list1->print_bkw();
	list2->print();

	//testing erase
	list1->erase("!");

	cout << "Delete all words \"!\" in class \"list1\":\n \t";
	list1->print();
	cout << "\t";
	list1->print_bkw();

	//testing unique
	list1->unique();

	cout << "Deletting all dublicates in class \"list1\":\n \t";
	list1->print();
	cout << "\t";
	list1->print_bkw();

	//testing assign
	list2->push_front(l_1);
	list2->push_front(l_2);
	list2->push_front(l_3);
	list2->push_front(l_4);
	list2->push_front(l_5);
	list2->assign(*list1, 1, 4);

	cout << "Assign from 1 to 4 in class \"list2\":\n \t";
	list2->print();
	cout << "\t";
	list2->print_bkw();
	cout << "list1:\n \t";
	list1->print();
	cout << "\t";
	list1->print_bkw();

	//testing splice
	list2->splice(4, *list1);

	cout << "Splice to class \"list2\" from 4th position:\n \t";
	list2->print();
	cout << "\t";
	list2->print_bkw();

	list2->splice(3, *list1, 2, 3);
	cout << "Splice to class \"list2\" from 3rd position elements 2, 3:\n \t";
	list2->print();
	cout << "\t";
	list2->print_bkw();
	delete list1;
	delete list2;
	char ch;
	gets(&ch);
	return 0;
}
