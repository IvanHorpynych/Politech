/************************************************************************
*file: ring_func.h
*author: Savin A.D.
*group: KV-31, FPM
*written: 07/10/2014
*last modified: 07/10/2014
************************************************************************/

#ifndef ring_func_H
#define ring_func_H
#include <stddef.h> // for  size_t
#include <stdio.h>
#include <stdlib.h>
#include <malloc.h>
#include <string.h>

typedef struct _tag{
	int id;
	struct _tag *prev;
	struct _tag *next;
} CNode;

CNode *fill_ring(int v[], int size);// fills list with elements
int isempty(const CNode *head);// cheks if list is empty
CNode *get_node(const CNode *head, int id);// returns pointer with set id
void append2list(CNode **head, const CNode *pn);// copies pn and adds it to the end of the list
void del_node(CNode **head, int id);// deletes all id
void clear(CNode **head);// deletes elements of the list
void ins_node(CNode **head, const CNode *pn, int id);// copies pn and inserts it before id
void reverse(CNode **head);// reverse pointers
void print_list(const CNode *head);// prints list
CNode *merge_unique(const CNode *head1, const CNode *head2);// forms list from different elements of lists
void unique(CNode **head);// deletes all adjoining duplicates of elements

#endif /* ring_func_H */