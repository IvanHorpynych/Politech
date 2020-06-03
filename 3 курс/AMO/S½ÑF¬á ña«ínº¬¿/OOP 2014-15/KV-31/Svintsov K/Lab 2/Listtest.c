/***********************************************************************
*file: listtest.c
*synopsis: Main project`s file made for testing functions from "list.h"
*related files: none
*author: Kyrylo Svintsov KV-31
*written: 4/11/2014
*last modified: 5/11/2014
************************************************************************/


#include "List.h"

CNodePTR head1PTR,head2PTR,startPTR = NULL; // pointers of lists
int i;
#define l 2

int main(){
	for (i = 0; i < 4; i++)
		insert(&startPTR, i);
	
	if (isEmpty(startPTR) == 0){
		printf("Current list:");
		print_list(startPTR);
		printf("\nList after adding new node to the end:");
		append2list(&startPTR, get_node(startPTR, 1));
		print_list(startPTR);
		printf("\nList after deleting the node:");
		del_node(&startPTR, l);
		print_list(startPTR);
		printf("\nList after adding new node before the chosen id:");
		ins_node(&startPTR, get_node(startPTR, 1), 3);
		print_list(startPTR);
		printf("\nList after reversing");
		reverse(&startPTR);
		print_list(startPTR);
		clear(&startPTR);
	}
	else
		printf("\nList is empty");
	printf("\n");
	for (i = 2; i < 5; i++)
		insert(&head1PTR,i);
	for (i = 4; i < 7; i++)
		insert(&head2PTR, i);
	printf("New list from head1 and head2:");
	print_list(head_merge_unique(head1PTR, head2PTR));
	printf("\n");
	if (isEmpty(startPTR)){
		insert(&startPTR, 2);
		insert(&startPTR, 1);
		insert(&startPTR, 1);
		insert(&startPTR, 2);
		printf("\nCurrent list:");
		print_list(startPTR);
		unique(&startPTR);
		printf("\nList after deleting same nodes:");
		print_list(startPTR);
	}
	else
		printf("\nList is empty");
	getchar();
	return 0;
}