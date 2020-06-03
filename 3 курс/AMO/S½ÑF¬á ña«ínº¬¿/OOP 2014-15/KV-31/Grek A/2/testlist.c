/************************************************************************
*file: testlist.c
*purpose: testing of list functions
*author: Alex Grek
*written: 24/11/2014
*last modified: 24/11/2014
*************************************************************************/

#include "list.h"

void main(){
	CNode *head = NULL, *merged, *head1, *head2;
	head = generateList(11);
	printf("list:\n");
	print_list(head);
	printf("\nReversed:\n");
	reverse(head);
	print_list(head);
	reverse(head);
	
	printf("\nAppend:\n");	
	append2list (&head,get_node(head,5));
	print_list(head);

	printf("\nInsert:\n");
	ins_node (&head, get_node(head,5), 1);
	print_list(head);
	
	printf("\nDelete:\n");
	del_node(&head, 4);
	print_list(head);
	
	printf("\nClear:\n");
	clear(&head);
	print_list(head);

	printf("\n\nHead merge unique");
	printf("\nHead1: ");
	head1 = generateList(11);
	print_list(head1);
	printf("\nHead2: ");
	head2 = generateList(6);
	print_list(head2);
	printf("\nMerged: ");
	merged = head_merge_unique(head1, head2);
	print_list(merged);

	clear(&head2);

	printf("\nUnique\nHead1: ");
    head1->id = 9;
	head2 = get_node(head1,1);
	head2->id = 0;
	print_list(head1);
	unique(&head1);
	printf("\nChanged: ");
	print_list(head1);
	
	clear(&head1);
	clear(&merged);
	getchar();
}