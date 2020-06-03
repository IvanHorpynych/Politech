/******************************************************************************************
	name:					listtest.c
	description:			this file call functions, that described in file "list.c" to
							test performance of this functions
	author:					Dima
	date of creation:		07.09.2014
	written:				07.09.2014
	date of last modified:	10.09.2014
******************************************************************************************/

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
	printf("Firstly\nList 1 = ");
	print_list(head1);
	printf("List 2 = ");
	print_list(head2);
	printf("List unique = ");
	print_list(head);
	printf("\n");

	del_node(&head1, 9);
	del_node(&head2, 5);
	del_node(&head, 7);
	printf("After delete\nList 1 = ");
	print_list(head1);
	printf("List 2 = ");
	print_list(head2);
	printf("List unique = ");
	print_list(head);
	printf("\n");

	{
		CNode *p = (CNode *)malloc(sizeof(CNode));
		p->id = 9;
		ins_node(&head1, p, 0);
		p->id = 6;
		ins_node(&head2, p, 6);
		p->id = 9;
		ins_node(&head, p, 7);
	}

	printf("After insert\nList 1 = ");
	print_list(head1);
	printf("List 2 = ");
	print_list(head2);
	printf("List unique = ");
	print_list(head);
	printf("\n");

	reverse(head1);
	reverse(head2);
	reverse(head);
	printf("After reversing\nList 1 = ");
	print_list(head1);
	printf("List 2 = ");
	print_list(head2);
	printf("List unique = ");
	print_list(head);
	printf("\n");

	unique(&head1);
	unique(&head2);
	unique(&head);
	printf("After unique\nList 1 = ");
	print_list(head1);
	printf("List 2 = ");
	print_list(head2);
	printf("List unique = ");
	print_list(head);
	printf("\n");

	clear(&head1);
	clear(&head2);
	clear(&head);
	printf("Last\nList 1 = ");
	print_list(head1);
	printf("List 2 = ");
	print_list(head2);
	printf("List unique = ");
	print_list(head);
	printf("\n");
	_getch();
	return 0;
}