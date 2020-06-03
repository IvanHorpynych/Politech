/************************************************************************
*file: mylist.c
*purpose: realization of all functions
*author: Khlevnoy Y.A.
*written: 25/11/2014
*last modified: 29/11/2014
*************************************************************************/

#include "mylist.h"


void addNodePos(struct DoubleList** headRef, int value, int position)
{
    struct DoubleList *node;

    if ((node = malloc (sizeof(struct DoubleList))) != NULL) {
        node->id = value;
        if ((*headRef) == NULL) {
            // points to itself
            node->next = node;
            node->prev = node;
            (*headRef) = node;
        } else {
            struct DoubleList *current = (*headRef);

            if (position == 0){
                // relink nodes
                current->prev->next = node;
                node->prev          = current->prev;
                node->next          = current;
                current->prev       = node;
                (*headRef) = (*headRef)->prev;
                return;
            }

            do {
                current = current->next;
                position--;
            } while(current!=(*headRef) && position>0);
            // relink nodes
            current->prev->next = node;
            node->prev          = current->prev;
            node->next          = current;
            current->prev       = node;
        }
    }
}


int deleteNodePos(struct DoubleList** headRef, int position)
{
    int i;

    if ((*headRef) == NULL) {
        //printf("\nList is empty.\n");
        return 0;
    }

    if ((*headRef) == (*headRef)->next) {
        free(*headRef);
        (*headRef) = NULL;
    } else {
        struct DoubleList *current = (*headRef);
        for (i = position; i > 1; i--)
            current = current->next;
        i = current->id; // for printf()
        if (current == (*headRef))
            (*headRef) = current->next;
        current->prev->next = current->next;
        current->next->prev = current->prev;
        free(current);
    }
    return 1;
}


void printList(struct DoubleList** headRef)
{
    if ((*headRef) != NULL) {
        printf("\n");
        struct DoubleList *current = (*headRef);
        do {
            printf("%d", current->id);
            current = current->next;
            if(current != (*headRef))
                printf(" <=> ");
        } while(current != (*headRef));
        printf("\n\n");
    } else
        if ((*headRef) == NULL)
            printf("\nList is empty.\n");
}


int isEmpty(struct DoubleList** headRef)
{
    return (*headRef) == NULL;
}


struct DoubleList* getNode(struct DoubleList** headRef, int value)
{
    if ((*headRef) != NULL) {
        struct DoubleList *current = (*headRef);
        do {
            if (current->id == value)
                return current;
            current = current->next;
        } while(current != (*headRef));
    }
    return NULL;
}


void appendList(struct DoubleList** headRef, int value)
{
    int pos = 1;

    if ((*headRef) != NULL)
    {
        struct DoubleList *current = (*headRef);
        do {
            current = current->next;
            pos++;
        } while(current != (*headRef));
    }

    addNodePos(headRef, value, pos);
}


void deleteNode(struct DoubleList** headRef, int value)
{
    int pos = 1;
    struct DoubleList* item = NULL;

    if ((*headRef) != NULL) {
        if (getNode(headRef, value) != NULL) {
            struct DoubleList *current = (*headRef);
            // Used type cast below to avoid warning:
            // "comparison of distinct pointer types lacks a cast"
            while(current!=(struct DoubleList *)headRef && current!=NULL){
                item = getNode(headRef, value);
                while(current != item) {
                    current = current->next;
                    // if there is no nodes to delete
                    if(item == NULL)
                        return;
                    pos++;
                }

                deleteNodePos(headRef, pos);
                // reset variables
                current = (*headRef);
                pos = 1;
            }
        }
    }
}


void clearList(struct DoubleList** headRef)
{
    if (headRef != NULL) {
        while (deleteNodePos(headRef, 1));
    } else
        if ((*headRef) == NULL)
            printf("\nList is empty.\n");
}


void insertNode(struct DoubleList** headRef, const struct DoubleList* pn, int value)
{
    struct DoubleList *node;
    int swap = 0;

    if ((*headRef) != NULL) {
        if ((node = malloc (sizeof(struct DoubleList))) != NULL ){
            node->id = pn->id; // make a copy
            if ((*headRef) == NULL) {
                // points to itself
                node->next = node;
                node->prev = node;
                (*headRef) = node;
            } else {
                struct DoubleList *current = (*headRef);
                // fix
                if (current->next == current)
                    swap = 1;

                do { current = current->next;
                } while(current!=(*headRef) && current->next->id!=value);

                if (current->next->id == value) {
                    // relink nodes (another version)
                    current->next->prev = node;
                    node->next          = current->next;
                    current->next       = node;
                    node->prev          = current;
                }
                // fix
                if(swap)
                    (*headRef) = (*headRef)->prev;
            }
        }
        //printf("Element %d has been added.\n\n", pn->id);
    }
}


void reverseList(struct DoubleList** headRef)
{
    if ((*headRef) != NULL) {
        struct DoubleList *current = (*headRef);
        struct DoubleList *tempLink = NULL; // necessary
        do {
            tempLink = current->next;

            current->next = current->prev;
            current->prev = tempLink;
            current       = current->prev; // because NEXT became PREV, and vise versa
        } while(current != (*headRef));
    }
    *headRef = (*headRef)->next;
}


struct DoubleList* mergeList(struct DoubleList** headRef, struct DoubleList** headRef2, struct DoubleList** headRef3)
{
    struct DoubleList *current = (*headRef2);

    if ((*headRef2) != NULL) {
        current = (*headRef2);
        do {
            if (getNode(headRef, current->id) == NULL)
                appendList(headRef, current->id);
            current = current->next;
        } while(current != (*headRef2));
    }

    if ((*headRef3) != NULL) {
        current = (*headRef3);
        do {
            if (getNode(headRef, current->id) == NULL)
                appendList(headRef, current->id);
            current = current->next;
        } while(current != (*headRef3));
    }
    return (*headRef);
}


void makeList(struct DoubleList** headRef)
{
    int value, position, choice;

    do {
        printf("1. Add element.\n");
        printf("2. Delete element.\n");
        printf("3. Print list.\n");
        printf("0. Exit.\n");
        printf("Your choice: ");
        scanf("%d", &choice);
        switch (choice) {
        case 1:
            printf("\nvalue: ");
            scanf("%d", &value);
            printf("position: ");
            scanf("%d", &position);
            addNodePos(headRef, value, position);
            break;
        case 2:
            printf("\nposition: ");
            scanf("%d", &position);
            deleteNodePos(headRef, position);
            break;
        case 3:
            printList(headRef);
            break;
        }
    } while (choice != 0);
}


void uniqueList(struct DoubleList** headRef)
{
    int pos = 1, iteration = 1;
    if ((*headRef) != NULL) {
        struct DoubleList *current  = (*headRef);
        struct DoubleList *item     = current->next;
        struct DoubleList *tempNode = item;

        while(current->next != (*headRef)){
            while(item != (*headRef)){
                pos++;
                if (item->id == current->id) {
                    tempNode = item->next;
                    deleteNodePos(headRef, pos);
                    pos--;
                    //printList(headRef);
                    item = tempNode;
                } else {
                    item = item->next;
                }
            }
            pos = ++iteration;
            current = current->next;
            item    = current->next;
        }
    }
}
