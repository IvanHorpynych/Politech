#include "mem.h"


void init(mem_dispatcher *md)
{
	mem_chunk* temp = malloc(sizeof(mem_chunk));
	temp->id = 0;
	temp->size = HEAP_SIZE;
	temp->status = FREE;
	temp->next = NULL;
	md->last_id_used = 0;
	md->first = temp;
}

int allocate(mem_dispatcher *md, int size)
{
	mem_chunk* temp = md->first;
	mem_chunk* buftemp = malloc(sizeof(mem_chunk));
	mem_chunk* buff = malloc(sizeof(mem_chunk));
	int id_use = md->last_id_used + 1, minsize = temp->size;
	int flag = 0;
	while (temp)
	{
		if (temp->status == FREE && temp->size >= size)
			if (temp->size <= minsize)
			{
				buftemp = temp;
				minsize = temp->size;
				flag = 1;
			}
		if (temp->next == NULL) break;
		temp = temp->next;
	}

	if (size == buftemp->size) {
		buftemp->status = ALLOCATED;
		return buftemp->id;
	}
	if (flag != 0)
	{
		buff->id = id_use;
		buff->size = size;
		buff->status = ALLOCATED;
		buff->next = NULL;
		buftemp->size -= size;
		temp->next = buff;
	}
	else printf("Allocation failed\n");

	return(id_use);


}


int deallocate(mem_dispatcher *md, int block_id) {
	mem_chunk *pointer = md->first, *p = NULL;

	if (pointer->id == block_id) {
		if (pointer->next !=NULL && pointer->next->status == FREE) {
			pointer->size += pointer->next->size;
			pointer->status = FREE;
			pointer = pointer->next;
			md->first->next = pointer->next;
			free(pointer);
			return 0;
		}
		pointer->status = FREE;
		return 0;
	}

	while (pointer->next != NULL && pointer->next->id != block_id)
		pointer = pointer->next;

	if (!pointer->next) return -1;

	pointer->next->status = FREE;
	if (pointer->status != FREE) pointer = pointer->next;
	while (pointer->next != NULL && pointer->next->status == FREE) {
		p = pointer->next;
		pointer->size += p->size;
		pointer->next = p->next;
		free(p);
	}
	return 0;
}

void defragment(mem_dispatcher *md){
	mem_chunk *pointer = md->first, *point = NULL, *p = NULL;

	while (pointer != NULL && pointer->status != FREE)
		pointer = pointer->next;
	
	if (pointer == NULL) return;

	point = pointer;
	while (point->next != NULL) {
		if (point->next->status == FREE) {
			p = point->next;
			pointer->size += p->size;
			point->next = p->next;
			free(p);
		}
		else point = point->next;
	}
}

void show_memory_map(mem_dispatcher *md)
{
	mem_chunk* temp = md->first;
	printf("\n");
	while (temp)
	{
		printf("id : %d", temp->id);
		printf("  size : %d", temp->size);
		if (temp->status==FREE) 
		printf("  status : FREE" );
		else printf("  status : allocated");
		printf("\n");
		temp=temp->next;
	   }
}