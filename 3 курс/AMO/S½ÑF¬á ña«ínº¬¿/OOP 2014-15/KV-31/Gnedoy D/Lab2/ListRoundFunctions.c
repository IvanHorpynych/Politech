/************************************************************************
*file: ListRoundFunction.c
*author: Gnedoj D. O.
*group: KV-31, FPM
*written: 10/09/2014
*last modified: 10/09/2014
************************************************************************/
#include "ListRound.h"

CNode *create_list (const int v[], int size){
	CNode *temp, *prevTemp = NULL, *head = NULL;
	for(int i = 0; i < size; i++) {
		temp = malloc(sizeof(CNode));
		temp->id = v[i];
		if (i == 0){
			temp->prev = temp;
			temp->next = temp;
			head = temp;
		}
		else{
			prevTemp->next = temp;
			temp->next = head;
			temp->prev = prevTemp;
			head->prev = temp;
		}
		prevTemp = temp;	
	}
	return head;
}
int isempty(const CNode *head){
	if (head = NULL) return 1;
	else return 0;
}
CNode *get_node(const CNode *head, int id){
	if (!head) return NULL;
	else{
		CNode* temp = head;
		do{
			if (temp->id == id) return temp;
			temp = temp->next;
		} while (temp != head);
	}
	return NULL;
}
void append2list(CNode **head, const CNode *pn){
	if (pn){
		CNode *temp = malloc(sizeof(CNode));
		temp->id = pn->id;
		temp->prev = NULL;
		temp->next = NULL;
		if (*head == NULL){
			temp->prev = temp;
			temp->next = temp;
			*head = temp;
		}
		else{
			temp->next = *head;
			temp->prev = (*head)->prev;
			temp->prev->next = temp;
			(*head)->prev = temp;
		}
	}
	else
		printf("There are problems with node! Try again.\n");
}
void del_node(CNode **head, int id){
	if (*head){
		CNode* temp = *head, *helpTemp = NULL;
		do{
			if (temp->id == id){
				if ((temp == *head) && (temp->next == *head)){
					free(temp);
					*head = NULL;
					return;	
				}
				else{
					if (temp == *head)
						*head = temp->next;
					helpTemp = temp;
					temp = helpTemp->next;
					helpTemp->prev->next = temp;
					temp->prev = helpTemp->prev;
					free(helpTemp);
				}
			}
			else 
				temp = temp->next;
		} while (temp != *head);
	}
}
void clear(CNode **head){
	if (head){
		CNode* temp = (*head)->next, *helpTemp = NULL;
		do{
			helpTemp = temp;
			temp = temp->next;
			helpTemp->prev->next = temp;
			temp->prev = helpTemp->prev;
			free(helpTemp);
		} while (temp != *head);
		free(*head);
		*head = NULL;
	}
}
void ins_node(CNode **head, const CNode *pn, int id){
	if (*head){
		CNode* temp = *head, *helpTemp = NULL;
		do{
			if (temp->id == id) break;
			temp = temp->next;
		} while (temp != *head);
		if (temp->id == id){
			CNode *pnCopy = malloc(sizeof(CNode));
			pnCopy->id = pn->id;
			pnCopy->prev = NULL;
			pnCopy->next = NULL;
			if (temp == *head)
				*head = pnCopy;
			pnCopy->prev = temp->prev;
			pnCopy->next = temp;
			temp->prev->next = pnCopy;
			temp->prev = pnCopy;
		}
		else
			printf("There are no matches! Try again.");
	}
	else printf("Node is empty!");
}
void reverse(CNode **head){
	if (*head){
		*head = (*head)->prev;
		CNode *temp = *head, *helpTemp = temp->prev;
		do{
			temp->prev = temp->next;
			temp->next = helpTemp;
			temp = helpTemp;
			helpTemp = temp->prev;
		} while (temp != *head);

	}
	else printf("Node is empty!\n");
}
void print_list(const CNode *head ){
	if (head){
		CNode* temp = head;
		do{
			printf("%d ",temp->id);
			temp = temp->next;
		} while (temp != head);
		printf("\n");
	}
	else printf("\nNode is empty!\n");
}
void print_list_reverse(const CNode *head){
	if (head){
		CNode* temp = head->prev;
		do{
			printf("%d ", temp->id);
			temp = temp->prev;
		} while (temp != head->prev);
		printf("\n");
	}
	else printf("\nNode is empty!\n");
}
CNode *merge_unique(const CNode *head1, const CNode *head2){
	if (!head1) return head2;
	else
		if (!head2) return head1;
		else
			if (!head1 && !head2) return NULL;
			else{
				CNode* temp1 = head1, *temp2 = head2, *NewHead = NULL;
				int count = 0;
				do{
					do{
						count = 0;
						if (temp1->id == temp2->id) 
							++count;
						temp2 = temp2->next;
					} while(temp2 != head2);
					if (count == 0 && get_node(NewHead, temp1->id) == NULL)
						append2list(&NewHead, temp1);
					temp1 = temp1->next;
				} while(temp1 != head1);

				do{
					do{
						count = 0;
						if (temp2->id == temp1->id) 
							++count;
						temp1 = temp1->next;
					} while(temp1 != head1);
					if (count == 0 && get_node(NewHead, temp2->id) == NULL)
						append2list(&NewHead, temp2);
					temp2 = temp2->next;
				} while(temp2 != head2);
	
				return NewHead;
			}
}
void unique(CNode **head){
	if (!(*head)) return;
	else{
		CNode *temp = *head, *tempDel = NULL;
		do{
			if (temp->id == temp->next->id){
				tempDel = temp->next;
				temp->next = tempDel->next;
				tempDel->next->prev = temp;
				free(tempDel);
			}
			else temp = temp->next;
		}while(temp->next != *head);
	}
}