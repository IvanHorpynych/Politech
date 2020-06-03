/************************************************************************
*file: list.c
*purpose: list
*author: Anastasiev D.
*written: 24/10/2014
*last modified: 24/10/2014
*************************************************************************/

#include "list.h"

CNode* create_list (int size) {
		int i;
		CNode *temp, *head;
		head = NULL;
		
		for(i = 0; i < size; i++) {
			temp = (CNode*)malloc(sizeof(CNode));
			temp->id = i; 
			if(head){
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


int isempty(const CNode *head){
	if(head == NULL) return 0;
	return 1;
}

CNode* get_node (const CNode *head, int id){
	CNode *tmp;
	if(isempty(head)==0)return NULL;
	tmp = head->next;
	do{
		if(tmp->id==id)return tmp;
		tmp= tmp->next;
	}while(tmp!=head->next);
	return NULL;
}


void print_list(const CNode *head){
	CNode *tmp; 
	if(isempty(head)==0)return;
	tmp = head->next;
	do{
		printf("%d ",tmp->id);
		tmp=tmp->next;
	}while(tmp!=head->next );
}

void append2list (CNode **head, const CNode *pn ){
	CNode *tmp;
	if(pn==NULL)return;
	tmp = (CNode*)malloc(sizeof(CNode));
	tmp->id = pn->id;
	if(*head){
				tmp->prev = *head;
				tmp->next = (*head)->next;
				(*head)->next->prev = tmp;
				(*head)->next = tmp;
				(*head) = tmp;				
			}
			else{
				*head = tmp;
				(*head)->next = *head;
				(*head)->prev = *head;
			}
}	


void del_node (CNode **head, int id){
	CNode *tmp;
	if(isempty(*head)==0)return;
	while(tmp = get_node(*head, id)){
		tmp->prev->next = tmp->next;
		tmp->next->prev = tmp->prev;
		if(tmp==*head)
			if(*head != (*head)->prev)*head = (*head)->prev;
			else{
				free(*head);
				*head = NULL;
				return;
			}
		free(tmp);
	}
}

void clear(CNode **head){
	CNode *p;
	if(isempty(*head) == 0)return;
	p=(*head)->next;
	while(*head!=p){
		p->next->prev = p->prev;
		p->prev->next = p->next;
		free(p);
		p=(*head)->next;
	}
	free(*head);
	*head = NULL;
}

void ins_node (CNode **head, const CNode *pn, int id){
	CNode *p, *tmp;
	if(isempty(*head) == 0)return;
	if(pn == NULL)return;
	p = (CNode*)malloc(sizeof(CNode));
	p->id = pn->id;
	tmp = get_node(*head, id);
	if(tmp == NULL)return;
	p->next = tmp;
	p->prev = tmp->prev;
	tmp->prev->next = p;
	tmp->prev = p;	
}

void reverse(CNode *head){
	CNode *ps, *pe;
	int tmp;
	if(isempty(head) == 0)return;
	ps = head->next;
	pe = ps->prev;
	do{
		tmp = ps->id;
		ps->id = pe->id;
		pe->id = tmp;
		pe = pe->prev;
		ps = ps->next;
	}while(ps!=pe && pe->next!=ps);
}

int renode(const CNode *head,int id){
	CNode *tmp;
	if(isempty==0)return -1;
	tmp = head->next;
	do{
		if(id == tmp->id)return 0;
		tmp=tmp->next;
	}while(tmp->prev!=head);
	return 1;
}

CNode *head_merge_unique(const CNode *head1, const CNode *head2){
	CNode *tmp = NULL,*p,*p_head;
	if(isempty == 0)return;
	p = head1->next;
	do{
		if(renode(head2,p->id))append2list(&tmp,p);
		p = p->next;
	}while(p->prev!=head1);
	p = head2->next;
	do{
		if(renode(head1,p->id))append2list(&tmp,p);
		p = p->next;
	}while(p->prev!=head2);

	if(tmp == NULL) return NULL;
	p_head = tmp->next;
	do{
		p = p_head->next;
		while(p!=p_head){
			if(p->id == p_head->id){
				del_node(&tmp,p_head->id);
				p_head = tmp->next;
				break;
			}
			p = p->next;
		}
		
		p_head= p_head->next;
	}while(p_head->next!=tmp);
	return tmp;
}


void unique(CNode **head){
	CNode *tmp,*del;
	if(isempty==0)return;
	tmp = (*head)->next;
	if(tmp==*head)return;

	while(tmp != *head){
		if(tmp->next->id == tmp->id){
			tmp->prev->next = tmp->next;
			tmp->next->prev = tmp->prev;
			del = tmp;
			if(tmp == *head)*head = (*head)->next;
			tmp=tmp->next;
			free(del);
		}else tmp=tmp->next;
	}

	if(tmp->next->id == tmp->id){
			tmp->prev->next = tmp->next;
			tmp->next->prev = tmp->prev;
			del = tmp;
			*head = (*head)->next;
			tmp=tmp->next;
			free(del);
	}
	
}