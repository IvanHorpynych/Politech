#include "list.h"
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
	else print_list(head1);


	printf("\nList after adding new node to the end:");
	for (i = 0; i <6; i++) {
		CNode *p = (CNode *)malloc(sizeof(CNode));
		p->id = i;
		append2list(&head1, p);
	}
	print_list(head1);


	printf("\n del_node elem : %d", del);
	printf("\nList after del_node:");
	del_node(&head1, del);
	print_list(head1);
	
	i = 8;
	elem->id = i;
	ins_node(&head1, elem, 2);
	print_list(head1);
	printf("\nList after reverse:");
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
	printf("\n After ins_node in list \n");
	print_list(head);
	printf("\n After unique in list\n");
	unique(&head);
	print_list(head);
	getchar();

}