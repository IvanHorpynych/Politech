/************************************************************************
*file: list.c
*purpose: list
*author: Prodan A.
*written: 24/11/2014
*last modified: 30/11/2014
*************************************************************************/

#include "list.h"


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


void del_node(CNode **head, int id){
	CNode *tmp;
	if (isempty(*head) == 0){
		while (tmp = get_node(*head, id)){
			tmp->prev->next = tmp->next;
			tmp->next->prev = tmp->prev;
			if (tmp == *head)
			if (*head != (*head)->prev)*head = (*head)->prev;
			else{
				free(*head);
				*head = NULL;
				return;
			}
			free(tmp);
		}
	}
}

void ins_node(CNode **head, const CNode *pn, int id)// copies pn and inserts it before id
{
	if (*head != NULL)
	{
		CNode *tmp = *head, *cp = *head;
		do {
			if (tmp->id == id) {
				CNode *cp = malloc(sizeof(CNode));
				cp->id = pn->id;
				cp->next = tmp;
				cp->prev = tmp->prev;
				tmp->prev->next = cp;
				tmp->prev = cp;
				if (tmp == *head)
					*head = cp;
			}
			tmp = tmp->next;
		} while (tmp != cp);
	}
	else printf("List is empty\n");
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

CNode *merge_unique(const CNode *head1, const CNode *head2)// forms list from different elements of lists
{
	if (head1 == NULL) head1 = head2; else
	if (head2 == NULL) head2 = head1; else
	{
		CNode *tmp1 = head1, *tmp2 = head2, *cp = NULL;
		int f = 0;
		do {
			do {
				if (tmp2->id == tmp1->id)
					f = 1;
				tmp2 = tmp2->next;
			} while (tmp2 != head2);
			if (f == 0) append2list(&cp, tmp1);
			tmp1 = tmp1->next;
			tmp2 = head2;
			f = 0;
		} while (tmp1 != head1);
		tmp1 = head1;
		tmp2 = head2;
		f = 0;
		do {
			do {
				if (tmp1->id == tmp2->id)
					f = 1;
				tmp1 = tmp1->next;
			} while (tmp1 != head1);
			if (f == 0) append2list(&cp, tmp2);
			tmp2 = tmp2->next;
			tmp1 = head1;
			f = 0;
		} while (tmp2 != head2);
		return cp;
	}
}

void unique(CNode **head)// deletes all adjoining duplicates of elements
{
	if (*head != NULL) {
		CNode *tmp = *head, *cp = NULL;
		int f = 0, size = 0, i = 1;
		do {
			size++;
			tmp = tmp->next;
		} while (tmp != *head);
		do {
			if (tmp->id == tmp->next->id) {
				cp = tmp->next;
				tmp->next = tmp->next->next;
				tmp->next->prev = tmp;
				free(cp);
				f = 1;
			}
			if (f == 0)
				tmp = tmp->next;
			f = 0;
			i++;
		} while (i < size);
	}
}


