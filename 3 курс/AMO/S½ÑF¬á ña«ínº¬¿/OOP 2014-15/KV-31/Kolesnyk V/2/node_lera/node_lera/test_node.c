#include "H_node.h"
#include <stdlib.h>
#include <stdio.h>
#include <conio.h>




int main()  {
	CNode *head1 = NULL, *head2 = NULL, *head = NULL;// pointers of lists
	CNode *elem = (CNode*)malloc(sizeof(CNode));
	int i;
	int del = 5;

	if (isempty(head1) == 1){
		printf("Current list is empty");
	}
	else {
		printf("Current list:  ");
		print_list(head1);
	}

	printf("\nList after adding new node to the end:  ");
	for (i = 0; i <6; i++) {
		CNode *p = (CNode *)malloc(sizeof(CNode));
		p->id = i;
		append2list(&head1, p);
	}
	print_list(head1);

	
	printf("\nLets delete element with id # %d", del);
	printf("\nList after del_node:  ");
	del_node(&head1, del);
	print_list(head1);

	i = 8;
	int before = 2;
	elem->id = i;
	printf("\nLets insert element with id # %d", i);
	printf(" before elem with id # %d", before);
	printf("\nList after insert:  ");
	ins_node(&head1, elem, before);
	print_list(head1);
	printf("\nList after reverse:  ");
	reverse(head1);
	print_list(head1);
	for (i = 0; i <2; i++) {
		CNode *p = (CNode *)malloc(sizeof(CNode));
		p->id = i;
		append2list(&head2, p);
	}
	head = merge_unique(head1, head2);
	printf("\nList 1 = ");
	print_list(head1);
	printf("List 2 = ");
	print_list(head2);
	printf("List unique  = ");
	print_list(head);
	printf("\n");
	i = 7;
	
	elem->id = i;
	ins_node(&head, elem, 2);
	ins_node(&head, elem, 2);
	ins_node(&head, elem, 7);
	printf("\nAfter insert in list \n");
	print_list(head);
	printf("\nAfter unique in list\n");
	unique(&head); //поскольку 2 семерки,вставилось два раза
	print_list(head);

	printf("\nLets clear current list\n");
	clear(&head);
	if (isempty(head) == 1){
		printf("Current list is empty");
	}
	else {
		printf("Current list:  ");
		print_list(head);
	}
	getchar();

}