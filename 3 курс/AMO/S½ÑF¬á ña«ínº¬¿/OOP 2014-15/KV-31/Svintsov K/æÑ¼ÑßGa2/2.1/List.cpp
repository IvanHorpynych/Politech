#include "List.h"
#include <string.h>

StringList::StringList()
{
    startPTR=NULL;
}

void StringList::AddHead(const char *name){
    ListNode* newNode = new ListNode();
    newNode->next=NULL;
    newNode->prev=NULL;
	newNode->str = new char[strlen(name)];
    strcpy(newNode->str,name);

    if(startPTR==NULL){
        startPTR=newNode;
		endPTR = newNode;
    }
    else{
        newNode->next=startPTR;
        startPTR->prev=newNode;
        startPTR=newNode;
    }
}

void StringList::AddHead(const StringList *addList){
	ListNode* currentPTR = addList->endPTR;
	if (addList->startPTR != NULL){
		do{
			AddHead(currentPTR->str);
			currentPTR = currentPTR->prev;
		} while (currentPTR != NULL);
	}
}

void StringList::AddTail(const char *name){
	ListNode* newNode = new ListNode();
	newNode->next = NULL;
	newNode->prev = NULL;
	newNode->str = new char[strlen(name)];
	strcpy(newNode->str, name);

	if (startPTR == NULL){
		startPTR = newNode;
		endPTR = newNode;
	}
	else{
		endPTR->next = newNode;
		newNode->prev = endPTR;
		endPTR = newNode;
	}
}

void StringList::AddTail(const StringList *addList){
	ListNode* currentPTR = addList->startPTR;
	if (addList->startPTR != NULL){
		do{
			AddTail(currentPTR->str);
			currentPTR = currentPTR->next;
		} while (currentPTR != NULL);
	}
}

void StringList::RemoveAll(){
	ListNode *currentPTR = startPTR;
	ListNode *helpPTR;
	do{
		helpPTR = currentPTR->next;
		delete currentPTR;
		currentPTR = helpPTR;
	} while (currentPTR != NULL);
	startPTR = NULL;
	endPTR = NULL;

}

void StringList::RemoveHead(){
	if (startPTR != NULL){
		ListNode *delPTR = startPTR;
		startPTR = startPTR->next;
		startPTR->prev = NULL;
		delete delPTR;
	}
}

void StringList::RemoveTail(){
	if (startPTR != NULL){
		ListNode *delPTR = endPTR;
		endPTR = endPTR->prev;
		endPTR->next = NULL;
		delete delPTR;
	}
}

void StringList::AppendExclusively(const StringList *addList){
	ListNode *runPTR;
	ListNode *currentPTR = addList->startPTR;
	bool flag;

	if ((addList->startPTR != NULL) && (startPTR != NULL)){
		do{
			runPTR = startPTR;
			flag = false;
			while (runPTR!=NULL)
			{
				if (strcmp(currentPTR->str, runPTR->str)==NULL){
					flag = true;
					break;
				}
				runPTR = runPTR->next;
			}
			if (flag == false)
				AddTail(currentPTR->str);
			currentPTR = currentPTR->next;
		} while (currentPTR != NULL);
	}
}

void StringList::Unique(){
	ListNode *currentPTR = startPTR;
	ListNode *runPTR;
	StringList *addList = new StringList;
	bool flag;
	//searches for an unique elements 
	while (currentPTR != NULL){
		runPTR = startPTR;
		flag = false;
		while (runPTR != NULL){
			if ((strcmp(currentPTR->str, runPTR->str) == NULL)&&(runPTR!=currentPTR)){
				flag = true;
				break;
			}
			runPTR = runPTR->next;
		}
		//adds them to additional list
		if (flag == false)
			addList->AddTail(currentPTR->str);
		currentPTR = currentPTR->next;
	}
	//deletes an old list with dublicate elements and adds elem from new
	//then deletes an additional list
	if (addList->startPTR != NULL){
		RemoveAll();
		AddTail(addList);
		addList->RemoveAll();
	}
}

