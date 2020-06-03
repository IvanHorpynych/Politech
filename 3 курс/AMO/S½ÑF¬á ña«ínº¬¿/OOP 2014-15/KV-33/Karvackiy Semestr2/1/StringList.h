/*
 * StringList.h
 *
 *  Created on: 11 февр. 2015 г.
 *      Author: supremist
 */

#ifndef STRINGLIST_H_
#define STRINGLIST_H_

#include <string.h>
#include <stdio.h>
#include <iostream>

struct ListNode{
	char *str;
	ListNode* next;
	ListNode* prev;
public:
	ListNode (const char *nstr){
			int len=strlen (nstr);
			str = new char [len];
			memcpy(str, nstr, len);
			prev=nullptr;
			next=nullptr;
	}
	~ListNode (){
		delete [] str;
	}
};

typedef  ListNode* POSITION;

class StringList{
public:
	//Constructs an empty list for ListNode objects.
	 StringList();
	~StringList();

		//Head/Tail Access
	POSITION GetHead() const;//Returns the head element of the list
	POSITION GetTail() const;//Returns the tail element of the list

		//Operations
	//Adds an element to the head of the list (makes a new head).
	void AddHead(const char *nstr);
	//Adds all the elements in another list to the head of the list (makes a new head).
	void AddHead(const StringList *nlst);
	//Adds an element to the tail of the list (makes a new tail).
	void AddTail(const char *nstr);
	//Adds all the elements in another list to the tail of the list (makes a new tail).
	void AddTail(const StringList *nlst);
	//Removes all the elements from this list.
	void RemoveAll();
	//Removes the element from the head of the list.
	void RemoveHead();
	//Removes the element from the tail of the list.
	void RemoveTail();
 	void AppendExclusively(const StringList *sl);
	void Splice(POSITION where, StringList *sl, POSITION first, POSITION last);
	//removes all duplicate elements
	void Unique();


		//Iteration
	//Gets the next element for iterating.
	POSITION GetNext();
	//Gets the previous element for iterating.
	POSITION GetPrev();

		//Retrieval/Modification
	POSITION GetHeadPosition();


	//Gets the element at a given position.
	const char* GetAt(int indx)const;
	//Removes an element from this list as specified by position.
	void RemoveAt(int indx);
	//Sets the element at a given position.
	void SetAt(char *text , int indx);

		//Insertion
	//Inserts a new element after a given position.
	void InsertAfter(char *text, int indx);
	//Inserts a new element before a given position.
	void InsertBefore(char *text, int indx);

	//Inserts a new element after a given position.
	void InsertAfter(POSITION where,const char * elem);
	//Inserts a new element before a given position.
	void InsertBefore(POSITION where,const char * elem);
	void Remove (POSITION where);

		//Searching
	//Gets the position of an element specified by string value.
	POSITION Find(const char *text)const;
	POSITION Find(int indx)const;
	//Gets the position of an element specified by a zero-based index.
	int FindIndex(char *text)const;

		//Status
	//Returns the number of elements in this list.
	int Getsize()const;
	//Tests for the empty list condition (no elements).
	bool IsEmpty()const;

	void Printnode( POSITION p);

private:
	ListNode *iterator;
	ListNode *head;
	ListNode *tail;
};


#endif /* STRINGLIST_H_ */
