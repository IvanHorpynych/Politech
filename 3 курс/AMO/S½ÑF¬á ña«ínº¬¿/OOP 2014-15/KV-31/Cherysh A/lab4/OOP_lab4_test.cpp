/************************************************************************************
*file: OOP_lab4_test.cpp															*
*Synopsis: this is a test file with main function.									*
*in the "DoublyLinkedList.cpp".														*
*related files: DoublyLinkedList.h,ListNode.cpp,ListNode.h							*
*author: Chernysh Andrey															*
*written: 1/12/2014															     	*
*last modified: 09/12/2014															*
************************************************************************************/
#include "DoublyLinkedList.h"

using namespace std;
int main(){
	DoublyLinkedList *mylist;//main list
	mylist = new DoublyLinkedList;
	
	//initializing helper list "testlist"
	DoublyLinkedList *testlist = new DoublyLinkedList;//helper list;
	ListNode temp("1");
	testlist->push_back(temp);
	ListNode temp2("2");
	testlist->push_back(temp2);
	ListNode temp3("3");
	testlist->push_back(temp3);
	ListNode temp4("4");
	testlist->push_back(temp4);
	ListNode temp5("5");
	testlist->push_back(temp5);
	ListNode temp6("6");
	testlist->push_back(temp6);
	ListNode temp7("7");
	testlist->push_back(temp7);
	
	//Print empty mylist;
	cout << "Print list : ";
	mylist->print();
	cout << endl;

	//Empty;
	cout << endl << "Is empty? : " << mylist->empty() << endl;
	cout << endl;

	//Push_back;
	cout << "push back elements : 'h', 'c', 'a' : \t";
	ListNode c("h");
	mylist->push_back(c);
	ListNode d("c");
	mylist->push_back(d);
	ListNode e("a");
	mylist->push_back(e);
	mylist->print();
	cout << endl;
	
	//Size;
	cout << endl << "Size = " << mylist->size();
	cout << endl << "Is empty? : " << mylist->empty() << endl;
	cout << endl;
	
	//Push_front;
	ListNode b("k");
	cout << "Push_front 'k' : ";
	mylist->push_front(b);
	mylist->print();
	cout << endl;
	
	//Sorting by Select #6;
	cout << endl << "Sorting by Select #6: ";
	mylist->sort();
	mylist->print();
	cout << endl;

	//Insert_ord;
	cout << endl << "Insert_ord(f) : ";
	ListNode k("f");
	mylist->insert_ord(k);
	mylist->print();
	cout << endl;

	//Insert after;
	cout << endl << "Insert 'ins' after 'a' : ";
	ListNode z("ins");
	if(mylist->insert_after("a", z))
		mylist->print();
	else cout << "ERROR!!!" << endl;
	cout << endl;

	//Overloaded of the assignment operator
	DoublyLinkedList *testlist2 = new DoublyLinkedList;//list for testing #2;
	cout << endl << "Testlist2 := mylist(memory location is different)" << endl << "\t Testlist2 : ";
	testlist2->operator=(*mylist);
	testlist2->print();
	cout << endl << "let see that memory location is different : " << endl << "insert 'ins' after 'a' in the list Testlist2: ";
	testlist2->insert_after("a", z);
	testlist2->print();
	cout << endl << "Mylist : ";
	mylist->print();
	cout << endl;

	//Pop front from testlist2;
	cout << endl << "Pop front" << endl;
	testlist2->pop_front();
	testlist2->print();
	cout << endl;

	//Pop back from testlist2;
	cout << endl << "Pop back" << endl;
	testlist2->pop_back();
	testlist2->print();
	cout << endl;

	//Merge and clear;
	cout << endl << "testlist2 : ";
	testlist2->print();
	mylist->merge(*testlist2);
	cout << "\n Mylist merge : ";
	mylist->print();
	cout << endl << "Testlist2(using clear) : ";
	testlist2->print();
	cout << endl;

	//Assign, using testlist1;
	cout << endl << "Mylist assign (1,3): ";
	mylist->assign(*testlist, 1, 3);
	mylist->print();
	cout << "\n\t testlist : ";
	testlist->print();
	cout << endl;

	//Mylist splice;
	cout << "\n mylist splice(after 6) : ";
	mylist->splice(6, *testlist);
	mylist->print();
	cout << endl;

	//Mylist splice2;
	cout << "\n mylist splice2 (1,2): ";
	mylist->splice(3,*testlist,1,2);
	mylist->print();
	cout << endl;
	
	//Print backward;
	cout << "\n Mylist print_bkw : ";
	mylist->print_bkw();
	cout << endl;

	//Erase
	cout << endl <<  "Mylist erase 'k' : ";
	mylist->erase("k");
	mylist->print();
	cout << endl;

	//Unique
	cout << "\n mylist unique : ";
	mylist->unique();
	mylist->print();
	cout << endl;
	
	//Clear
	testlist->clear();
	cout << "cleared testlist : " << endl;
	testlist->print();

	_getch();
	return 0;
}