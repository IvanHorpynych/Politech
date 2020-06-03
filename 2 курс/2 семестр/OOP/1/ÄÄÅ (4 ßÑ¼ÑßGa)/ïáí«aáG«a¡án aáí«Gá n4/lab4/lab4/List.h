#pragma once
#include <iostream>
 
template <typename ElemType>
class List
{

	struct Node
	{
		Node();
		Node(ElemType _data, Node *_next);

		ElemType data;
		Node *next;
		
		friend std::ostream& operator<<(std::ostream& os, const Node &dt);
	};


	public:
		class Iterator
		{
			private:
				Node *ptr;

			public:
				Iterator(Node* p);
				Iterator(const Iterator &itr);

				operator bool() const;

				Iterator &operator++ ();

				ElemType &operator* ();
				ElemType &operator-> ();

				bool operator== (Iterator &right);
				bool operator!= (Iterator &right);
		};


	private:
		Node *head;
		int _size;

	public:

		//constructors / destructors
		List();
		~List();

		//methods

		void load(const char *filename);

		Iterator begin();										//Returns an iterator addressing the first element 
		Iterator end();											//Returns an iterator that addresses the location succeeding the last element


		void clear();											//Erases all the elements of a list.
		bool empty();											//Tests if a list is empty.

		Iterator find(const ElemType &val);						//Returns an iterator to the first element in a list that match a specified value.

		void push_front(const ElemType &val);					//Adds an element to the beginning of a list.
		void pop_front();										//Deletes the element at the beginning of a list.

		void remove(Iterator &itr);
		void remove(const ElemType &val);						//Erases first element in a list that match a specified value.

		int size();												//Returns the number of elements in a list.

		void splice(Iterator &_Where, List<ElemType>& _Right);	//Removes element from the target list and inserts 
																//it in first position of the argument list.

		void conjunction(List &_list);
	
		friend std::ostream& operator<<(std::ostream& os, const List &dt);
};




#pragma region Node_Implementation

template <typename ElemType>
List<ElemType>::Node::Node() : next(nullptr)
{}

template <typename ElemType>
List<ElemType>::Node::Node(ElemType _data, Node *_next) : data(_data), next(_next)
{}

//----------------------------------------------------------

template <typename ElemType>
std::ostream& operator<<(std::ostream& os, typename const List<ElemType>::Node &dt)
{
	os << dt.data;
	return os;
}

#pragma endregion



#pragma region Iterator_Implementation

template<typename ElemType>
List<ElemType>::Iterator::Iterator(Node *p) : ptr(p)
{}

template<typename ElemType>
List<ElemType>::Iterator::Iterator(const Iterator &itr) : ptr(itr.ptr)
{}

//----------------------------------------------------------

template<typename ElemType>
List<ElemType>::Iterator::operator bool() const
{
	return ptr ? true : false;
}

//----------------------------------------------------------

template<typename ElemType>
typename List<ElemType>::Iterator &List<ElemType>::Iterator::operator++ ()
{
	ptr = ptr->next;
	return *this;
}

//----------------------------------------------------------

template<typename ElemType>
ElemType& List<ElemType>::Iterator::operator* ()
{
	return ptr->data;
}

template<typename ElemType>
ElemType& List<ElemType>::Iterator::operator-> ()
{
	return ptr->data;
}

//----------------------------------------------------------

template<typename ElemType>
bool List<ElemType>::Iterator::operator== (Iterator &right_expr)
{
	return ptr == right_expr.ptr;
}

template<typename ElemType>
bool List<ElemType>::Iterator::operator!= (Iterator &right_expr)
{
	return ptr != right_expr.ptr;
}

#pragma endregion



#pragma region List_Implementation

template<typename ElemType>
List<ElemType>::List() : head(nullptr), _size(0)
{}

template<typename ElemType>
List<ElemType>::~List()
{
	clear();
}

//----------------------------------------------------------

template<typename ElemType>
void List<ElemType>::load(const char *filename)
{}

//----------------------------------------------------------

template<typename ElemType>
typename List<ElemType>::Iterator List<ElemType>::begin()
{
	return Iterator(head);
}

template<typename ElemType>
typename List<ElemType>::Iterator List<ElemType>::end()
{
	return Iterator(nullptr);
}

//----------------------------------------------------------

template<typename ElemType>
void List<ElemType>::clear()
{
	while (head)
		pop_front();
}

template<typename ElemType>
bool List<ElemType>::empty()
{
	return head ? false : true;
}

//----------------------------------------------------------

template<typename ElemType>
typename List<ElemType>::Iterator List<ElemType>::find(const ElemType &val)
{
	Node *runner = head;

	while (runner != nullptr && runner->data != val)
		runner = runner->next;

	return Iterator(runner);
}

//----------------------------------------------------------

template<typename ElemType>
void List<ElemType>::push_front(const ElemType &val)
{
	head = new Node(val, head);
	_size++;
}

template<typename ElemType>
void List<ElemType>::pop_front()
{
	Node *old_head = head;
	head = head->next;
	_size--;
	delete old_head;
}

//----------------------------------------------------------

template<typename ElemType>
void List<ElemType>::remove(Iterator &itr)
{
	Iterator old_iter(itr);
	++itr;
	remove(*old_iter);
}

template<typename ElemType>
void List<ElemType>::remove(const ElemType &val)
{
	Node *runner = head;

	while (runner->next != nullptr && runner->next->data != val && runner->data != val)
		runner = runner->next;
	
	if (runner->next)
	{		
		if (runner != head)
		{
			runner->next = runner->next->next;
			delete runner->next;
		}
		else
		{
			head = head->next;
			delete runner;
		}	
		_size--;
	}
	
}

//----------------------------------------------------------

template<typename ElemType>
int List<ElemType>::size()
{
	return _size;
}

//----------------------------------------------------------

template<typename ElemType>
void List<ElemType>::splice(Iterator &_Where, List<ElemType>& _Right)
{
	_Right.push_front(*_Where);
	remove(_Where);
}

//----------------------------------------------------------

template<typename ElemType>
void List<ElemType>::conjunction(List &_list)
{
	if (&_list != this && !_list.empty())
	{
		Node *tail = head;

		while (tail->next != nullptr)
			tail = tail->next;

		tail->next = _list.head;
		_size += _list._size;

		_list.head = nullptr;
		_list._size = 0;
	}
}

//----------------------------------------------------------

template<typename ElemType>
std::ostream& operator<<(std::ostream& os, const List<ElemType> &dt)
{
	for (Node &x : dt)
		os << x;

	return os;
}

#pragma endregion