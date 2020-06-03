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
		int i;

		do {
			i = head->data;
			head = head->next;
		} while ((head != first) && (i != id));

		if (i == id){
			copy->data = (head->prev)->data;
			copy->next = NULL;
			copy->prev = NULL;
			return copy;
		}
		else return NULL;
	}
	else return NULL;
}

void append2list(CNode **head, const CNode *pn){
	if ((isempty(*head) == 1) && (pn != NULL)){
		CNode *temp = *head, *copy = malloc(sizeof(CNode));
		copy->data = pn->data;

		temp = temp->prev;
		temp->next = copy;
		(temp->next)->prev = temp;
		(temp->next)->next = *head;
		temp = *head;
		temp->prev = copy;
	}
}

void del_node(CNode **head, int id){
	if (isempty(*head) == 1){
		CNode *temp = *head, *f = *head;

		do{
			if (temp->data == id){
				f = temp;
				(temp->prev)->next = temp->next;
				(temp->next)->prev = temp->prev;
				if (temp == *head){
					
					*head = temp->next;
				}
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

		do {
			temp = temp->next;
		} while ((temp->data != id) && (*head != temp));

		if (temp != *head){
			copy->next = temp;
			copy->prev = temp->prev;
			(temp->prev)->next = copy;
			temp->prev = copy;
		}
	}
}

void reverse(CNode *head){
	if (isempty(head) == 1){
		CNode *dop = malloc(sizeof(CNode)), *temp = head;

		do{
			dop = temp->next;
			temp->next = temp->prev;
			temp->prev = dop;
			temp = temp->prev;
		} while (temp != head);
		head = head->next;
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

CNode* merge_unique(const CNode *head1, const CNode *head2){
	if ((isempty(head1) == 1) && (isempty(head2) == 1)){
		CNode *head = NULL, *p = malloc(sizeof(CNode)), *first1 = head1, *first2 = head2;
		int f = 0;
		do {
			do {			
				if (head2->data == head1->data)
					f = 1;
				head2 = head2->next;
			} while ((head2 != first2) && (f != 1));

			if ((f == 0) && (get_node(head, head1->data) == NULL)){
				if (head != NULL)
					append2list(&head, head1);
				else{
					p->data = head1->data;
					p->next = p;
					p->prev = p;
					head = p;
				}	
			}	
			f = 0;
			head2 = first2;
			head1 = head1->next;
		} while (head1 != first1);
		return head;
	}
	else return NULL;
}

void unique(CNode **head) {
	CNode *temp = *head, *f = malloc(sizeof(CNode));

	do {
		if (temp->data == (temp->next)->data){
			f = temp;
			(temp->prev)->next = temp->next;
			(temp->next)->prev = temp->prev;
			temp = temp->next;
			free(f);
		}
		else 
			temp = temp->next;
	} while (temp != *head);
}