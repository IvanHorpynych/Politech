/*
*author: Khupovetch Dmitry
*written: 14/12/2014
*/
#include "DoublyLinkedList.h"

using namespace std;
int main()
{
	DoublyLinkedList *making_list;
	making_list = new DoublyLinkedList;

	cout << "Push back elements to making_list : 'a', 'b', 'c' : \n";
	ListNode a("a");
	making_list->push_back(a);
	ListNode c("c");
	making_list->push_back(c);
	ListNode b("b");
	making_list->push_back(b);
	making_list->print();
	cout << endl << "-------------------------" << endl;

	cout << endl << "Size of making_list = " << making_list->size();
	cout << endl << "-------------------------" << endl << endl;

	ListNode k("k");
	cout << "Push front 'k' to making_list : ";
	making_list->push_front(k);
	making_list->print();
	cout << endl << "-------------------------" << endl;

	cout << endl << "Sorting making_list: ";
	making_list->sort();
	making_list->print();
	cout << endl << "-------------------------" << endl;

	cout << endl << "Insert ord 'w' to making_list : ";
	ListNode w("w");
	making_list->insert_ord(w);
	making_list->print();
	cout << endl << "-------------------------" << endl;

	cout << endl << "Insert 'y' to making_list : ";
	ListNode y("y");
	if (making_list->insert_after("a", y))
		making_list->print();
	else cout << "ERROR!!!" << endl;
	cout << endl << "-------------------------" << endl;

	DoublyLinkedList *list_for_test = new DoublyLinkedList;
	list_for_test = new DoublyLinkedList;
	ListNode tm("1");
	list_for_test->push_back(tm);
	ListNode tm2("2");
	list_for_test->push_back(tm2);
	ListNode tm3("3");
	list_for_test->push_back(tm3);
	ListNode tm4("4");
	list_for_test->push_back(tm4);
	ListNode tm5("5");
	list_for_test->push_back(tm5);
	cout << "Before list_for_test:\n ";
	list_for_test->print();
	cout << endl;
	list_for_test->operator=(*making_list);
	cout << "After list_for_test:\n ";
	list_for_test->print();
	cout << endl << "-------------------------" << endl;

	cout << endl << "Pop front for making_list: \n" << endl;
	making_list->pop_front();
	making_list->print();
	cout << endl << "-------------------------" << endl;

	cout << endl << "Pop back for making_list: \n" << endl;
	making_list->pop_back();
	making_list->print();
	cout << endl << "-------------------------" << endl;

	cout << endl << "List_for_test : ";
	list_for_test->push_back(tm);
	list_for_test->push_back(tm2);
	list_for_test->push_back(tm3);
	list_for_test->push_back(tm4);
	list_for_test->push_back(tm5);
	list_for_test->print();
	making_list->merge(*list_for_test);
	cout << "\nMaking_merge : ";
	making_list->print();
	cout << endl << "-------------------------" << endl << endl;

	list_for_test = new DoublyLinkedList;
	list_for_test->push_back(tm);
	list_for_test->push_back(tm2);
	list_for_test->push_back(tm3);
	list_for_test->push_back(tm4);
	list_for_test->push_back(tm5);
	list_for_test->print();
	cout << endl << "Making_list assign (2,4): ";
	making_list->assign(*list_for_test, 2, 4);
	making_list->print();
	cout << endl << "-------------------------" << endl << endl;
	
	list_for_test = new DoublyLinkedList;
	list_for_test->push_back(tm);
	list_for_test->push_back(tm2);
	list_for_test->push_back(tm3);
	list_for_test->push_back(tm4);
	list_for_test->push_back(tm5);
	list_for_test->print();
	cout << "\n Making_list splice(after 3) : ";
	making_list->splice(3, *list_for_test);
	making_list->print();
	cout << endl << "-------------------------" << endl << endl;

	cout << "\n Making_list splice(after 8) (1,3): ";
	making_list->splice(8, *list_for_test, 1, 3);
	making_list->print();
	cout << endl << "-------------------------" << endl << endl;

	cout << "\n Making_list print_bkw : ";
	making_list->print_bkw();
	cout << endl << "-------------------------" << endl << endl;

	cout << endl << "Making_list erase 'k' : ";
	making_list->erase("k");
	making_list->print();
	cout << endl << "-------------------------" << endl << endl;

	cout << "\n Making_list unique : ";
	making_list->push_back(a);
	making_list->push_back(a);
	making_list->push_back(a);
	making_list->push_back(a);
	making_list->push_back(a);
	making_list->print();
	making_list->unique();
	cout << "Before:" << endl;
	making_list->print();
	cout << endl << "-------------------------" << endl << endl;

	making_list->clear();
	cout << "Cleared making_list : " << endl;
	making_list->print();
	cout << endl << "-------------------------" << endl << endl;

	_getch();
	return 0;
}