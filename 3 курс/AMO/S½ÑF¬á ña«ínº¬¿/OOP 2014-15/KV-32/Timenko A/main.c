#include "list.h"
#include <stdio.h>

int main(int argc, const char **argv)
{
    int listSize = 10;
    LNode *listHead;
    int getId = 6;
    LNode *getNode;
    LNode appendNode = {45, NULL, NULL};
    int deleteId = 45;
    LNode insertNode = {38, NULL, NULL};
    int beforeInsertId = 0;
    LNode *secondListHead;
    LNode *mergeHead;
    LNode nearbyInsertNode = {6, NULL, NULL};
    
    listHead = createList(listSize);
    printf("Creating list with %i elements: ", listSize);
    printList(listHead);
    printf("\n\n");
    
    getNode = getListNode(listHead, getId);
    printf("Getting node with id \"%i\": %i\n\n", getId, getNode->id);
    
    appendToList(&listHead, &appendNode);
    printf("Appending node with id \"%i\": ", appendNode.id);
    printList(listHead);
    printf("\n\n");
    
    deleteFromList(&listHead, deleteId);
    printf("Deleting node with id \"%i\": ", deleteId);
    printList(listHead);
    printf("\n\n");
    
    insertToList(listHead, &insertNode, beforeInsertId);
    printf("Inserting node with id \"%i\" before node with id \"%i\": ", insertNode.id, beforeInsertId);
    printList(listHead);
    printf("\n\n");
    
    revertList(listHead);
    printf("Reverting list: ");
    printList(listHead);
    printf("\n\n");
    
    secondListHead = createList(15);
    mergeHead = mergeLists(listHead, secondListHead);
    printf("Merging lists:\n");
    printf("First list: ");
    printList(listHead);
    printf("\nSecond List: ");
    printList(secondListHead);
    printf("\nResult list: ");
    printList(mergeHead);
    printf("\n\n");
    
    insertToList(listHead, &nearbyInsertNode, 6);
    insertToList(listHead, &nearbyInsertNode, 6);
    insertToList(listHead, &nearbyInsertNode, 6);
    insertToList(listHead, &nearbyInsertNode, 6);
    nearbyInsertNode.id = 45;
    insertToList(listHead, &nearbyInsertNode, 45);
    insertToList(listHead, &nearbyInsertNode, 45);
    printf("Cleaning nearby equal nodes:\n");
    printf("List: ");
    printList(listHead);
    cleanListNearbyEqualNodes(&listHead);
    printf("\nResult: ");
    printList(listHead);
    printf("\n\n");
    
    destroyList(&listHead);
    destroyList(&secondListHead);
    destroyList(&mergeHead);
    
    return 0;
}
