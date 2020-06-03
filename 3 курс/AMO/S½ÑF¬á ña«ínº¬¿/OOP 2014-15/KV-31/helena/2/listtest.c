#include <stdio.h>
#include "list.h"

void merge_test(){
    CNode *head1 = (CNode*)malloc(sizeof(CNode));
    CNode *head2 = (CNode*)malloc(sizeof(CNode));
    CNode *temp  = (CNode*)malloc(sizeof(CNode));
    int i;

    head1->next = head1;
    head1->prev = head1;
    head2->prev = head2;
    head2->next = head2;
    for(i = 0;i<12;i++){
        temp->id = i*4;
        append2list(head1,temp);
    }
    printf("HEAD1:\n");
    print_list(head1);

    for(i=0;i<12;i++){
        temp->id = i*i;
        append2list(head2,temp);
    }
    printf("\nHEAD2:\n");
    print_list(head2);
    free(temp);
    temp = merge_unique(head1,head2);
    printf("New list:\n");
    print_list(temp);
    
}

void clear_test(){
    CNode *head = (CNode*)malloc(sizeof(CNode));
    CNode *temp = (CNode*)malloc(sizeof(CNode));
    int i = 0;
    head->next = head;
    head->prev = head;
    for(;i<12;i++){
        temp->id = i*4;
        append2list(head,temp);
    }
    printf("\nSTART STATE\n");
    print_list(head);
    clear(head);
    printf("AFTER CLEANING:\n");
    print_list(head);
}

void reverse_test(){
    CNode *head = (CNode*)malloc(sizeof(CNode));
    CNode *temp = (CNode*)malloc(sizeof(CNode));
    int i = 0;
    head->next = head;
    head->prev = head;
    for(;i<12;i++){
        temp->id = i*4;
        append2list(head,temp);
    }
    printf("\nSTART STATE\n");
    print_list(head);
    reverse(head);
    printf("AFTER REVERSE:\n");
    print_list(head);
	printf("\n");
}

void unique_test(){
    CNode *head = (CNode*)malloc(sizeof(CNode));
    CNode *temp = (CNode*)malloc(sizeof(CNode));
    int v[12] = {1,2,3,3,2,4,5,3,3,3,22,12};
    int i = 0;
    head->next = head;
    head->prev = head;

    for(;i<12;i++){
        temp->id = v[i];
        append2list(head,temp);
    }

    printf("\nSTART STATE\n");
    print_list(head);
    unique(head);
    printf("\nAFTER USE OF UNIQUE:\n"); 
    print_list(head);
}

int main(){
    clear_test();
    reverse_test();
    merge_test();
    unique_test();
	//system("PAUSE");
    return 0;
}