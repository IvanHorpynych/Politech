/************************************************************************
*file: ListRound.h
*author: Gnedoj D. O.
*group: KV-31, FPM
*written: 10/09/2014
*last modified: 10/09/2014
************************************************************************/

#ifndef ListRound_H
#define ListRound_H
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

CNode *create_list (int v[], int size);
int isempty(const CNode *head);
CNode *get_node (const CNode *head, int id );
void append2list (CNode **head, const CNode *pn);  
void del_node (CNode **head, int id);
void clear(CNode **head);
void ins_node (CNode **head, const CNode *pn, int id); 
void reverse(CNode **head);
void print_list(const CNode *head ); 
void print_list_reverse(const CNode *head);

CNode *merge_unique(const CNode *head1, const CNode *head2);
void unique(CNode **head);

#endif /* ListRound_H */