/***********************************************************************
*file: list.c
*synopsis: List`s functions create list,insert new nodes,delete nodes,
print it and make it reversed. Also include a special function for making
new list from 2 lists.
* These functions are declared in the include file "list.h".
*related files: none
*author: Kyrylo Svintsov KV-31
*written: 4/11/2014
*last modified: 5/11/2014
************************************************************************/

#include "List.h"


/*-----------------------------------------------------------*
Name insert
Usage insert(&list,id);
Prototype in "list.h"
Synopsis Add new node to the end of list.If list is empty then
function creates it.
Return  void function
*--------------------------------------------------------- */
void insert(CNodePTR *cPtr, int value){
	CNodePTR newPTR, previousPTR, currentPTR;
	//allocates new node for list
	newPTR = malloc(sizeof(CNode));
	if (newPTR){
		newPTR->id = value;
		newPTR->next = NULL;
		newPTR->prev = previousPTR = NULL;
		currentPTR = *cPtr;

		//if list is empty=> creates it
		if (isEmpty(*cPtr)==1){
			*cPtr = newPTR;
			newPTR->next = newPTR->prev = *cPtr; // tail-end
		}
		//if list isn`t empty=> add node to the end
		else{
			do{
				previousPTR = currentPTR;
				currentPTR = currentPTR->next;
			} while (currentPTR != *cPtr);

			previousPTR->next = newPTR;
			newPTR->next = *cPtr; // tail-end
			newPTR->prev = previousPTR;
			(*cPtr)->prev = newPTR;
		}
	}
	else
		printf("%d not inserted. No memory available.\n", value);
}

/*-----------------------------------------------------------*
Name isEmpty
Usage int flag =isEmpty(list);
Prototype in "list.h"
Synopsis Check the list for emptiness
Return  if list is empty then return 1. if not => 0
*--------------------------------------------------------- */
int isEmpty(CNodePTR cPtr){
	return cPtr == NULL;
}

/*-----------------------------------------------------------*
Name print_list
Usage print_list(list);
Prototype in "list.h"
Synopsis Prints the list
Return  void function
*--------------------------------------------------------- */
void print_list(const CNodePTR cPtr){
	CNodePTR currentPTR;
	currentPTR = cPtr;
	//if list isn`t empty moves by nodes and prints.
	if (isEmpty(cPtr) == 0){
		printf("\n");
		do{
			printf("%d ", currentPTR->id);
			currentPTR = currentPTR->next;
		} while (currentPTR != cPtr);
	}
	else{
		printf("\nEmpty list");
	}
}

/*-----------------------------------------------------------*
Name get_node
Usage CNodePTR searchedPTR=get_node(list,id);
Prototype in "list.h"
Synopsis Search the node in list by id
Return  Pointer to the node which have been found or NULL if not
*--------------------------------------------------------- */
CNodePTR get_node(const CNodePTR head, int id){
	CNodePTR start = head;
	//if list isn`t empty then it moves by list and check the nodes
	//until node will be found
	if (isEmpty(head) == 0){
		while (start->id != id){
			if (start->next!= head)
				start = start->next;
			else
				return NULL;
		}
		return start;
	}
	//if not found returns NULL
	else
		return NULL;
}

/*-----------------------------------------------------------*
Name append2list
Usage append2list(&list,searchedPTR);
Prototype in "list.h"
Synopsis Makes the copy of node and add it to the end of list
Similar to function insert. Different is formal parametr. Here it is pointer
Return  void function
*--------------------------------------------------------- */
void append2list(CNodePTR *head, const CNodePTR pn){
	CNodePTR newPTR, currentPTR, previousPTR;
	newPTR = malloc(sizeof(CNode));
	if (newPTR){
		//Copies information from node and write to the new
		newPTR ->id= pn->id;
		currentPTR = *head;
		//moves to the end of list
		do{
			previousPTR = currentPTR;
			currentPTR = currentPTR->next;
		} while (currentPTR != *head);

		//inputs a new node
		previousPTR->next = newPTR;
		newPTR->next = *head; // tail-end
		newPTR->prev = previousPTR;
		(*head)->prev = newPTR;
	}
	else
		printf("\nCan`t append new element");
}

/*-----------------------------------------------------------*
Name del_node
Usage del_node(&list,id)
Prototype in "list.h"
Synopsis Deletes all nodes from list with that id 
Return  void function
*--------------------------------------------------------- */
void del_node(CNodePTR *head, int id){
	CNodePTR currentPTR, previousPTR;
	currentPTR = *head;
	previousPTR = NULL;
	//deletes required nodes 
	do{
		if (currentPTR->id == id){
			previousPTR->next = currentPTR->next;
			currentPTR->next->prev = previousPTR;
			free(currentPTR);
			currentPTR = previousPTR->next;

		}
		//moves by list and searches for nodes with required id
		else{
			previousPTR = currentPTR;
			currentPTR = currentPTR->next;
		}
	} while (currentPTR != *head);
}

/*-----------------------------------------------------------*
Name clear
Usage clear(&list);
Prototype in "list.h"
Synopsis Deletes all nodes from list
Return  void function
*--------------------------------------------------------- */
void clear(CNodePTR *head){
	CNodePTR nextPTR,currentPTR = *head;
	//deletes nodes
	do{
		nextPTR = currentPTR->next;
		free(currentPTR);
		currentPTR = nextPTR;
	} while (currentPTR != *head);
	*head = NULL;
}

