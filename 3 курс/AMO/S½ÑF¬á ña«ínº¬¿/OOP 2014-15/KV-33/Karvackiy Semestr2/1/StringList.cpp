/*
 * StringList.cpp
 *
 *  Created on: 11 февр. 2015 г.
 *      Author: supremist
 */

#include "StringList.h"


StringList::StringList(){
	head=nullptr;
	tail=nullptr;
	iterator=nullptr;
}

StringList::~StringList(){
	RemoveAll();
}

POSITION StringList::GetHead()const {
	return head;
}

POSITION StringList::GetTail()const {
	return tail;
}

void StringList::AddHead(const char *nstr){
	/*ListNode *p = new ListNode(nstr);
	p->next=head;
	if (head)
		head->prev=p;
	head=p;*/
	InsertBefore (head, nstr);
}

void StringList::AddHead(const StringList *nlst){
	const ListNode *p = nlst->GetTail();
	while (p){
		AddHead(p->str);
		p=p->prev;
	}
}

void StringList::AddTail(const char *nstr){
	/*ListNode *p = new ListNode(nstr);
	p->prev=tail;
	if (tail)
		tail->next=p;
	tail=p;*/
	InsertAfter(tail, nstr);
}
void StringList::AddTail(const StringList *nlst){
	const ListNode *p = nlst->GetHead();
	while (p){
		AddTail(p->str);
		p=p->next;
	}
}

void StringList::RemoveAll(){
	iterator=head;
	ListNode *tmp;
	while  (iterator){
		tmp=iterator;
		iterator=iterator->next;
		delete tmp;
	}
}

void StringList::RemoveHead(){
	Remove (head);
}

void StringList::RemoveTail(){
	/*if (!tail) return;
	ListNode *tmp = tail;
	tail=tail->prev;
	tail->next=nullptr;
	delete tmp;*/
	Remove (tail);
}

POSITION StringList::Find(const char *text)const{
	POSITION iter=head;
	while (iter && strcmp(iter->str, text) ) iter=iter->next;
	return iter;
}

void StringList::AppendExclusively(const StringList *sl){
	const ListNode *p = sl->GetHead();
	while (p){
		if (!Find(p->str))
			AddTail(p->str);
		p=p->next;
	}
}

void StringList::Splice(POSITION where, StringList *sl, POSITION first, POSITION last){
	if (!first || !last || !where) return;
	/*
	sl->AddHead("");
	sl->AddTail("");
	first->prev->next=last->next;
	last->next->prev=first->prev;
	sl->RemoveHead();
	sl->RemoveTail();
	ListNode *tmp = where->next;
	where->next = first;
	first->prev = where;
	if (tmp){
		tmp->prev=last;
		last->next=tmp;
	}
	else {
		last->next=nullptr;
		tail=last;
	}*/
	ListNode * iter=first, *wh = where, *tmp;
	while (iter!=last->next && iter){
		InsertAfter(wh, iter->str);
		tmp=iter;
		iter=iter->next;
		wh=wh->next;
		sl->Remove(tmp);
	}
}


void StringList::InsertAfter (POSITION where,const char * elem){
	if (!where){
		if (!head)
			tail=head=new ListNode(elem);
		return;
	}
	ListNode *tmp  = where->next;
	ListNode *item = new ListNode(elem);
	if (!tmp)
		tail = item;
	else
		tmp->prev   = item;
	where->next = item;
	item->next  = tmp;
	item->prev  = where;
}

void StringList::InsertBefore(POSITION where,const char * elem){
	if (!where){
		if (!head)
			tail=head=new ListNode(elem);
		return;
	}
	ListNode *tmp  = where->prev;
	ListNode *item = new ListNode(elem);
	if (!tmp)
		head = item;
	else
		tmp ->next  = item;
	where->prev = item;
	item->prev  = tmp;
	item->next  = where;
}

void StringList::Remove (POSITION where){
	if (!where) return;
	if (where->next)
		where->next->prev = where->prev;
	else tail=where->prev;
	if (where->prev)
		where->prev->next = where->next;
	else head=where->next;
	delete where;
}


void StringList::Unique(){
	ListNode * iter = tail, *tmp;
	while (iter){
		if (Find(iter->str)!=iter){
			tmp=iter;
			iter=iter->prev;
			Remove(tmp);
		}
		else iter=iter->prev;
	}
}

POSITION StringList::GetNext(){
	return iterator=iterator->next;
}

POSITION StringList::GetPrev(){
	return iterator=iterator->prev;
}

POSITION StringList::GetHeadPosition(){
	return iterator=head;
}

POSITION StringList::Find(int indx)const {
	POSITION iter=head;
	for (int i=0; iter && i<indx; i++ ) iter=iter->next;
	return iter;
}

const char* StringList::GetAt(int indx)const{
	return Find (indx)->str;
}

void StringList::RemoveAt(int indx){
	Remove(Find(indx));
}

void StringList::SetAt(char *text , int indx){
	if (indx==0) AddHead(text);
	else InsertAfter (Find(indx-1), text);
}

void StringList::InsertAfter(char *text, int indx){
	InsertAfter (Find(indx), text);
}

void StringList::InsertBefore(char *text, int indx){
	InsertBefore (Find(indx), text);
}

int StringList::FindIndex(char *text)const{
	POSITION iter=head;
	int indx=0;
	for (; iter && strcmp(iter->str, text); indx++ ) iter=iter->next;
	if (iter) return indx;
	else return -1;
}

int StringList::Getsize()const{
	POSITION iter=head;
	int indx=0;
	for (; iter; indx++ ) iter=iter->next;
	return indx;
}

bool StringList::IsEmpty()const{
	return head;
}

void StringList::Printnode( POSITION p){
	std::cout<<p->str<<' ';
}
