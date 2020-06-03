#include "list.h"

int empty(const CNode *head){
    if (!head) return -1;
    if (head->next == head){
        return 1;        
    }else return 0;
}

CNode *get_node (const CNode *head, int id ){
    
    CNode *temp;
    if (!head) return 0;
    if (empty(head))return 0;
	temp = (CNode*)head;

    while ((temp = temp->next) != head){
        if (temp->id == id) return temp;
    }
    return 0;
}

void append2list (CNode *head, const CNode *pn){
    CNode *newelem;
    if (!head || !pn) return;
    if (!(newelem = (CNode*)malloc(sizeof(CNode)))) return;
    
    newelem->id = pn->id;
    newelem->next = head;
    newelem->prev = head->prev;
    (head->prev)->next = newelem;
    head->prev = newelem;
}


void del_node (CNode *head, int id){
    CNode *temp;   
    
    if (!head) return;
    if (empty(head)) return;
    
	temp = head;

    while((temp = temp->next)!=head){
        if(temp->id == id){
            (temp->prev)->next = temp->next;
            (temp->next)->prev = temp->prev;
            temp = temp->next;
            free(temp->prev);
        }else {
            temp = temp->next;
        }
    }

/*
    while (temp = get_node(head,id)) {
            (temp->prev)->next = temp->next;
            (temp->next)->prev = temp->prev;
            free(temp);
    }
*/
}

void clear(CNode *head){
	CNode *temp = 0;
	if (!head) return;
    if (empty(head)) return;



	temp = head->next;
    while (temp != head){
        temp = temp->next;
        free(temp->prev);
    }
    head->next = head;
    head->prev = head;
}

void ins_node (CNode *head, const CNode *pn, int id){
    CNode *enterpoint;
    CNode *temp;
    if (!head) return;
    
    enterpoint = get_node(head,id);
    if (!enterpoint){
        append2list(head,pn);
        return;
    }
    if (!(temp = (CNode*)malloc(sizeof(CNode)))) return;
    temp->id = pn->id;
    
    (enterpoint->prev)->next = temp;
    temp->prev = enterpoint->prev;
    enterpoint->prev = temp;
    temp->next = enterpoint;

}

void reverse(CNode *head){

    CNode *temp;
    CNode *next;

    if (!head) return;

    next = head;
    temp = head;
    do{
        next = temp->next;
        temp->next = temp->prev;
        temp->prev = next;
        
        temp = temp->next;
    }while (temp != head);
    
}

void print_list(const CNode *head){
    CNode *temp;
    int i = 0;
    
    if (!head) return;
    temp = head->next;
    
    while(temp != head){
        printf("[%d] = %d\n",i++,temp->id);
        temp = temp->next;
    }
}

CNode *merge_unique(const CNode *head1, const CNode *head2){
    CNode *newlist,*i,*temp;
    
    if (!head1 || !head2) return 0;
    
    if(!(newlist= (CNode*)malloc(sizeof(CNode)))) return 0;    
    newlist->next = newlist;
    newlist->prev = newlist;
    i = head1->next;
    
    while (i != head1){
        if (!(temp = get_node(head2,i->id))){
            append2list(newlist,i);
        }
        i = i->next;
    }
    i = head2->next;
    while (i != head2){
        if (!(temp = get_node(head1,i->id))){
            append2list(newlist,i);
        }
        i = i->next;
    }    
    return newlist;
}
    
void unique(CNode *head){
    int deleted_amount;
    CNode *i,*temp;
    
    if (!head) return;
    
    do{
        deleted_amount = 0;
        i = head->next;
        while (i != head->prev){
            if (i->id == (i->next)->id){
                ++deleted_amount;
                temp = i->next->next;
                (i->prev)->next = (i->next)->next;
                (i->next)->next->prev = i->prev;
                free(i->next);
                free(i);
                i = temp;
            }
            i = i->next;
        }
        
    }while (deleted_amount);
}