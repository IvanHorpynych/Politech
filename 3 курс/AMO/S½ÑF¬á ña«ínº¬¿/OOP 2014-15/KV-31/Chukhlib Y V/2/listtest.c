#include "list.h"
#include <stdlib.h>
#include <stdio.h>
#include <conio.h>

int main() {
	CNode *head1 = NULL, *head2 = NULL, *head = NULL;
	int i;
	for (i = 0; i < 10; i++) {
		CNode *p = (CNode *)malloc(sizeof(CNode));
		p->id = i;
		append2list(&head1, p);
	}
	for (i = 5; i < 15; i++) {
		CNode *p = (CNode *)malloc(sizeof(CNode));
		p->id = i;
		append2list(&head2, p);
	}
	head = merge_unique(head1, head2);
	printf("Function merge_unique\nList 1 = ");
	print_list(head1);
	printf("List 2 = ");
	print_list(head2);
	printf("List unique = ");
	print_list(head);
	printf("\n");

	del_node(&head1, 0);
	printf("After delete\nList 1 = ");
	print_list(head1);
	printf("\n");

    CNode *p = (CNode *)malloc(sizeof(CNode));
	p->id = 9;
	ins_node(&head1, p, 1);
	
	printf("After insert\nList 1 = ");
	print_list(head1);

	reverse(head1);
	printf("After reversing\nList 1 = ");
	print_list(head1);
	printf("\n");

	unique(&head1);
	unique(&head2);
	unique(&head);
	printf("After unique\nList 1 = ");
	print_list(head1);
	printf("\n");

	clear(&head1);
	printf("Last\nList 1 = ");
	print_list(head1);
	printf("\n");
	_getch();
	return 0;
}