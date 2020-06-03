/************************************************************************
*file: DoublyLinkedList.cpp
*synopsis: all base functions which work with lists.
* These functions are declared in the include file "DoublyLinkedList.h".
*related files: none
*author: Chernysh Andrey
*written: 01/12/2014
*last modified: 09/12/2014
************************************************************************/
#include "DoublyLinkedList.h"

DoublyLinkedList::DoublyLinkedList(void){}
int DoublyLinkedList::size(){
	ListNode *temp = head;
	int count;
	for (count = 0; temp; count++){
		temp = temp->next;
	}
	return count;
}
bool DoublyLinkedList::empty(){
	if (!head)
		return true;
	else return false;
}
void DoublyLinkedList::clear(){
	ListNode *temp = new ListNode;
	while (head->next){
		temp = head;
		head = head->next;
		delete(temp);
	}
	head = NULL;
}
void DoublyLinkedList::push_back(const ListNode &a){
	ListNode *p, *temp = new ListNode;
	temp->data = a.data;
	if (!head){
		head = temp;
	}
	else
	{
		p = head;
		while (p->next)
			p = p->next;
		p->next = temp;
		temp->prev = p;
	}
}
void DoublyLinkedList::push_front(const ListNode &a){
	ListNode *temp = new ListNode;
	temp->data = a.data;
	if (!head){
		head = temp;
	}
	else
	{
		head->prev = temp;
		temp->next = head;
		head = temp;
	}
}
void DoublyLinkedList::pop_front(){
	if (empty()) return;
	else if ((head->next == head) && (head->prev == head)){
		head = NULL;
	}
	else{
		ListNode *temp = head;	
		head = head->next;
		delete(temp);
	}
}
void DoublyLinkedList::pop_back(){
	if (empty()) return;
	else if ((head->next == head) && (head->prev == head)){
		head = NULL;
	}
	else{
		ListNode *p, *temp = head;
		while(temp->next){
			temp = temp->next;
		}
		p = temp->prev;
		p->next = NULL;
		delete(temp);
	}
	return;
}
void DoublyLinkedList::insert_ord(const ListNode &nd){
	ListNode *p = head, *temp = new ListNode;
	temp->data = nd.data;
	while (strcmp(p->data, temp->data) < 0)
		p = p->next;
	p = p->prev;
	temp->next = p->next;
	temp->prev = p;
	p->next = temp;
	p = temp->next;
	p->prev = temp;
}
void DoublyLinkedList::sort(){
	ListNode *minp, *sp = head, *ip;
	char* temp;
	int imin, s;
	int size = DoublyLinkedList::size();
	for (s = 0; s< size; s++){
		imin = s;
		minp = sp;
		ip = sp->next;
		for (int i = s + 1; i < size; i++){
			if (strcmp(ip->data,minp->data) < 0){
				imin = i;
				minp = ip;
			}
			ip = ip->next;
		}
		if (imin != s){
			temp = minp->data;
			minp->data = sp->data;
			sp->data = temp;
		}
		sp = sp->next;
	}
}
bool DoublyLinkedList::insert_after(char *dat, const ListNode &nd){
	ListNode *ins = new ListNode, *p = head;
	ins->data = nd.data;
	while ((!(strcmp(p->data, dat) == 0)) && (p->next))
		p = p->next;
	if (strcmp(p->data, dat) != 0)
		return false;
	else{
		ins->next = p->next;
		ins->prev = p;
		p->next = ins;
		p = ins->next;
		p->prev = ins;
		return true;
	}
}
void DoublyLinkedList::operator=(const DoublyLinkedList &list){
	head = new ListNode;
	head->data = list.head->data;
	ListNode *ptrlist = list.head->next;
	ListNode *ptrhead = head;
	ListNode *temp;
	while (ptrlist){
		temp = new ListNode;
		temp->data = ptrlist->data;
		ptrhead->next = temp;
		temp->prev = ptrhead;
		ptrhead = temp;
 		ptrlist = ptrlist->next;
	}
}
void DoublyLinkedList::merge(DoublyLinkedList &list){
	ListNode *ptrlist = list.head, *ptrhead = head, *temp = new ListNode;
	while (ptrlist){
		DoublyLinkedList::push_back(*ptrlist);
		ptrlist = ptrlist->next;
	}
	list.clear();
	DoublyLinkedList::sort();
}
void DoublyLinkedList::erase(char *dat){
	ListNode *p = head, *temp = new ListNode;
	while (p){
		temp = new ListNode;
		if (p->data == dat){
			if (p == head){
				temp = head;
				head = head->next;
			}
			else{
				if (!p->next){
					temp = p;
					p = p->prev;
					p->next = NULL;
				}

				else{
					temp = p->prev;
					temp->next = p->next;
					temp = p->next;
					temp->prev = p->prev;
					temp = p;
				}
			}
		}
		p = p->next;
		delete(temp);
	}
}
void DoublyLinkedList::unique(){
	DoublyLinkedList::sort();
	ListNode *beg = head, *p = head->next;
	while (beg->next){
		if (beg->data == p->data){
			if (beg == head){
				head = p->next;
				head->prev = NULL;
				beg = head;
				p = head->next;
			}
			else{
				if (!p->next){
					beg = beg->prev;
					beg->next = NULL;
				}
				else{
					if (p->next->data == p->data){
						if (p->next->next != NULL)
							p = p->next->next;
						else {
							beg = beg->prev;
							beg->next = NULL;
							return;
						}
					}
					else p = p->next;
					
					beg = beg->prev;
					beg->next = p;
					p->prev = beg;
					beg = beg->next;
					p = p->next;
				}
			}
		}
		else{
			beg = beg->next;
			p = p->next;
		}
	}
}
void DoublyLinkedList::assign(DoublyLinkedList &dl, int first, int last){
	ListNode *p = dl.head, *temp,*beg,*end;
	int i = 0;
	if ((last > DoublyLinkedList::size()) || (dl.empty() == true)){
		printf("ERROR");
		return;
	}
	if (first == 0){
		beg = dl.head;
		while (i<last){
			beg = beg->next;
			i++;
		}
		beg = beg->next;
		dl.head = beg;
	}
	else{
		for (i; i < first - 1; i++)
			p = p->next;
		beg = p;
		p = p->next;
	}
	for (i = first - 1; i < last; i++){
		DoublyLinkedList::push_back(*p);
		p = p->next;
	}
	if (first != 0){
		end = p;
		beg->next = end;
		end->prev = beg;
	}
	else dl.head->prev = NULL;
}
void DoublyLinkedList::splice(int where, const DoublyLinkedList &dl){
	ListNode *p = head, *ptr = dl.head;
	for (int i = 0; i < where; i++){
		p = p->next;
	}
	while (ptr->next){
		ptr = ptr->next;
	}
	while (ptr){
		DoublyLinkedList::insert_after(p->data, *ptr);
		//p = p->next;
		ptr = ptr->prev;
	}
}
void DoublyLinkedList::splice(int where, const DoublyLinkedList &dl, int first, int last){
	ListNode *p = head, *ptr = dl.head;
	int i = 0;
	for (int i = 0; i < where; i++){
		p = p->next;
	}
	while (i < first){
		ptr = ptr->next;
		i++;
	}
	while (i<=last){
	    DoublyLinkedList::insert_after(p->data, *ptr);
		ptr = ptr->next;
		i++;
	}
}
void DoublyLinkedList::print(){
	if (DoublyLinkedList::empty()){
		printf("Empty!!");
		return;
	}
	ListNode *temp = head;
	while (temp){
		printf("%s ",temp->data);
		temp = temp->next;
	}
}
void DoublyLinkedList::print_bkw(){
	if (DoublyLinkedList::empty()){
		printf("Empty!!");
		return;
	}
	ListNode *p = head;
	while (p->next){
		p = p->next;
	}
	while (p){
		printf("%s ", p->data);
		p = p->prev;
	}
}
DoublyLinkedList::~DoublyLinkedList(void){}