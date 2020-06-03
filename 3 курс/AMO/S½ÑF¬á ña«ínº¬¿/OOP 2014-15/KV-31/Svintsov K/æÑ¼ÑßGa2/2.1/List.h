#pragma once
#include <string.h>
#include <stdio.h>
struct ListNode{
    char *str;
    ListNode* next;
    ListNode* prev;
};

typedef const ListNode* POSITION;

class StringList{
    public:
        //Constructs an empty list for ListNode objects.
        StringList();
        ~StringList();

        POSITION GetHead();//Returns the head element of the list
        POSITION GetTail();//Returns the tail element of the list
        //Adds an element to the head of the list (makes a new head).
        void AddHead(const char *);
		//Adds all the elements in another list to the head of the list (makes a new head).
		void AddHead(const StringList *);
		//Adds an element to the tail of the list (makes a new tail).
		void AddTail(const char *);
		//Adds all the elements in another list to the tail of the list (makes a new tail).
		void AddTail(const StringList *);
		//Removes all the elements from this list.
		void RemoveAll();
		//Removes the element from the head of the list.
		void RemoveHead();
		//Removes the element from the tail of the list.
		void RemoveTail();
		//Adds the elements to the tail of another list except those which are already in 
		void AppendExclusively(const StringList *);
		//removes all duplicate elements
		void Unique();
		//Inserts a new element after a given position.
		void InsertAfter(char *, int);
		//Inserts a new element before a given position.
		void InsertBefore(char *, int);
		//Gets the element at a given position.
		const char* GetAt(int)const;
		//Removes an element from this list as specified by position.
		void RemoveAt(int);
		//Sets the element at a given position.
		void SetAt(char *, int);
		//Gets the position of an element specified by string value.
		POSITION Find(char *);
		//Gets the position of an element specified by a zero-based index.
		int FindIndex(char *)const;
		void Splice(POSITION where, StringList *sl, POSITION first, POSITION last);
		//Gets the next element for iterating.
		POSITION GetNext();
		//Gets the previous element for iterating.
		POSITION GetPrev();
		POSITION GetHeadPosition();
		//Prints the element of a list
		void PrintNode(POSITION);
		//Gets the size of a list
		int GetSize()const;
		//Checks the list for emptiness
		bool IsEmpty()const;
		//Prints all the elements of a list
		void PrintList();
    private:
		ListNode* startPTR, *endPTR;
		POSITION itrPTR;
};

