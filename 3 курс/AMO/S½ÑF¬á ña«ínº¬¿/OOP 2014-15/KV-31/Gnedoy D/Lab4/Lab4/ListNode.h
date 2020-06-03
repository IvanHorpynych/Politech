#include <stdlib.h>
#include <malloc.h>
#include <iostream>

class ListNode{
	char *data;
	ListNode *prev;
	ListNode *next;
public:
	friend class DoublyLinkedList;
	ListNode();
	ListNode(char *_data);
	~ListNode(void);
};