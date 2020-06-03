/************************************************************************
*file: H_node.h
*author:Kolesnyk V.S.
*written: 24/11/2014
*last modified: 24/11/2014
************************************************************************/

typedef struct tag_CNode {
	//attributes
	int id;//node's inex

	struct tag_CNode *prev;//Pointer to the previous node in the node's list
	struct tag_CNode *next;//Pointer to the next     node in the node's list

} CNode;

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
