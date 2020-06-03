/*******************************************************************************
 * File: lab2.h
 * Synopsis: declarations for lab2 functions
 * Author: Мельник О.О., КВ-32
 * Written:
 * Last modified:
*******************************************************************************/

# ifndef LAB2_H
# define LAB2_H
# include <stdio.h>
# include <stdlib.h>

typedef struct tag_CNode {
	int id;
	struct tag_CNode *prev;
	struct tag_CNode *next;
} CNode;

// head is a special 'empty' node which's id is meaningless

int isempty(const CNode *head);
CNode *get_node(const CNode *head, int id);
void append2list(CNode **head, const CNode *pn);
void del_node(CNode **head, int id);
void clear(CNode **head);
void ins_node(CNode **head, const CNode *pn, int id);
void reverse(CNode *head);
void print_list(const CNode *head);
CNode *merge_unique(const CNode *head1, const CNode *head2);
void unique(CNode **head);


# endif // LAB2_H
