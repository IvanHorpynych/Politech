/************************************************************************
*file: OOP_lab3.h
*purpose: declarations for list functions, types, constants
*author: Andrey Chernysh
*written: 20/10/2014
*last modified: 26/10/2014
*************************************************************************/
#ifndef _OOP_LAB3_H
#define _OOP_LAB3_H
#include <stdio.h>
#include <stdlib.h>

typedef struct node{
	//attributes
	int id;//node's index

	struct node *prev;//Pointer to the previous node in the node's list
	struct node *next;//Pointer to the next     node in the node's list

} Cnode;

void makelist(Cnode **head, int plus);
void print_lst(Cnode *head);
int isempty(const Cnode *head);
Cnode *get_node(const Cnode *head, int id);
void append2list(Cnode **head, const Cnode *pn);
void del_node(Cnode **head, int id);
void clear(Cnode **head);
Cnode *head_merge_unique(const Cnode *head1, const Cnode *head2);
void ins_node(Cnode **head, const Cnode *pn, int id);
void reverse(Cnode *head);
void unique(Cnode **head);

#endif