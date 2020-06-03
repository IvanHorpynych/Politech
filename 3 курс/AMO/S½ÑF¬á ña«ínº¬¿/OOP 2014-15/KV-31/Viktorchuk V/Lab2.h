#ifndef _LIST_H
#define _LIST_H

#include <stdio.h>
#include <stdlib.h>

typedef struct tag_node{
	int data;
	struct tag_node *next;
	struct tag_node *prev;
}CNode;

CNode* list_create(int len);
int isempty(const CNode *head);
CNode *get_node(const CNode *head, int id);
void append2list(CNode **head, const CNode *pn);
void del_node(CNode **head, int id);
void clear(CNode **head);
void ins_node(CNode **head, const CNode *pn, int id);
void reverse(CNode *head);
void print_lst(const CNode *head);
CNode* merge_unique(const CNode *head1, const CNode *head2);
void unique(CNode **head);

#endif