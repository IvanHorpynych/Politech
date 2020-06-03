#include "Laba2.h"

void main(){
	int len = 9, id = 9;
	CNode *head1 = NULL, *head2 = NULL;

	head1 = list_create(len);

	if (isempty(head1) == 1){
		print_lst(head1);


		CNode *pn = get_node(head1, id);
		if (pn != NULL){

			append2list(&head1, pn);
			printf("\n\n> Append '%d' to list: ", pn->data);
			print_lst(head1);

			id = 5;
			del_node(&head1, id);
			printf("\n\n> Delete '%d' from list: ", id);
			print_lst(head1);

			

			id = 8;
			ins_node(&head1, pn, id);
			printf("\n\n> Insert '%d' before '%d': ", pn->data, id);
			print_lst(head1);

			reverse(head1);
			head1 = head1->next;
			printf("\n>Changing the order of elements in list to opposite: ");
			print_lst(head1);

			head2 = list_create(len);
		
			CNode *head3 = merge_unique(head1, head2);
			if (head3 != NULL) {
				printf("\n\n1: ");
				print_lst(head1);
				printf("\n2: ");
				print_lst(head2);
				printf("\n>List of unique elements: ");
				print_lst(head3);
			}
			unique(&head3);
			printf("\n>no duplicate elements: ");
			print_lst(head3);


			clear(&head1);
			clear(&head2);
		}
		else  printf("\nThere is no element with number %d!", id);
	}
	else printf("List is empty");

	getchar();
}