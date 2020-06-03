#ifndef LIST_H
#define LIST_H

#define false 0
#define true 1

typedef unsigned char bool;

typedef struct LNode
{
    int id;
    struct LNode *prev;
    struct LNode *next;
} LNode;

LNode *createList(int size);
LNode *getListNode(const LNode *head, int id);
void appendToList(LNode **head, const LNode *entry);
void deleteFromList(LNode **head, int id);
void insertToList(LNode *head, const LNode *entry, int id);
void revertList(LNode *head);
LNode *mergeLists(const LNode *head1, const LNode *head2);
void cleanListNearbyEqualNodes(LNode **head);
void printList(LNode *head);
void destroyList(LNode **head);

#endif