void StringList::InsertAfter(char *name, int num){
	ListNode *currentPTR = startPTR;
	int count = 0;
	ListNode *newNode = new ListNode();
	newNode->str = new char[strlen(name)];
	strcpy(newNode->str, name);
	//searches for position to insert
	while (currentPTR != endPTR){
		if (count == num){
			newNode->next = currentPTR->next;
			currentPTR->next->prev = newNode;
			newNode->prev = currentPTR;
			currentPTR->next = newNode;
			return;
		}
		count++;
		currentPTR = currentPTR->next;
	}
	//if reaches the end of list then inserts to the tail
	AddTail(name);
}

void StringList::InsertBefore(char *name, int num){
	ListNode *currentPTR = startPTR;
	int count = 0;
	ListNode *newNode = new ListNode();
	newNode->str = new char[strlen(name)];
	strcpy(newNode->str, name);
	//searches for position to insert
	while (currentPTR != NULL){
		if (count == num){
			newNode->next = currentPTR;
			currentPTR->prev->next = newNode;
			newNode->prev = currentPTR->prev;
			currentPTR->prev = newNode;
			return;
		}
		count++;
		currentPTR = currentPTR->next;
	}
	//if list consist only 1 elem or empty then inserts to the head
	AddHead(name);
}

const char* StringList::GetAt(int indx)const{
	ListNode *currentPTR = startPTR;
	int count = 0;
	while (currentPTR != NULL){
		if (count == indx)
			return currentPTR->str;
		count++;
		currentPTR = currentPTR->next;
	}
	return NULL;
}

void StringList::RemoveAt(int indx){
	ListNode* currentPTR = startPTR;
	int count = 0;
	if (indx == 0){
		RemoveHead();
		return;
	}
	while (currentPTR != endPTR){
		if (count == indx){
			currentPTR->next->prev = currentPTR->prev;
			currentPTR->prev->next = currentPTR->next;
			delete currentPTR;
			return;
		}
		count++;
		currentPTR = currentPTR->next;		
	}
	if (currentPTR==endPTR)
		RemoveTail();
	else
		printf("\nIndex is out of range\n");
}

void StringList::SetAt(char *name, int indx){
	ListNode* currentPTR = startPTR;
	int count = 0;
	while (currentPTR != NULL){
		if (count == indx){
			currentPTR->str = new char[strlen(name)];
			strcpy(currentPTR->str, name);
			return;
		}
		count++;
		currentPTR = currentPTR->next;
	}
	printf("\nIndex is out of range\n");

}POSITION StringList::Find(char* name){
	POSITION currentPTR = startPTR;
	while (currentPTR != NULL){
		if (strcmp(currentPTR->str, name) == NULL){
			return currentPTR;
		}
		currentPTR = currentPTR->next;
	}
	printf("\nPosition is not found\n");
	return NULL;
}

int StringList::FindIndex(char* name)const{
	POSITION currentPTR = startPTR;
	int count = 0;
	while (currentPTR != NULL){
		if (strcmp(currentPTR->str, name) == NULL){
			return count;
		}
		currentPTR = currentPTR->next;
		count++;
	}
	printf("\nIndex is not found\n");
	return NULL;
}

void StringList::Splice(POSITION where, StringList *sl, POSITION first, POSITION last){
	POSITION delPTR = first;
	POSITION addPTR;
	POSITION currentPTR = startPTR;
	bool flag = 0;
	while ((delPTR != NULL)&&(!flag)){
		if (delPTR == last)
			flag = 1;
		InsertAfter(delPTR->str, FindIndex(where->str));
		addPTR = delPTR;
		delPTR = delPTR->next;
		sl->RemoveAt(sl->FindIndex(addPTR->str));
		currentPTR = currentPTR->next;
	}
}

POSITION StringList::GetHeadPosition(){
	itrPTR = startPTR;
	return itrPTR;
}

POSITION StringList::GetNext(){
	return itrPTR = itrPTR->next;
}

POSITION StringList::GetPrev(){
	return itrPTR = itrPTR->prev;
}

void StringList::PrintNode(POSITION p){
	printf(" %s ", p->str);
}

int StringList::GetSize()const{
	POSITION currentPTR = startPTR;
	int size = 0;
	while (currentPTR != NULL){
		size++;
		currentPTR = currentPTR->next;
	}
	return size;
}

bool StringList::IsEmpty()const{
	return (startPTR) ? false : true;
}

POSITION StringList::GetTail(){
	return endPTR;
}

void StringList::PrintList(){
	printf("\n");
	for (POSITION pos =GetHeadPosition(); pos != NULL; pos =GetNext())
		PrintNode(pos);
}
