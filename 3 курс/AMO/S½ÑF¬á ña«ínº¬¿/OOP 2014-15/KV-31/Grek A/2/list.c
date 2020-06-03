/************************************************************************
*file: list.c
*purpose: list
*author: Alex Grek
*written: 24/11/2014
*last modified: 24/11/2014
*************************************************************************/

#include "list.h"

int isempty(const CNode *head){
	if (head == NULL)
		return 1; //if list is empty
	return 0;
}

//creates the list with elements from 0 to size
CNode* generateList(int size) {
	CNode *temp, *head;
	head = NULL;

	int i;
	for (i = 0; i < size; i++) {
		temp = (CNode*)malloc(sizeof(CNode));
		temp->id = i;
		if (head){
			temp->prev = head;
			temp->next = head->next;
			head->next->prev = temp;
			head->next = temp;
			head = temp;
		}
		else{
			head = temp;
			head->next = head;
			head->prev = head;
		}
	}
	return head;
}

CNode* get_node(const CNode *head, int id){
	CNode *temp; //temporary element
	if (isempty(head))
		return NULL;
	temp = head->next;
	//search for element
	do{
		if (temp->id == id) //if element found
			return temp;
		temp = temp->next;
	} while (temp != head->next); //end of list reached
	return NULL;
}


void print_list(const CNode *head){
	CNode *temp; //temporary element
	if (isempty(head))
		return;
	temp = head->next;
	do{
		printf("%d ", temp->id);
		temp = temp->next;
	} while (temp != head->next); //until the end of list
}

void append2list(CNode **head, const CNode *pn){
	CNode *temp; //new element

	if (pn == NULL)
		return;

	//allocate memory for it
	temp = (CNode*)malloc(sizeof(CNode));
	temp->id = pn->id;

	//add it to list
	if (*head){
		temp->prev = *head;
		temp->next = (*head)->next;
		(*head)->next->prev = temp;
		(*head)->next = temp;
		(*head) = temp;
	}
	else{ //if list is empty
		*head = temp;
		(*head)->next = *head;
		(*head)->prev = *head;
	}
}


void del_node(CNode **head, int id){
	CNode *temp; //element to delete

	if (isempty(*head))
		return;

	//get element and pete it
	while (temp = get_node(*head, id)){
		//update prev and next pointers of elements around the deleted element
		temp->prev->next = temp->next;
		temp->next->prev = temp->prev;
		if (temp == *head)
		if (*head != (*head)->prev)*head = (*head)->prev;
		else{
			free(*head);
			*head = NULL;
			return;
		}
		free(temp);
	}
}

void clear(CNode **head){
	CNode *temp; //temporary element

	if (isempty(*head))
		return;

	temp = (*head)->next;
	//delete all elements
	while (*head != temp) {
		temp->next->prev = temp->prev;
		temp->prev->next = temp->next;
		free(temp);
		temp = (*head)->next;
	}
	//delete head
	free(*head);
	*head = NULL;
}

void ins_node(CNode **head, const CNode *pn, int id){
	CNode *p /*new element*/, *temp /*element to insert after*/;
	if (isempty(*head) || pn == NULL)
		return;
	p = (CNode*)malloc(sizeof(CNode));
	p->id = pn->id;

	temp = get_node(*head, id);
	if (temp == NULL) //if element was not found in list
		return;

	p->next = temp;
	p->prev = temp->prev;
	temp->prev->next = p;
	temp->prev = p;
}

void reverse(CNode *head){
	CNode *up, *dn;
	int tmp;

	if (isempty(head))
		return;

	up = head->next;
	dn = up->prev;
	do{
		tmp = up->id;
		up->id = dn->id;
		dn->id = tmp;
		dn = dn->prev;
		up = up->next;
	} while (up != dn && dn->next != up);
}

int isNotIn(const CNode *head, int id){
	CNode *temp;

	if (isempty(head))
		return 0;

	temp = head->next;
	do{
		if (id == temp->id)
			return 0;
		temp = temp->next;
	} while (temp->prev != head);
	return 1;
}

CNode *head_merge_unique(const CNode *head1, const CNode *head2){
	CNode *temp = NULL /*new list*/, *p, *p_head;
	if (isempty(head1))
		return NULL;
	p = head1->next;
	do{
		if (isNotIn(head2, p->id))
			append2list(&temp, p);
		p = p->next;
	} while (p->prev != head1);

	p = head2->next;
	do{
		if (isNotIn(head1, p->id))
			append2list(&temp, p);
		p = p->next;
	} while (p->prev != head2);

	if (temp == NULL) 
		return NULL;

	p_head = temp->next;
	do{
		p = p_head->next;
		while (p != p_head){
			if (p->id == p_head->id){
				del_node(&temp, p_head->id);
				p_head = temp->next;
				break;
			}
			p = p->next;
		}
		p_head = p_head->next;
	} while (p_head->next != temp);

	return temp;
}


void unique(CNode **head){
	CNode *temp /*temporary element*/, *p /*element to delete*/;

	if (isempty(*head))
		return;

	temp = (*head)->next;

	if (temp == *head)
		return;

	while (temp != *head){
		if (temp->next->id == temp->id){
			temp->prev->next = temp->next;
			temp->next->prev = temp->prev;
			p = temp;
			if (temp == *head)*head = (*head)->next;
			temp = temp->next;
			free(p);
		}
		else temp = temp->next;
	}

	if (temp->next->id == temp->id){
		temp->prev->next = temp->next;
		temp->next->prev = temp->prev;
		p = temp;
		*head = (*head)->next;
		temp = temp->next;
		free(p);
	}
}