#include <stdio.h>
#include <stdlib.h>
#include <math.h>

int lim = 1024,size_mem = 1024, num = 9;
int mem_mas[] = {4,8,16,32,64, 128, 256, 512, 1024};
bool* mem;


struct headline
{
	bool state;
	int free_sz;
	int all_len;
	int addr;
	headline *next;
	headline *prev;
};

struct headline *main_mem;
struct headline* queue[9];

	void InitHead ( headline *head )
	{
		head->all_len = size_mem;
		head->free_sz = size_mem;
		head->state = false;
		head->addr = 0;
		head->next = NULL;
		head->prev = NULL;
	}

	void InitQueue ( )
	{
		int i, rest_mem = size_mem;
		for ( i = 0; i < num; i++ )
			queue[i] = NULL;
		headline *beg = NULL, *node;
		/*
        for ( i = 0; i < size_mem/lim; i++ )
		{
			node = (headline *)malloc(sizeof(headline));
			node->addr = i*lim;
			node->all_len = lim;
			node->free_sz = node->all_len;
			node->state = false;
			node->next = NULL;
			node->prev = beg;
			if ( beg == NULL )
				queue[num-1] = node;
			else beg->next = node;
			beg = node;
			node = NULL;
		}
		*/
		node = (headline *)malloc(sizeof(headline));
			node->addr = 0;
			node->all_len = size_mem;
			node->free_sz = node->all_len;
			node->state = false;
			node->next = NULL;
			node->prev = NULL;
			queue[num-1] = node;
		return;
	}

	void CopyData(headline *el_to, headline el_from )
	{
		el_to->addr = el_from.addr;
		el_to->free_sz = el_from.free_sz;
		el_to->all_len = el_from.all_len;
		el_to->state = el_from.state;
		return;
	}

	void PrintQu()
	{
		int i = 0;
		headline *ptr = NULL;
		
		for ( int i = 0; i<num; i++ )
		{
			ptr = queue[i];
			printf("\n\t\t mem_size: %d\n", mem_mas[i]);
			while ( ptr != NULL )
			{
				printf("address %d full_size %d free_size %d state %d\n", ptr->addr, ptr->all_len, ptr->free_sz,ptr->state);
				ptr = ptr->next;
			}
		}
		
		

		return;
	}

	headline* add_el ( headline buf, int pos )
	{
		headline *ptr = queue[pos], *ins_el;
		ins_el = (headline *)malloc(sizeof(headline));
		CopyData(ins_el,buf);
		if ( pos < 0 )
			return NULL;
		
		if ( queue[pos] == NULL )
		{
			ins_el->next = NULL;
			ins_el->prev = NULL;
			queue[pos] = ins_el;
			return ins_el;
		}

		while ( ptr != NULL && ptr->addr < ins_el->addr )
		{
			ptr = ptr->next;
		}
		
        if ( ptr == NULL )
		{
			ptr = queue[pos];
			while ( ptr->next != NULL )
				ptr = ptr->next;
			ptr->next = ins_el;
			ins_el->prev = ptr;
			ins_el->next = NULL;
			return ins_el;
		}
		
		
		ins_el->next = ptr;
		ins_el->prev = ptr->prev;
		if ( ptr->prev != NULL )
			ptr->prev->next = ins_el;
		else
			queue[pos] = ins_el;
		ptr->prev = ins_el;
		

		/*
		ins_el->next = ptr->next;
		ins_el->prev = ptr;
		if ( ptr->next != NULL )
			ptr->next->prev = ins_el;
		ptr->next = ins_el;
		*/
		return ins_el;
	}

	int alloc_fun ( size_t size, int pos )
	{
		int full_sz = mem_mas[pos];
		
		headline *buf, *ptr, *free_el;
		buf = (headline *)malloc(sizeof(headline));
		free_el = (headline *)malloc(sizeof(headline));
		buf->state = false;
		
		while ( pos < num )
		{
			ptr = queue[pos];
			while ( ptr != NULL && ptr->state == true )
				ptr = ptr->next;
			
			if ( ptr == NULL )
				pos++;
			else 
				{
					full_sz = mem_mas[pos];
					break;
				}
		}
		
		while ( ptr!= NULL && full_sz > 4 && size <= full_sz/2 )
		{
			free_el->all_len = full_sz/2;
			free_el->free_sz = free_el->all_len;
			free_el->state = false;
			free_el->addr = ptr->addr + free_el->all_len;
			add_el(*free_el, pos-1);
			ptr->all_len = full_sz/2;
			ptr->free_sz = ptr->all_len - size;
			ptr->state = true;
			if ( ptr->prev == NULL )
				queue[pos] = ptr->next;
			else
				ptr->prev->next = ptr->next;

			if ( ptr->next != NULL )
				ptr->next->prev = ptr->prev;
			
			ptr = add_el(*ptr, pos-1);
			pos--;
			full_sz /=2;
		}
		if ( ptr == NULL )
			return -1;
		ptr->free_sz = ptr->all_len - size;
		ptr->state = true;
		return ptr->addr;
	}

	int mem_alloc( size_t size )
	{
		int i = 0, full_sz = 0;
		while ( i < num && size > mem_mas[i] )
			i++;
		if ( i >= num )
			return -1;
		full_sz = mem_mas[i];

		headline *node = queue[i];
		while ( node == NULL )
		{
			full_sz*=2;
			node = queue[++i];
		}
		if ( full_sz > lim )
			return -1;
		
		return alloc_fun(size,i);
	}

	int mem_free( int _addr )
	{
		int i = 0, pos = 0;
		headline *ptr = NULL, *ptr2 = NULL;
		while ( i < num )
		{
			ptr = queue[i];
			while ( ptr != NULL )
			{
				if ( ptr->addr == _addr )
				{
					pos = i;
					i = num;	
					break;
				}else
				ptr = ptr->next;
			}
			i++;
		}

		if ( ptr == NULL )
			return -1;

		ptr->state = false;
		ptr->free_sz = ptr->all_len;
		i = pos;
		while ( i < num-1 )
		{
			if ( ptr->next != NULL && ptr->next->state == false && (ptr->next->addr - ptr->addr ) == mem_mas[i])
				ptr2 = ptr->next;
			
			else
				if ( ptr->prev != NULL && ptr->prev->state == false && (ptr->addr - ptr->prev->addr ) == mem_mas[i])
				{
					ptr2 = ptr;
					ptr = ptr->prev;
				}else
					break;

				ptr->all_len *= 2;
				ptr->free_sz = ptr->all_len;
				ptr->next = ptr2->next;
				if ( ptr2->next != NULL )
					ptr2->next->prev = ptr;
				ptr2->next = NULL;
				ptr2->prev = NULL;
				ptr2 = NULL;

				if ( ptr->prev == NULL )
					queue[i] = ptr->next;
				else
					ptr->prev->next = ptr->next;
				if ( ptr->next != NULL )
					ptr->next->prev = ptr->prev;
				ptr->next = NULL;
				ptr->prev = NULL;
				ptr = add_el(*ptr, i+1);
				i++;
		}

		return ptr->addr;
	}

int main ()
{
	InitQueue();
	PrintQu();
	printf("\n");
	mem_alloc(64);
	mem_alloc(64);
	mem_alloc(63);
	PrintQu();
	mem_free(0);
	mem_free(64);
	//mem_free(128);
	PrintQu();
	return 0;
}