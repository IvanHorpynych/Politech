/************************************************************************
*file: ring_func.h
*author: Savin A.D.
*group: KV-31, FPM
*written: 07/10/2014
*last modified: 07/10/2014
************************************************************************/

#include "ring_func.h"

CNode *fill_ring(const int v[], int size)// fills list with elements
{
	CNode *tmp, *cp = NULL, *head = NULL;
	for (int i = 0; i < size; i++) {
		tmp = malloc(sizeof(CNode));
		tmp->id = v[i];
		if (i == 0){
			tmp->prev = tmp;
			tmp->next = tmp;
			head = tmp;
		}
		else{
			cp->next = tmp;
			tmp->next = head;
			tmp->prev = cp;
			head->prev = tmp;
		}
		cp = tmp;
	}
	return head;
}

int isempty(const CNode *head)// cheks if list is empty
{
	if (head == NULL) return 1;
	else return 0;
}

void print_list(const CNode *head)// prints list
{
	if (head != NULL){
		CNode *tmp = head;
		printf("Printing list:\n");
		do {
			printf("%d ", tmp->id);
			tmp = tmp->next;
		} while (tmp != head);
		printf("\n");
	}
	else printf("\nList is empty\n");
}

CNode *get_node(const CNode *head, int id)// returns pointer with set id
{
	int f = 0;
	if (isempty(head) == 1) return NULL; else
	{
		CNode *tmp = head;
		do {
			if (tmp->id == id) {
				f = 1;
				return tmp;
			}
			tmp = tmp->next;
		} while (tmp != head);
	}
	if (f == 0) return NULL;
}


void append2list(CNode **head, const CNode *pn)// copies pn and adds it to the end of the list
{
	CNode *hlp = pn;
	if (pn != NULL){
		CNode *tmp = malloc(sizeof(CNode));
		if (*head == NULL){
			tmp->id = pn->id;
			tmp->prev = tmp;
			tmp->next = tmp;
			*head = tmp;
		}
		else {
			tmp->id = pn->id;
			tmp->next = *head;
			tmp->prev = (*head)->prev;
			tmp->prev->next = tmp;
			(*head)->prev = tmp;
		} 
	}
}

void del_node(CNode **head, int id)// deletes all id
{
	CNode *tmp;
	if (*head != NULL) do {
		tmp = *head;
		tmp = get_node(tmp, id);
		if ((tmp == *head) && (tmp == (*head)->next))
			*head = NULL;
		if (tmp == *head)
			*head = (*head)->next;
		if (tmp != NULL){
			tmp->prev->next = tmp->next;
			tmp->next->prev = tmp->prev;
			free(tmp);
		}
	} while (tmp != NULL);
}


void clear(CNode **head)// deletes elements of the list
{
	CNode *tmp, *cp;
	if (*head != NULL) do {
		tmp = (*head)->next;
		cp = tmp;
		tmp = tmp->next;
		cp->prev->next = tmp;
		tmp->prev = cp->prev;
		free(cp);
	} while (tmp != *head);
	free(*head);
	*head = NULL;
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

void reverse(CNode **head)// reverse pointers
{
	if (*head != NULL){
		CNode *tmp = *head, *cp = NULL;
		do {
			cp = tmp->next;
			tmp->next = tmp->prev;
			tmp->prev = cp;
			tmp = cp;
		} while (tmp != *head);
		*head = tmp->next;
	}
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

