#include "Header.h" 

void main()
{
	int size = 10;
	node *list=NULL;
	list = make(size);
	printf("Creating list with %i elements: \n\n", size);
	printlist(list);
	printf("\n------------------\n");
	
	printf("Isempty\n\n");
	isempty(list);
	
	printf("\n------------------\n");
    int getId = 10;
	node *get = get_node(list, getId);
	if (get == NULL)
	printf("Not fined");
	else
	printf("Getting node with el \"%i\": %i\n", getId, get->el);
	printf("\n------------------\n");
	
	node appendnode = { 45, NULL, NULL };
	append2list(&list, &appendnode);
	printf("Appending node with el \"%i\": \n", appendnode.el);
	printlist(list);
	printf("\n------------------\n");
	
	int deleteId = 3;
	del_node(&list, deleteId);
	printf("Deleting node with el \"%i\": ", deleteId);
	printlist(list);
	printf("\n------------------\n");
	
	node insrt = { 38, NULL, NULL };
	int beforeInsrt = 5;
	ins_node(&list, &insrt, beforeInsrt);
	printf("Inserting node with el \"%i\" before node with el \"%i\": ", insrt.el, beforeInsrt);
	printlist(list);
	printf("\n------------------\n");
	
	reverse(&list);
	printf("Reverting list: ");
	printlist(list);
	printf("\n------------------\n");
	
	node *head2 = make(10);
	printf("List for merging\n\n");
	printlist(head2);
	printf("\nMerged lists:\n\n");
	node *merged = head_merge_unique(list, head2);
	printlist(merged);
	printf("\n------------------\n");
	
	appendnode.el = 67;
	append2list(&list, &appendnode); 
	append2list(&list, &appendnode);
	append2list(&list, &appendnode);
	append2list(&list, &appendnode);
	append2list(&list, &appendnode);
	append2list(&list, &appendnode);
	appendnode.el = 95;
	append2list(&list, &appendnode);
	append2list(&list, &appendnode);
	append2list(&list, &appendnode);
	append2list(&list, &appendnode);
	append2list(&list, &appendnode);
	append2list(&list, &appendnode);
	printlist(list);
	unique(&list);
	printlist(list);
	printf("\n------------------\n");
	printf("Clear list:\n");
	clear(&list);
	isempty(list);
	printf("Clear head2:\n");
	clear(&head2);
	isempty(head2);
	printf("Clear merged:\n");
	clear(&merged);
	isempty(merged);
}

