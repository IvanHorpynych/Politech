#include "DoublyLinkedList.h"

void test_push();
void test_order();
void test_delete();
void test_merge();

int main(){
	test_push();
	test_order();
	test_delete();
	test_merge();
	return 0;
}

void test_push(){
	DoublyLinkedList list;
	ListNode first("first");
	ListNode second("second");
	ListNode third("third");

	std::cout << "EMPTY LIST TEST:\n";
	list.print();

	list.push_back(first);
	list.push_front(first);

	std::cout << "PUSHED \"first\":\n";
	list.print();
	std::cout << "SIZE = " << list.size() << "\n";

	list.push_back(third);

	std::cout << "PUSHED \"third\":\n";
	list.print();
	std::cout << "SIZE = " << list.size() << "\n";

	list.push_front(second);

	std::cout << "PUSHED \"second\" TO FRONT:\n";
	list.print();
	std::cout << "SIZE = " << list.size() << "\n";

	list.push_back(second);

	std::cout << "PUSHED \"second\" TO BACK:\n";
	list.print();
	std::cout << "SIZE = " << list.size() << "\n";

	list.insert_after("third",first);
	std::cout << "PUSHED \"first\" AFTER \"third\":\n";
	list.print();
	std::cout << "SIZE = " << list.size() << "\n";

	list.pop_back();
	list.pop_front();

	std::cout << "POPPED ELEMENTS FROM BACK AND FRONT\n";
	list.print();
	std::cout << "SIZE = " << list.size() << "\n";


}

void test_order(){
	ListNode item("aaa"),item2("bbb"),item3("ccc");
	DoublyLinkedList list;
	for(int i = 0; i<22; i++){
		list.push_front(item);
		//list.push_front(item2);
		list.push_front(item3);
	}
	std::cout << "START STATE:\nSIZE = " << list.size() << "\n";
	system("PAUSE");
	list.print();

	list.sort();
	std::cout << "SORTED:\nSIZE = " << list.size() << "\n";
	system("PAUSE");
	list.print();


	list.insert_ord(item2);
	std::cout << "PUSHED \"bbb\" IN ORDER:\nSIZE = " << list.size() << "\n";
	system("PAUSE");
	list.print();

	std::cout << "BACKWARD OUTPUT TEST\n";
	system("PAUSE");
	list.print_bkw();

}

void test_delete(){

	DoublyLinkedList list;
	ListNode first("first");
	ListNode second("second");
	ListNode third("third");

	for(int i = 0; i<5; i++){
		list.push_front(first);
		list.push_front(second);
		list.push_front(third);
	}
	
	std::cout << "START STATE:\nSIZE = " << list.size() << "\n";
	system("PAUSE");
	list.print();

	list.erase("first");
	std::cout << "DELETED ALL INSTANCES OF FIRST first:\nSIZE = " << list.size() << "\n";
	system("PAUSE");
	list.print();

	list.push_front(first);
	list.unique();
	std::cout << "applied unique():\nSIZE = " << list.size() << "\n";
	system("PAUSE");
	list.print();

	list.clear();
	std::cout << "CLEARED LIST:\nSIZE = " << list.size() << "\n";
	list.print();

}

void test_merge(){
	DoublyLinkedList list1,list2;
	ListNode merge1("unique1"),merge2("unique2"),merge3("unique3");

	for (int i = 0; i < 6; i++){
		list1.push_front(merge1);
	}
	std::cout << "operator= test:\n LIST1:\n";
	list1.print();
	list2 = list1;
	std::cout << "LIST2:\n";
	list2.print();

	system("PAUSE");
	list2.clear();
	list2.push_back(merge3);
	list2.push_back(merge2);

	std::cout << "NEW CONTENT OF LIST2\n";
	list2.print();
	list1.merge(list2);
	std::cout << "LIST1 MERGED WITH LIST2\n";
	list1.print();

	list2.assign(list1,4,7);
	std::cout << "LIST2 CONTENT AFTER ASSIGNING ELEMENTS FROM 4 TO 7\n";
	list2.print();
	list2.push_front(merge3);
	list2.push_front(merge2);
	list2.push_back(merge1);

	system("PAUSE");
	system("cls");
	std::cout <<"LIST1\n";
	list1.print();
	std::cout <<"LIST2\n";
	list2.print();

	std::cout <<"list1.splice(2,list2)\n";
	list1.splice(4,list2);
	list1.print();

	list1.clear();
	for(int i = 0; i < 4; i++){
		list1.push_back(merge1);
	}
	std::cout <<"list1.splice(2,list2,1,7)\n";
	list1.splice(4,list2,1,7);
	list1.print();
}
