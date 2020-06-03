#include "DoublyLinkedList.h"

DoublyLinkedList::DoublyLinkedList(void) // конструктор 
{}

int DoublyLinkedList::size() // подсчет количества элементов
{
	ListNode *help = head;
	int count;
	for (count = 0; help; count++)
	{
		help = help->next;
	}
	return count;
}
bool DoublyLinkedList::empty() //проверка на пустоту
{
	if (!head)
		return true;
	else 
		return false;
}
void DoublyLinkedList::clear() //очистка списка
{
	ListNode *help = new ListNode;
	while (head->next)
	{
		help = head;
		head = head->next;
		delete(help);
	}
	head = NULL;
}

void DoublyLinkedList::push_back(const ListNode &list)//добавление в конец списка
{
	ListNode *p, *help = new ListNode;
	help->data = list.data;
	if (!head)
	{
		head = help;
	}
	else
	{
		p = head;
		while (p->next)
			p = p->next;
		p->next = help;
		help->prev = p;
	}
}

void DoublyLinkedList::push_front(const ListNode &list) //добавление в начало 
{
	ListNode *help = new ListNode;
	help->data = list.data;
	if (!head)
	{
		head = help;
	}
	else
	{
		head->prev = help;
		help->next = head;
		head = help;
	}
}

void DoublyLinkedList::pop_front()  //удаление первого элемента
{
	if (empty())
	{
		printf("ERROR\n List is empty\n");
		return;
	}
	else 
	if (DoublyLinkedList::size()==1)
	{
		head = NULL;
	}
	else
	{
		ListNode *help = head;
		head = head->next;
		delete(help);
	}
}
void DoublyLinkedList::pop_back() //удаление последнего элемента
{
	if (empty())
	{
		printf("ERROR\n List is empty\n");
		return;
	}
	else 
	if (DoublyLinkedList::size() == 1)
	{
		head = NULL;
	}
	else
	{
		ListNode *help, *beg = head;
		while (beg->next)
		{
			beg = beg->next;
		}
		help = beg->prev;
		help->next = NULL;
		delete(beg);
	}
	return;
}

void DoublyLinkedList::insert_ord(const ListNode &list)  //вставка символа, что бы не нарушилась сортировка
{
	ListNode *beg = head, *help = new ListNode;
	help->data = list.data;
	int size = DoublyLinkedList::size(), i;
	for (i = 1; i < size; i++)
	{
		if (strcmp(beg->data, list.data) >= 0)
		{
			break;
		}
		else
		{
			beg = beg->next;
		}
	}
	if (i != size)
	{
		printf("zd = %d\n", i);
		help->data = list.data;
		beg->next->prev = help;
		help->next = beg->next;
		beg->next = help;
		help->prev = beg;
	}
	else
	{
		beg->next = help;
		help->prev = beg;
	}
}

void DoublyLinkedList::sort() // сортировка по неубыванию 
{
	ListNode *beg = head, *p;
	char *str;
	int size = DoublyLinkedList::size(), boo = 1, i = 1;
	while (boo) //сортировка прямым обменом с флажком
	{
		boo = 0;
		beg = head;
		for (i = 1; i <= size - 1; i++)
		{
			if (strcmp(beg->data, beg->next->data) > 0)
			{
				str = beg->data;
				beg->data = beg->next->data;
				beg->next->data = str;
				boo = 1;
			}
			beg = beg->next;
		}
		size--;
	}
}

bool DoublyLinkedList::insert_after(char *dat, const ListNode &list) //вставка после заданого символа
{
	ListNode *help = new ListNode, *beg = head;
	help->data = list.data;
	int size = DoublyLinkedList::size(), i;
	for (i = 1; i < size; i++)
	{
		if (strcmp(beg->data, dat) == 0)
		{
			break;
		}
		else
		{
			beg = beg->next;
		}
	}
	if (strcmp(beg->data, dat) != 0)
		return false;
	if (i != size)
	{
		help->next = beg->next;
		help->prev = beg;
		beg->next = help;
		beg = help->next;
		beg->prev = help;
	}
	else
	{
		beg->next = help;
		help->prev = beg;
	}
	return true;
}

