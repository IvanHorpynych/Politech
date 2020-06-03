/************************************************************************
*file: list.h
*synopsis: declarations for list functions, types
*author: Kyrylo Svintsov KV-31
*written: 4/11/2014
*last modified: 5/11/2014
************************************************************************/


#ifndef LIST_H
#define	LIST_H

#include <stdlib.h>
#include <stdio.h>

typedef struct tag_CNode
{
	//attributes
	int id;//node's index
	struct tag_CNode *prev;//Pointer to the previous node in the node's list
	struct tag_CNode *next;//Pointer to the next node in the node's list
}CNode;
typedef CNode *CNodePTR; //for comfort use of pointers of type CNode.

void insert(CNodePTR *, int);
int isEmpty(CNodePTR);
void print_list(const CNodePTR);
CNodePTR get_node(const CNodePTR, int);
void append2list(CNodePTR *, const CNodePTR );
void del_node(CNodePTR *, int id);
void clear(CNodePTR *);
void ins_node(CNodePTR *, const CNodePTR, int);
void reverse(CNodePTR );
CNodePTR head_merge_unique(const CNodePTR, const CNodePTR);
void unique(CNodePTR *);

#endif