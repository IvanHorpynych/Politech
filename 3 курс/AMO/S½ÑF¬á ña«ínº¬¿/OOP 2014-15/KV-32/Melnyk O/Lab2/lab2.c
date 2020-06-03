/*******************************************************************************
 * File: lab2.c
 * Synopsis: various functions created as requested in labwork, every one
 *           explicitly described
 *           These functions are declared in the header file lab2.h
 ***
 **PLEASE TAKE NOTE THAT every single one of them will segfault if given NULL
 **instead of a valid pointer. Don't do that.
 ***
 * Related files: lab2.h - header with function prototypes
 *                lab2_test.c - sample program to test every function
 * Author: Мельник О.О., КВ-32
 * Written:
 * Last modified:
*******************************************************************************/

# include "lab2.h"

/*******************************************************************************
 * Name         isempty - checks if list is empty
 * Usage        tmpint = isempty(head);
 * Prototype in lab2.h
 * Synopsis     'head' is always special in this kind of list. It's 'id' has no
 *               meaning, so list is assumed to be empty when there is only
 *              'head', both 'prev' and 'next' of which point to it. This
 *              function checks just that.
 * Return Value Returns 1 if list is empty and 0 otherwise (even if given head
 *              isn't actually head of a list).
*******************************************************************************/

int isempty(const CNode *head){
	if((head == head->prev) && (head == head->next))
		return 1;
	return 0;
}

/*******************************************************************************
 * Name         get_node - checks if list is empty
 * Usage        tmpcnode = get_node(head, id);
 * Prototype in lab2.h
 * Synopsis     Checks if given list is empty; if it is - returns NULL, else
 *              compares all id's in list with given id until comes back to head
 *              and returns node with same id if it is found
 * Return Value Returns pointer to cnode which has same id as given one. If
 *              there is no such node (or list is empty) returns NULL.
*******************************************************************************/

CNode *get_node(const CNode *head, int id){
	CNode *tmp = head->next;
	if(!isempty(head)){
		do
			if(tmp->id == id) return tmp;
		while((tmp = tmp->next) != head);
	}
	return NULL;
}

/*******************************************************************************
 * Name         append2list - appends given node to the end of the given list
 * Usage        append2list(&head, pn);
 * Prototype in lab2.h
 * Synopsis     Adds new node before 'head' (taking circularity in account that
 *              means 'at the end of the list') by malloc'ing new one, copying
 *              'id' and changind pointers accordingly
 * Return Value Void
*******************************************************************************/

void append2list(CNode **head, const CNode *pn){
	CNode *tmp = (CNode*) malloc(sizeof(CNode));
	if(NULL == tmp)
		return;
	tmp->id = pn->id;
	tmp->next = *head;
	tmp->prev = (*head)->prev;
	(*head)->prev->next = tmp;
	(*head)->prev = tmp;
}

/*******************************************************************************
 * Name         del_node - deletes all nodes with given 'id' from given list
 * Usage        del_node(&head,id);
 * Prototype in lab2.h
 * Synopsis     'get_node's node with given 'id' and removes it by modifying
 *              pointers accordingly and 'free'ing. Repeats until there are no
 *              such nodes.
 * Return Value Void
*******************************************************************************/

void del_node(CNode **head, int id){
	CNode *tmp;
	while(NULL != (tmp = get_node(*head, id))){
		tmp->prev->next = tmp->next;
		tmp->next->prev = tmp->prev;
		free((void*) tmp);
	}
}

/*******************************************************************************
 * Name         clear - removes all nodes from a list
 * Usage        clear(&head);
 * Prototype in lab2.h
 * Synopsis     Loops through whole list and 'free's every element while
 *              modifying pointers accordingly.
 * Return Value Void
*******************************************************************************/

void clear(CNode **head){
	CNode *tmp = (*head)->next;
	while(tmp != *head){
		tmp = tmp->next;
		free((void*) tmp->prev);
	}
	(*head)->next=*head;
	(*head)->prev=*head;
}

/*******************************************************************************
 * Name         ins_node - inserts given node into given list before node with
 *              given 'id'
 * Usage        ins_node(&head,pn,id);
 * Prototype in lab2.h
 * Synopsis     'get_node's node with given 'id' and inserts a copy of given
 *              'pn' before it by modifying pointers accordingly.
 * Return Value Void
*******************************************************************************/

void ins_node(CNode **head, const CNode *pn, int id){
	CNode *tmp;
	CNode *new;
	if(NULL != (tmp = get_node(*head, id))){
		new = (CNode*) malloc(sizeof(CNode));
		new->id = pn->id;
		new->prev = tmp->prev;
		new->next = tmp;
		tmp->prev->next = new;
		tmp->prev = new;
	}
}

/*******************************************************************************
 * Name         reverse - reverses given list
 * Usage        reverse(head);
 * Prototype in lab2.h
 * Synopsis     Swaps 'next' and 'prev' in every node until it runs out of nodes
 * Return Value Void
*******************************************************************************/

void reverse(CNode *head){
	CNode *tmp = head->prev;
	head->prev = head->next;
	head->next = tmp;
	if(head == head->prev->prev)
		reverse(head->prev);
}

/*******************************************************************************
 * Name         print_list - outputs list nicely
 * Usage        print_list(head);
 * Prototype in lab2.h
 * Synopsis     Just 'printf's every 'id' on a new line, also adding some visual
 *              bells and whistles to show that the list is circular
 * Return Value Void
*******************************************************************************/

void print_list(const CNode *head){
	CNode *tmp;
	printf("\n/\\/\\/\\\n||||||\n<head>\n");
	for(tmp = head->next; tmp != head; tmp = tmp->next)
		printf("%d\n", tmp->id);
	printf("||||||\n\\/\\/\\/\n\n");
}

/*******************************************************************************
 * Name         merge_unique - creates new list from two given by copying there
 *              all nodes that are not in both lists
 * Usage        tmpcnode = merge_unique(head1, head2)
 * Prototype in lab2.h
 * Synopsis     'get_node's every node of first given list from second one and
 *              appends it to the new list if there was no such node in second
 *              one. Then repeats same thing for second one.
 * Return Value Returns pointer to the 'head' of new list.
*******************************************************************************/

CNode *merge_unique(const CNode *head1, const CNode *head2){
	CNode *tmp;
	CNode *new = (CNode*) malloc(sizeof(CNode));
	new->prev = new;
	new->next = new;
	for(tmp = head1->next; tmp != head1; tmp = tmp->next)
		if(NULL == get_node(head2, tmp->id))
			append2list(&new, tmp);
	for(tmp = head2->next; tmp != head2; tmp = tmp->next){
		if(NULL == get_node(head1, tmp->id))
			append2list(&new, tmp);
	}
	return new;
}

/*******************************************************************************
 * Name         unique - for every node in given list removes all of it's
 *              consecutive duplicates
 * Usage        unique(head);
 * Prototype in lab2.h
 * Synopsis     Takes node and compares with next one. If next one has same id -
 *              deletes next one. If id's are different - takes next one as
 *              current.
 * Return Value Void
*******************************************************************************/

void unique(CNode **head){
	CNode *tmp;
	CNode *tmp2;
	for(tmp = (*head)->next; tmp != *head; tmp = tmp->next){
		while(tmp->id == tmp->next->id){
			tmp2 = tmp->next->next;
			free((void*) tmp->next);
			tmp->next = tmp2;
			tmp2->prev = tmp;
		}
	}
}
