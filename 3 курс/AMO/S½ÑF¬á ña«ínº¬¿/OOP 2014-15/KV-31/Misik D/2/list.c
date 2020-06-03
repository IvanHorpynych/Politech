/******************************************************************************************
	name:					list.c
	description:			file contains functions "list_last" that returns pointer to
							last node in list or NULL if list is empty.
							function "append2list" add node to list. Node - copy of
							parameter pn. Also, you can add new list in the end of 
							the list. In copy changes only pointer to previous node.
							parameter head - head of list. is list is empty - function
							create list, changes parameter head. else - add node in list
							which starts from head
							function "create_list" returns pointer to head to list
							this function creates list uses function "append2list"
							function "isempty" return 0, if list has nodes and returns
							1 if list empty (list empty if head == NULL)
							function "clear" clear list, which starts from head
							function "get_node" returns pointer to first node, that 
							id is id or NULL if list doesn't contain node with id id
							function "del_node" deletes all nodes that id is id and change
							list and head if it is necessary
							function "ins_node" insert copy of pn before ellement with id 
							id into list which starts from head. Function change in copy
							pointer to previous and next nodes
							function "reverse" reverse list and change head necessarily
							function "print_list" displays list, if it isn't empty or 
							print 'Empty list' if list is empty
							functon "isunique" check, if nodes p1 id unique in list,
							which starts from p2 (node id unique - id doesn't meet in
							list p2 or meets once and pointer p1 point the same adress
							that pointer in list p2). if id of p1 is unique - function
							returns 1, else - 0.
							function "merge_unique" creates list of unique ides, that 
							meets in list with start in head1 and list that start in
							head2. if it is possible - returns pointer to head of unique
							list, else returns NULL.
							function "unique" deletes in list which
							starts in head all neighbours nodes  that id is the same.
	author:					Dima
	date of creation:		07.09.2014
	written:				07.09.2014
	date of last modified:	10.09.2014
******************************************************************************************/

#include "list.h"
#include <stdlib.h>
#include <stdio.h>

int isempty(const CNode *head) {
	if (head == NULL) return 1;
	else return 0;
}

CNode *get_node(const CNode *head, int id) {
	if (!isempty(head)) {
		CNode *p = head;
		while (p->id != id && p->next != head) p = p->next;
		if (p->id == id) return p;
	} 
	return NULL;
}

void append2list(CNode **head, const CNode *pn) {
	CNode *p = (CNode *)malloc(sizeof(CNode));
	p->id = pn->id;
	if (*head == NULL) {
		*head = p;
	}
	else {
		(*head)->prev->next = p;
		p->prev = (*head)->prev;
	}	
	p->next = *head;
	(*head)->prev = p;
}

void del_node(CNode **head, int id) {
	CNode *p = *head;
	if (!isempty(*head)) do {
		if (p->id == id) {
			CNode *temp = p;
			p->prev->next = p->next;
			p->next->prev = p->prev;
			if (p == *head) *head = p->prev->next;
			p = p->next;
			if (temp == p) {
				p = NULL;
				*head = NULL;
			}
			free(temp);
		}
		else if (p->next != *head) p = p->next;
		else p = NULL;
	} while (p != NULL);
}

void clear(CNode **head) {
	if (!isempty(*head)) {
		CNode *p = *head;
		p->prev->next = NULL;
		while (p != NULL) {
			*head = (*head)->next;
			free(p);
			p = *head;
		}
	}
}

void ins_node(CNode **head, const CNode *pn, int id) {
	CNode *p_id = get_node(*head, id);
	if (p_id != NULL) {
		CNode *p = (CNode *)malloc(sizeof(CNode));
		p->id = pn->id;
		p->prev = p_id->prev;
		p->next = p_id;
		p_id->prev->next = p;
		p_id->prev = p;
		if (p_id == *head) *head = p_id->prev;
	}
}

void reverse(CNode *head) {
	CNode *p = head;
	if (!isempty(head)) do {
		CNode *temp = p->next;
		p->next = p->prev;
		p->prev = temp;
		p = p->prev;
	} while (p != head);
}

void print_list(const CNode *head) {
	CNode *p = head;
	if (!isempty(head)) {
		if (p->next != head) do {
			printf("%d, ", p->id);
			p = p->next;
		} while (p->next != head);
		printf("%d\n", p->id);
	} else printf("Empty\n");
}

CNode *merge_unique(const CNode *head1, const CNode *head2) {
	CNode *head = NULL;
	CNode *p = head1;
	if (!isempty(head1)) do {
		CNode *p1 = head2;
		while (head2 != NULL && p->id != p1->id && p1->next != head2) p1 = p1->next;
		if (head2 == NULL || p->id != p1->id) append2list(&head, p);
		p = p->next;
	} while (p != head1);

	p = head2;
	if (!isempty(head2)) do {
		CNode *p1 = head1;
		while (head1 != NULL && p->id != p1->id && p1->next != head1) p1 = p1->next;
		if (head1 == NULL || p->id != p1->id) append2list(&head, p);
		p = p->next;
	} while (p != head2);

	return head;
}

void unique(CNode **head) {
	if (!isempty(*head)) {
		CNode *p = *head;
		do {
			if (p->id == p->next->id && p->next != p) {
				CNode *temp = p;
				if (p == *head) *head = (*head)->next->next;
				else if (p->next == *head) *head = (*head)->next;
				p->prev->next = p->next->next;
				p->next->next->prev = p->prev;
				p = p->next->next;
				if (temp == p) {
					p = NULL;
					*head = NULL;
				}
				free(temp->next);
				free(temp);
			}
			else if (p->next != *head) p = p->next;
			else p = NULL;
		} while (p != NULL);
	}
}