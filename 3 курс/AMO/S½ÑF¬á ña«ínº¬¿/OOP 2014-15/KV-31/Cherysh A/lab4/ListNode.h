/************************************************************************
*file: ListNode.h
*purpose: declarations for class
*author: Andrey Chernysh
*written: 01/12/2014
*last modified: 09/12/2014
*************************************************************************/
class ListNode{
	char *data;
	ListNode *prev;
	ListNode *next;
public:
	friend class DoublyLinkedList;
	ListNode();//default constructor
	ListNode(char *_data);
	~ListNode(void);
};


