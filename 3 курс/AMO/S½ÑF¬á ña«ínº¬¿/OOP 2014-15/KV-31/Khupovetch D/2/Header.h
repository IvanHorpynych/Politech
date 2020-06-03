/*
File: func.c
Author: Khupovetch D.Y.
Group: KV-31, FPM
Written: 2.11.2014
*/

#include <stdlib.h>
#include <stdio.h>
#include <cstdbool>
#include <malloc.h>

typedef struct node
{
	int el;
	struct node *prev;
	struct node *next;
} node;

node* make(int size);
void printlist(const node *head);
int isempty(const node *p);
node *get_node(const node *head, int id);
void append2list(node **head, const node *pn);
void del_node(node **head, int el);
void clear(node **head);
void ins_node(node **head, const node *pn, int el);
void reverse(node **head);
node *head_merge_unique(const node *head1, const node *head2);
void unique(node **head);

