#include "Header.h"

node* make(int size)
{
	node *tmp=NULL, *begl = NULL, *endl = NULL;
	for (int i = 1; i <= size; i++) 
	{
		tmp = (node*)malloc(sizeof(node));
		tmp->el = i;
		if (i==1) //создание списка 
		{
			tmp->prev = tmp; 
			tmp->next = tmp;
			begl = tmp;
		}
		else
		{
			endl->next = tmp; 
			tmp->next = begl;
			tmp->prev = endl;
			begl->prev = tmp;
		}
		endl = tmp;
	}
	return begl;
}

void printlist(const node *head)// prints list
{
	if (head != NULL){
		node *tmp = head;
		printf("Printing list:\n");
		do 
		{
			printf("%d ", tmp->el);
			tmp = tmp->next;
		} while (tmp != head);
		printf("\n");
	}
	else printf("\nList is empty\n");
}

int isempty(const node *p)
{
	if (p)
	{
		printf("OK\n");
		return 1;
	}
	else
	{
		printf("List is empty!\n");
		return 0;
	}
}

node *get_node(const node *head, int id)
{
	node *tmp = head;
	do {
		if (tmp->el == id) 
		{
			return tmp;
		}
		tmp = tmp->next;
	} while (tmp != head);
	return NULL;
}

void append2list(node **head, const node *pn)
{
	if (pn == NULL) return NULL;
	else 
	{
		node *tmp = malloc(sizeof(node));
		if (*head == NULL)
		{
			tmp->el = pn->el;
			tmp->prev = tmp;
			tmp->next = tmp;
			*head = tmp;
		}
		else 
		{
			tmp->el = pn->el;
			tmp->next = *head;
			tmp->prev = (*head)->prev;
			tmp->prev->next = tmp;
			(*head)->prev = tmp;
		}
	}
}

void del_node(node **head, int el)
{
	node *help = *head, *tmp=NULL;
	while (true)
	{
		if (help->el == el)
		{
			/*if (help == *head)
				*head = help->prev;*/
			tmp = help;
			help->next->prev = help->prev;
			help->prev->next = help->next;
			help = help->prev;
		}
		help = help->next;
		if (help == *head)
			break;
	}
}

void clear(node **head)
{
	node *p;
	if (isempty(*head) == 0)return;
	p = (*head)->next;
	while (*head != p)
	{
		p->next->prev = p->prev;
		p->prev->next = p->next;
		free(p);
		p = (*head)->next;
	}
	free(*head);
	*head = NULL;
}


void ins_node(node **head, const node *pn, int el)
{
	node *p, *tmp=*head;
	if (pn == NULL)
		return;
	do
	{
		if (tmp->el == el)
		{
			p = (node*)malloc(sizeof(node));
			p->el = pn->el;
			p->next = tmp;
			p->prev = tmp->prev;
			tmp->prev->next = p;
			tmp->prev = p;
		}
		tmp = tmp->next;
	} while (tmp != *head);
}

void reverse(node **head)
{
	if (*head != NULL)
	{
		node *tmp = *head, *help = NULL;
		do {
			help = tmp->next;
			printf("%d\n", help->el);
			tmp->next = tmp->prev;
			tmp->prev = help;
			tmp = help;
			} while (tmp != *head);
		*head = tmp->next;
	}
}

node *head_merge_unique(const node *head1, const node *head2)
{
	node *tmp1 = head1, *tmp2 = head2, *head3 = NULL;
	int boo = 0;
	while (true)
	{
		while (true) 
		{
			if (tmp2->el == tmp1->el)
			{
				boo = 1;
				break;
			}
			tmp2 = tmp2->next;
			if (tmp2 == head2)
				break;
		} 
		if (boo == 0) append2list(&head3, tmp1);
		tmp1 = tmp1->next;
		if (tmp1 == head1)
			break;
		tmp2 = head2;
		boo = 0;
	} 
	tmp1 = head1;
	tmp2 = head2;
	boo = 0;
	while (true)
	{
		while (true) 
		{
			if (tmp1->el == tmp2->el)
			{
				boo = 1;
				break;
			}
			tmp1 = tmp1->next;
			if (tmp1 == head1)
				break;
		} 
		if (boo == 0) append2list(&head3, tmp2);
		tmp2 = tmp2->next;
		if (tmp2 == head2)
			break;
		tmp1 = head1;
		boo = 0;
	} 
	return head3;
}

void unique(node **head)
{
	node *tmp=*head, *del;
	do{
		if (tmp->next->el == tmp->el)
		{
			tmp->prev->next = tmp->next;
			tmp->next->prev = tmp->prev;
			del = tmp;
			/*if (tmp == *head)
				*head = (*head)->next;*/
		}
		tmp = tmp->next;
	} while (tmp != *head);
}

