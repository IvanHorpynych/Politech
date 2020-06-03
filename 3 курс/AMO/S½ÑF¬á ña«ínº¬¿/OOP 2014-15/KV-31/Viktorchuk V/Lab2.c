#include "Laba2.h"

CNode* list_create(int len){
		int value, size = 10, i;
		CNode *temp, *p = malloc(sizeof(CNode)), *head = NULL;

		p = head;
		for (int i = 1; i <= len; i++){
			temp = malloc(sizeof(CNode));
			temp->data = rand() % 10 + 1;
			temp->prev = p;
			if (head != NULL)
				p->next = temp;
			else
				head = temp;
			p = temp;
		}
		p->next = head;
		head->prev = p;
		return head;
}

int isempty(const CNode *head){
	if (head == NULL) return 0;
	else return 1;
}

CNode *get_node(const CNode *head, int id){
	if (isempty(head) == 1){
		CNode *copy = malloc(sizeof(CNode)), *first = head;
		copy = head;
		do
		{
			copy = copy -> next;
		} while (copy->data != id);

		return copy;
	}
	return NULL;
}

void append2list(CNode **head, const CNode *pn) {
	CNode *p = (CNode *)malloc(sizeof(CNode));
	p->data = pn->data;
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

void del_node(CNode **head, int id){
	if (isempty(*head) == 1){
		CNode *temp = *head, *f = *head;

		do{
			if (temp->data == id){
				f = temp;
				(temp->prev)->next = temp->next;
				(temp->next)->prev = temp->prev;
				if (temp == *head)
					*head = temp->next;
				temp = temp->next;
				free(f);
			}
			else
				temp = temp->next;
		} while (temp != *head);
	}
}

void clear(CNode **head){
	if (isempty(*head) == 1){
		CNode *temp;
		int id = 0;

		do{
			id = (*head)->data;
			del_node(head, id);
		} while ((*head)->next != *head);
		free(*head);
	}
}

void ins_node(CNode **head, const CNode *pn, int id){
	if ((isempty(*head) == 1) && (pn != NULL)){
		CNode *temp = *head, *copy = malloc(sizeof(CNode));
		copy->data = pn->data;

		while (temp->data != id) {
			temp = temp->next;
			if (temp == *head)
				break;
		} 

		if (temp->data == id){
			copy->next = temp;
			copy->prev = temp->prev;
			(temp->prev)->next = copy;
			temp->prev = copy;
		}
	}
}

void reverse(CNode *head){
	if (isempty(head) == 1){
		CNode *p = malloc(sizeof(CNode)), *temp = head;

		do{
			p = temp->next;
			temp->next = temp->prev;
			temp->prev = p;
			temp = temp->prev;
			
		} while (temp != head);
		
		head = head->next;printf("\n\n");
	}	
}

void print_lst(const CNode *head){
	if (isempty(head) == 1){
		CNode *first = head;
		do {
			printf("%d ", head->data);
			head = head->next;
		} while (head != first);
	}
}

CNode *merge_unique(const CNode *head1, const CNode *head2) {
	CNode *head = NULL;
	CNode *p = head1;
	if (isempty(head1)==1) do {
		CNode *p1 = head2;
		int k = 0;
		if (p->data == p1->data) k++;
		do{
			p1 = p1->next;
			if (p->data == p1->data) k++;
		} while (p1->next != head2);
		if (k == 0) 
			if (isempty(head) == 0){
				head = malloc(sizeof(CNode));
				head->data = p->data;
				head->next = head;
				head->prev = head;
			}
			else
				append2list(&head, p);
		p = p->next;
	
	} while (p != head1);

	p = head2;
	if (isempty(head2)==1) do {
		CNode *p1 = head1;
		int k = 0;
		if (p->data == p1->data) k++;
		do{
			p1 = p1->next;
			if (p->data == p1->data) k++;
		} while (p1->next != head1);
		if (k == 0)
		if (isempty(head) == 0){
			head = malloc(sizeof(CNode));
			head->data = p->data;
			head->next = head;
			head->prev = head;
		}
		else
			append2list(&head, p);
		p = p->next;

	} while (p != head2);

	return head;
}

void unique(CNode **head) {
	if (isempty(*head)) {
		CNode *p = *head, *f;
		do{
			if (p->next->data == p->data){
				f = p;
				p->next->prev = p->prev;
				p->prev->next = p->next;
				if (p == *head)
					*head = p->next;
				p = p->next;
				free(f);
			}
			else
				p = p -> next;
		} while (p->next != *head);
	}
}