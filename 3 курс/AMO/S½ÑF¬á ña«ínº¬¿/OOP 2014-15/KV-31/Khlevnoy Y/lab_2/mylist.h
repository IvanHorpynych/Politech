/************************************************************************
*file: mylist.h
*purpose: declarations for functions and struct node type
*author: Khlevnoy Y.A.
*written: 25/11/2014
*last modified: 29/11/2014
*************************************************************************/

#ifndef LIST_H
#define LIST_H
    #include <stdio.h>
    #include <stdlib.h>

    struct DoubleList
    {
        int id;
        struct DoubleList *next;
        struct DoubleList *prev;
    };

    /* prototypes */
    int  isEmpty      (struct DoubleList** headRef);
    void makeList     (struct DoubleList** headRef);
    void clearList    (struct DoubleList** headRef);
    void reverseList  (struct DoubleList** headRef);
    void printList    (struct DoubleList** headRef);
    void uniqueList   (struct DoubleList** headRef);
    void appendList   (struct DoubleList** headRef, int value);
    void deleteNode   (struct DoubleList** headRef, int value);
    int  deleteNodePos(struct DoubleList** headRef, int position);
    void addNodePos   (struct DoubleList** headRef, int value, int position);
    void insertNode   (struct DoubleList** headRef, const struct DoubleList* pNode, int value);

    struct DoubleList* getNode  (struct DoubleList** headRef, int value);
    struct DoubleList* mergeList(struct DoubleList** headRef, struct DoubleList** headRef2, struct DoubleList** headRef3);

#endif /* LIST_H */





