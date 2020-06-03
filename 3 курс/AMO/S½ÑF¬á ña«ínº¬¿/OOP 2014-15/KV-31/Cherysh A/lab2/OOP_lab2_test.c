/************************************************************************************
*file: OOP_lab3_test.c																*
*Synopsis: this is a test file with main function.									*
*in the "OOP_lab3.c".																*
*related files: OOP_lab3.c															*
*author: Chernysh Andrey															*
*written: 20/10/2014																	*
*last modified: 26/10/2014															*
************************************************************************************/

#include "OOP_lab2.h"
int main(){

	Cnode *head;
	makelist(&head, 0);
	printf("\tMy list : ");
	print_lst(head);

	printf("\nTask 1: \n\tIsempty : %d", isempty(head));

	printf("\nTask 2: \n\tget_node : %d", get_node(head, 3)->id);

	Cnode *addp;
	addp = malloc(sizeof(Cnode));
	addp->id = 155;
	printf("\nTask 3 : \n\taddp = %d;\n\tappend2list : ", addp->id);
	append2list(&head, addp);
	print_lst(head);

	printf("\nTask 4 : \n\tdel_node : ");
	del_node(&head, 0);
	print_lst(head);

	printf("\nTask 6 : \n\tins_node : ");
	ins_node(&head, addp, 3);
	print_lst(head);

	printf("\nTask 7 : \n\treverse : ");
	reverse(head);
	print_lst(head);

	printf("\nTask 8 : \n\tprint_lst : ");

	Cnode *head1, *head2;
	makelist(&head1, 5);
	makelist(&head2, 8);
	printf("\n\tMy list 1 : ");
	print_lst(head1);
	printf("\n\tMy list 2 : ");
	print_lst(head2);
	printf("\nTask9 : \n\thead_merge_unique : ");
	print_lst(head_merge_unique(head1, head2));

	Cnode *addp2;
	addp2 = malloc(sizeof(Cnode));
	addp2->id = 155;
	ins_node(&head, addp2, 9);
	printf("\nTask10 : \nstart list : ");
	print_lst(head);
	printf("\n\tunique : ");
	unique(&head);
	print_lst(head);

	printf("\nTask5 : \n\tClear : ");
	clear(&head);
	printf("\n\tIsempty : %d", isempty(head));

	_getch();
	return 0;
}