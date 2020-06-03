/*
*author: Khupovetch Dmitry
*written: 14/12/2014
*/
#pragma once
#include <conio.h>
#include <iostream>

class ListNode
{
	char *data;
	ListNode *prev;
	ListNode *next;
public:
	friend class DoublyLinkedList;
	ListNode();//default constructor
	ListNode(char *_data);
	~ListNode(void);
};

