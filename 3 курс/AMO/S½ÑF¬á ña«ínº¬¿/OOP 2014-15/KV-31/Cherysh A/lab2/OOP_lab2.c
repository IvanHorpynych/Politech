/************************************************************************
*file: OOP_lab3.c
*synopsis: all base functions which work with lists.  
* These functions are declared in the include file "OOP_LAB3.h".
*related files: none
*author: Chernysh Andrey
*written: 20/10/2014
*last modified: 26/10/2014
************************************************************************/
#include <stdio.h>
#include <stdlib.h>
#include "OOP_lab2.h"
//Making doubly linked circular list;
void makelist(Cnode **head, int plus){
	int size = 10, i;
	Cnode *temp,*p;
	*head = NULL;
	p = *head;
	for (i = 0; i < size; i++) {
		temp = malloc(sizeof(Cnode));
		temp->id = i + plus;
		temp->prev = p;
		if (i != 0)
			p->next = temp;
		else
			*head = temp;
		p = temp;
	}
	p->next = *head;
	(*head)->prev = p;
}
//Writing your list in the console;
void print_lst(Cnode *head) {
	int i = 0;
	Cnode *beg = head;
	if (head)
	do {
		printf("%d ", beg->id);
		beg = beg->next;
		i++;
	} while ((beg != head));
}
//List checking on the empty;
int isempty(const Cnode *head){
	//Cnode *p;
	if (head == NULL)
		return 1;
	return 0;
}
//Get index of element with index "id";
Cnode *get_node(const Cnode *head, int id){
	Cnode *beg = head;
	do{
		if (beg->id == id)
			return beg;
		beg = beg->next;
	} while (beg != head);
	return NULL;
}
// Addiction to the end of list some node;
void append2list(Cnode **head, const Cnode *pn){
	Cnode *p,*tempn = pn;
	p = (*head)->prev;
	p->next = tempn;
	tempn->next = *head;
	(*head)->prev = tempn;
	tempn->prev = p;
}
//Deleting node with index "id";
void del_node(Cnode **head, int id){
	Cnode *p,*beg = *head;
	do{
		if (beg->id == id){
			if (beg == *head){
				*head = beg->prev;
				(*head)->next = beg->next;
				p = beg->next;
				p->prev = *head;
			}
			else{
				p = beg->prev;
				p->next = beg->next;
				p = beg->next;
				p->prev = beg->prev;
			}
			p = beg;
			beg = beg->next;
			free(p);
		}
		beg = beg->next;
	} while (beg != *head);
}
//Deleting all elements of list;
void clear(Cnode **head){
	Cnode *p = (*head)->prev;
	p->next = NULL;
	while ((*head) != NULL){
		p = *head;
		*head = (*head)->next;
		free(p);
	}
}
//Inserting elements before node with index "id";
void ins_node(Cnode **head, const Cnode *pn, int id){
	Cnode *p,*tempn;
	Cnode *beg = *head;
	tempn = malloc(sizeof(Cnode));
	tempn->id = pn->id;
	do{
		if (beg->id == id){
			p = beg->prev;
			p->next = tempn;
			tempn->next = beg;
			beg->prev = tempn;
			tempn->prev = p;
		}
		beg = beg->next;
	}while (beg != *head);
}
/*making reverse list;
E.G : star list    : 1 2 3 4 5;
	  reverse list : 5 4 3 2 1;
*/
void reverse(Cnode *head){
	struct node *temp;
	Cnode *p;
	Cnode *beg = head;
	do{
		p = beg;
		beg = beg->next;
		temp = p->next;
		p->next = p->prev;
		p->prev = temp;
	} while (beg != head);
}
/*Make new list using some rules:
E.G: list1 : 1 2 3 4 5 ;
	 list2 : 4 5 6 7 8 ;
That Merge_unique_list : 1 2 3 6 7 8 ;
Head will be 1 ;*/
Cnode *head_merge_unique(const Cnode *head1, const Cnode *head2){
	Cnode *head = NULL, *beg1 = head1, *beg2 = head2, *tmpbeg1 = head1, *p, *temp;
	int flag = 0, i = 0, j = 0;

	p = head;
	do{
		do{
			if (flag == 0){
				do{
					if (tmpbeg1->id == beg2->id)
						break;
					tmpbeg1 = tmpbeg1->next;
					j++;
				} while (tmpbeg1 != head1);
				if (tmpbeg1 == head1 && j > 0){
					temp = malloc(sizeof(Cnode));
					temp->id = beg2->id;
					temp->prev = p;
					if (head != NULL)
						p->next = temp;
					else
						head = temp;
					p = temp;
				}
				else tmpbeg1 = head1;
			}
			if (beg1->id == beg2->id)
				break;
			beg2 = beg2->next;
			i++;
		} while (beg2 != head2);
		flag = 1;
		j = 0;
		if (beg2 == head2 && i > 0){
			temp = malloc(sizeof(Cnode));
			temp->id = beg1->id;
			temp->prev = p;
			if (head != NULL)
				p->next = temp;
			else
				head = temp;
			p = temp;
		}
		else beg2 = head2;
		beg1 = beg1->next;
		i = 0;
	} while (beg1 != head1);

	p->next = head;
	head->prev = p;

	return head;
}
void unique(Cnode **head){
	Cnode *beg = *head, *p = (*head)->next, *temp;
	int i = 0;
	do{
		if (beg->id == p->id){
			temp = p->next;
			if (temp->id == p->id){
				p = p->next;
				i = 3;
			}
			temp = beg->prev;
			temp->next = p->next;
			temp = p->next;
			if ((*head)->id == beg->id)
				*head = temp;
			if (i == 3)
				beg = beg->next;
			for (int i = 0; i < 2; i++){
				p = p->next;
				beg = beg->next;
			}
			beg->prev = temp;	
		}
		else{
			p = p->next;
			beg = beg->next;
		}
	} while (beg != *head);
}

