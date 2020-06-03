/************************************************************************
*file: main.c
*purpose: Test-drive program that illustrates the correctness of all functions.
*         Data structure: Doubly-Linked Circular List.
*author: Khlevnoy Y.A.
*written: 25/11/2014
*last modified: 29/11/2014
*************************************************************************/

#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include "mylist.h"


int main()
{
    /*** test-drive: start ***/
    struct DoubleList* list1 = NULL;
    struct DoubleList* list2 = NULL;
    struct DoubleList* list3 = NULL;
    struct DoubleList* node = NULL;


    node = malloc (sizeof(struct DoubleList));
    node->id = 9999;

    printf("\n< &list1 >\n");
    makeList(&list1);
    printf("\n< &list2 >\n");
    makeList(&list2);
    printf("\n< &list3 >\n");
    makeList(&list3);


    printf("\n\n>--------test-drive--------<\n");
    printf("isEmpty: %d\n", isEmpty(&list1));

    addNodePos(&list1, 5, 1);
    appendList(&list1, 2015);
    appendList(&list1, 5000);
    appendList(&list1, 5000);
    appendList(&list1, 5000);
      printf("appendList (1): ");
      printList(&list1);
    appendList(&list2, 2222);
      printf("appendList (2): ");
      printList(&list2);
    appendList(&list3, 3333);
      printf("appendList (3): ");
      printList(&list3);

    printf("getNode: %d or %p\n", getNode(&list1, 2015)->id, getNode(&list1, 2015) );

    reverseList(&list1);
      printf("reverseList: ");
      printList(&list1);

    deleteNode(&list1, 2015);
      printf("deleteNode: ");
      printList(&list1);

    deleteNodePos(&list1, 1);
      printf("deleteNodePos: ");
      printList(&list1);

    insertNode(&list1, node, 5);
      printf("insertNode: ");
      printList(&list1);

    mergeList(&list1, &list2, &list3);
      printf("mergeList: ");
      printList(&list1);

    uniqueList(&list1);
      printf("uniqueList: ");
      printList(&list1);

    clearList(&list1);
    clearList(&list2);
    clearList(&list3);
    printf("\nsize: %d", sizeof(struct DoubleList));
    /*** test-drive: finish ***/

    return 0;
}