/*-----------------------------------------------------------*
Name ins_node
Usage ins_node(&list,searchedPTR,id);
Prototype in "list.h"
Synopsis Makes the copy of node and inputs it before node with required id
Return  void function
*--------------------------------------------------------- */
void ins_node(CNodePTR *head, const CNodePTR pn , int id){
	CNodePTR newPTR,previousPTR, currentPTR = *head;
	previousPTR = NULL;
	char count, flag = 0;
	count = 0;
	//allocates new node
	newPTR = malloc(sizeof(CNode));
	if (newPTR){
		newPTR->id = pn->id;
		do{
			//inputs to the begin of list
			if ((currentPTR->id == id) && (flag == 0)){
				if (count == 0){
					newPTR->next = currentPTR;
					currentPTR->prev->next = newPTR;
					newPTR->prev = currentPTR->prev;
					currentPTR->prev = newPTR;
					*head = newPTR;

				}
				//inputs to another place in list
				else{
					previousPTR->next = newPTR;
					currentPTR->prev = newPTR;
					newPTR->next = currentPTR;
					newPTR->prev = previousPTR;
					
				}

				flag++;
				break;
				
			}
			//moves by list and searchs for node with required id
			else{
				previousPTR = currentPTR;
				currentPTR = currentPTR->next;
				count++;
			}
		} while (currentPTR != *head);
		if (flag == 0)
			printf("\nCan`t insert node before id:%d", id);
	}
	else
		printf("\nCan`t insert node before id:%d", id);
}

/*-----------------------------------------------------------*
Name reverse
Usage reverse(&list)
Prototype in "list.h"
Synopsis Changes the direction of list 
Return  void function
*--------------------------------------------------------- */
void reverse(CNodePTR *head){
	CNodePTR addPTR, currentPTR = *head;
	currentPTR = currentPTR->prev;
	*head = currentPTR;
	// moves by list and changes node`s links
	do{
		addPTR = currentPTR->next;
		currentPTR->next = currentPTR->prev;
		currentPTR->prev = addPTR;
		currentPTR = currentPTR->prev;
	} while (currentPTR != *head);
}

/*-----------------------------------------------------------*
Name head_merge_unique
Usage CNodePTR new_list=head_merge_unique(list1,list2);
Prototype in "list.h"
Synopsis Fuction creates new list from another 2 and inserts only unique id
from nodes.
Return  Pointer to the list or NULL.
*--------------------------------------------------------- */
CNodePTR head_merge_unique(const CNodePTR head1, const CNodePTR head2){
	CNodePTR newPTR, currentPTR, currentMPTR,previousPTR, startPTR = NULL;
	char found = 0;
	//checks fro emptiness 2 lists
	if ((isEmpty(head1) == 0) && (isEmpty(head2) == 0)){
		currentPTR = head1;
		previousPTR = NULL;
		//moves by first list and takes nodes from it
		do{
			//allocates new node
			newPTR = malloc(sizeof(CNode));
			if (newPTR){
				//insert node to the beginning of list
				if (isEmpty(startPTR) == 1){
					newPTR->id = currentPTR->id;
					newPTR->next = newPTR->prev = newPTR;
					startPTR = newPTR;
					previousPTR = startPTR;
				}
				//to the end of list
				else
				{
					previousPTR->next = newPTR;
					newPTR->id = currentPTR->id;
					newPTR->next = startPTR;
					newPTR->prev = previousPTR;
					startPTR->prev = newPTR;
					previousPTR = newPTR;
				}
				currentPTR = currentPTR->next;
			}
			else
				return NULL;
		} while (currentPTR != head1);
		currentPTR = head2;
		//moves by second list and takes the nodes, also check in a new list 
		//if such id unique
		do{
			//allocates new node
			newPTR = malloc(sizeof(CNode));
			if (newPTR){
				currentMPTR = startPTR;
				found = 0;
				//search for required id in a new list
				do{
					if (currentPTR->id == currentMPTR->id){
						found = 1;
						break;
					}
					else
						currentMPTR = currentMPTR->next;
				} while (currentMPTR != startPTR);
				//if required id is unique then isert to the end of list
				if (!found){

					previousPTR->next = newPTR;
					newPTR->id = currentPTR->id;
					newPTR->next = startPTR;
					newPTR->prev = previousPTR;
					startPTR->prev = newPTR;
					previousPTR = newPTR;
				}
				currentPTR = currentPTR->next;
			}
			else
				return NULL;
		} while (currentPTR != head2);
	}
	else
		return NULL;	
	return startPTR;
}

/*-----------------------------------------------------------*
Name unique
Usage unique(&list);
Prototype in "list.h"
Synopsis Deletes all dublicates of nodes in list
Return  void function
*--------------------------------------------------------- */
void unique(CNodePTR *head){
	CNodePTR currentPTR, previousPTR;
	char count = 0;
	currentPTR = *head;
	previousPTR = currentPTR->prev;
	//moves by list
	do{
		//searches for dublicates and deletes them
		if ((currentPTR->id == previousPTR->id)&&(count!=0)){
			currentPTR->next->prev = previousPTR;
			previousPTR->next = currentPTR->next;
			free(currentPTR);
			currentPTR = previousPTR->next;
		}
		else{
			previousPTR = currentPTR;
			currentPTR = currentPTR->next;
		}
		count++;
	} while (currentPTR != *head);
}