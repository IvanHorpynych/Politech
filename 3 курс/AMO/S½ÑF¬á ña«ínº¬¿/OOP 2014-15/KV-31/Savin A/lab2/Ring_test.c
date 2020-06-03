#include "ring_func.h"
int main()
{
	int v[10] = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
	/*initializing empty list*/
	CNode *head = NULL;
	printf("Initializing empty list. If 1 it's empty, if 0 - it's not:");
	printf("%d", isempty(head));
	print_list(head);
	/*filling list with elements*/
	head = fill_ring(v, 10);
	printf("List was filled with data. If 1 it has failed, if 0 - it has been successful: ");
	printf("%d\n", isempty(head));
	print_list(head);
	/*searching for id entry in the list*/
	int id = 6;
	printf("Searching for %d in the list.", id);
	CNode *gn = get_node(head, id);
	if (gn != NULL)
		printf("\nGot element: %d.\n", gn->id);
	else printf("\nThere is no element %d in the list.\n", id);
	/*Copying pn to the main list*/
	printf("Copying get_node to the main list.\n");
	append2list(&head, gn);
	print_list(head);
	/*Deletes id from the list*/
	printf("Deleteing all CNode with id %d from the list.\n", id);
	del_node(&head, id);
	print_list(head);
	/* Clears the list */
	printf("Clearing whole list.");
	clear(&head);
	print_list(head);
	/*filling list with elements*/
	head = fill_ring(v, 10);
	printf("Filling up list with data again.\n");
	print_list(head);
	/*getting CNode for insertion*/
	gn = malloc(sizeof(CNode));
	gn->id = 0;
	id = 1;
	printf("Inserting CNode with data %d before id %d to the main list.\n", gn->id, id);
	/*inserting pn before id*/
	ins_node(&head, gn, id);
	print_list(head);
	/*reversing list*/
	printf("List will be reversed now.\n");
	reverse(&head);
	print_list(head);
	printf("List will be reversed back now.\n");
	reverse(&head);
	print_list(head);
	/*forming list for merging*/
	int v2[20] = { 1, 1, 2, 2, 3, 54, 6, 87, 23, 5, 5, 5, 4, 5, 24, 5, 77, 4, 3, 2 };
	CNode *head2 = fill_ring(v2, 20);
	printf("List for merging with main.\n");
	print_list(head2);
	printf("Merged lists:\n");
	CNode *merged = merge_unique(head, head2);
	print_list(merged);
	int v3[20] = { 1, 1, 2, 3, 3, 3, 4, 4, 5, 6, 6, 6, 6, 7, 8, 8, 9, 9, 10, 10 };
	/*Deleting duplicates of ajoining elements*/
	CNode *duplicate = fill_ring(v3, 20);
	printf("Source list.\n");
	print_list(duplicate);
	unique(&duplicate);
	printf("List with deleted duplicates\n");
	print_list(duplicate);
	printf("Freeing memory from all used lists.\n");
	clear(&duplicate);
	printf("Duplicate:");
	print_list(duplicate);
	clear(&head);
	printf("Head:");
	print_list(head);
	clear(&merged);
	printf("Merged:");
	print_list(merged);
	clear(&head2);
	printf("Head2:");
	print_list(head2);
}