#ifndef LIST_NODE_H
#define LIST_NODE_H
#include <string.h>

class ListNode{
	char *data;
	ListNode *prev;
	ListNode *next;
public:
	friend class DoublyLinkedList;
	ListNode():data(0),prev(0),next(0){};//default constructor
	ListNode(const char *_data ):prev(0),next(0){
		if (data){
			data = new char[strlen(_data)+1];
			strcpy(data,_data); 
		}
	};
	~ListNode(void){ delete[] data; };
};
#endif
