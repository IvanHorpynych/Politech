/***********************************************************************
*file: Gspisok.c
*author: Maxim K. E.
*group: KV-31, FPM
*written: 10/11/2014
*last modified: 30/11/2014
************************************************************************/

#include "Hspisok.h"

#include <stdlib.h>
#include <stdio.h>

int isempty(const CNode *head){

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

void append2list(CNode **head, const CNode *pn){
	CNode *p = (CNode *)malloc(sizeof(CNode));
	p->id = pn->id;
	if (*head == NULL) { *head = p; }
	else
	{
		(*head)->prev->next = p;
		p->prev = (*head)->prev;
	}
	(*head)->prev = p;
	p->next = (*head);

}

void print_list(const CNode *head)

{
	CNode *p = head;
	if (isempty(head) == 0) {
		while (p->next != head){
			printf("%d ", p->id);
			p = p->next;
		}
		printf("%d\n", p->id);
		printf("\n");

	}
	else printf("Current list is empty \n");
}

void del_node(CNode **head, int id)
{
	int i = 0;
	CNode *km = *head;
	if (isempty(*head) == 1) {
		printf("Current list is empty \n");
	}
	else {
		if (km != 0) do
		{
			km = km->next; i++;
		} while (km != *head);

		while (km != NULL){

			CNode *dptr = km;
			if (km->id == id){
				km->prev->next = km->next;
				km->next->prev = km->prev;
				if (km == *head) *head = km->next;
				km = km->next;

				if (i == 1) { km = NULL; *head = NULL; }
				free(dptr);
			}
			else  if (km->next != *head) km = km->next;
			else km = NULL;
		}

	}
}
/*void clear(CNode **head)
{
CNode *km=NULL;
while((*head)!=NULL){
km=*head;
km->prev->next=NULL;
*head =(*head)->next;

free(km);
}
free(*head);
} */

void ins_node(CNode **head, const CNode *pn, int id)
{
	CNode*p = (CNode*)malloc(sizeof(CNode));

	CNode*pos = *head;
	if (pn == NULL) { printf("\n net vstavki\n"); return; }
	pos = get_node(*head, id);
	if (pos == NULL){ printf("net elementa dla vstavki\n"); return; }
	if (pos != NULL)
	{
		p->id = pn->id;
		p->prev = pos->prev;
		p->next = pos;
		pos->prev->next = p;
		pos->prev = p;
		if (pos == (*head)) {
			(*head) = (*head)->prev;
		}
	}
}




void reverse(CNode *head) {
	CNode *p = head;
	int flag = 0;
	if (isempty(head) == 0) do {

		CNode *temp = p->next;

		p->next = p->prev;
		p->prev = temp;

		p = p->next;

	} while (p != head);

}


CNode * merge_unique(const CNode *head1, const CNode *head2)
{
	CNode*head = NULL;
	CNode *p1 = head1;
	if (isempty(head1) == 0 && isempty(head2) == 0) do {
		CNode *p2 = head2;
		while (head2 != NULL && p1->id != p2->id && p2->next != head2) p2 = p2->next;
		if (head2 == NULL || p1->id != p2->id) append2list(&head, p1);
		p1 = p1->next;
	} while (p1 != head1); p1 = head2;
	if (isempty(head1) == 0 && isempty(head2) == 0) do {
		CNode *p2 = head1;
		while (head1 != NULL && p1->id != p2->id && p2->next != head1) p2 = p2->next;
		if (head1 == NULL || p1->id != p2->id) append2list(&head, p1);
		p1 = p1->next;
	} while (p1 != head2);
	return head;
}
void unique(CNode **head)
{
	CNode *p = *head;
	if (isempty(*head) == 0) do{
		if (p->id == p->next->id)
		{
			CNode *dm = p;
			if (p == *head) *head = p->next->next;
			else if (p->next == *head) *head = (*head)->next;
			p->prev->next = p->next->next;
			p->next->next->prev = p->prev;
			p = p->next->next;
			if (dm == p) {
				p = NULL;
				*head = NULL;
			}
			free(dm->next);
			free(dm);
		}
		else if (p->next != *head) p = p->next;
		else p = NULL;
	} while (p != NULL);
}
