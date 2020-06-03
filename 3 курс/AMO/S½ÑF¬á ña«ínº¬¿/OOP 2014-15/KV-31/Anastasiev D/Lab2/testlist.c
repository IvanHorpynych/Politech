/************************************************************************
*file: testlist.c
*purpose: list
*author: Anastasiev D.
*written: 24/10/2014
*last modified: 24/10/2014
*************************************************************************/

#include "list.h"
#define N 11

void main(){
	int  i;
	CNode *head = NULL, *test, *head1, *head2;
	head = create_list(N);	
	printf("list:\n");
	print_list(head);
	printf("\nreversed list:\n");
	reverse(head);
	print_list(head);
	reverse(head);
	

	printf("\nappend2list:\n");	
	append2list (&head,get_node(head,1));
	print_list(head);

	printf("\nins_node:\n");
	ins_node (&head, get_node(head,5), 1);
	print_list(head);


	printf("\ndel_node:\n");
	del_node(&head, 0);
	print_list(head);
	

	printf("\nclear:\n");
	clear(&head);
	print_list(head);



	printf("\nHead_merge_unique:\n");
	printf("\nHead1:\n");
	head1 = create_list(N);
	//ins_node (&head1, get_node(head1,9), 10);
	print_list(head1);
	printf("\nHead2:\n");
	head2 = create_list(N-4);
	print_list(head2);
	printf("\nTest:\n");
	test = head_merge_unique(head1, head2);
	print_list(test);

	clear(&head2);

	printf("\nunique:\n");
	printf("\nHead1:\n");
    head1->id = 9;
	head2 = get_node(head1,1);
	head2->id = 0;
	//ins_node (&head1, get_node(head1,0), 2);
	print_list(head1);
	unique(&head1);
	printf("\n");
	print_list(head1);


	clear(&head1);
	clear(&test);
	getchar();
}