/*******************************************************************************
 * File: lab2_test.c
 * Synopsis: simple tests for functions from lab2.c
 *           These functions are declared in the header file lab2.h
 ***
 **PLEASE TAKE NOTE THAT every single one of them will segfault if given NULL
 **instead of a valid pointer. Don't do that.
 ***
 * Related files: lab2.h - header with function prototypes
 *                lab2.c - functions themselves
 * Author: Мельник О.О., КВ-32
 * Written:
 * Last modified:
*******************************************************************************/

# include "lab2.h"

int main() {
	CNode *head = (CNode*) malloc(sizeof(CNode));
	CNode *head2 = (CNode*) malloc(sizeof(CNode));
	CNode *tmp;
	CNode new_one;
// 	char i;
	head->prev = head;
	head->next = head;
	head2->prev = head2;
	head2->next = head2;
	head->id = head2->id = 0;
	printf("Simple tests for labwork2 functions. Press enter to skip to the next \
message.\nprint_list was tested with every list output.\n");
	getchar();
	puts("Testing append2list adding 5 elements");
	for(new_one.id = 1; new_one.id <= 5; new_one.id++){
		append2list(&head, &new_one);
	}
	print_list(head);
	getchar();
	puts("Testing print_list with id=3 as 'head' - circularity demonstration:");
	print_list(head->next->next->next);
	getchar();
	printf("Testing isempty: ");
	if(isempty(head))
		puts("List is empty");
	else
		puts("List isn't empty");
	getchar();
	printf("Testing get_node:\nFound node with id %d\nFound node with id %d\n",
	       (get_node(head, 3))->id, (get_node(head, 5))->id);
	getchar();
	puts("Testing del_node; First add 2 more elements with id=4:");
	new_one.id = 4;
	append2list(&head, &new_one);
	append2list(&head, &new_one);
	print_list(head);
	getchar();
	puts("Then delete all of them:");
	del_node(&head, 4);
	print_list(head);
	getchar();
	puts("Testing ins_node; Insert one with id=4 where it belongs:");
	ins_node(&head, &new_one, 5);
	print_list(head);
	getchar();
	puts("Testing reverse:");
	reverse(head);
	print_list(head);
	getchar();
	puts("Testing merge_unique; Firstly create second list as a duplicate of \
first with some inserts, deletes and trailing non-uniques:");
	for(tmp = head->next; tmp != head; tmp = tmp->next)
		append2list(&head2, tmp);
	append2list(&head2, tmp->prev);
	append2list(&head2, tmp->prev);
	for(new_one.id = 256; new_one.id <= 1280; new_one.id += 256){
		ins_node(&head2, &new_one, new_one.id/255);
	}
	del_node(&head2, 3);
	print_list(head2);
	getchar();
	puts("Then reverse first list back:");
	reverse(head);
	print_list(head);
	getchar();
	puts("Then merge_unique them:");
	print_list(merge_unique(head, head2));
	getchar();
	puts("Testing clear with first list:");
	clear(&head);
	print_list(head);
	getchar();
	puts("Is it really empty? Let's make sure.");
	printf("Testing isempty: ");
	if(isempty(head))
		puts("List is empty");
	else
		puts("List isn't empty");
	getchar();
	puts("Now let's test unique with second list which has those trailing 1's:");
	unique(&head2);
	print_list(head2);
	puts("\n\nEnd of test\n");
}
