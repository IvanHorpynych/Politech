#include <iostream>
#include "List.h"

using namespace std;

int main()
{
    StringList *strList = new StringList();
    strList->AddHead("N2");
    strList->AddHead("N1");
	strList->AddTail("N3");

	strList->PrintList();
	printf("\nThe size of list1 is:%d\n", strList->GetSize());
	printf("Prints 1 if empty and 0 if not:%d\n", strList->IsEmpty());


	StringList *strList2 = new StringList();
	strList2->AddTail("N5");
	strList2->AddTail("N6");
	strList2->AddTail("N7");

	strList2->PrintList();
	printf("\nThe size of list2 is:%d\n", strList2->GetSize());


	StringList *strList3 = new StringList();
	strList3->AddTail("N8");
	strList3->AddTail("N9");

	strList3->PrintList();
	printf("\nThe size of list3 is:%d\n\n", strList3->GetSize());
	

	strList->AddHead(strList2);
	strList->PrintList();

	strList->AddTail(strList3);
	strList->PrintList();

	strList->RemoveTail();
	strList->RemoveHead();
	strList->PrintList();

	strList->AppendExclusively(strList2);
	strList->PrintList();

	strList->InsertAfter("N3", 2);
	strList->PrintList();
	strList->Unique();
	strList->PrintList();

	strList->InsertBefore("N99", 1);
	strList->PrintList();

	printf("\nChosen string is:%s", strList->GetAt(1));

	strList->RemoveAt(1);
	strList->PrintList();

	strList->SetAt("N99", 1);
	strList->PrintList();
	

	POSITION where = strList->Find("N1");
	POSITION first = strList2->Find("N6");
	POSITION last = strList2->Find("N7");
	if ((where) && (first) && (last))
		strList->Splice(where, strList2, first,last);
	strList->PrintList();

	strList->RemoveAll();
	strList2->RemoveAll();
	strList3->RemoveAll();
	printf("\nPrints 1 if empty and 0 if not:%d\n", strList->IsEmpty());
	

	getchar();

	return 0;
}
