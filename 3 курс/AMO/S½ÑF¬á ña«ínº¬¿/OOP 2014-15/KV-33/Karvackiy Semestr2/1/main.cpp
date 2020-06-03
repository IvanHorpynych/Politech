/*
 * main.cpp
 *
 *  Created on: 11 февр. 2015 г.
 *      Author: supremist
 */

#include "StringList.h"

using namespace std;

int main (){
	StringList * list = new StringList();
	list->AddHead("Hello");
	list->AddTail("world!");
	list->AddTail("1234");
	StringList * list2 = new StringList();
	list2->AddTail("1234");
	list2->AddHead("4");
	list2->AddHead("2");
	list2->AddHead("3");
	list2->AddHead("2");
	list2->AddHead("1");
	list2->Unique();
	list->Splice(list->Find(1), list2, list2->Find("1"), list2->Find(2));
	list->AppendExclusively(list2);
	list2->Printnode(list2->GetHead());
	list2->Printnode(list2->GetTail());
	cout<<"\n";
	for (POSITION iterator=list->GetHeadPosition(); iterator; iterator=list->GetNext()){
		list->Printnode(iterator);
	}
	delete list;
	delete list2;
	return 0;
}