void DoublyLinkedList::operator=(const DoublyLinkedList &list) //перекопирование с одного списка 
{
	head = new ListNode;
	head->data = list.head->data;
	ListNode *help = list.head->next;
	ListNode *beg = head;
	ListNode *p;
	while (true)
	{
		p = new ListNode;
		p->data = help->data;
		beg->next = p;
		p->prev = beg;
		beg = p;
		help = help->next;
		if (!help)
		{
			break;
		}
	}
}

void DoublyLinkedList::merge(DoublyLinkedList &list) // очистка list, перенос в основной список, сортировка
{
	ListNode *beg = list.head;
	while (beg)
	{
		DoublyLinkedList::push_back(*beg);
		beg = beg->next;
	}
	DoublyLinkedList::sort();
	list.clear();
}

void DoublyLinkedList::erase(char *dat) // удаляет элемент, который равный dat
{
	ListNode *beg = head, *help = new ListNode;
	while (beg)
	{
		help = new ListNode;
		if (beg->data == dat)
		{
			if (beg == head)
			{
				help = head;
				head = head->next;
			}
			else
			{
				if (!beg->next)
				{
					help = beg;
					beg = beg->prev;
					beg->next = NULL;
				}
				else
				{
					help = beg->prev;
					help->next = beg->next;
					help = beg->next;
					help->prev = beg->prev;
					help = beg;
				}
			}
		}
		beg = beg->next;
		delete(help);
	}
}

void DoublyLinkedList::unique()//удаляет смежные узлы в отсортированном списке
{
	if (!head) 
		return;
	DoublyLinkedList::sort();
	ListNode *beg = head, *help = beg->next;
	while (help)
	{
		if (strcmp(beg->data, help->data) == 0)
		{
			if (help->next == NULL)
			{
				beg->prev->next = help;
				help->prev = beg->prev;
				delete (beg);
				return;
			}
			else
			{
				help->next->prev = beg;
				beg->next = help->next;
				delete (help);
				help = beg->next;
				continue;
			}
		}
		beg = beg->next;
		help = help->next;
	}
}

void DoublyLinkedList::assign(DoublyLinkedList &list, int first, int last) //удаляет с list элементы с first по last позиции и вставляет их в конец главного списка
{
	ListNode *help = list.head, *beg;
	int i = 1;
	if ((last > DoublyLinkedList::size()) || (list.empty() == true))
	{
		printf("ERROR");
		return;
	}
	if (first == 1)
	{
		beg = list.head;
		while (i < last)
		{
			beg = beg->next;
			i++;
		}
		beg = beg->next;
		list.head = beg;
		list.head->prev = NULL;
	}
	else
	{
		while (i < first)
		{
			help = help->next;
			i++;
		}
		beg = help;
		for (i = first; i <= last; i++)
		{
			DoublyLinkedList::push_back(*help);
			help = help->next;
		}
	}
}

void DoublyLinkedList::splice(int where, const DoublyLinkedList &list) //вставляет из list начиная с where в главный список 
{
	ListNode *p = head, *help = list.head;
	for (int i = 0; i < where-1; i++)
	{
		p = p->next;
	}
	while (help->next)
	{
		help = help->next;
	}
	while (help)
	{
		DoublyLinkedList::insert_after(p->data, *help);
		help = help->prev;
	}
}

void DoublyLinkedList::splice(int where, const DoublyLinkedList &list, int first, int last)  //вставляет из list с first по last начиная с where в главный список
{
	ListNode *p = head, *help = list.head;
	int i = 0;
	for (i = 0; i < where; i++)
	{
		p = p->next;
	}
	i = last;
	while (i>first)
	{
		help = help->next;
		i--;
	}
	while (i <= last)
	{
		DoublyLinkedList::insert_after(p->data, *help);
		help = help->prev;
		i++;
	}
}

void DoublyLinkedList::print() //печать от головы к хвосту
{
	if (DoublyLinkedList::empty())
	{
		printf("List is empty");
		return;
	}
	ListNode *help = head;
	while (help)
	{
		printf("%s ", help->data);
		help = help->next;
	}
}

void DoublyLinkedList::print_bkw()  //печать от хвоста к голове списка
{
	if (DoublyLinkedList::empty())
	{
		printf("Empty!!");
		return;
	}
	ListNode *p = head;
	while (p->next)
	{
		p = p->next;
	}
	while (p->prev)
	{
		printf("%s ", p->data);
		p = p->prev;
	}
}

DoublyLinkedList::~DoublyLinkedList(void){}