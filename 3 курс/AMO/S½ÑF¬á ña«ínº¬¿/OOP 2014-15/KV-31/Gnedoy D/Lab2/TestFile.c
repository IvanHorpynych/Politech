/************************************************************************
*file: TestFile.c
*author: Gnedoj D. O.
*group: KV-31, FPM
*written: 10/09/2014
*last modified: 10/09/2014
************************************************************************/
#include "ListRound.h"
int main(){
	int v[10] = {1, 2, 3, 3, 3, 6, 7, 8, 9, 10};
	CNode *head = create_list(v, 10);
	printf("Creating:\n\t");
	print_list(head);
	print_list_reverse(head);

	if (isempty(head)) printf("\n\nHead == NULL\n");
	else printf("\n\nHead == GOOD\n");
		
	CNode *GetN = get_node(head, 3);
	printf("\nGet Element: %d\n", GetN->id);
	
	append2list(&head, GetN);
	printf("\nAppending:\n\t");
	print_list(head);
	print_list_reverse(head);
	
	del_node(&head, 1);
	printf("\n\nDel node id = %d:\n\t", 1);
	print_list(head);
	print_list_reverse(head);

	clear(&head);
	printf("\n\n");
	print_list(head);
	print_list_reverse(head);
	
	ins_node(&head, GetN, 3);
	printf("\n\nInsert node before id = %d:\n\t", 3);
	print_list(head);
	print_list_reverse(head);
	
	reverse(&head);
	printf("\n\nReverse node:\n\t");
	print_list(head);
	print_list_reverse(head);
	
	int v1[10] = {5, 6, 7, 8, 9, 10, 11, 12, 13, 14};
	CNode *head2 = create_list(v1, 10);
	printf("\n\nNode1:\n\t");
	print_list(head);
	printf("\n\nNode2:\n\t");
	print_list(head2);
	CNode* NewHead = merge_unique(head, head2);
	printf("\n\nMerge unique:\n\t");
	print_list(NewHead);
	print_list_reverse(NewHead);
/*	*/	
	unique(&head);
	printf("\n\nUnique:\n\t");
	print_list(head);
	print_list_reverse(head);

	return 0;
}